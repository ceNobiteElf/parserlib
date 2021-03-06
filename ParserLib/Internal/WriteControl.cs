﻿using System;

namespace ParserLib.Internal
{
	internal abstract class WriteControl<TWriterOptions> : IDisposable where TWriterOptions : WriterOptions, new()
	{
		#region Properties
		public TWriterOptions Options { get; }

		public bool ForceAscii { get => Options.ForceAscii; }

		public int IndentationLevel
		{
			get => mIndentationLevel;
			private set
			{
				mIndentationLevel = value;
				Indentation = GetIndentation(mIndentationLevel);
			}
		}

		public string Indentation { get; private set; }
		#endregion


		#region Variables
		private int mIndentationLevel;
		#endregion


		#region Constructors
		protected WriteControl(TWriterOptions options)
		{
			Options = options ?? new TWriterOptions();

			IndentationLevel = 0;
		}
		#endregion


		#region Interface Implementation - IControl
		public virtual void Dispose() { }
		#endregion


		#region Public API
		public string GetIndentation(int level)
			=> level > 0 ? (Options.UseTabCharacter ? new string('\t', level) : new string(' ', level * Options.TabWidth)) : string.Empty;

		public void Indent()
			=> ++IndentationLevel;

		public void Unindent()
			=> --IndentationLevel;

		public void WriteIndentation()
		{
			if (Options.PrettyPrint) 
			{
				Write(Indentation);
			}
		}

		public abstract void Write(string value);

		public void WriteLine()
			=> WriteLine(string.Empty);

		public void WriteLine(string value)
			=> Write($"{value}{(Options.PrettyPrint ? Options.NewLine : string.Empty)}");

		public void WriteWithSpace(string value)
			=> Write($"{value}{(Options.PrettyPrint ? " " : string.Empty)}");
		#endregion
	}
}
