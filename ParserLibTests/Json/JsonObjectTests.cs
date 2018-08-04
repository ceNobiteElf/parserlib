using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ParserLib.Json;

namespace ParserLibTests.Json
{
	[TestClass]
	public class JsonObjectTests
	{
		#region Tests - Constructors
		[TestMethod, TestCategory("JsonObject - Constructors")]
		public void Ctor_Dictionary_MatchingCollection()
		{
			var data = new Dictionary<JsonString, JsonElement> {
				["name"] = "Tester",
				["surname"] = "McTesty",
				["age"] = 100,
				["isTester"] = true,
				["null"] = JsonNull.Value,
				["offspring"] = new JsonArray { "Little Test" }
			};

			var result = new JsonObject(data);

			Assert.AreEqual(data.Count, result.Count);
			Assert.AreEqual("Tester", (string)result["name"]);
			Assert.AreEqual(100, (double)result["age"]);
			Assert.IsTrue((bool)result["isTester"]);
			Assert.IsInstanceOfType(result["offspring"], typeof(JsonArray));
		}

		[TestMethod, TestCategory("JsonObject - Constructors")]
		public void Ctor_Default_EmptyCollection()
		{
			var result = new JsonObject();

			Assert.AreEqual(0, result.Count);
		}
		#endregion


		#region Tests - Equals
		[TestMethod, TestCategory("JsonObject - Equality")]
		public void Equals_SameInstance_True()
		{
			var obj = new JsonObject { ["name"] = "Tester" };

			bool result = obj.Equals(obj);

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonObject - Equality")]
		public void Equals_JsonObjectAndJsonObjectSameValues_True()
		{
			var lhs = new JsonObject { ["name"] = "Tester" };
			var rhs = new JsonObject { ["name"] = "Tester" };

			bool result = lhs.Equals(rhs);

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonObject - Equality")]
		public void Equals_DifferentObjectTypes_False()
		{
			var lhs = new JsonObject { ["name"] = "Tester" };
			var rhs = new object();

			bool result = lhs.Equals(rhs);

			Assert.IsFalse(result);
		}

		[TestMethod, TestCategory("JsonObject - Equality")]
		public void Equals_JsonObjectAndJsonObjectDifferentValues_False()
		{
			var lhs = new JsonObject { ["name"] = "Tester" };
			var rhs = new JsonObject { ["name"] = "Testa" };

			bool result = lhs.Equals(rhs);

			Assert.IsFalse(result);
		}

		[TestMethod, TestCategory("JsonObject - Equality")]
		public void Equals_JsonObjectAndDictionarySameValues_True()
		{
			var lhs = new JsonObject { ["name"] = "Tester" };
			var rhs = new Dictionary<JsonString, JsonElement> { ["name"] = "Tester" };

			bool result = lhs.Equals(rhs);

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonObject - Equality")]
		public void Equals_JsonObjectAndNull_False()
		{
			var lhs = new JsonObject { ["name"] = "Tester" };
			JsonObject rhs = null;

			bool result = lhs.Equals(rhs);

			Assert.IsFalse(result);
		}
		#endregion


		#region Tests - Hash Codes
		[TestMethod, TestCategory("JsonObject - Hash Codes")]
		public void GetHashCode_SameValues_HashCodesAreTheSame()
		{
			var obj1 = new JsonObject { ["name"] = "Tester" };
			var obj2 = new JsonObject(new Dictionary<JsonString, JsonElement> { ["name"] = "Tester" });

			int result1 = obj1.GetHashCode();
			int result2 = obj2.GetHashCode();

			Assert.IsTrue(obj1.Equals(obj2));
			Assert.IsTrue(result1 == result2);
		}
		#endregion
	}
}
