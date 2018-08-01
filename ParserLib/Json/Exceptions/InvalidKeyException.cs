namespace ParserLib.Json.Exceptions
{
	public class InvalidKeyException : JsonException
	{
		#region Constructors
		public InvalidKeyException()
			: base("A key with invalid type was encountered. Keys must be of type JsonString.") { }
		#endregion
	}
}
