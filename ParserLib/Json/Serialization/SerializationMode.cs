namespace ParserLib.Json.Serialization
{
	public enum SerializationMode
	{
		/// <summary>
		/// Only public serializable fields and properties will be included.
		/// </summary>
		Public,

		/// <summary>
		/// All serializable fields and properties regardless of access modifier will be included.
		/// </summary>
		All,

		/// <summary>
		/// Only fields and properties that have opted in will be included.
		/// </summary>
		OptIn
	}
}
