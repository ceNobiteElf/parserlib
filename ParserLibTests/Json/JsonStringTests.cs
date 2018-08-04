using Microsoft.VisualStudio.TestTools.UnitTesting;

using ParserLib.Json;

namespace ParserLibTests.Json
{
	[TestClass]
	public class JsonStringTests
	{
		#region Tests - Constructors
		[TestMethod, TestCategory("JsonString - Constructors")]
		public void Ctor_String()
		{
			var result = new JsonString("TestString");

			Assert.AreEqual("TestString", result.Value);
		}

		[TestMethod, TestCategory("JsonString - Constructors")]
		public void Ctor_Default_EmptyString()
		{
			var result = new JsonString();

			Assert.AreEqual(string.Empty, result.Value);
		}

		[TestMethod, TestCategory("JsonString - Constructors")]
		public void Ctor_Null_EmptyString()
		{
			var result = new JsonString(null);

			Assert.AreEqual(string.Empty, result.Value);
		}
		#endregion


		#region Tests - Equals
		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsJsonString_SameInstance_True()
		{
			JsonString obj = "test";

			bool result = obj.Equals(obj);

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsJsonString_SameValues_True()
		{
			JsonString lhs = "test";
			JsonString rhs = "test";

			bool result = lhs.Equals(rhs);

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsJsonString_DifferentValues_False()
		{
			JsonString lhs = "test1";
			JsonString rhs = "test2";

			bool result = lhs.Equals(rhs);

			Assert.IsFalse(result);
		}

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsJsonString_Null_False()
		{
			JsonString lhs = "test1";
			JsonString rhs = null;

			bool result = lhs.Equals(rhs);

			Assert.IsFalse(result);
		}

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsString_SameValues_True()
		{
			JsonString lhs = "test";
			string rhs = "test";

			bool result = lhs.Equals(rhs);

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsString_DifferentValues_False()
		{
			JsonString lhs = "test1";
			string rhs = "test2";

			bool result = lhs.Equals(rhs);

			Assert.IsFalse(result);
		}

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsString_null_False()
		{
			JsonString lhs = "test";
			string rhs = null;

			bool result = lhs.Equals(rhs);

			Assert.IsFalse(result);
		}

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsObject_SameInstance_True()
		{
			object obj = new JsonString("test");

			bool result = obj.Equals(obj);

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsObject_DifferentTypes_False()
		{
			JsonString lhs = "test";
			var rhs = new object();

			bool result = lhs.Equals(rhs);

			Assert.IsFalse(result);
		}

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsObject_SameValues_True()
		{
			JsonString lhs = "test";
			object rhs = new JsonString("test");

			bool result = lhs.Equals(rhs);

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonString - Equality")]
		public void EqualsObject_DifferentValues_False()
		{
			JsonString lhs = "test1";
			object rhs = new JsonString("test2");

			bool result = lhs.Equals(rhs);

			Assert.IsFalse(result);
		}
		#endregion


		#region Tests - Hash Codes
		[TestMethod, TestCategory("JsonString - Hash Codes")]
		public void GetHashCode_SameValues_HashCodesAreTheSame()
		{
			JsonString obj1 = "test";
			JsonString obj2 = "test";

			int result1 = obj1.GetHashCode();
			int result2 = obj2.GetHashCode();

			Assert.IsTrue(obj1.Equals(obj2));
			Assert.IsTrue(result1 == result2);
		}
		#endregion
	}
}
