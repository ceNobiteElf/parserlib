using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

			public int CurrentStreamPosition { get; private set; }
			public int BytesRead { get; private set; }

			public int CurrentBufferPosition { get; private set; }

			public Control(string filePath, int blockSize)
			{
				FileStream stream = File.OpenRead(filePath);
				StreamReader reader = new StreamReader(stream);

				BlockSize = blockSize;
				Buffer = new char[blockSize];

				CurrentStreamPosition = 0;
			}

			public char GetNextCharacter()
			{
				if (BytesRead == 0 || CurrentBufferPosition >= BytesRead)
				{
					ReadBlock();

					if (BytesRead == 0)
					{
						return '\0';
					}
				}

				return Buffer[CurrentBufferPosition++];
			}

			public int ReadBlock()
			{
				BytesRead = Reader.Read(Buffer, CurrentStreamPosition, BlockSize);

				CurrentStreamPosition += BytesRead;
				CurrentBufferPosition = 0;

				return BytesRead;
			}

			public void Dispose()
			{
				Reader.Dispose();
				Stream.Dispose();
			}
		}


		[Flags]
		private enum States
		{
			None = 0x00,
			Quoted = 0x01,
			Escaped = 0x02,
			Negated = 0x04,
			IsArray = 0x08
		}
		#endregion


		#region Constants
		public const int DefaultBlockSize = 4096;
		#endregion


		#region Properties
		private static IDictionary<string, Control> Controls { get; set; }
		#endregion


		#region Constructors
		static Parser()
		{
			Controls = new Dictionary<string, Control>();
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

			using (var control = new Control(fileInfo.FullName, blockSize))
			{
				Controls.Add(fileInfo.FullName, control);

				char currentChar;
				while ((currentChar = control.GetNextCharacter()) != '\0')
				{
					switch (currentChar)
					{
						case '{':
							ParseObject(control);
							break;
					}
				}

				Controls.Remove(fileInfo.FullName);
			}

			return null;


			/*while (true)
				{
					bytesRead = reader.Read(buffer, currentPosition, blockSize);
					currentPosition += bytesRead;

					if (bytesRead > 0)
					{
						foreach (char c in buffer)
						{
							switch (c)
							{
								case '{':
									// TODO create temporary node

									break;

								case '}':
									// TODO collection as value to node
									// TODO add temporary node to the tree
									break;

								case '"':
									if (!states.HasFlag(States.Quoted))
									{
										states |= States.Quoted;
									}
									else
									{
										states &= ~States.Quoted;
									}
									break;

								case ':':
									// TODO assign key
									break;

								case ',':
									// TODO add key/pair or value to collection
									break;

								case '\\':
									states |= States.Escaped;
									break;

								case '[':
									states |= States.IsArray;
									break;

								case ']':
									states &= ~States.IsArray;
									break;

								case '-':
									states |= States.Negated;
									break;

								default:
									if (!states.HasFlag(States.Quoted) && Char.IsWhiteSpace(c))
									{
										continue;
									}

									stringBuilder.Append(c);
									break;
							}
						}
					}
					else
					{
						break;
					}
				}*/
		}
		#endregion


		#region Helper Functions
		static JsonObject ParseObject(Control control)
		{
			var obj = new JsonObject();

			var key = new JsonString();
			var value = new JsonElement();

			char currentChar;
			while ((currentChar = control.GetNextCharacter()) != '}')
			{
				switch (currentChar)
				{
					case '"':
						break;

					case '[':
						value = ParseArray(control);
						break;

					case '\0':
						throw new Exception();
				}
			}

			return obj;
		}

		static JsonArray ParseArray(Control control)
		{
			return null;
		}

		static JsonString ParseString(Control control)
		{
			return null;
		}

		static JsonNumber ParseNumber(Control control)
		{
			return null;
		}
		#endregion
	}
}
