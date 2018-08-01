namespace ParserLib.Json
{
	public enum MultipleRootsBehaviour
	{
		/// <summary>
		/// The parser will raise an exception if multiple roots are detected.
		/// </summary>
		ThrowException,

		/// <summary>
		/// Only the first root element will be returned if multiple roots are detected.
		/// </summary>
		ReturnFirst
	}
}
