using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ParserLib.Json;

using ParserLibTests.Internal;

namespace ParserLibTests.Json
{
	[TestClass]
	public sealed class JsonObjectTests
	{
		#region Tests - Constructors
		[TestMethod, TestCategory("JsonObject - Constructors")]
		public void Ctor_Default_EmptyCollection()
			=> Assert.AreEqual(0, new JsonObject().Count);

		[TestMethod, TestCategory("JsonObject - Constructors")]
		public void Ctor_Dictionary_MatchingCollection()
		{
			var data = GetData();

			var result = new JsonObject(data);

			Assert.AreEqual(data.Count, result.Count);
			Assert.AreEqual("Tester", (string)result["name"]);
			Assert.AreEqual(100, (double)result["age"]);
			Assert.IsTrue((bool)result["isTester"]);
			Assert.IsNull(result["trueNull"]);
			Assert.IsInstanceOfType(result["offspring"], typeof(JsonArray));
		}
		#endregion


		#region Tests - Equals
		[TestMethod, TestCategory("JsonObject - Equality")]
		public void EqualsObject_SameInstance_True()
		{
			JsonObject obj = GetData();
			EqualityTester.AssertEquals<JsonObject>(obj, obj);
		}

		[TestMethod, TestCategory("JsonObject - Equality")]
		public void EqualsObject_DifferentTypes_False()
			=> EqualityTester.AssertNotEquals<JsonObject>(GetData(), new object());

		[TestMethod, TestCategory("JsonObject - Equality")]
		public void EqualsObject_SameValues_True()
			=> EqualityTester.AssertEquals<JsonObject>(GetData(), new JsonObject(GetData()));

		[TestMethod, TestCategory("JsonObject - Equality")]
		public void EqualsObject_SameValuesVariation_True()
			=> EqualityTester.AssertEquals<JsonObject>(GetDataVariation(), new JsonObject(GetDataVariation()));

		[TestMethod, TestCategory("JsonObject - Equality")]
		public void EqualsObject_DifferentValues_False()
			=> EqualityTester.AssertNotEquals<JsonObject>(GetData(), new JsonObject(GetDataVariation()));

		[TestMethod, TestCategory("JsonObject - Equality")]
		public void EqualsObject_Null_False()
			=> EqualityTester.AssertNotEquals<JsonObject>(GetData(), null);
		#endregion


		#region Tests - Hash Codes
		[TestMethod, TestCategory("JsonObject - Hash Codes")]
		public void GetHashCode_SameValues_HashCodesAreTheSame()
			=> EqualityTester.AssertSameHashCodes<JsonObject>(GetData(), GetData());

		[TestMethod, TestCategory("JsonObject - Hash Codes")]
		public void GetHashCode_SameValuesVariation_HashCodesAreTheSame()
			=> EqualityTester.AssertSameHashCodes<JsonObject>(GetDataVariation(), GetDataVariation());
		#endregion


		#region Helper Functions
		private static Dictionary<JsonString, JsonElement> GetData()
		{
			return new Dictionary<JsonString, JsonElement> {
				["name"] = "Tester",
				["surname"] = "McTesty",
				["age"] = 100,
				["isTester"] = true,
				["null"] = JsonNull.Value,
				["trueNull"] = null,
				["offspring"] = new JsonArray { "Little Test" },
				["subObject"] = new JsonObject()
			};
		}

		private static Dictionary<JsonString, JsonElement> GetDataVariation()
		{
			return new Dictionary<JsonString, JsonElement>
			{
				["name"] = "Tester",
				["age"] = 100,
				["isTester"] = false,
				["null"] = null,
				["origin"] = "Testerville",
				["aliases"] = new JsonArray { "AnchorJack", "Wavey" }
			};
		}
		#endregion
	}
}
