using Microsoft.VisualStudio.TestTools.UnitTesting;

using ParserLib.Json;

using ParserLibTests.Internal;

namespace ParserLibTests.Json
{
	[TestClass]
	public sealed class JsonBoolTests
	{
		#region Tests - Constructors
		[TestMethod, TestCategory("JsonBool - Constructors")]
		public void Ctor_True()
			=> Assert.IsTrue(new JsonBool(true));

		[TestMethod, TestCategory("JsonBool - Constructors")]
		public void Ctor_False()
			=> Assert.IsFalse(new JsonBool(false));
		#endregion


		#region Tests - Comparison
		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareToJsonBool_SameValues_Zero()
			=> ComparisonTester.AssertEqualsZero<JsonBool, JsonBool>(true, true);

		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareToJsonBool_LhsTrueRhsFalse_GTZero()
			=> ComparisonTester.AssertGreaterThanZero<JsonBool, JsonBool>(true, false);

		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareToJsonBool_LhsFalseRhsTrue_LTZero()
			=> ComparisonTester.AssertLessThanZero<JsonBool, JsonBool>(false, true);

		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareToJsonBool_LhsTrueRhsNull_GTZero()
			=> ComparisonTester.AssertGreaterThanZero<JsonBool, JsonBool>(true, null);

		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareToBool_SameValues_Zero()
			=> ComparisonTester.AssertEqualsZero<JsonBool, bool>(true, true);

		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareToBool_LhsTrueRhsFalse_GTZero()
			=> ComparisonTester.AssertGreaterThanZero<JsonBool, bool>(true, false);

		[TestMethod, TestCategory("JsonBool - Comparison")]
		public void CompareToBool_LhsFalseRhsTrue_LTZero()
			=> ComparisonTester.AssertLessThanZero<JsonBool, bool>(false, true);
		#endregion


		#region Tests - Equals
		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsJsonBool_SameInstance_True()
		{
			JsonBool obj = true;
			EqualityTester.AssertEquals(obj, obj);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsJsonBool_SameValues_True()
			=> EqualityTester.AssertEquals<JsonBool, JsonBool>(true, true);

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsJsonBool_DifferentValues_False()
			=> EqualityTester.AssertNotEquals<JsonBool, JsonBool>(true, false);

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsJsonBool_Null_False()
			=> EqualityTester.AssertNotEquals<JsonBool, JsonBool>(true, null);

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsBool_SameValues_True()
			=> EqualityTester.AssertEquals<JsonBool, bool>(true, true);

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsBool_DifferentValues_False()
			=> EqualityTester.AssertNotEquals<JsonBool, bool>(true, false);

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsObject_SameInstance_True()
		{
			JsonBool obj = true;
			EqualityTester.AssertEquals<JsonBool>(obj, obj);
		}

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsObject_DifferentTypes_False()
			=> EqualityTester.AssertNotEquals<JsonBool>(true, new object());

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsObject_SameValues_True()
			=> EqualityTester.AssertEquals<JsonBool>(true, new JsonBool(true));

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualsObject_DifferentValues_False()
			=> EqualityTester.AssertNotEquals<JsonBool>(true, new JsonBool(false));
		#endregion


		#region Tests - Equality Operator
		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualityJsonBoolAndJsonBool_SameValues_True()
			=> AssertEqualityTrue(true, true);

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualityJsonBoolAndJsonBool_DifferentValues_False()
			=> AssertEqualityFalse(true, false);

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualityJsonBoolAndJsonBool_LhsNullRhsFalse_False()
			=> AssertEqualityFalse(null, false);

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualityJsonBoolAndJsonBool_LhsFalseRhsNull_False()
			=> AssertEqualityFalse(false, null);

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void EqualityJsonBoolAndJsonBool_LhsNullRhsNull_True()
			=> AssertEqualityTrue(null, null);
		#endregion


		#region Tests - Inequality Operator
		[TestMethod, TestCategory("JsonBool - Equality")]
		public void InequalityJsonBoolAndJsonBool_LhsTrueRhsFalse_True()
			=> AssertInequalityTrue(true, false);

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void InequalityJsonBoolAndJsonBool_SameValues_False()
			=> AssertInequalityFalse(true, true);

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void InequalityJsonBoolAndJsonBool_LhsNullRhsFalse_True()
			=> AssertInequalityTrue(null, false);

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void InequalityJsonBoolAndJsonBool_LhsFalseRhsNull_True()
			=> AssertInequalityTrue(false, null);

		[TestMethod, TestCategory("JsonBool - Equality")]
		public void InequalityJsonBoolAndJsonBool_LhsNullRhsNull_False()
			=> AssertInequalityFalse(null, null);
		#endregion


		#region Tests - Hash Codes
		[TestMethod, TestCategory("JsonBool - Hash Codes")]
		public void GetHashCode_SameValues_HashCodesAreTheSame()
			=> EqualityTester.AssertSameHashCodes<JsonBool>(true, true);
		#endregion


		#region Helper Functions
		static void AssertEqualityTrue(JsonBool lhs, JsonBool rhs)
			=> Assert.IsTrue(lhs == rhs);

		static void AssertEqualityFalse(JsonBool lhs, JsonBool rhs)
			=> Assert.IsFalse(lhs == rhs);

		static void AssertInequalityTrue(JsonBool lhs, JsonBool rhs)
			=> Assert.IsTrue(lhs != rhs);

		static void AssertInequalityFalse(JsonBool lhs, JsonBool rhs)
			=> Assert.IsFalse(lhs != rhs);
		#endregion
	}
}
