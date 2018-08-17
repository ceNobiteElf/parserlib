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

		private int BytesRead { get; set; }

		private char[] Buffer { get; }
		private int ReadHead { get; set; }
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
		public override char Read()
		{
			CurrentCharacter = ReadCharacter(ReadHead);
			++ReadHead;

			NextCharacter = ReadCharacter(ReadHead);

			return CurrentCharacter;
		}
		#endregion


		#region Helper Functions
		char ReadCharacter(int index)
		{
			if (BytesRead == 0 || index >= BytesRead)
			{
				ReadBlock();
				index = 0;

				if (BytesRead == 0)
				{
					return '\0';
				}
			}

			return Buffer[index];
		}

		int ReadBlock()
		{
			BytesRead = Reader.Read(Buffer, 0, Buffer.Length);
			ReadHead = 0;

			return BytesRead;
		}
		#endregion
	}
}
