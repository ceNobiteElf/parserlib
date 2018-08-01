namespace ParserLib.Json.Exceptions
{
	public class UnexpectedTokenException : JsonException
	{
		#region Constructors
		public UnexpectedTokenException()
			: base("The parser did not transition from a previous state correctly before reaching an unexpected token.") { }

		public UnexpectedTokenException(string message)
			: base(message) { }
		#endregion
	}
}
