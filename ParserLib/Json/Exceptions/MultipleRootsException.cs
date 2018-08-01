namespace ParserLib.Json.Exceptions
{
	public class MultipleRootsException : JsonException
	{
		#region Constructors
		public MultipleRootsException()
			: base("The parser encountered more than one root.") { }
		#endregion
	}
}
