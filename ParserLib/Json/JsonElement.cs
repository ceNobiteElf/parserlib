namespace ParserLib.Json
{
	public abstract class JsonElement
	{
		#region Operator Overloads
		public JsonElement this[int index]
		{
			get => ((JsonArray)this)[index];
			set => ((JsonArray)this)[index] = value;
		}

		public JsonElement this[JsonString index]
		{
			get => ((JsonObject)this)[index];
			set => ((JsonObject)this)[index] = value;
		}

		public static explicit operator string(JsonElement obj)
			=> (string)(obj as JsonString);

		public static explicit operator bool(JsonElement obj)
			=> (bool)(obj as JsonBool);

		public static explicit operator double(JsonElement obj)
			=> (double)(obj as JsonNumber);
		#endregion
	}
}
