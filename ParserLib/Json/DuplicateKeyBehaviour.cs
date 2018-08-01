namespace ParserLib.Json
{
	public enum DuplicateKeyBehaviour
	{
		/// <summary>
		/// The parser will raise an exception if a duplicate key is detected while parsing a JSON object.
		/// </summary>
		ThrowException,

		/// <summary>
		/// The value for the key will be overwritten with that of its latest instance.
		/// </summary>
		Overwrite,

		/// <summary>
		/// The value for key will be set to the first instance, any other instances will be ignored. 
		/// </summary>
		Ignore
	}
}
