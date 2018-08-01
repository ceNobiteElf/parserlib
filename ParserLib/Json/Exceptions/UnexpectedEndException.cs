namespace ParserLib.Json.Exceptions
{
	public class UnexpectedEndException : JsonException
	{
		#region Constructors
		public UnexpectedEndException()
			: base("The end of the reader was reached while parsing a JSON value.") { }

		public UnexpectedEndException(string message)
			: base(message) { }
		#endregion
	}
}
