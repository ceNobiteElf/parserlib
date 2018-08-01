using Microsoft.VisualStudio.TestTools.UnitTesting;

using ParserLib.Json;
using ParserLib.Json.Exceptions;

namespace ParserLibTests.Json
{
	[TestClass]
	public class ParserTests
	{
		#region Tests - General
		[TestMethod, TestCategory("General"), ExpectedException(typeof(UnexpectedEndException))]
		public void ParseFromString_PartialJson_ThrowsException()
		{
			string jsonString = "{'name': 'Quarter', ";

			JsonParser.ParseFromString(jsonString);
		}

		[TestMethod, TestCategory("General")]
		public void ParseFromString_InvalidJson_ReturnsNull()
		{
			string jsonString = "{'name': 'Quarter', ";

			var result = JsonParser.ParseFromString(jsonString, new ReaderOptions { NullOnExceptions = true });

			Assert.IsNull(result);
		}

		[TestMethod, TestCategory("General")]
		public void ParseFromString_InvalidJson_ReturnsNullWithCast()
		{
			string jsonString = "{'name': 'Quarter', ";

			var result = JsonParser.ParseFromString<JsonObject>(jsonString, new ReaderOptions { NullOnExceptions = true });

			Assert.IsNull(result);
		}

		[TestMethod, TestCategory("General"), ExpectedException(typeof(MultipleRootsException))]
		public void ParseFromString_InvalidJson_MultipleRootsThrowsException()
		{
			string jsonString = "['first'] ['second']";

			JsonParser.ParseFromString(jsonString, new ReaderOptions { MultipleRootsBehaviour = MultipleRootsBehaviour.ThrowException });
		}

		[TestMethod, TestCategory("General")]
		public void ParseFromString_InvalidJson_MultipleRootsReturnsFirst()
		{
			string jsonString = "['first'] ['second']";

			var result = JsonParser.ParseFromString<JsonArray>(jsonString, new ReaderOptions { MultipleRootsBehaviour = MultipleRootsBehaviour.ReturnFirst });

			Assert.AreEqual(1, result.Count);
			Assert.AreEqual("first", (string)result[0]);
		}
		#endregion


		#region Tests - JsonObject
		[TestMethod, TestCategory("JsonObject")]
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

		[TestMethod, TestCategory("JsonObject"), ExpectedException(typeof(DuplicateKeyException))]
		public void ParseFromString_InvalidJsonObject_DuplicateKeysThrowsException()
		{
			string jsonString = "{'name': 'First', 'name': 'Second'}";

			JsonParser.ParseFromString(jsonString, new ReaderOptions { DuplicateKeyBehaviour = DuplicateKeyBehaviour.ThrowException });
		}

		[TestMethod, TestCategory("JsonObject")]
		public void ParseFromString_InvalidJsonObject_DuplicateKeysIgnore()
		{
			string jsonString = "{'name': 'First', 'name': 'Second'}";

			var result = JsonParser.ParseFromString<JsonObject>(jsonString, new ReaderOptions { DuplicateKeyBehaviour = DuplicateKeyBehaviour.Ignore });

			Assert.AreEqual(1, result.Count);
			Assert.AreEqual("First", (string)result["name"]);
		}

		[TestMethod, TestCategory("JsonObject")]
		public void ParseFromString_InvalidJsonObject_DuplicateKeysOverwrite()
		{
			string jsonString = "{'name': 'First', 'name': 'Second'}";

			var result = JsonParser.ParseFromString<JsonObject>(jsonString, new ReaderOptions { DuplicateKeyBehaviour = DuplicateKeyBehaviour.Overwrite });

			Assert.AreEqual(1, result.Count);
			Assert.AreEqual("Second", (string)result["name"]);
		}

		[TestMethod, TestCategory("JsonObject"), ExpectedException(typeof(UnexpectedTokenException))]
		public void ParseFromString_MalformedJsonObject_ThrowsException()
		{
			string jsonString = "{'name', 'Quarter': 'surname', 'Century'}";

			JsonParser.ParseFromString(jsonString);
		}
		#endregion


		#region Tests - JsonArray
		[TestMethod, TestCategory("JsonArray")]
		public void ParseFromString_ValidJsonArray_CorrectValues()
		{
			string jsonString = "[{'name': 'Con', 'occupation': 'Developer'}, {'name': 'Kirsten', 'occupation': 'Programmer'}, {'name' : 'Robin', 'occupation': 'Engineer'}]";

			var result = JsonParser.ParseFromString<JsonArray>(jsonString);

			Assert.AreEqual(3, result.Count);
			Assert.AreEqual("Engineer", (string)result[2]["occupation"]);
		}

		[TestMethod, TestCategory("JsonArray"), ExpectedException(typeof(UnexpectedTokenException))]
		public void ParseFromString_MalformedJsonArray_ThrowsException()
		{
			string jsonString = "['1st'; '2nd': '3rd', null]";

			JsonParser.ParseFromString(jsonString);
		}
		#endregion


		#region Tests - JsonString
		[TestMethod, TestCategory("JsonString")]
		public void ParseFromString_ValidJsonString_SingleQuotes()
		{
			string jsonString = "{'name': 'Stabbins'}";

			var result = JsonParser.ParseFromString<JsonObject>(jsonString);

			Assert.AreEqual(1, result.Count);
			Assert.AreEqual("Stabbins", (string)result["name"]);
		}

