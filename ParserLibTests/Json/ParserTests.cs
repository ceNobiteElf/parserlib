using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ParserLib.Json;

namespace ParserLibTests.Json
{
	[TestClass]
	public class ParserTests
	{
		[TestMethod, TestCategory("ParseFromString")]
		public void ParseFromString_ValidJson_CorrectValues()
		{
			string jsonString = "[{'name': 'Con', 'occupation': 'Developer'}, {'name': 'Kirsten', 'occupation': 'Programmer'}, {'name' : 'Robin', 'occupation': 'Engineer'}]";

			var result = JsonParser.ParseFromString<JsonArray>(jsonString);

			Assert.AreEqual(3, result.Count);
			Assert.AreEqual("Engineer", (string)result[2]["occupation"]);
		}


		[TestMethod, TestCategory("ParseFromString")]
		public void ParseFromString_ValidJson_DifferentTypes()
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
	}
}
