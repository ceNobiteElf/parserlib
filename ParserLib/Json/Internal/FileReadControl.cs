using System;
using System.IO;

namespace ParserLib.Json.Internal
{
	internal sealed class FileReadControl : ReadControl
	{
		#region Properties
		public string FilePath { get; } 

		public FileStream Stream { get; }
		public StreamReader Reader { get; }

		public char[] Buffer { get; }

		public int BytesRead { get; private set; }
		public int CurrentBufferPosition { get; private set; }
		#endregion


		#region Constructors
		public FileReadControl(string filePath, ReaderOptions options)
			: base (options)
		{
			var fileInfo = new FileInfo(filePath);

			if (!fileInfo.Exists)
			{
				throw new FileNotFoundException();
			}

			FilePath = fileInfo.FullName;

			Stream = File.OpenRead(filePath);
			Reader = Options.FileEncoding != null ? new StreamReader(Stream, Options.FileEncoding) : new StreamReader(Stream, true);

			if (Options.BufferSize > 0)
			{
				Buffer = new char[Options.BufferSize];
			}
			else
			{
				throw new ArgumentOutOfRangeException(nameof(Options.BufferSize), "Buffer size must be a value greater than zero.");
			}
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
