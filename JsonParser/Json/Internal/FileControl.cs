using System.IO;

namespace ParserLib.Json.Internal
{
	internal sealed class FileControl : Control
	{
		#region Constants
		public const int DefaultBlockSize = 4096;
		#endregion


		#region Properties
		public string FilePath { get; } 

		public FileStream Stream { get; }
		public StreamReader Reader { get; }

		public int BlockSize { get; }
		public char[] Buffer { get; }

		public int BytesRead { get; private set; }
		public int CurrentBufferPosition { get; private set; }
		#endregion


		#region Constructors
		public FileControl(string filePath, int blockSize)
		{
			var fileInfo = new FileInfo(filePath);

			if (!fileInfo.Exists)
			{
				throw new FileNotFoundException();
			}

			FilePath = fileInfo.FullName;

			Stream = File.OpenRead(filePath);
			Reader = new StreamReader(Stream);

			BlockSize = blockSize > 0 ? blockSize : DefaultBlockSize;
			Buffer = new char[blockSize];
		}
		#endregion


		#region Interface Implementation - IDisposable
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
			BytesRead = Reader.Read(Buffer, 0, BlockSize);

			CurrentBufferPosition = 0;

			return BytesRead;
		}
		#endregion
	}
}
