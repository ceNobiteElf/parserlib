using System.Text;

namespace ParserLib.Json.Internal
{
	internal sealed class StringWriteControl : WriteControl
	{
		#region Properties
		public string FilePath { get; }

		private StringBuilder Builder { get; }
		public string Result { get => Builder.ToString(); }
		#endregion


		#region Constructors
		public StringWriteControl(bool prettyPrint, string newLine)
			: base(prettyPrint, newLine)
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
