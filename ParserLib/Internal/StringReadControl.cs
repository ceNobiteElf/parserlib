namespace ParserLib.Internal
{
	internal sealed class StringReadControl<TReaderOptions> : ReadControl<TReaderOptions> where TReaderOptions : ReaderOptions, new()
	{
		#region Properties
		public string RawInput { get; }

		public int ReadHead { get; private set; }
		#endregion


		#region Constructors
		public StringReadControl(string rawInput, TReaderOptions options)
			: base(options)
		{
			RawInput = rawInput ?? string.Empty;
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
			=> ReadHead < RawInput.Length ? RawInput[index] : '\0';
		#endregion
	}
}
