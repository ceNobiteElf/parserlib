using System;

namespace ParserLib.Json
{
	public class JsonException : Exception
	{
		#region Constructors
		public JsonException()
			: base() { }

		public JsonException(string message)
			: base(message) { }

		public JsonException(string message, Exception innerException)
			: base(message, innerException) { }
		#endregion
	}
}
