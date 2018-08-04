using System.Collections.Generic;

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
			=> obj as JsonString;

		public static explicit operator bool(JsonElement obj)
			=> obj as JsonBool;

		public static explicit operator double(JsonElement obj)
			=> obj as JsonNumber;

		public static implicit operator JsonElement(string obj)
			=> (JsonString)obj;

		public static implicit operator JsonElement(bool obj)
			=> (JsonBool)obj;

		public static implicit operator JsonElement(double obj)
			=> (JsonNumber)obj;

		public static implicit operator JsonElement(List<JsonElement> obj)
			=> (JsonArray)obj;

		public static implicit operator JsonElement(Dictionary<JsonString, JsonElement> obj)
			=> (JsonObject)obj;
		#endregion


		#region Object Overrides
		public abstract override int GetHashCode();
		public abstract override bool Equals(object obj);
		#endregion
	}
}
