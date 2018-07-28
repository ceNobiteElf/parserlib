﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using ParserLib.Json.Internal;

namespace ParserLib.Json
{
	public class Parser
	{
		#region Properties
		private static IDictionary<string, Control> FileControls { get; set; }

		private static IDictionary<char, char> EscapeSequenceLookup { get; set; }
		#endregion


		#region Constructors
		static Parser()
		{
			FileControls = new Dictionary<string, Control>();

			EscapeSequenceLookup = new Dictionary<char, char> {
				{ '"', '\"' }, { '\\', '\\' }, { '/', '/' },
				{ 'a', '\a' }, { 'b', '\b' }, { 'f', '\f' },
				{ 'n', '\n' }, { 'r', '\r' }, { 't', '\t' },
				{ 'v', '\a' }
			};
		}
		#endregion


		#region Public API
		public static JsonObject ParseFile(string filePath, int blockSize = FileControl.DefaultBlockSize)
		{
			var control = new FileControl(filePath, blockSize);

			if (FileControls.ContainsKey(control.FilePath))
			{
				throw new Exception();
			}

			FileControls.Add(control.FilePath, control);

			try
			{
				return Parse(control);
			}
			finally
			{
				FileControls.Remove(control.FilePath);
				control.Dispose();
			}
		}

		public static JsonObject ParseString(string jsonString)
		{
			var control = new StringControl(jsonString);

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
		static JsonObject Parse(Control control)
		{
			JsonObject result = null;

			while (control.ReadNextCharacter() != '\0')
			{
				if (control.CurrentCharacter == '{')
				{
					result = ParseObject(control);
				}
			}

			return result;
		}

		static JsonObject ParseObject(Control control)
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

		static JsonArray ParseArray(Control control)
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

		static JsonString ParseString(Control control)
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

		static JsonNumber ParseNumber(Control control)
		{
			var sb = new StringBuilder();
			do
			{
				sb.Append(control.CurrentCharacter);

				control.ReadNextCharacter();
			} while (!char.IsWhiteSpace(control.CurrentCharacter) && control.CurrentCharacter != ',' && control.CurrentCharacter != '}');

			return new JsonNumber(double.Parse(sb.ToString(), CultureInfo.InvariantCulture));
		}

		static JsonBool ParseBool(Control control)
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

		static JsonNull ParseNull(Control control)
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
