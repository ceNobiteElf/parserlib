namespace ParserLib.Json
{
	public class JsonBool : JsonElement
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
	}
}
