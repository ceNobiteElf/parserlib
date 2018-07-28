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
			=> other != null && Value.Equals(other.Value);
		#endregion


		#region Interface Implementation - IEquatable<bool>
		public bool Equals(bool other)
			=> Value.Equals(other);
		#endregion


		#region Object Overrides
		public override int GetHashCode()
			=> Value.GetHashCode();

		public override bool Equals(object obj)
			=> Value.Equals(obj);

		public override string ToString()
			=> Value.ToString();
		#endregion


		#region Operator Overloads
		public static implicit operator JsonBool(bool value)
			=> new JsonBool(value);

		public static explicit operator bool(JsonBool obj)
			=> obj.Value;
		#endregion
	}
}
