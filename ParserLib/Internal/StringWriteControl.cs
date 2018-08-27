using System.Text;

namespace ParserLib.Internal
{
	internal sealed class StringWriteControl<TWriterOptions> : WriteControl<TWriterOptions> where TWriterOptions : WriterOptions, new()
	{
		#region Properties
		public string FilePath { get; }

		private StringBuilder Builder { get; }
		public string Result { get => Builder.ToString(); }
		#endregion


		#region Constructors
		public StringWriteControl(TWriterOptions options)
			: base(options)
		{
			Builder = new StringBuilder();
		}
		#endregion


		#region Public API
		public override void Write(string value)
			=> Builder.Append(value);
		#endregion
	}
}
