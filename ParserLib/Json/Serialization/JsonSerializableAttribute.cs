using System;

namespace ParserLib.Json.Serialization
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public class JsonSerializableAttribute : Attribute
	{
		#region PROPERTIES
		public SerializationMode Mode { get; }
		#endregion


		#region CONSTRUCTORS
		public JsonSerializableAttribute()
			: this(SerializationMode.All) { }

		public JsonSerializableAttribute(SerializationMode mode)
		{
			Mode = mode;
		}
		#endregion
	}
}
