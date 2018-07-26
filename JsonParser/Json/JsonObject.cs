using System.Collections;
using System.Collections.Generic;

namespace ParserLib.Json
{
	public sealed class JsonObject : JsonElement, IEnumerable, IEnumerable<KeyValuePair<JsonString, JsonElement>>, IDictionary<JsonString, JsonElement>
	{
		#region Properties
		private IDictionary<JsonString, JsonElement> Elements { get; set; }

		public int Count { get { return Elements.Count; } }
		public bool IsReadOnly { get { return Elements.IsReadOnly; } }

		public ICollection<JsonString> Keys { get { return Elements.Keys; } }
		public ICollection<JsonElement> Values { get { return Elements.Values; } }
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
			return GetEnumerator();
		}
		#endregion


		#region Interface Implementation - IEnumerable<KeyValuePair<JsonString, JsonElement>>
		public IEnumerator<KeyValuePair<JsonString, JsonElement>> GetEnumerator()
		{
			return Elements.GetEnumerator();
		}
		#endregion


		#region Interface Implementation - IDictionary<JsonString, JsonElement>
		public void Clear()
		{
			Elements.Clear();
		}

		public bool Contains(KeyValuePair<JsonString, JsonElement> item)
		{
			return Elements.Contains(item);
		}

		public bool ContainsKey(JsonString key)
		{
			return Elements.ContainsKey(key);
		}

		public bool TryGetValue(JsonString key, out JsonElement value)
		{
			return Elements.TryGetValue(key, out value);
		}

		public void Add(JsonString key, JsonElement value)
		{
			Elements.Add(key, value);
		}

		public void Add(KeyValuePair<JsonString, JsonElement> item)
		{
			Elements.Add(item);
		}

		public bool Remove(JsonString key)
		{
			return Elements.Remove(key);
		}

		public bool Remove(KeyValuePair<JsonString, JsonElement> item)
		{
			return Elements.Remove(item);
		}

		public void CopyTo(KeyValuePair<JsonString, JsonElement>[] array, int arrayIndex)
		{
			Elements.CopyTo(array, arrayIndex);
		}
		#endregion


		#region Object Overrides
		public override int GetHashCode()
		{
			return Elements.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return Elements.Equals(obj);
		}
		#endregion


		#region Operator Overloads
		public JsonElement this[JsonString index]
		{
			get
			{
				JsonElement result;
				TryGetValue(index, out result);

				return result;
			}

			set
			{
				if (ContainsKey(index))
				{
					Elements[index] = value;
				}
				else
				{
					Add(index, value);
				}
			}
		}
		#endregion
	}
}
