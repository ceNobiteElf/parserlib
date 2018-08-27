using System;

namespace ParserLib.Internal
{
	internal abstract class ReadControl<TReaderOptions> : IDisposable where TReaderOptions : ReaderOptions, new()
	{
		#region Properties
		public TReaderOptions Options { get; }

		public char CurrentCharacter { get; protected set; }
		public char NextCharacter { get; protected set; }
		#endregion


		#region Constructors
		protected ReadControl(TReaderOptions options)
		{
			Options = options ?? new TReaderOptions();
		}
		#endregion


		#region Interface Implementation - IControl
		public virtual void Dispose() { }
		#endregion


		#region Public API
		public abstract char Read();
		#endregion
	}
}
