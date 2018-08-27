using System;

namespace ParserLib.Json.Serialization
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
	public class JsonPropertyAttribute : Attribute
	{
		#region Properties
		public string Name { get; }
		#endregion


		#region Constructors
		public JsonPropertyAttribute()
			: this(null) { }

		public JsonPropertyAttribute(string name)
		{
			Name = name;
		}
		#endregion
	}
}
