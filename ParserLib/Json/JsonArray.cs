using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ParserLib.Json
{
	public sealed class JsonArray : JsonElement, IJsonRoot, IEnumerable, IEnumerable<JsonElement>, IList<JsonElement>
	{
		#region Properties
		private IList<JsonElement> Values { get; }

		public int Count { get => Values.Count; }
		public bool IsReadOnly { get => Values.IsReadOnly; }
		#endregion


		#region Constructors
		public JsonArray()
			: this(new List<JsonElement>()) { }

		public JsonArray(IList<JsonElement> values)
		{
			Values = values;
		}
		#endregion


		#region Interface Implementation - IEnumerable
		IEnumerator IEnumerable.GetEnumerator()
			=> GetEnumerator();
		#endregion


		#region Interface Implementation - IEnumerable<JsonElement>
		public IEnumerator<JsonElement> GetEnumerator()
			=> Values.GetEnumerator();
		#endregion


		#region Interface Implementation - IList<JsonElement>
		public void Clear()
			=> Values.Clear();

		public bool Contains(JsonElement value)
			=> Values.Contains(value);
		
		public int IndexOf(JsonElement item)
			=> Values.IndexOf(item);

		public void Add(JsonElement value)
			=> Values.Add(value);

		public void Insert(int index, JsonElement item)
			=> Values.Insert(index, item);

		public void RemoveAt(int index)
			=> Values.RemoveAt(index);

		public bool Remove(JsonElement item)
			=> Values.Remove(item);

		public void CopyTo(JsonElement[] array, int arrayIndex)
			=> Values.CopyTo(array, arrayIndex);
		#endregion


		#region Object Overrides
		public override int GetHashCode()
		{
			int hashCode = 764305191;

			unchecked
			{
				foreach (JsonElement value in Values)
				{
					hashCode = hashCode * -1521134295 + value?.GetHashCode() ?? 0;
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
			IList<JsonElement> other = null;

			if (obj is JsonArray otherObject)
			{
				other = otherObject.Values;
			}
			else if (obj is IList<JsonElement> otherList)
			{
				other = otherList;
			}

			if (other != null)
			{
				equals = Values.Count == other.Count && Values.SequenceEqual(other);
			}

			return equals;
		}
		#endregion


		#region Operator Overloads
		new public JsonElement this[int index]
		{
			get => Values[index];
			set => Values[index] = value;
		}

		public static implicit operator JsonArray(List<JsonElement> values)
			=> new JsonArray(values);

		public static implicit operator JsonArray(JsonElement[] values)
			=> new JsonArray(values);
		#endregion


		#region Public API
		public List<JsonElement> ToList()
			=> new List<JsonElement>(Values);

		public JsonElement[] ToArray()
			=> ((List<JsonElement>)Values).ToArray();
		#endregion
	}
}
