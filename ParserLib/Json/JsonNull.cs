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

		public static explicit operator string(JsonNull obj)
			=> null;

		public static explicit operator bool(JsonNull obj)
			=> false;

		public static explicit operator int(JsonNull obj)
			=> 0;

		public static explicit operator float(JsonNull obj)
			=> 0f;

		public static explicit operator double(JsonNull obj)
			=> 0.0;
		#endregion
	}
}
