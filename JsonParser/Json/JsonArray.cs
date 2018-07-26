using System.Collections;
using System.Collections.Generic;

namespace ParserLib.Json
{
	public sealed class JsonArray : JsonElement, IEnumerable, IEnumerable<JsonElement>, IList<JsonElement>
	{
		#region Properties
		private IList<JsonElement> Values { get; set; }

		public int Count { get { return Values.Count; } }
		public bool IsReadOnly { get { return Values.IsReadOnly; } }
		#endregion


		#region Constructors
		public JsonArray()
		{
			Values = new List<JsonElement>();
		}

		public JsonArray(IList<JsonElement> values)
		{
			Values = new List<JsonElement>(values);
		}
		#endregion


		#region Interface Implementation - IEnumerable
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		#endregion


		#region Interface Implementation - IEnumerable<JsonElement>
		public IEnumerator<JsonElement> GetEnumerator()
		{
			return Values.GetEnumerator();
		}
		#endregion


		#region Interface Implementation - IList<JsonElement>
		public void Clear()
		{
			Values.Clear();
		}

		public bool Contains(JsonElement value)
		{
			return Values.Contains(value);
		}

		public int IndexOf(JsonElement item)
		{
			return Values.IndexOf(item);
		}

		public void Add(JsonElement value)
		{
			Values.Add(value);
		}

		public void Insert(int index, JsonElement item)
		{
			Values.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			Values.RemoveAt(index);
		}

		public bool Remove(JsonElement item)
		{
			return Values.Remove(item);
		}

		public void CopyTo(JsonElement[] array, int arrayIndex)
		{
			Values.CopyTo(array, arrayIndex);
		}
		#endregion


		#region Object Overrides
		public override int GetHashCode()
		{
			return Values.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return Values.Equals(obj);
		}
		#endregion


		#region Operator Overloads
		new public JsonElement this[int index]
		{
			get { return Values[index]; }
			set { Values[index] = value; }
		}

		public static implicit operator JsonArray(List<JsonElement> values)
		{
			return new JsonArray(values);
		}

		public static explicit operator List<JsonElement>(JsonArray obj)
		{
			return (List<JsonElement>)obj.Values;
		}
		#endregion


		#region Public API
		public List<JsonElement> ToList()
		{
			return new List<JsonElement>(Values);
		}

		public JsonElement[] ToArray()
		{
			return ((List<JsonElement>)Values).ToArray();
		}
		#endregion
	}
}
