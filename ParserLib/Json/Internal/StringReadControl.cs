namespace ParserLib.Json.Internal
{
	internal sealed class StringReadControl : ReadControl
	{
		#region Properties
		public string RawJson { get; }

		public int CurrentPosition { get; private set; }
		#endregion


		#region Constructors
		public StringReadControl(string rawJson, ReaderOptions options)
			: base(options)
		{
			RawJson = rawJson ?? string.Empty;
		}
		#endregion


		#region Public API
		public override char ReadNextCharacter()
			=> CurrentCharacter = CurrentPosition < RawJson.Length ? RawJson[CurrentPosition++] : '\0';
		#endregion
	}
}
