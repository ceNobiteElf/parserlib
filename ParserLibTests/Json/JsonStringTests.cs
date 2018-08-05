using Microsoft.VisualStudio.TestTools.UnitTesting;

using ParserLib.Json;

using ParserLibTests.Internal;

namespace ParserLibTests.Json
{
	[TestClass]
	public sealed class JsonStringTests
	{
		#region Tests - Constructors
		[TestMethod, TestCategory("JsonString - Constructors")]
		public void Ctor_String()
			=> Assert.AreEqual("TestString", new JsonString("TestString").Value);

		[TestMethod, TestCategory("JsonString - Constructors")]
		public void Ctor_Default_EmptyString()
			=> Assert.AreEqual(string.Empty, new JsonString().Value);

		[TestMethod, TestCategory("JsonString - Constructors")]
		public void Ctor_Null_EmptyString()
			=> Assert.AreEqual(string.Empty, new JsonString(null).Value);
		#endregion


		#region Tests - Equals
		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsJsonString_SameInstance_True()
		{
			JsonString obj = "test";
			EqualityTester.AssertEquals(obj, obj);
		}

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsJsonString_SameValues_True()
			=> EqualityTester.AssertEquals<JsonString, JsonString>("test", "test");

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsJsonString_DifferentValues_False()
			=> EqualityTester.AssertNotEquals<JsonString, JsonString>("test1", "test2");

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsJsonString_Null_False()
			=> EqualityTester.AssertNotEquals<JsonString, JsonString>("test", null);

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsString_SameValues_True()
			=> EqualityTester.AssertEquals<JsonString, string>("test", "test");

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsString_DifferentValues_False()
			=> EqualityTester.AssertNotEquals<JsonString, string>("test1", "test2");

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsString_Null_False()
			=> EqualityTester.AssertNotEquals<JsonString, string>("test", null);

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsObject_SameInstance_True()
		{
			JsonString obj = "test";
			EqualityTester.AssertEquals<JsonString>(obj, obj);
		}

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsObject_DifferentTypes_False()
			=> EqualityTester.AssertNotEquals<JsonString>("test", new object());

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsObject_SameValues_True()
			=> EqualityTester.AssertEquals<JsonString>("test", new JsonString("test"));

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsObject_DifferentValues_False()
			=> EqualityTester.AssertNotEquals<JsonString>("test1", new JsonString("test2"));
		#endregion


		#region Tests - Hash Codes
		[TestMethod, TestCategory("JsonString - Hash Codes")]
		public void GetHashCode_SameValues_HashCodesAreTheSame()
			=> EqualityTester.AssertSameHashCodes<JsonString>("test", "test");
		#endregion
	}
}
