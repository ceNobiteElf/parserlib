using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ParserLib.Json;

namespace ParserLibTests.Json
{
	[TestClass]
	public class JsonArrayTests
	{
		#region Tests - Constructors
		[TestMethod, TestCategory("JsonArray - Constructors")]
		public void Ctor_Default_EmptyCollection()
		{
			var result = new JsonArray();

			Assert.AreEqual(0, result.Count);
		}

		[TestMethod, TestCategory("JsonArray - Constructors")]
		public void Ctor_List_MatchingCollection()
		{
			var data = new List<JsonElement> { "Test", 1, true, null, new JsonObject() };

			var result = new JsonArray(data);

			Assert.IsFalse(result.IsReadOnly);
			Assert.AreEqual(data.Count, result.Count);
			Assert.AreEqual("Test", (string)result[0]);
			Assert.AreEqual(1, (double)result[1]);
			Assert.IsTrue((bool)result[2]);
			Assert.IsNull(result[3]);
			Assert.IsInstanceOfType(result[4], typeof(JsonObject));
		}

		[TestMethod, TestCategory("JsonArray - Constructors")]
		public void Ctor_Array_MatchingCollectionAndReadonly()
		{
			var data = new JsonElement[] { "Test", 1, true, null, new JsonObject() };

			var result = new JsonArray(data);

			Assert.IsTrue(result.IsReadOnly);
			Assert.AreEqual(data.Length, result.Count);
			Assert.AreEqual("Test", (string)result[0]);
			Assert.AreEqual(1, (double)result[1]);
			Assert.IsTrue((bool)result[2]);
			Assert.IsNull(result[3]);
			Assert.IsInstanceOfType(result[4], typeof(JsonObject));
		}
		#endregion
	}
}
