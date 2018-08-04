using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ParserLib.Json;

namespace ParserLibTests.Json
{
	[TestClass]
	public class JsonNumberTests
	{
		#region Tests - Constructors
		[TestMethod, TestCategory("JsonNumber - Constructors")]
		public void Ctor_Default_Zero()
		{
			var result = new JsonNumber();

			AssertEqual(0, result);
		}

		[TestMethod, TestCategory("JsonNumber - Constructors")]
		public void Ctor_Int()
		{
			var result = new JsonNumber(10);

			AssertEqual(10, result);
		}

		[TestMethod, TestCategory("JsonNumber - Constructors")]
		public void Ctor_Long()
		{
			var result = new JsonNumber(50L);

			AssertEqual(50L, result);
		}

		[TestMethod, TestCategory("JsonNumber - Constructors")]
		public void Ctor_Double()
		{
			var result = new JsonNumber(200.123);

			AssertEqual(200.123, result);
		}

		[TestMethod, TestCategory("JsonNumber - Constructors")]
		public void Ctor_DoubleWithExponent()
		{
			var result = new JsonNumber(200.5e2);

			AssertEqual(200.5e2, result);
		}
		#endregion


		#region Tests - Comparison
		[TestMethod, TestCategory("JsonNumber - Comparison")]
		public void CompareToJsonNumber_SameValues_Zero()
		{
			JsonNumber lhs = 50;
			JsonNumber rhs = 50;

			int result = lhs.CompareTo(rhs);

			Assert.IsTrue(result == 0);
		}

		[TestMethod, TestCategory("JsonNumber - Comparison")]
		public void CompareToJsonNumber_Lhs100Rhs50_GTZero()
		{
			JsonNumber lhs = 100;
			JsonNumber rhs = 50;

			int result = lhs.CompareTo(rhs);

			Assert.IsTrue(result > 0);
		}

		[TestMethod, TestCategory("JsonNumber - Comparison")]
		public void CompareToJsonNumber_Lhs50Rhs100_LTZero()
		{
			JsonNumber lhs = 50;
			JsonNumber rhs = 100;

			int result = lhs.CompareTo(rhs);

			Assert.IsTrue(result < 0);
		}

		[TestMethod, TestCategory("JsonNumber - Comparison")]
		public void CompareToJsonNumber_Lhs50RhsNull_GTZero()
		{
			JsonNumber lhs = 50;
			JsonNumber rhs = null;

			int result = lhs.CompareTo(rhs);

			Assert.IsTrue(result > 0);
		}

		[TestMethod, TestCategory("JsonNumber - Comparison")]
		public void CompareToDouble_SameValues_Zero()
		{
			JsonNumber lhs = 50;
			double rhs = 50;

			int result = lhs.CompareTo(rhs);

			Assert.IsTrue(result == 0);
		}

		[TestMethod, TestCategory("JsonNumber - Comparison")]
		public void CompareToDouble_Lhs100Rhs50_GTZero()
		{
			JsonNumber lhs = 100;
			double rhs = 50;

			int result = lhs.CompareTo(rhs);

			Assert.IsTrue(result > 0);
		}

		[TestMethod, TestCategory("JsonNumber - Comparison")]
		public void CompareToDouble_Lhs50Rhs100_LTZero()
		{
			JsonNumber lhs = 50;
			double rhs = 100;

			int result = lhs.CompareTo(rhs);

			Assert.IsTrue(result < 0);
		}
		#endregion


		#region Helper Functions
		static bool AssertEqual(double expectedValue, double result)
			=> Math.Abs(result - expectedValue) < double.Epsilon;
		#endregion
	}
}
