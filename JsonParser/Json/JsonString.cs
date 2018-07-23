using System;
using System.Collections;

namespace ParserLib.Json
{
	public class JsonString : JsonElement, IEnumerable, IEquatable<JsonString>
	{
		#region Properties
		public string Value { get; set; }
		#endregion


		#region Constructors
		public JsonString()
			: this(string.Empty) { }

		public JsonString(string value)
		{
			Value = value;
		}
		#endregion


		#region Interface Implementation - IEnumerable
		IEnumerator IEnumerable.GetEnumerator()
		{
			return Value.GetEnumerator();
		}
		#endregion


		#region Interface Implementation - IEquatable
		public bool Equals(JsonString other)
		{
			return other != null && Value.Equals(other.Value);
		}
		#endregion


		#region Object Overloads
		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return Value.Equals(obj);
		}

		public override string ToString()
		{
			return Value;
		}
		#endregion


		#region Operator Overloads
		public static implicit operator JsonString(string str)
		{
			return new JsonString(str);
		}

		public static explicit operator string(JsonString str)
		{
			return str.Value;
		}
		#endregion
	}
}
