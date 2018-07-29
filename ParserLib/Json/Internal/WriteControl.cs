using System;

namespace ParserLib.Json.Internal
{
	internal abstract class WriteControl : IControl
	{
		#region Properties
		public bool PrettyPrint { get; }

		public int IndentationLevel
		{
			get => mIndentationLevel;
			private set
			{
				mIndentationLevel = value;
				Indentation = GetIndentation(mIndentationLevel);
			}
		}

		public int TabWidth { get; }
		public string Indentation { get; private set; }

		public string NewLine { get; }
		#endregion


		#region Variables
		private int mIndentationLevel;
		#endregion


		#region Constructors
		public WriteControl(bool prettyPrint, string newLine)
		{
			PrettyPrint = prettyPrint;

			TabWidth = 2;
			Indentation = string.Empty;

			NewLine = newLine;
		}
		#endregion


		#region Interface Implementation - IControl
		public virtual void Dispose() { }
		#endregion


		#region Public API
		public string GetIndentation(int level)
			=> level > 0 ? new String(' ', level * TabWidth) : string.Empty;

		public void Indent()
			=> ++IndentationLevel;

		public void Unindent()
			=> --IndentationLevel;

		public void WriteIndentation()
		{
			if (PrettyPrint) 
			{
				Write(Indentation);
			}
		}

		public abstract void Write(string value);

		public void WriteLine()
			=> Write(PrettyPrint ? NewLine : string.Empty);

		public void WriteLine(string value)
			=> Write($"{value}{(PrettyPrint ? NewLine : string.Empty)}");

		public void WriteWithSpace(string value)
			=> Write($"{value}{(PrettyPrint ? " " : string.Empty)}");
		#endregion
	}
}
