namespace ParserLib.Json.Internal
{
	internal sealed class StringControl : Control
	{
		#region Properties
		public string RawJson { get; }

		public int CurrentPosition { get; private set; }
		#endregion


		#region Constructors
		public StringControl(string rawJson)
		{
			RawJson = rawJson;
		}
		#endregion


		#region Public API
		public override char ReadNextCharacter()
			=> CurrentCharacter = CurrentPosition < RawJson.Length ? RawJson[CurrentPosition++] : '\0';
		#endregion
	}
}
