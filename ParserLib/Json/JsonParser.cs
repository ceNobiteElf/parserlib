using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

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
				while (control.ReadNextCharacter() != '\0')
				{
					if (control.CurrentCharacter == '{' || control.CurrentCharacter == '[')
					{
						if (result == null && ParserLookup.TryGetValue(control.CurrentCharacter, out Func<ReadControl, JsonElement> parser))
						{
							result = parser.Invoke(control);
						}
						else
						{
							throw new Exception();
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (control.NullOnExceptions)
				{
					result = null;
				}
				else
				{
					Console.WriteLine(ex);
					throw ex;
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

		static JsonObject ParseObject(ReadControl control)
		{
			var obj = new JsonObject();

			JsonString currentKey = null;
			JsonElement currentValue = null;
			bool pairAdded = false;

			do
			{
				control.ReadNextCharacter();

				if (ParserLookup.TryGetValue(control.CurrentCharacter, out Func<ReadControl, JsonElement> parser))
				{
					currentValue = parser.Invoke(control);
				}
				else
				{
					switch (control.CurrentCharacter)
					{
						case '\0':
							throw new Exception();

						case ':':
							if (currentKey == null)
							{
								throw new Exception();
							}
							break;

						case ',':
							if (!pairAdded)
							{
								throw new Exception();
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
						currentKey = currentValue as JsonString ?? throw new Exception();
					}
					else
					{
						obj[currentKey] = currentValue;
						currentKey = null;
						pairAdded = true;
					}

					currentValue = null;
				}

			} while (control.CurrentCharacter != '}');

			return obj;
		}

		static JsonArray ParseArray(ReadControl control)
		{
			JsonArray array = new JsonArray();

			JsonElement currentValue = null;
			bool valueAdded = false;

			do
			{
				control.ReadNextCharacter();

				if (ParserLookup.TryGetValue(control.CurrentCharacter, out Func<ReadControl, JsonElement> parser))
				{
					currentValue = parser.Invoke(control);
				}
				else
				{
					switch (control.CurrentCharacter)
					{
						case '\0':
						case ':':
							throw new Exception();

						case ',':
							if (!valueAdded)
							{
								throw new Exception();
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

			} while (control.CurrentCharacter != ']');

			return array;
		}

		static JsonString ParseString(ReadControl control)
		{
			var value = new StringBuilder();

			char enclosingChar = control.CurrentCharacter;

			while (control.ReadNextCharacter() != enclosingChar)
			{
				if (control.CurrentCharacter == '\\')
				{
					control.ReadNextCharacter();

					char escapedChar;
					EscapeSequenceLookup.TryGetValue(control.CurrentCharacter, out escapedChar);

					if (escapedChar == '\0')
					{
						if (control.CurrentCharacter == 'u')
						{
							var unicodeHex = new StringBuilder();

							for (int i = 0; i < 4; ++i)
							{
								unicodeHex.Append(control.ReadNextCharacter());
							}

							escapedChar = (char)UInt16.Parse(unicodeHex.ToString(), NumberStyles.AllowHexSpecifier);
						}
						else
						{
							throw new Exception();
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
			var value = new StringBuilder();

			do
			{
				value.Append(control.CurrentCharacter);
			} while (!IsEndOfLiteral(control.ReadNextCharacter()));

			return new JsonNumber(double.Parse(value.ToString(), CultureInfo.InvariantCulture));
		}

		static JsonBool ParseBool(ReadControl control)
		{
			var result = new JsonBool();

			var value = new StringBuilder();

			do
			{
				value.Append(control.CurrentCharacter);
			} while (!IsEndOfLiteral(control.ReadNextCharacter()));

			switch (value.ToString())
			{
				case "true":
					result.Value = true;
					break;

				case "false":
					result.Value = false;
					break;

				default:
					throw new Exception();
			}

			return result;
		}

		static JsonNull ParseNull(ReadControl control)
		{
			var value = new StringBuilder();

			do
			{
				value.Append(control.CurrentCharacter);
			} while (!IsEndOfLiteral(control.ReadNextCharacter()));

			if (!value.ToString().ToLowerInvariant().Equals("null"))
			{
				throw new Exception();
			}

			return JsonNull.Instance;
		}
		#endregion
	}
}
