using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ParserLib.Json
{
	public sealed class JsonObject : JsonElement, IJsonRoot, IEnumerable, IEnumerable<KeyValuePair<JsonString, JsonElement>>, IDictionary<JsonString, JsonElement>
	{
		#region Properties
		private IDictionary<JsonString, JsonElement> Elements { get; }

		public int Count { get => Elements.Count; }
		public bool IsReadOnly { get => Elements.IsReadOnly; } 

		public ICollection<JsonString> Keys { get => Elements.Keys; }
		public ICollection<JsonElement> Values { get => Elements.Values; }
		#endregion


		#region Constructors
		public JsonObject()
			: this(new Dictionary<JsonString, JsonElement>()) { }

		public JsonObject(IDictionary<JsonString, JsonElement> elements)
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
		{
			int hashCode = 764305191;

			unchecked
			{
				foreach (KeyValuePair<JsonString, JsonElement> pair in Elements)
				{
					hashCode = hashCode * -1521134295 + pair.Key.GetHashCode();
					hashCode = hashCode * -1521134295 + pair.Value?.GetHashCode() ?? 0;
				}
			}

			return hashCode;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			bool equals = false;
			IDictionary<JsonString, JsonElement> other = null;

			if (obj is JsonObject otherObject)
			{
				other = otherObject.Elements;
			}
			else if (obj is IDictionary<JsonString, JsonElement> otherDictionary)
			{
				other = otherDictionary;
			}

			if (other != null)
			{
				equals = Elements.Count == other.Count && Keys.All(key => {
					if (other.ContainsKey(key))
					{
						JsonElement lhs = Elements[key];
						JsonElement rhs = other[key];

						return (lhs == null && rhs == null) || (lhs != null && lhs.Equals(rhs));
					}

					return false;
				});
			}

			return equals;
		}
		#endregion


		#region Operator Overloads
		new public JsonElement this[JsonString index]
		{
			get
			{
				TryGetValue(index, out JsonElement result);

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

		public static implicit operator Dictionary<JsonString, JsonElement>(JsonObject obj)
			=> (Dictionary<JsonString, JsonElement>)obj.Elements;
		#endregion


		#region Public API
		public Dictionary<JsonString, JsonElement> ToDictionary()
			=> new Dictionary<JsonString, JsonElement>(Elements);
		#endregion
	}
}
