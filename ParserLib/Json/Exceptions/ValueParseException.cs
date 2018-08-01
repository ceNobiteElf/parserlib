using System;

namespace ParserLib.Json.Exceptions
{
	public class ValueParseException : JsonException
	{
		#region Constructors
		public ValueParseException(object rawValue, Type targetType)
			: base($"Value \"{rawValue}\" could not be parsed to type {targetType.Name}.") { }
		#endregion
	}
}
