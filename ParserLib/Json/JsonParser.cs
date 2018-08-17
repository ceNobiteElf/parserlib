using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using ParserLib.Json.Exceptions;
using ParserLib.Json.Internal;

namespace ParserLib.Json
{
	public static class JsonParser
	{
		#region Properties
		private static IDictionary<char, Func<ReadControl, JsonElement>> ParserLookup { get; }

		private static IDictionary<char, char> EscapeSequenceLookup { get; }

		private static IList<char> LiteralTerminators { get; }
		#endregion


		#region Constructors
		static JsonParser()
		{
			ParserLookup = new Dictionary<char, Func<ReadControl, JsonElement>> {
				{ '{',  ParseObject },
				{ '[',  ParseArray },
				{ '"',  ParseString },
				{ '\'', ParseString },
				{ 't',  ParseBool },
				{ 'f',  ParseBool },
				{ 'n',  ParseNull },
			};

			EscapeSequenceLookup = new Dictionary<char, char> {
				{ '\'', '\'' }, { '"', '\"' }, { '\\', '\\' },
				{ '/', '/' },   { 'a', '\a' }, { 'b', '\b' },
				{ 'f', '\f' },  { 'n', '\n' }, { 'r', '\r' },
				{ 't', '\t' },  { 'v', '\v' }
			};

			LiteralTerminators = new [] {':', ',', ']', '}', '\0'};
		}
		#endregion


		#region Public API - From File
		public static T ParseFromFile<T>(string filePath) where T : JsonElement, IJsonRoot
			=> ParseFromFile(filePath, null) as T;

		public static T ParseFromFile<T>(string filePath, ReaderOptions options) where T : JsonElement, IJsonRoot
			=> ParseFromFile(filePath, options) as T;

		public static JsonElement ParseFromFile(string filePath)
			=> ParseFromFile(filePath, null);

		public static JsonElement ParseFromFile(string filePath, ReaderOptions options)
			=> Parse(new FileReadControl(filePath, options));
		#endregion


		#region Public API - From String
		public static T ParseFromString<T>(string rawJson) where T : JsonElement, IJsonRoot
			=> ParseFromString(rawJson, null) as T;

		public static T ParseFromString<T>(string rawJson, ReaderOptions options) where T : JsonElement, IJsonRoot
			=> ParseFromString(rawJson, options) as T;

		public static JsonElement ParseFromString(string rawJson)
			=> ParseFromString(rawJson, null);

		public static JsonElement ParseFromString(string rawJson, ReaderOptions options)
			=> Parse(new StringReadControl(rawJson, options));
		#endregion


		#region Helper Functions
		static JsonElement Parse(ReadControl control)
		{
			JsonElement result = null;

			try
			{
				while (control.Read() != '\0')
				{
					if (control.CurrentCharacter == '{' || control.CurrentCharacter == '[')
					{
						Func<ReadControl, JsonElement> parser = ParserLookup[control.CurrentCharacter];

						if (result == null)
						{	
							result = parser.Invoke(control);
						}
						else if (control.MultipleRootsBehaviour == MultipleRootsBehaviour.ThrowException)
						{
							throw new MultipleRootsException();
						}
					}
				}
			}
			catch (Exception)
			{
				if (control.NullOnExceptions)
				{
					result = null;
				}
				else
				{
					throw;
				}
			}
			finally
			{
				control.Dispose();
			}

			return result;
		}

		static bool CanParseNumber(char currentCharacter)
			=> char.IsNumber(currentCharacter) || currentCharacter == '-';

		static bool IsEndOfLiteral(char currentCharacter)
			=> char.IsWhiteSpace(currentCharacter) || LiteralTerminators.Contains(currentCharacter);

		static string GetLiteral(ReadControl control)
		{
			var value = new StringBuilder();
			value.Append(control.CurrentCharacter);

			while (!IsEndOfLiteral(control.NextCharacter))
			{
				value.Append(control.Read());
			}

			return value.ToString();
		}

