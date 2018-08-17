namespace ParserLib.Json.Internal
{
	internal abstract class ReadControl : IControl
	{
		#region Properties
		protected ReaderOptions Options { get; }

		public bool NullOnExceptions { get => Options.NullOnExceptions; }
		public DuplicateKeyBehaviour DuplicateKeyBehaviour { get => Options.DuplicateKeyBehaviour; }
		public MultipleRootsBehaviour MultipleRootsBehaviour { get => Options.MultipleRootsBehaviour; }

		public char CurrentCharacter { get; protected set; }
		public char NextCharacter { get; protected set; }
		#endregion


		#region Constructors
		public ReadControl(ReaderOptions options)
		{
			Options = options ?? new ReaderOptions();
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
