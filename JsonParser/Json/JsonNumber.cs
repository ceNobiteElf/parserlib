namespace ParserLib.Json
{
	public class JsonNumber : JsonElement
	{
		#region Properties
		public double Value { get; set; }
		#endregion


		#region Constructors
		public JsonNumber()
			: this(default(double)) { }

		public JsonNumber(double value)
		{
			Value = value;
		}
		#endregion
	}
}
