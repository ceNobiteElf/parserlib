using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ParserLib.Json;

using ParserLibTests.Internal;

namespace ParserLibTests.Json
{
	[TestClass]
	public sealed class JsonNumberTests
	{
		#region Tests - Constructors
		[TestMethod, TestCategory("JsonNumber - Constructors")]
		public void Ctor_Default_Zero()
			=> AssertEqual(0, new JsonNumber());

		[TestMethod, TestCategory("JsonNumber - Constructors")]
		public void Ctor_Int()
			=> AssertEqual(10, new JsonNumber(10));

		[TestMethod, TestCategory("JsonNumber - Constructors")]
		public void Ctor_Long()
			=> AssertEqual(50L, new JsonNumber(50L));

		[TestMethod, TestCategory("JsonNumber - Constructors")]
		public void Ctor_Double()
			=> AssertEqual(200.123, new JsonNumber(200.123));

		[TestMethod, TestCategory("JsonNumber - Constructors")]
		public void Ctor_DoubleWithExponent()
			=> AssertEqual(200.5e2, new JsonNumber(200.5e2));
		#endregion


		#region Tests - Comparison
		[TestMethod, TestCategory("JsonNumber - Comparison")]
		public void CompareToJsonNumber_SameValues_Zero()
			=> ComparisonTester.AssertEqualsZero<JsonNumber, JsonNumber>(50, 50);

		[TestMethod, TestCategory("JsonNumber - Comparison")]
		public void CompareToJsonNumber_Lhs100Rhs50_GTZero()
			=> ComparisonTester.AssertGreaterThanZero<JsonNumber, JsonNumber>(100, 50);

		[TestMethod, TestCategory("JsonNumber - Comparison")]
		public void CompareToJsonNumber_Lhs50Rhs100_LTZero()
			=> ComparisonTester.AssertLessThanZero<JsonNumber, JsonNumber>(50, 100);

		[TestMethod, TestCategory("JsonNumber - Comparison")]
		public void CompareToJsonNumber_Lhs50RhsNull_GTZero()
			=> ComparisonTester.AssertGreaterThanZero<JsonNumber, JsonNumber>(50, null);

		[TestMethod, TestCategory("JsonNumber - Comparison")]
		public void CompareToDouble_SameValues_Zero()
			=> ComparisonTester.AssertEqualsZero<JsonNumber, double>(50, 50);

		[TestMethod, TestCategory("JsonNumber - Comparison")]
		public void CompareToDouble_Lhs100Rhs50_GTZero()
			=> ComparisonTester.AssertGreaterThanZero<JsonNumber, double>(100, 50);

		[TestMethod, TestCategory("JsonNumber - Comparison")]
		public void CompareToDouble_Lhs50Rhs100_LTZero()
			=> ComparisonTester.AssertLessThanZero<JsonNumber, double>(50, 100);
		#endregion


		#region Tests - Equals
		[TestMethod, TestCategory("JsonNumber - Equality")]
		public void EqualsJsonNumber_SameInstance_True()
		{
			JsonNumber obj = 1;
			EqualityTester.AssertEquals(obj, obj);
		}

		[TestMethod, TestCategory("JsonNumber - Equality")]
		public void EqualsJsonNumber_SameValues_True()
			=> EqualityTester.AssertEquals<JsonNumber, JsonNumber>(25, 25);

		[TestMethod, TestCategory("JsonNumber - Equality")]
		public void EqualsJsonNumber_DifferentValues_False()
			=> EqualityTester.AssertNotEquals<JsonNumber, JsonNumber>(25, 30);

		[TestMethod, TestCategory("JsonNumber - Equality")]
		public void EqualsJsonNumber_Null_False()
			=> EqualityTester.AssertNotEquals<JsonNumber, JsonNumber>(25, null);

		[TestMethod, TestCategory("JsonNumber - Equality")]
		public void EqualsDouble_SameValues_True()
			=> EqualityTester.AssertEquals<JsonNumber, double>(25, 25);

		[TestMethod, TestCategory("JsonNumber - Equality")]
		public void EqualsDouble_DifferentValues_False()
			=> EqualityTester.AssertNotEquals<JsonNumber, double>(25, 30);

		[TestMethod, TestCategory("JsonNumber - Equality")]
		public void EqualsObject_SameInstance_True()
		{
			JsonNumber obj = 1;
			EqualityTester.AssertEquals<JsonNumber>(obj, obj);
		}

		[TestMethod, TestCategory("JsonNumber - Equality")]
		public void EqualsObject_DifferentTypes_False()
			=> EqualityTester.AssertNotEquals<JsonNumber>(1, new object());

		[TestMethod, TestCategory("JsonNumber - Equality")]
		public void EqualsObject_SameValues_True()
			=> EqualityTester.AssertEquals<JsonNumber>(25, new JsonNumber(25));

		[TestMethod, TestCategory("JsonNumber - Equality")]
		public void EqualsObject_DifferentValues_False()
			=> EqualityTester.AssertNotEquals<JsonNumber>(25, new JsonNumber(30));
		#endregion


		#region Tests - Hash Codes
		[TestMethod, TestCategory("JsonNumber - Hash Codes")]
		public void GetHashCode_SameValues_HashCodesAreTheSame()
			=> EqualityTester.AssertSameHashCodes<JsonNumber>(25, 25);

		[TestMethod, TestCategory("JsonNumber - Hash Codes")]
		public void GetHashCode_SameValuesVariation_HashCodesAreTheSame()
			=> EqualityTester.AssertSameHashCodes<JsonNumber>(1234.567, 1234.567);
		#endregion


		#region Helper Functions
		static void AssertEqual(double expectedValue, JsonNumber result)
			=> Assert.IsTrue(Math.Abs(result - expectedValue) < double.Epsilon);
		#endregion
	}
}
