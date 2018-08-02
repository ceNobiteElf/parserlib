using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ParserLib.Json;

namespace ParserLibTests.Json
{
	[TestClass]
	public class JsonBoolTests
	{
		#region Tests
		[TestMethod, TestCategory("JsonBool - Constructors")]
		public void Ctor_True()
		{
			var result = new JsonBool(true);

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonBool - Constructors")]
		public void Ctor_False()
		{
			var result = new JsonBool(false);

			Assert.IsFalse(result);
		}
		#endregion


		#region Tests - Comparison
		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareTo_JsonBoolAndBool_Zero()
		{
			JsonBool lhs = true;
			bool rhs = true;

			int result = lhs.CompareTo(rhs);

			Assert.AreEqual(0, result);
		}

		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareTo_JsonBoolAndBool_GTZero()
		{
			JsonBool lhs = true;
			bool rhs = false;

			int result = lhs.CompareTo(rhs);

			Assert.IsTrue(result > 0);
		}

		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareTo_JsonBoolAndBool_LTZero()
		{
			JsonBool lhs = false;
			bool rhs = true;

			int result = lhs.CompareTo(rhs);

			Assert.IsTrue(result < 0);
		}

		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareTo_JsonBoolAndJsonBool_Zero()
		{
			JsonBool lhs = true;
			JsonBool rhs = true;

			int result = lhs.CompareTo(rhs);

			Assert.AreEqual(0, result);
		}

		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareTo_JsonBoolAndJsonBool_GTZero()
		{
			JsonBool lhs = true;
			JsonBool rhs = false;

			int result = lhs.CompareTo(rhs);

			Assert.IsTrue(result > 0);
		}

		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareTo_JsonBoolAndJsonBool_LTZero()
		{
			JsonBool lhs = false;
			JsonBool rhs = true;

			int result = lhs.CompareTo(rhs);

			Assert.IsTrue(result < 0);
		}

		[TestMethod, TestCategory("JsonBool - Comparison"), ExpectedException(typeof(NullReferenceException))]
		public void CompareTo_NullAndNull_ThrowsException()
		{
			JsonBool lhs = null;
			JsonBool rhs = null;

			lhs.CompareTo(rhs);
		}

		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareTo_JsonBoolAndNull_GTZero()
		{
			JsonBool lhs = true;
			JsonBool rhs = null;

			int result = lhs.CompareTo(rhs);

			Assert.IsTrue(result > 0);
		}
		#endregion


		#region Tests - Operators
		[TestMethod, TestCategory("JsonBool - Equality")]
		public void Equality_JsonBoolAndJsonBool_True()
		{
			JsonBool lhs = true;
			JsonBool rhs = true;

			bool result = lhs == rhs;

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void Equality_JsonBoolAndJsonBool_False()
		{
			JsonBool lhs = true;
			JsonBool rhs = false;

			bool result = lhs == rhs;

			Assert.IsFalse(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void Equality_JsonBoolAndNull_False()
		{
			JsonBool lhs = false;
			JsonBool rhs = null;

			bool result = lhs == rhs;

			Assert.IsFalse(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void Equality_NullAndJsonBool_False()
		{
			JsonBool lhs = null;
			JsonBool rhs = false;

			bool result = lhs == rhs;

			Assert.IsFalse(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void Equality_NullAndNull_True()
		{
			JsonBool lhs = null;
			JsonBool rhs = null;

			bool result = lhs == rhs;

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonBool - Inequality")]
		public void Inequality_JsonBoolAndJsonBool_True()
		{
			JsonBool lhs = true;
			JsonBool rhs = false;

			bool result = lhs != rhs;

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonBool - Inequality")]
		public void Inequality_JsonBoolAndJsonBool_False()
		{
			JsonBool lhs = true;
			JsonBool rhs = true;

			bool result = lhs != rhs;

			Assert.IsFalse(result);
		}

		[TestMethod, TestCategory("JsonBool - Inequality")]
		public void Inequality_JsonBoolAndNull_True()
		{
			JsonBool lhs = false;
			JsonBool rhs = null;

			bool result = lhs != rhs;

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonBool - Inequality")]
		public void Inequality_NullAndJsonBool_True()
		{
			JsonBool lhs = null;
			JsonBool rhs = false;

			bool result = lhs != rhs;

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonBool - Inequality")]
		public void Inequality_NullAndNull_False()
		{
			JsonBool lhs = null;
			JsonBool rhs = null;

			bool result = lhs != rhs;

			Assert.IsFalse(result);
		}
		#endregion
	}
}
