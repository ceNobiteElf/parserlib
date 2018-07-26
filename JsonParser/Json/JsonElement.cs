namespace ParserLib.Json
{
	public abstract class JsonElement
	{
		#region Operator Overloads
		public JsonElement this[int index]
		{
			get { return ((JsonArray)this)[index]; }
			set { ((JsonArray)this)[index] = value; }
		}

		public JsonElement this[JsonString index]
		{
			get { return ((JsonObject)this)[index]; }
			set { ((JsonObject)this)[index] = value; }
		}

		public static explicit operator string(JsonElement obj)
		{
			return (string)(obj as JsonString);
		}

		public static explicit operator bool(JsonElement obj)
		{
			return (bool)(obj as JsonBool);
		}

		public static explicit operator double(JsonElement obj)
		{
			return (double)(obj as JsonNumber);
		}
		#endregion
	}
}
