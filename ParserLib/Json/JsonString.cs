using System;
using System.Collections;
using System.Collections.Generic;

namespace ParserLib.Json
{
	public sealed class JsonString : JsonElement, IEnumerable, IEnumerable<char>, IEquatable<JsonString>, IEquatable<string>
	{
		#region Properties
		public string Value { get; set; }

		public int Length { get => Value?.Length ?? 0; }
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
			=> other != null && Value.Equals(other.Value);
		#endregion


		#region Interface Implementation - IEquatable<string>
		public bool Equals(string other)
			=> Value.Equals(other);
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
		new public char this[int index] { get => Value?[index] ?? '\0'; }

		public static implicit operator JsonString(string str)
			=> new JsonString(str);

		public static explicit operator string(JsonString obj)
			=> obj.Value;
		#endregion
	}
}
