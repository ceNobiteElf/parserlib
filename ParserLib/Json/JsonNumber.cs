using System;
using System.Globalization;

namespace ParserLib.Json
{
	public sealed class JsonNumber : JsonElement, IEquatable<JsonNumber>, IEquatable<double>, IComparable<JsonNumber>, IComparable<double>, IConvertible, IFormattable
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
			=> other != null && Value.Equals(other.Value);
		#endregion


		#region Interface Implementation - IEquatable<double>
		public bool Equals(double other)
			=> Value.Equals(other);
		#endregion


		#region Interface Implementation - IComparable<JsonNumber>
		public int CompareTo(JsonNumber other)
			=> CompareTo(other.Value);
		#endregion


		#region Interface Implementation - IComparable<double>
		public int CompareTo(double other)
			=> Value.CompareTo(other);
		#endregion


		#region Interface Implementation - IConvertible
		public TypeCode GetTypeCode()
			=> Value.GetTypeCode();

		bool IConvertible.ToBoolean(IFormatProvider provider)
			=> ((IConvertible)Value).ToBoolean(provider);

		char IConvertible.ToChar(IFormatProvider provider)
			=> ((IConvertible)Value).ToChar(provider);

		sbyte IConvertible.ToSByte(IFormatProvider provider)
			=> ((IConvertible)Value).ToSByte(provider);

		byte IConvertible.ToByte(IFormatProvider provider)
			=> ((IConvertible)Value).ToByte(provider);

		short IConvertible.ToInt16(IFormatProvider provider)
			=> ((IConvertible)Value).ToInt16(provider);

		ushort IConvertible.ToUInt16(IFormatProvider provider)
			=> ((IConvertible)Value).ToUInt16(provider);

		int IConvertible.ToInt32(IFormatProvider provider)
			=> ((IConvertible)Value).ToInt32(provider);

		uint IConvertible.ToUInt32(IFormatProvider provider)
			=> ((IConvertible)Value).ToUInt32(provider);

		long IConvertible.ToInt64(IFormatProvider provider)
			=> ((IConvertible)Value).ToInt64(provider);

		ulong IConvertible.ToUInt64(IFormatProvider provider)
			=> ((IConvertible)Value).ToUInt64(provider);

		float IConvertible.ToSingle(IFormatProvider provider)
			=> ((IConvertible)Value).ToSingle(provider);

		double IConvertible.ToDouble(IFormatProvider provider)
			=> ((IConvertible)Value).ToDouble(provider);

		decimal IConvertible.ToDecimal(IFormatProvider provider)
			=> ((IConvertible)Value).ToDecimal(provider);

		DateTime IConvertible.ToDateTime(IFormatProvider provider)
			=> ((IConvertible)Value).ToDateTime(provider);

		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
			=> ((IConvertible)Value).ToType(conversionType, provider);
		#endregion


		#region Interface Implementation - IFormattable
		public string ToString(string format, IFormatProvider formatProvider)
			=> Value.ToString(format, formatProvider);
		#endregion


		#region Object Overrides
		public override int GetHashCode()
			=> Value.GetHashCode();

		public override bool Equals(object obj)
			=> Value.Equals(obj);

		public override string ToString()
			=> Value.ToString(CultureInfo.InvariantCulture);
		#endregion


		#region Operator Overloads
		public static implicit operator JsonNumber(double value)
			=> new JsonNumber(value);

		public static explicit operator double(JsonNumber obj)
			=> obj.Value;
		#endregion


		#region Operator Overloads - Equality Operators (JsonNumber, double)
		public static bool operator ==(JsonNumber lhs, double rhs)
			=> lhs.Equals(rhs);

		public static bool operator !=(JsonNumber lhs, double rhs)
			=> !lhs.Equals(rhs);

		public static bool operator >(JsonNumber lhs, double rhs)
			=> lhs.CompareTo(rhs) > 0;

		public static bool operator <(JsonNumber lhs, double rhs)
			=> lhs.CompareTo(rhs) < 0;

		public static bool operator >=(JsonNumber lhs, double rhs)
			=> lhs == rhs || lhs > rhs;

		public static bool operator <=(JsonNumber lhs, double rhs)
			=> lhs == rhs || lhs < rhs;
		#endregion


		#region Operator Overloads - Equality Operators (JsonNumber, JsonNumber)
		public static bool operator ==(JsonNumber lhs, JsonNumber rhs)
			=> lhs.Equals(rhs);

		public static bool operator !=(JsonNumber lhs, JsonNumber rhs)
			=> !lhs.Equals(rhs);

		public static bool operator >(JsonNumber lhs, JsonNumber rhs)
			=> lhs.CompareTo(rhs) > 0;

		public static bool operator <(JsonNumber lhs, JsonNumber rhs)
			=> lhs.CompareTo(rhs) < 0;

		public static bool operator >=(JsonNumber lhs, JsonNumber rhs)
			=> lhs == rhs || lhs > rhs;

		public static bool operator <=(JsonNumber lhs, JsonNumber rhs)
			=> lhs == rhs || lhs < rhs;
		#endregion


		#region Operator Overloads - Equality Operators (double, JsonNumber)
		public static bool operator ==(double lhs, JsonNumber rhs)
			=> lhs.Equals(rhs.Value);

		public static bool operator !=(double lhs, JsonNumber rhs)
			=> !lhs.Equals(rhs.Value);

		public static bool operator >(double lhs, JsonNumber rhs)
			=> lhs.CompareTo(rhs.Value) > 0;

		public static bool operator <(double lhs, JsonNumber rhs)
			=> lhs.CompareTo(rhs.Value) < 0;

		public static bool operator >=(double lhs, JsonNumber rhs)
			=> lhs == rhs || lhs > rhs;

		public static bool operator <=(double lhs, JsonNumber rhs)
			=> lhs == rhs || lhs < rhs;
		#endregion


		#region Public API
		public string ToString(string format)
			=> Value.ToString(format, CultureInfo.InvariantCulture);

		public string ToString(IFormatProvider formatProvider)
			=> Value.ToString(formatProvider);
		#endregion
	}
}
