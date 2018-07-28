using System.Collections;
using System.Collections.Generic;

namespace ParserLib.Json
{
	public sealed class JsonObject : JsonElement, IEnumerable, IEnumerable<KeyValuePair<JsonString, JsonElement>>, IDictionary<JsonString, JsonElement>
	{
		#region Properties
		private IDictionary<JsonString, JsonElement> Elements { get; set; }

		public int Count { get => Elements.Count; }
		public bool IsReadOnly { get => Elements.IsReadOnly; } 

		public ICollection<JsonString> Keys { get => Elements.Keys; }
		public ICollection<JsonElement> Values { get => Elements.Values; }
		#endregion


		#region Constructors
		public JsonObject()
			: this(new Dictionary<JsonString, JsonElement>()) { }

		public JsonObject(Dictionary<JsonString, JsonElement> elements)
		{
			Elements = elements;
		}
		#endregion


		#region Interface Implementation - IEnumerable
		IEnumerator IEnumerable.GetEnumerator()
			=> GetEnumerator();
		#endregion


		#region Interface Implementation - IEnumerable<KeyValuePair<JsonString, JsonElement>>
		public IEnumerator<KeyValuePair<JsonString, JsonElement>> GetEnumerator()
			=> Elements.GetEnumerator();
		#endregion


		#region Interface Implementation - IDictionary<JsonString, JsonElement>
		public void Clear()
			=> Elements.Clear();

		public bool Contains(KeyValuePair<JsonString, JsonElement> item)
			=> Elements.Contains(item);

		public bool ContainsKey(JsonString key)
			=> Elements.ContainsKey(key);

		public bool TryGetValue(JsonString key, out JsonElement value)
			=> Elements.TryGetValue(key, out value);

		public void Add(JsonString key, JsonElement value)
			=> Elements.Add(key, value);

		public void Add(KeyValuePair<JsonString, JsonElement> item)
			=> Elements.Add(item);

		public bool Remove(JsonString key)
			=> Elements.Remove(key);

		public bool Remove(KeyValuePair<JsonString, JsonElement> item)
			=> Elements.Remove(item);

		public void CopyTo(KeyValuePair<JsonString, JsonElement>[] array, int arrayIndex)
			=> Elements.CopyTo(array, arrayIndex);
		#endregion


		#region Object Overrides
		public override int GetHashCode()
			=> Elements.GetHashCode();

		public override bool Equals(object obj)
			=> Elements.Equals(obj);
		#endregion


		#region Operator Overloads
		new public JsonElement this[JsonString index]
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

		public static implicit operator JsonObject(Dictionary<JsonString, JsonElement> elements)
			=> new JsonObject(elements);

		public static explicit operator Dictionary<JsonString, JsonElement>(JsonObject obj)
			=> (Dictionary<JsonString, JsonElement>)obj.Elements;
		#endregion


		#region Public API
		public Dictionary<JsonString, JsonElement> ToDictionary()
			=> new Dictionary<JsonString, JsonElement>(Elements);
		#endregion
	}
}
