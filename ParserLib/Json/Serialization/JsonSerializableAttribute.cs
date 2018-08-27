using System;

namespace ParserLib.Json.Serialization
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	public class JsonSerializableAttribute : Attribute
	{
		#region Properties
		public SerializationMode Mode { get; }
		#endregion


		#region Constructors
		public JsonSerializableAttribute()
			: this(SerializationMode.All) { }

		public JsonSerializableAttribute(SerializationMode mode)
		{
			Mode = mode;
		}
		#endregion
	}
}
