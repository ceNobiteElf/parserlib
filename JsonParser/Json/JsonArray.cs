using System.Collections;
using System.Collections.Generic;

namespace ParserLib.Json
{
	public class JsonArray : JsonElement, IEnumerable
	{
		#region Properties
		public IList<JsonElement> Values { get; protected set; }
		#endregion


		#region Constructors
		public JsonArray()
		{
			Values = new List<JsonElement>();
		}
		#endregion


		#region Interface Implementation - IEnumerable
		IEnumerator IEnumerable.GetEnumerator()
		{
			return Values.GetEnumerator();
		}
		#endregion


		#region Operator Overloads
		public JsonElement this[int index]
		{
			get { return Values[index]; }
			set { Values[index] = value; }
		}
		#endregion
	}
}
