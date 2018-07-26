using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ParserLib.Json
{
	public class Parser
	{
		#region Internal Types
		private class Control : IDisposable
		{
			public FileStream Stream { get; private set; }
			public StreamReader Reader { get; private set; }

			public int BlockSize { get; private set; }
			public char[] Buffer { get; private set; }

			public int BytesRead { get; private set; }

			public int CurrentBufferPosition { get; private set; }
			public char CurrentCharacter { get; private set; }

			public Control(string filePath, int blockSize)
			{
				Stream = File.OpenRead(filePath);
				Reader = new StreamReader(Stream);

				BlockSize = blockSize;
				Buffer = new char[blockSize];
			}

			public char ReadNextCharacter()
			{
				if (BytesRead == 0 || CurrentBufferPosition >= BytesRead)
				{
					ReadBlock();

					if (BytesRead == 0)
					{
						return '\0';
					}
				}

				CurrentCharacter = Buffer[CurrentBufferPosition++];

				return CurrentCharacter;
			}

			public int ReadBlock()
			{
				BytesRead = Reader.Read(Buffer, 0, BlockSize);

				CurrentBufferPosition = 0;

				return BytesRead;
			}

			public void Dispose()
			{
				Reader.Dispose();
				Stream.Dispose();
			}
		}
		#endregion


		#region Constants
		public const int DefaultBlockSize = 4096;
		#endregion


		#region Properties
		private static IDictionary<string, Control> Controls { get; set; }

		private static IDictionary<char, char> EscapeSequenceLookup { get; set; }
		#endregion


		#region Constructors
		static Parser()
		{
			Controls = new Dictionary<string, Control>();

			EscapeSequenceLookup = new Dictionary<char, char> {
				{ '"', '\"' }, { '\\', '\\' }, { '/', '/' },
				{ 'a', '\a' }, { 'b', '\b' }, { 'f', '\f' },
				{ 'n', '\n' }, { 'r', '\r' }, { 't', '\t' },
				{ 'v', '\a' }
			};
		}
		#endregion


		#region Public API
		public static JsonObject Parse(string filePath, int blockSize = DefaultBlockSize)
		{
			var fileInfo = new FileInfo(filePath);

			if (!fileInfo.Exists)
			{
				throw new Exception();
			}

			if (Controls.ContainsKey(fileInfo.FullName))
			{
				throw new Exception();
			}

			JsonObject result = null;

			using (var control = new Control(fileInfo.FullName, blockSize))
			{
				Controls.Add(fileInfo.FullName, control);

				while (control.ReadNextCharacter() != '\0')
				{
					switch (control.CurrentCharacter)
					{
						case '{':
							result = ParseObject(control);
							break;
					}
				}

				Controls.Remove(fileInfo.FullName);
			}

			return result;
		}
		#endregion


		#region Helper Functions
		static JsonObject ParseObject(Control control)
		{
			var obj = new JsonObject();

			JsonString key = null;
			JsonElement value = null;

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
						}
						break;

					case ':':
						if (key == null)
						{
							throw new Exception();
						}
						break;

					case ',':
						if (value == null)
						{
							throw new Exception();
						}
						else
						{
							obj.Add(key, value);

							key = null;
							value = null;
						}
						break;

					case '\0':
						throw new Exception();

					case '[':
						value = ParseArray(control);
						break;

					case 'T':
					case 't':
					case 'F':
					case 'f':
						value = ParseBool(control);
						break;

					case 'N':
					case 'n':
						value = ParseNull(control);
						break;

					default:
						if (char.IsNumber(control.CurrentCharacter) || control.CurrentCharacter == '-')
						{
							value = ParseNumber(control);
						}
						break;
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

			while (control.ReadNextCharacter() != ']')
			{
				switch (control.CurrentCharacter)
				{
					case '{':
						value = ParseObject(control);
						break;

					case '\'':
					case '"':
						value = ParseString(control);
						break;

					case ':':
						throw new Exception();

					case ',':
						if (value == null)
						{
							throw new Exception();
						}
						else
						{
							array.Add(value);

							value = null;
						}
						break;

					case '\0':
						throw new Exception();

					case '[':
						value = ParseArray(control);
						break;

					case 'T':
					case 't':
					case 'F':
					case 'f':
						value = ParseBool(control);
						break;

					case 'N':
					case 'n':
						value = ParseNull(control);
						break;

					default:
						if (char.IsNumber(control.CurrentCharacter) || control.CurrentCharacter == '-')
						{
							value = ParseNumber(control);
						}
						break;
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
							throw new NotImplementedException();
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
			return null;
		}

		static JsonBool ParseBool(Control control)
		{
			var result = new JsonBool();

			var sb = new StringBuilder();
			do
			{
				sb.Append(control.CurrentCharacter);

				control.ReadNextCharacter();
			} while (!char.IsWhiteSpace(control.CurrentCharacter) && control.CurrentCharacter != '}');

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
			} while (!char.IsWhiteSpace(control.CurrentCharacter) && control.CurrentCharacter != '}');

			if (!sb.ToString().ToLowerInvariant().Equals("null"))
			{
				throw new Exception();
			}

			return JsonNull.Instance;
		}
		#endregion
	}
}
