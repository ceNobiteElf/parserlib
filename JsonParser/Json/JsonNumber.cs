using System;

namespace ParserLib.Json
{
	public sealed class JsonNumber : JsonElement, IEquatable<JsonNumber>, IEquatable<double>
	{
		#region Properties
		public double Value { get; set; }
		#endregion


		#region Constructors
		public JsonNumber()
			: this(default(double)) { }

		public JsonNumber(double value)
		{
			Value = value;
		}
		#endregion


		#region Interface Implementation - IEquatable<JsonNumber>
		public bool Equals(JsonNumber other)
		{
			return other != null && Value.Equals(other.Value);
		}
		#endregion


		#region Interface Implementation - IEquatable<double>
		public bool Equals(double other)
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
		public static implicit operator JsonNumber(double value)
		{
			return new JsonNumber(value);
		}

		public static explicit operator double(JsonNumber obj)
		{
			return obj.Value;
		}
		#endregion
	}
}
