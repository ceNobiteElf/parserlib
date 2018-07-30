using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ParserLib.Json;

namespace ParserLibTests.Json
{
	[TestClass]
	public class ParserTests
	{
		[TestMethod, TestCategory("ParseFromString")]
		public void ParseFromString_ValidJsonArray_CorrectValues()
		{
			string jsonString = "[{'name': 'Con', 'occupation': 'Developer'}, {'name': 'Kirsten', 'occupation': 'Programmer'}, {'name' : 'Robin', 'occupation': 'Engineer'}]";

			var result = JsonParser.ParseFromString<JsonArray>(jsonString);

			Assert.AreEqual(3, result.Count);
			Assert.AreEqual("Engineer", (string)result[2]["occupation"]);
		}

		[TestMethod, TestCategory("ParseFromString")]
		public void ParseFromString_ValidJsonObject_DifferentTypes()
		{
			string jsonString = "{'name': 'Quarter', 'surname': 'Century', 'occupation': 'Developer', 'age': 25, 'height': 181.5, 'married': false, 'hasCat': true, 'children': [], 'lastKnownMeal': null}";

			var result = JsonParser.ParseFromString<JsonObject>(jsonString);

			Assert.AreEqual(9, result.Count);
			Assert.IsInstanceOfType(result["name"], typeof(JsonString));
			Assert.IsInstanceOfType(result["surname"], typeof(JsonString));
			Assert.IsInstanceOfType(result["occupation"], typeof(JsonString));
			Assert.IsInstanceOfType(result["age"], typeof(JsonNumber));
			Assert.IsInstanceOfType(result["height"], typeof(JsonNumber));
			Assert.IsInstanceOfType(result["married"], typeof(JsonBool));
			Assert.IsInstanceOfType(result["hasCat"], typeof(JsonBool));
			Assert.IsInstanceOfType(result["children"], typeof(JsonArray));
			Assert.IsInstanceOfType(result["lastKnownMeal"], typeof(JsonNull));
		}

		[TestMethod, TestCategory("ParseFromString")]
		public void ParseFromString_ValidJsonBool_True()
		{
			string jsonString = "{'isValid': true}";

			var result = JsonParser.ParseFromString<JsonObject>(jsonString);

			Assert.AreEqual(1, result.Count);
			Assert.AreEqual(true, (bool)result["isValid"]);
		}

		[TestMethod, TestCategory("ParseFromString")]
		public void ParseFromString_ValidJsonBool_False()
		{
			string jsonString = "{'isValid': false}";

			var result = JsonParser.ParseFromString<JsonObject>(jsonString);

			Assert.AreEqual(1, result.Count);
			Assert.AreEqual(false, (bool)result["isValid"]);
		}

		[TestMethod, TestCategory("ParseFromString"), ExpectedException(typeof(Exception))]
		public void ParseFromString_InvalidJsonBool_ThrowsException()
		{
			string jsonString = "{'isValid': truestring}";

			JsonParser.ParseFromString(jsonString);
		}

		[TestMethod, TestCategory("ParseFromString"), ExpectedException(typeof(Exception))]
		public void ParseFromString_InvalidJsonPairs_ThrowsException()
		{
			string jsonString = "{'name', 'Quarter': 'surname', 'Century'}";

			JsonParser.ParseFromString(jsonString);
		}
	}
}
