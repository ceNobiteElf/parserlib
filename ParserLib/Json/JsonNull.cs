namespace ParserLib.Json
{
	public sealed class JsonNull : JsonElement
	{
		#region Properties
		private static JsonNull Instance { get => mInstance ?? (mInstance = new JsonNull()); }

		public static JsonNull Value { get => Instance; }
		#endregion


		#region Variables
		private static JsonNull mInstance;
		#endregion


		#region Constructors
		private JsonNull() { }
		#endregion


		#region Object Overrides
		public override int GetHashCode()
			=> 0;

		public override bool Equals(object obj)
			=> ReferenceEquals(this, obj);

		public override string ToString()
			=> "null";
		#endregion


		#region Operator Overloads
		public static implicit operator JsonObject(JsonNull obj)
			=> null;

		public static implicit operator JsonArray(JsonNull obj)
			=> null;

		public static implicit operator JsonString(JsonNull obj)
			=> null;

		public static implicit operator JsonNumber(JsonNull obj)
			=> null;

		public static implicit operator JsonBool(JsonNull obj)
			=> null;

		public static explicit operator char(JsonNull obj)
			=> default(char);

		public static explicit operator string(JsonNull obj)
			=> default(string);

		public static explicit operator bool(JsonNull obj)
			=> default(bool);

		public static explicit operator short(JsonNull obj)
			=> default(short);

		public static explicit operator int(JsonNull obj)
			=> default(int);

		public static explicit operator long(JsonNull obj)
			=> default(long);

		public static explicit operator float(JsonNull obj)
			=> default(float);

		public static explicit operator double(JsonNull obj)
			=> default(double);
		#endregion
	}
}
