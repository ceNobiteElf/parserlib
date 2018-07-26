using System;
using System.Collections;
using System.Collections.Generic;

namespace ParserLib.Json
{
	public class JsonString : JsonElement, IEnumerable, IEnumerable<char>, IEquatable<JsonString>, IEquatable<string>
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


		#region Interface Implementation - IEnumerable<char>
		public IEnumerator<char> GetEnumerator()
		{
			return Value.GetEnumerator();
		}
		#endregion


		#region Interface Implementation - IEquatable<JsonString>
		public bool Equals(JsonString other)
		{
			return other != null && Value.Equals(other.Value);
		}
		#endregion


		#region Interface Implementation - IEquatable<string>
		public bool Equals(string other)
		{
			return Value.Equals(other);
		}
		#endregion


		#region Object Overrides
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

		public static explicit operator string(JsonString obj)
		{
			return obj.Value;
		}
		#endregion
	}
}
