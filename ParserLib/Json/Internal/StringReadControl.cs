namespace ParserLib.Json.Internal
{
	internal sealed class StringReadControl : ReadControl
	{
		#region Properties
		public string RawJson { get; }

		public int ReadHead { get; private set; }
		#endregion


		#region Constructors
		public StringReadControl(string rawJson, ReaderOptions options)
			: base(options)
		{
			RawJson = rawJson ?? string.Empty;
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
			=> ReadHead < RawJson.Length ? RawJson[index] : '\0';
		#endregion
	}
}
