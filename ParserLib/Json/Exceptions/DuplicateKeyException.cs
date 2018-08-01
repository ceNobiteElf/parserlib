namespace ParserLib.Json.Exceptions
{
	public class DuplicateKeyException : JsonException
	{
		#region Constructors
		public DuplicateKeyException()
			: base("An item with the same key already exists.") { }

		public DuplicateKeyException(string key)
			: base($"An item with the same key ('{key}') already exists.") { }
		#endregion
	}
}