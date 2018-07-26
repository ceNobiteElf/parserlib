using System;

namespace ParserLib.Json
{
	public sealed class JsonBool : JsonElement, IEquatable<JsonBool>, IEquatable<bool>
	{
		#region Properties
		public bool Value { get; set; }
		#endregion


		#region Constructors
		public JsonBool()
			: this(default(bool)) { }

		public JsonBool(bool value)
		{
			Value = value;
		}
		#endregion


		#region Interface Implementation - IEquatable<JsonBool>
		public bool Equals(JsonBool other)
		{
			return other != null && Value.Equals(other.Value);
		}
		#endregion


		#region Interface Implementation - IEquatable<bool>
		public bool Equals(bool other)
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
			return Value.ToString();
		}
		#endregion


		#region Operator Overloads
		public static implicit operator JsonBool(bool value)
		{
			return new JsonBool(value);
		}

		public static explicit operator bool(JsonBool obj)
		{
			return obj.Value;
		}
		#endregion
	}
}
