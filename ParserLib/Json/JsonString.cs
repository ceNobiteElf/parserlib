using System;
using System.Collections;
using System.Collections.Generic;

namespace ParserLib.Json
{
	public sealed class JsonString : JsonElement, IEnumerable, IEnumerable<char>, IEquatable<JsonString>, IEquatable<string>, IComparable<JsonString>, IComparable<string>
	{
		#region Properties
		public string Value
		{
			get => mValue;
			set => mValue = value ?? string.Empty;
		}

		public int Length { get => Value.Length; }
		#endregion


		#region Variables
		public string mValue;
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
			=> Value.Equals(obj);

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
	}
}
