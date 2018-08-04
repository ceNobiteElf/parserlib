using System;
using System.Collections;
using System.Collections.Generic;

namespace ParserLib.Json
{
	public sealed class JsonString : JsonElement, IEnumerable, IEnumerable<char>, IEquatable<JsonString>, IEquatable<string>, IComparable<JsonString>, IComparable<string>
	{
		#region Properties
		public string Value { get; }

		public int Length { get => Value.Length; }
		#endregion


		#region Constructors
		public JsonString()
			: this(string.Empty) { }

		public JsonString(string value)
		{
			Value = value ?? string.Empty;
		}
		#endregion


		#region Interface Implementation - IEnumerable
		IEnumerator IEnumerable.GetEnumerator()
			=> Value.GetEnumerator();
		#endregion


		#region Interface Implementation - IEnumerable<char>
		public IEnumerator<char> GetEnumerator()
			=> Value.GetEnumerator();
		#endregion


		#region Interface Implementation - IEquatable<JsonString>
		public bool Equals(JsonString other)
			=> Value.Equals(other?.Value);
		#endregion


		#region Interface Implementation - IEquatable<string>
		public bool Equals(string other)
			=> Value.Equals(other);
		#endregion


		#region Interface Implementation - IComparable<JsonString>
		public int CompareTo(JsonString other)
			=> Value.CompareTo(other?.Value);
		#endregion


		#region Interface Implementation - IComparable<string>
		public int CompareTo(string other)
			=> Value.CompareTo(other);
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

			if (obj is JsonString json)
			{
				return Equals(json);
			}

			return Value.Equals(obj);
		}

		public override string ToString()
			=> Value;
		#endregion


		#region Operator Overloads
		new public char this[int index] { get => Value[index]; }

		public static implicit operator JsonString(string str)
			=> new JsonString(str);

		public static implicit operator string(JsonString obj)
			=> obj.Value;

		public static bool operator ==(JsonString lhs, JsonString rhs)
			=> lhs?.Value == rhs?.Value;

		public static bool operator !=(JsonString lhs, JsonString rhs)
			=> lhs?.Value != rhs?.Value;
		#endregion


		#region Public API
		public bool Equals(JsonString obj, StringComparison comparisonType)
			=> Value.Equals(obj?.Value, comparisonType);

		public bool Equals(string value, StringComparison comparisonType)
			=> Value.Equals(value, comparisonType);
		#endregion
	}
}