		[TestMethod, TestCategory("JsonString")]
		public void ParseFromString_ValidJsonString_DoubleQuotes()
		{
			string jsonString = "{'name': 'Stabbins'}";

			var result = JsonParser.ParseFromString<JsonObject>(jsonString);

			Assert.AreEqual(1, result.Count);
			Assert.AreEqual("Stabbins", (string)result["name"]);
		}

		[TestMethod, TestCategory("JsonString"), ExpectedException(typeof(JsonException), AllowDerivedTypes = true)]
		public void ParseFromString_MalformedJsonString_ThrowsException()
		{
			string jsonString = "{'name': \"Stabbins'}";

			JsonParser.ParseFromString(jsonString);
		}

		[TestMethod, TestCategory("JsonString")]
		public void ParseFromString_ValidJsonString_ValidEscapeSequences()
		{
			string jsonString = "{'text': '\\t\\n\\''}";

			var result = JsonParser.ParseFromString<JsonObject>(jsonString);

			Assert.AreEqual(1, result.Count);
			Assert.AreEqual("\t\n\'", (string)result["text"]);
		}

		[TestMethod, TestCategory("JsonString"), ExpectedException(typeof(InvalidEscapeSequenceException))]
		public void ParseFromString_ValidJsonString_InvalidEscapeSequenceThrowsException()
		{
			string jsonString = "{'text': '\\g'}";

			JsonParser.ParseFromString(jsonString);
		}
		#endregion


		#region Tests - JsonNumber
		[TestMethod, TestCategory("JsonNumber")]
		public void ParseFromString_ValidJsonNumber_PositiveNumbers()
		{
			string jsonString = "[1, 2.2, 3.14159, 42, 1337]";

			var result = JsonParser.ParseFromString<JsonArray>(jsonString);

			Assert.AreEqual(5, result.Count);
			Assert.AreEqual(1, (double)result[0]);
			Assert.AreEqual(2.2, (double)result[1]);
			Assert.AreEqual(3.14159, (double)result[2]);
			Assert.AreEqual(42, (double)result[3]);
			Assert.AreEqual(1337, (double)result[4]);
		}

		[TestMethod, TestCategory("JsonNumber")]
		public void ParseFromString_ValidJsonNumber_NegativeNumbers()
		{
			string jsonString = "[-1, -2.2, -3.14159, -42, -1337]";

			var result = JsonParser.ParseFromString<JsonArray>(jsonString);

			Assert.AreEqual(5, result.Count);
			Assert.AreEqual(-1, (double)result[0]);
			Assert.AreEqual(-2.2, (double)result[1]);
			Assert.AreEqual(-3.14159, (double)result[2]);
			Assert.AreEqual(-42, (double)result[3]);
			Assert.AreEqual(-1337, (double)result[4]);
		}

		[TestMethod, TestCategory("JsonNumber")]
		public void ParseFromString_ValidJsonNumber_Exponents()
		{
			string jsonString = "[1e10, 2E10, 3e-10, 4E-10]";

			var result = JsonParser.ParseFromString<JsonArray>(jsonString);

			Assert.AreEqual(4, result.Count);
			Assert.AreEqual(1e10, (double)result[0]);
			Assert.AreEqual(2E10, (double)result[1]);
			Assert.AreEqual(3e-10, (double)result[2]);
			Assert.AreEqual(4E-10, (double)result[3]);
		}

		[TestMethod, TestCategory("JsonNumber"), ExpectedException(typeof(ValueParseException))]
		public void ParseFromString_InvalidJsonNumber_ThrowsException()
		{
			string jsonString = "[12l, 13d, 44e10.5]";

			JsonParser.ParseFromString(jsonString);
		}
		#endregion


		#region Tests - JsonBool
		[TestMethod, TestCategory("JsonBool")]
		public void ParseFromString_ValidJsonBool_True()
		{
			string jsonString = "{'isValid': true}";

			var result = JsonParser.ParseFromString<JsonObject>(jsonString);

			Assert.AreEqual(1, result.Count);
			Assert.IsTrue((bool)result["isValid"]);
		}

		[TestMethod, TestCategory("JsonBool")]
		public void ParseFromString_ValidJsonBool_False()
		{
			string jsonString = "{'isValid': false}";

			var result = JsonParser.ParseFromString<JsonObject>(jsonString);

			Assert.AreEqual(1, result.Count);
			Assert.IsFalse((bool)result["isValid"]);
		}

		[TestMethod, TestCategory("JsonBool"), ExpectedException(typeof(ValueParseException))]
		public void ParseFromString_InvalidJsonBool_ThrowsException()
		{
			string jsonString = "{'isValid': truestring}";

			JsonParser.ParseFromString(jsonString);
		}
		#endregion


		#region Tests - JsonNull
		[TestMethod, TestCategory("JsonNull")]
		public void ParseFromString_ValidJsonNull()
		{
			string jsonString = "{'null': null}";

			var result = JsonParser.ParseFromString<JsonObject>(jsonString);

			Assert.AreEqual(1, result.Count);
			Assert.AreEqual(JsonNull.Instance, result["null"]);
		}

		[TestMethod, TestCategory("JsonNull"), ExpectedException(typeof(ValueParseException))]
		public void ParseFromString_InvalidJsonNull_ThrowsException()
		{
			string jsonString = "{'null': nullstring}";

			JsonParser.ParseFromString(jsonString);
		}
		#endregion
	}
}
