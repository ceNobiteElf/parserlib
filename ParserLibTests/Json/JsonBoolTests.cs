using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ParserLib.Json;

namespace ParserLibTests.Json
{
	[TestClass]
	public class JsonBoolTests
	{
		#region Tests - Constructors
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
		public void CompareToJsonBool_SameValues_Zero()
		{
			JsonBool lhs = true;
			JsonBool rhs = true;

			int result = lhs.CompareTo(rhs);

			Assert.IsTrue(result == 0);
		}

		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareToJsonBool_LhsTrueRhsFalse_GTZero()
		{
			JsonBool lhs = true;
			JsonBool rhs = false;

			int result = lhs.CompareTo(rhs);

			Assert.IsTrue(result > 0);
		}

		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareToJsonBool_LhsFalseRhsTrue_LTZero()
		{
			JsonBool lhs = false;
			JsonBool rhs = true;

			int result = lhs.CompareTo(rhs);

			Assert.IsTrue(result < 0);
		}

		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareToJsonBool_LhsTrueRhsNull_GTZero()
		{
			JsonBool lhs = true;
			JsonBool rhs = null;

			int result = lhs.CompareTo(rhs);

			Assert.IsTrue(result > 0);
		}

		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareToBool_SameValues_Zero()
		{
			JsonBool lhs = true;
			bool rhs = true;

			int result = lhs.CompareTo(rhs);

			Assert.IsTrue(result == 0);
		}

		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareToBool_LhsTrueRhsFalse_GTZero()
		{
			JsonBool lhs = true;
			bool rhs = false;

			int result = lhs.CompareTo(rhs);

			Assert.IsTrue(result > 0);
		}

		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareToBool_LhsFalseRhsTrue_LTZero()
		{
			JsonBool lhs = false;
			bool rhs = true;

			int result = lhs.CompareTo(rhs);

			Assert.IsTrue(result < 0);
		}
		#endregion


		#region Tests - Equals
		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsJsonBool_SameInstance_True()
		{
			JsonBool obj = true;

			bool result = obj.Equals(obj);

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsJsonBool_SameValues_True()
		{
			JsonBool lhs = true;
			JsonBool rhs = true;

			bool result = lhs.Equals(rhs);

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsJsonBool_DifferentValues_False()
		{
			JsonBool lhs = true;
			JsonBool rhs = false;

			bool result = lhs.Equals(rhs);

			Assert.IsFalse(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsJsonBool_Null_False()
		{
			JsonBool lhs = true;
			JsonBool rhs = null;

			bool result = lhs.Equals(rhs);

			Assert.IsFalse(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsBool_SameValues_True()
		{
			JsonBool lhs = true;
			bool rhs = true;

			bool result = lhs.Equals(rhs);

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsBool_DifferentValues_False()
		{
			JsonBool lhs = true;
			bool rhs = false;

			bool result = lhs.Equals(rhs);

			Assert.IsFalse(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsObject_SameInstance_True()
		{
			object obj = new JsonBool(true);

			bool result = obj.Equals(obj);

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsObject_DifferentTypes_False()
		{
			JsonBool lhs = true;
			var rhs = new object();

			bool result = lhs.Equals(rhs);

			Assert.IsFalse(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsObject_SameValues_True()
		{
			JsonBool lhs = true;
			object rhs = new JsonBool(true);

			bool result = lhs.Equals(rhs);

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsObject_DifferentValues_False()
		{
			JsonBool lhs = true;
			object rhs = new JsonBool(false);

			bool result = lhs.Equals(rhs);

			Assert.IsFalse(result);
		}
		#endregion


		#region Tests - Equality Operator
		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualityJsonBoolAndJsonBool_SameValues_True()
		{
			JsonBool lhs = true;
			JsonBool rhs = true;

			bool result = lhs == rhs;

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualityJsonBoolAndJsonBool_DifferentValues_False()
		{
			JsonBool lhs = true;
			JsonBool rhs = false;

			bool result = lhs == rhs;

			Assert.IsFalse(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualityJsonBoolAndJsonBool_LhsNullRhsFalse_False()
		{
			JsonBool lhs = null;
			JsonBool rhs = false;

			bool result = lhs == rhs;

			Assert.IsFalse(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualityJsonBoolAndJsonBool_LhsFalseRhsNull_False()
		{
			JsonBool lhs = false;
			JsonBool rhs = null;

			bool result = lhs == rhs;

			Assert.IsFalse(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualityJsonBoolAndJsonBool_LhsNullRhsNull_True()
		{
			JsonBool lhs = null;
			JsonBool rhs = null;

			bool result = lhs == rhs;

			Assert.IsTrue(result);
		}
		#endregion


		#region Tests - Inequality Operator
		[TestMethod, TestCategory("JsonBool - Equality")]
		public void InequalityJsonBoolAndJsonBool_LhsTrueRhsFalse_True()
		{
			JsonBool lhs = true;
			JsonBool rhs = false;

			bool result = lhs != rhs;

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void InequalityJsonBoolAndJsonBool_SameValues_False()
		{
			JsonBool lhs = true;
			JsonBool rhs = true;

			bool result = lhs != rhs;

			Assert.IsFalse(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void InequalityJsonBoolAndJsonBool_LhsFalseRhsNull_True()
		{
			JsonBool lhs = false;
			JsonBool rhs = null;

			bool result = lhs != rhs;

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void InequalityJsonBoolAndJsonBool_LhsNullRhsFalse_True()
		{
			JsonBool lhs = null;
			JsonBool rhs = false;

			bool result = lhs != rhs;

			Assert.IsTrue(result);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void InequalityJsonBoolAndJsonBool_LhsNullRhsNull_False()
		{
			JsonBool lhs = null;
			JsonBool rhs = null;

			bool result = lhs != rhs;

			Assert.IsFalse(result);
		}
		#endregion


		#region Tests - Hash Codes
		[TestMethod, TestCategory("JsonBool - Hash Codes")]
		public void GetHashCode_SameValues_HashCodesAreTheSame()
		{
			JsonBool obj1 = true;
			JsonBool obj2 = true;

			int result1 = obj1.GetHashCode();
			int result2 = obj2.GetHashCode();

			Assert.IsTrue(obj1.Equals(obj2));
			Assert.IsTrue(result1 == result2);
		}
		#endregion
	}
}