		static JsonObject ParseObject(ReadControl control)
		{
			var obj = new JsonObject();

			JsonString currentKey = null;
			JsonElement currentValue = null;
			bool pairAdded = false;

			while (control.Read() != '}')
			{
				if (ParserLookup.TryGetValue(control.CurrentCharacter, out Func<ReadControl, JsonElement> parser))
				{
					currentValue = parser.Invoke(control);
				}
				else
				{
					switch (control.CurrentCharacter)
					{
						case '\0':
							throw new UnexpectedEndException();

						case ':':
							if (currentKey == null)
							{
								throw new UnexpectedTokenException();
							}
							break;

						case ',':
							if (!pairAdded)
							{
								throw new UnexpectedTokenException();
							}

							pairAdded = false;
							break;

						default:
							if (CanParseNumber(control.CurrentCharacter))
							{
								currentValue = ParseNumber(control);
							}
							break;
					}
				}

				if (currentValue != null)
				{
					if (currentKey == null)
					{
						currentKey = currentValue as JsonString ?? throw new InvalidKeyException();
					}
					else
					{
						if (obj.ContainsKey(currentKey))
						{
							switch (control.DuplicateKeyBehaviour)
							{
								case DuplicateKeyBehaviour.Overwrite:
									obj[currentKey] = currentValue;
								break;

								case DuplicateKeyBehaviour.Ignore:
									// We do nothing, as the value for the key will remain unchanged.
									break;

								case DuplicateKeyBehaviour.ThrowException:
								default:
									throw new DuplicateKeyException(currentKey);
							}
						}
						else
						{
							obj.Add(currentKey, currentValue);
						}

						currentKey = null;
						pairAdded = true;
					}

					currentValue = null;
				}
			}

			return obj;
		}

		static JsonArray ParseArray(ReadControl control)
		{
			JsonArray array = new JsonArray();

			JsonElement currentValue = null;
			bool valueAdded = false;

			while (control.Read() != ']')
			{
				if (ParserLookup.TryGetValue(control.CurrentCharacter, out Func<ReadControl, JsonElement> parser))
				{
					currentValue = parser.Invoke(control);
				}
				else
				{
					switch (control.CurrentCharacter)
					{
						case '\0':
							throw new UnexpectedEndException();

						case ':':
							throw new UnexpectedTokenException();

						case ',':
							if (!valueAdded)
							{
								throw new UnexpectedTokenException();
							}

							valueAdded = false;
							break;

						default:
							if (CanParseNumber(control.CurrentCharacter))
							{
								currentValue = ParseNumber(control);
							}
							break;
					}
				}

				if (currentValue != null)
				{
					array.Add(currentValue);

					currentValue = null;
					valueAdded = true;
				}
			}

			return array;
		}

		static JsonString ParseString(ReadControl control)
		{
			var value = new StringBuilder();

			char enclosingChar = control.CurrentCharacter;

			while (control.Read() != enclosingChar)
			{
				if (control.CurrentCharacter == '\0')
				{
					throw new UnexpectedEndException();
				}
				else if (control.CurrentCharacter == '\\')
				{
					control.Read();

					char escapedChar;

					if (!EscapeSequenceLookup.TryGetValue(control.CurrentCharacter, out escapedChar))
					{
						if (control.CurrentCharacter == 'u')
						{
							var unicodeHex = new StringBuilder();

							for (int i = 0; i < 4; ++i)
							{
								unicodeHex.Append(control.Read());
							}

							escapedChar = (char)UInt16.Parse(unicodeHex.ToString(), NumberStyles.AllowHexSpecifier);
						}
						else
						{
							throw new InvalidEscapeSequenceException($"\\{control.CurrentCharacter}");
						}
					}

					value.Append(escapedChar);
				}
				else
				{
					value.Append(control.CurrentCharacter);
				}
			}

			return new JsonString(value.ToString());
		}

		static JsonNumber ParseNumber(ReadControl control)
		{
			var value = GetLiteral(control);

			if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out double result))
			{
				return new JsonNumber(result);
			}

			throw new ValueParseException(value, typeof(JsonNumber));
		}

		static JsonBool ParseBool(ReadControl control)
		{
			var value = GetLiteral(control);

			switch (value)
			{
				case "true":
					return true;

				case "false":
					return false;

				default:
					throw new ValueParseException(value, typeof(JsonBool));
			}
		}

		static JsonNull ParseNull(ReadControl control)
		{
			string value = GetLiteral(control);

			if (value.Equals("null"))
			{
				return JsonNull.Value;
			}

			throw new ValueParseException(value, typeof(JsonNull));
		}


		#endregion
	}
}
