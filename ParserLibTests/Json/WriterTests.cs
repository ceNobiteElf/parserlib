using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ParserLib.Json;

namespace ParserLibTests.Json
{
	[TestClass]
	public class WriterTests
	{
		#region Tests - General
		[TestMethod, TestCategory("JsonWriter - General"), ExpectedException(typeof(ArgumentNullException))]
		public void WriteToString_Null_ThrowsException()
		{
			JsonObject json = null;

			JsonWriter.WriteToString(json);
		}

		[TestMethod, TestCategory("JsonWriter - General"), ExpectedException(typeof(ArgumentException))]
		public void WriteToString_InvalidJsonType_ThrowsException()
		{
			JsonString json = string.Empty;

			JsonWriter.WriteToString(json);
		}
		#endregion


		#region Tests - JsonObject
		[TestMethod, TestCategory("JsonWriter - Write JsonObject")]
		public void WriteToString_JsonObject_DifferentTypes()
		{
			var json = new JsonObject
			{
				["name"] = "Tester",
				["surname"] = "McTesty",
				["age"] = 100,
				["isTester"] = true,
				["null"] = JsonNull.Value,
				["offspring"] = new JsonArray { "Little Test" },
				["friends"] = new JsonArray()
			};

			string result = JsonWriter.WriteToString(json);

			Assert.AreEqual("{\"name\":\"Tester\",\"surname\":\"McTesty\",\"age\":100,\"isTester\":true,\"null\":null,\"offspring\":[\"Little Test\"],\"friends\":[]}", result);
		}
		#endregion


		#region Tests - JsonArray
		[TestMethod, TestCategory("JsonWriter - Write JsonArray")]
		public void WriteToString_JsonArray_DifferentTypes()
		{
			var json = new JsonArray {
				"Red", "Green", "Blue", 1, 2.5, true, false, null
			};

			string result = JsonWriter.WriteToString(json);

			Assert.AreEqual("[\"Red\",\"Green\",\"Blue\",1,2.5,true,false,null]", result);
		}
		#endregion


		#region Test - JsonString
		[TestMethod, TestCategory("JsonWriter - Write JsonArray")]
		public void WriteToString_JsonStringWithEscapedCharacters_ValidEscapedSequences()
		{
			var json = new JsonArray { "\' \" \\ / \a \b \f \n \r \t \v" };

			string result = JsonWriter.WriteToString(json);

			Assert.AreEqual(@"[""\' \"" \\ \/ \a \b \f \n \r \t \v""]", result);
		}
		#endregion
	}
}
