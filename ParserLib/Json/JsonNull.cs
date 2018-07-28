namespace ParserLib.Json
{
	public sealed class JsonNull : JsonElement
	{
		#region Properties
		public static JsonNull Instance { get => mInstance ?? (mInstance = new JsonNull()); }

		public object Value { get => null; }
		#endregion


		#region Variables
		private static JsonNull mInstance;
		#endregion


		#region Constructors
		private JsonNull() {}
		#endregion


		#region Object Overrides
		public override string ToString()
			=> "null";
		#endregion


		#region Operator Overloads
		public static implicit operator string(JsonNull obj)
			=> null;
		
		public static implicit operator bool(JsonNull obj)
			=> false;

		public static implicit operator int(JsonNull obj)
			=> 0;

		public static implicit operator float(JsonNull obj)
			=> 0f;

		public static implicit operator double(JsonNull obj)
			=> 0.0;
		#endregion
	}
}
