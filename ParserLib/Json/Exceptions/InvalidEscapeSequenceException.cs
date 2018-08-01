namespace ParserLib.Json.Exceptions
{
	public class InvalidEscapeSequenceException : JsonException
	{
		#region Constructors
		public InvalidEscapeSequenceException(string sequence)
			: base($"The escape sequence ('{sequence}') is invalid and could not be parsed.") { }
		#endregion
	}
}
