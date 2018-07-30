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
		private static IDictionary<char, char> EscapeSequenceLookup { get; }
		#endregion


		#region Constructors
		static JsonParser()
		{
			EscapeSequenceLookup = new Dictionary<char, char> {
				{ '\'', '\'' }, { '"', '\"' }, { '\\', '\\' },
				{ '/', '/' },   { 'a', '\a' }, { 'b', '\b' },
				{ 'f', '\f' },  { 'n', '\n' }, { 'r', '\r' },
				{ 't', '\t' },  { 'v', '\v' }
			};
		}
		#endregion


		#region Public API
		public static T ParseFromFile<T>(string filePath) where T : JsonElement, IJsonRoot
			=> (T)ParseFromFile(filePath, null);

		public static T ParseFromFile<T>(string filePath, ReaderOptions options) where T : JsonElement, IJsonRoot
			=> (T)ParseFromFile(filePath, options);

		public static JsonElement ParseFromFile(string filePath)
			=> ParseFromFile(filePath, null);

		public static JsonElement ParseFromFile(string filePath, ReaderOptions options)
		{
			var control = new FileReadControl(filePath, options);

			try
			{
				return Parse(control);
			}
			finally
			{
				control.Dispose();
			}
		}

		public static T ParseFromString<T>(string rawJson) where T : JsonElement, IJsonRoot
			=> (T)ParseFromString(rawJson);

		public static JsonElement ParseFromString(string rawJson)
		{
			var control = new StringReadControl(rawJson);

			try
			{
				return Parse(control);
			}
			finally
			{
				control.Dispose();
			}
		}
		#endregion


		#region Helper Functions
		static JsonElement Parse(ReadControl control)
		{
			JsonElement result = null;

			while (control.ReadNextCharacter() != '\0')
			{
				if (control.CurrentCharacter == '{')
				{
					result = ParseObject(control);
				}
				else if (control.CurrentCharacter == '[')
				{
					result = ParseArray(control);
				}
			}

			return result;
		}

		static JsonObject ParseObject(ReadControl control)
		{
			var obj = new JsonObject();

			JsonString key = null;
			JsonElement value = null;
			bool valueAdded = false;

			while (control.ReadNextCharacter() != '}')
			{
				switch (control.CurrentCharacter)
				{
					case '{':
						value = ParseObject(control);
						break;

					case '\'':
					case '"':
						if (key == null)
						{
							key = ParseString(control);
						}
						else
						{
							value = ParseString(control);
							valueAdded = false;
						}
						break;

					case ':':
						if (key == null)
						{
							throw new Exception();
						}
						break;

					case ',':
						if (!valueAdded)
						{
							throw new Exception();
						}
						break;

					case '\0':
						throw new Exception();

					case '[':
						value = ParseArray(control);
						valueAdded = false;
						break;

					case 'T':
					case 't':
					case 'F':
					case 'f':
						value = ParseBool(control);
						valueAdded = false;
						break;

					case 'N':
					case 'n':
						value = ParseNull(control);
						valueAdded = false;
						break;

					default:
						if (char.IsNumber(control.CurrentCharacter) || control.CurrentCharacter == '-')
						{
							value = ParseNumber(control);
							valueAdded = false;
						}
						break;
				}

				if (value != null)
				{
					obj.Add(key, value);

					key = null;
					value = null;
					valueAdded = true;
				}
			}

			if (key != null && value != null)
			{
				obj.Add(key, value);
			}

			return obj;
		}

		static JsonArray ParseArray(ReadControl control)
		{
			JsonArray array = new JsonArray();

			JsonElement value = null;
			bool valueAdded = false;

			while (control.ReadNextCharacter() != ']')
			{
				switch (control.CurrentCharacter)
				{
					case '{':
						value = ParseObject(control);
						valueAdded = false;
						break;

					case '\'':
					case '"':
						value = ParseString(control);
						valueAdded = false;
						break;

					case ':':
						throw new Exception();

					case ',':
						if (!valueAdded)
						{
							throw new Exception();
						}
						break;

					case '\0':
						throw new Exception();

					case '[':
						value = ParseArray(control);
						valueAdded = false;
						break;

					case 'T':
					case 't':
					case 'F':
					case 'f':
						value = ParseBool(control);
						valueAdded = false;
						break;

					case 'N':
					case 'n':
						value = ParseNull(control);
						valueAdded = false;
						break;

					default:
						if (char.IsNumber(control.CurrentCharacter) || control.CurrentCharacter == '-')
						{
							value = ParseNumber(control);
							valueAdded = false;
						}
						break;
				}

				if (value != null)
				{
					array.Add(value);

					value = null;
					valueAdded = true;
				}
			}

			if (value != null)
			{
				array.Add(value);
			}

			return array;
		}

		static JsonString ParseString(ReadControl control)
		{
			char enclosingChar = control.CurrentCharacter;

			var sb = new StringBuilder();

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

							escapedChar = (char)int.Parse(unicodeHex.ToString(), NumberStyles.AllowHexSpecifier);
						}
						else
						{
							throw new Exception();
						}
					}

					sb.Append(escapedChar);
				}
				else
				{
					sb.Append(control.CurrentCharacter);
				}
			}

			return new JsonString(sb.ToString());
		}

		static JsonNumber ParseNumber(ReadControl control)
		{
			var sb = new StringBuilder();
			do
			{
				sb.Append(control.CurrentCharacter);

				control.ReadNextCharacter();
			} while (!char.IsWhiteSpace(control.CurrentCharacter) && control.CurrentCharacter != ',' && control.CurrentCharacter != '}');

			return new JsonNumber(double.Parse(sb.ToString(), CultureInfo.InvariantCulture));
		}

		static JsonBool ParseBool(ReadControl control)
		{
			var result = new JsonBool();

			var sb = new StringBuilder();
			do
			{
				sb.Append(control.CurrentCharacter);

				control.ReadNextCharacter();
			} while (!char.IsWhiteSpace(control.CurrentCharacter) && control.CurrentCharacter != ',' && control.CurrentCharacter != '}');

			switch (sb.ToString().ToLowerInvariant())
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
			var sb = new StringBuilder();
			do
			{
				sb.Append(control.CurrentCharacter);

				control.ReadNextCharacter();
			} while (!char.IsWhiteSpace(control.CurrentCharacter) && control.CurrentCharacter != ',' && control.CurrentCharacter != '}');

			if (!sb.ToString().ToLowerInvariant().Equals("null"))
			{
				throw new Exception();
			}

			return JsonNull.Instance;
		}
		#endregion
	}
}
