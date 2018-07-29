using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ParserLib.Json;

namespace ParserLibTests
{
	[TestClass]
	public class ParserTests
	{
		[TestMethod]
		public void ParseFromString_ValidJson()
		{
			var result = JsonParser.ParseFromString<JsonArray>("[{'name': 'Con', 'occupation': 'Developer'}, {'name': 'Kirsten', 'occupation': 'Programmer'}, {'name' : 'Robin', 'occupation': 'Engineer'}]");

			Assert.AreEqual(3, result.Count);
			Assert.AreEqual("Engineer", (string)result[2]["occupation"]);
		}
	}
}
