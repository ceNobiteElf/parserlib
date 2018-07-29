using System.IO;

namespace ParserLib.Json.Internal
{
	internal sealed class FileReadControl : ReadControl
	{
		#region Constants
		public const int DefaultBufferSize = 4096;
		#endregion


		#region Properties
		public string FilePath { get; } 

		public FileStream Stream { get; }
		public StreamReader Reader { get; }

		public int BufferSize { get; }
		public char[] Buffer { get; }

		public int BytesRead { get; private set; }
		public int CurrentBufferPosition { get; private set; }
		#endregion


		#region Constructors
		public FileReadControl(string filePath, int bufferSize)
		{
			var fileInfo = new FileInfo(filePath);

			if (!fileInfo.Exists)
			{
				throw new FileNotFoundException();
			}

			FilePath = fileInfo.FullName;

			Stream = File.OpenRead(filePath);
			Reader = new StreamReader(Stream);

			BufferSize = bufferSize > 0 ? bufferSize : DefaultBufferSize;
			Buffer = new char[bufferSize];
		}
		#endregion


		#region Interface Implementation - IControl
		public override void Dispose()
		{
			Reader.Dispose();
			Stream.Dispose();
		}
		#endregion


		#region Public API
		public override char ReadNextCharacter()
		{
			if (BytesRead == 0 || CurrentBufferPosition >= BytesRead)
			{
				ReadBlock();

				if (BytesRead == 0)
				{
					return CurrentCharacter = '\0';
				}
			}

			return CurrentCharacter = Buffer[CurrentBufferPosition++];
		}

		public int ReadBlock()
		{
			BytesRead = Reader.Read(Buffer, 0, BufferSize);

			CurrentBufferPosition = 0;

			return BytesRead;
		}
		#endregion
	}
}
