﻿using System;

namespace ParserLib.Json
{
	public sealed class JsonNumber : JsonElement, IEquatable<JsonNumber>, IEquatable<double>, IComparable<JsonNumber>, IComparable<double>, IFormattable
	{
		#region Properties
		public double Value { get; }
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
			=> Value.Equals(other?.Value);
		#endregion


		#region Interface Implementation - IEquatable<double>
		public bool Equals(double other)
			=> Value.Equals(other);
		#endregion


		#region Interface Implementation - IComparable<JsonNumber>
		public int CompareTo(JsonNumber other)
			=> Value.CompareTo(other?.Value);
		#endregion


		#region Interface Implementation - IComparable<double>
		public int CompareTo(double other)
			=> Value.CompareTo(other);
		#endregion


		#region Interface Implementation - IFormattable
		public string ToString(string format, IFormatProvider formatProvider)
			=> Value.ToString(format, formatProvider);
		#endregion


		#region Object Overrides
		public override int GetHashCode()
			=> Value.GetHashCode();

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if (obj is JsonNumber json)
			{
				return Equals(json);
			}

			return Value.Equals(obj);
		}

		public override string ToString()
			=> Value.ToString();
		#endregion


		#region Operator Overloads
		public static implicit operator JsonNumber(double value)
			=> new JsonNumber(value);

		public static explicit operator short(JsonNumber obj)
			=> (short)obj.Value;

		public static explicit operator int(JsonNumber obj)
			=> (int)obj.Value;

		public static explicit operator long(JsonNumber obj)
			=> (long)obj.Value;

		public static explicit operator float(JsonNumber obj)
			=> (float)obj.Value;

		public static implicit operator double(JsonNumber obj)
			=> obj.Value;

		public static bool operator ==(JsonNumber lhs, JsonNumber rhs)
			=> lhs?.Value == rhs?.Value;

		public static bool operator !=(JsonNumber lhs, JsonNumber rhs)
			=> lhs?.Value != rhs?.Value;

		public static bool operator >(JsonNumber lhs, JsonNumber rhs)
			=> lhs?.Value > rhs?.Value;

		public static bool operator <(JsonNumber lhs, JsonNumber rhs)
			=> lhs?.Value < rhs?.Value;

		public static bool operator >=(JsonNumber lhs, JsonNumber rhs)
			=> lhs == rhs || lhs > rhs;

		public static bool operator <=(JsonNumber lhs, JsonNumber rhs)
			=> lhs == rhs || lhs < rhs;
		#endregion


		#region Public API
		public string ToString(string format)
			=> Value.ToString(format);

		public string ToString(IFormatProvider formatProvider)
			=> Value.ToString(formatProvider);
		#endregion
	}
}
