using System.IO;

namespace ParserLib.Json.Internal
{
	internal sealed class FileWriteControl : WriteControl
	{
		#region Properties
		public string FilePath { get; }

		public FileStream Stream { get; }
		public StreamWriter Writer { get; }
		#endregion


		#region Constructors
		public FileWriteControl(string filePath, WriterOptions options)
			: base(options)
		{
			var fileInfo = new FileInfo(filePath);

			if (fileInfo.Exists)
			{
				fileInfo.Delete();
			}

			FilePath = fileInfo.FullName;

			Stream = File.OpenWrite(filePath);
			Writer = new StreamWriter(Stream, options.FileEncoding, options.BufferSize);
		}
		#endregion


		#region Interface Implementation - IControl
		public override void Dispose()
		{
			Writer.Dispose();
			Stream.Dispose();
		}
		#endregion


		#region Public API
		public override void Write(string value)
			=> Writer.Write(value);
		#endregion
	}
}
