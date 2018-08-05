using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ParserLib.Json;

using ParserLibTests.Internal;

namespace ParserLibTests.Json
{
	[TestClass]
	public sealed class JsonArrayTests
	{
		#region Tests - Constructors
		[TestMethod, TestCategory("JsonArray - Constructors")]
		public void Ctor_Default_EmptyCollection()
			=> Assert.AreEqual(0, new JsonArray().Count);

		[TestMethod, TestCategory("JsonArray - Constructors")]
		public void Ctor_List_MatchingCollection()
			=> AssertConstructor(GetData());

		[TestMethod, TestCategory("JsonArray - Constructors")]
		public void Ctor_Array_MatchingCollectionAndReadonly()
			=> AssertConstructor(GetReadonlyData());
		#endregion


		#region Tests - Equals
		[TestMethod, TestCategory("JsonArray - Equality")]
		public void EqualsObject_SameInstance_True()
		{
			var obj = new JsonArray(GetData());
			EqualityTester.AssertEquals<JsonArray>(obj, obj);
		}

		[TestMethod, TestCategory("JsonArray - Equality")]
		public void EqualsObject_DifferentTypes_False()
			=> EqualityTester.AssertNotEquals<JsonArray>(GetData(), new object());

		[TestMethod, TestCategory("JsonArray - Equality")]
		public void EqualsObject_SameValues_True()
			=> EqualityTester.AssertEquals<JsonArray>(GetData(), new JsonArray(GetReadonlyData()));

		[TestMethod, TestCategory("JsonArray - Equality")]
		public void EqualsObject_SameValuesDifferentOrder_False()
		=> EqualityTester.AssertNotEquals<JsonArray>(new JsonArray { "a", "b", "c"}, new JsonArray { "c", "a", "b" });

		[TestMethod, TestCategory("JsonArray - Equality")]
		public void EqualsObject_DifferentValues_False()
			=> EqualityTester.AssertNotEquals<JsonArray>(GetData(), new JsonArray { "hello", 5 });

		[TestMethod, TestCategory("JsonArray - Equality")]
		public void EqualsObject_Null_False()
			=> EqualityTester.AssertNotEquals<JsonArray>(GetData(), null);
		#endregion


		#region Tests - Hash Codes
		[TestMethod, TestCategory("JsonArray - Hash Codes")]
		public void GetHashCode_SameValues_HashCodesAreTheSame()
			=> EqualityTester.AssertSameHashCodes<JsonArray>(GetData(), GetReadonlyData());
		#endregion


		#region Helper Functions
		private static List<JsonElement> GetData()
			=> new List<JsonElement> { "Test", 1, true, null, new JsonObject() };

		private static JsonElement[] GetReadonlyData()
			=> new JsonElement[] { "Test", 1, true, null, new JsonObject() };

		static void AssertConstructor(IList<JsonElement> data)
		{
			var result = new JsonArray(data);

			Assert.AreEqual(data.IsReadOnly, result.IsReadOnly);
			Assert.AreEqual(data.Count, result.Count);

			Assert.AreEqual("Test", (string)result[0]);
			Assert.AreEqual(1, (double)result[1]);
			Assert.IsTrue((bool)result[2]);
			Assert.IsNull(result[3]);
			Assert.IsInstanceOfType(result[4], typeof(JsonObject));
		}
		#endregion
	}
}
