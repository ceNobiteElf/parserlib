using System.Collections;
using System.Collections.Generic;

namespace ParserLib.Json
{
	public class JsonObject : JsonElement, IEnumerable
	{
		#region Properties
		public IDictionary<JsonString, JsonElement> Elements { get; private set; }

		public int Count { get { return Elements.Count; } }
		#endregion


		#region Constructors
		public JsonObject()
		{
			Elements = new Dictionary<JsonString, JsonElement>();
		}
		#endregion


		#region Interface Implementation - IEnumerable
		IEnumerator IEnumerable.GetEnumerator()
		{
			return Elements.GetEnumerator();
		}
		#endregion


		#region Operator Overloads
		public JsonElement this[JsonString index]
		{
			get
			{
				JsonElement result;
				Elements.TryGetValue(index, out result);

				return result;
			}

			set
			{
				if (Elements.ContainsKey(index))
				{
					if (value != null)
					{
						Elements[index] = value;
					}
					else
					{
						Remove(index);
					}
				}
				else
				{
					Add(index, value);
				}
			}
		}
		#endregion


		#region Public API
		public void Add(JsonString key, JsonElement value)
		{
			if (value != null)
			{
				Elements.Add(key, value);
			}
		}

		public void Remove(JsonString key)
		{
			Elements.Remove(key);
		}
		#endregion
	}
}
