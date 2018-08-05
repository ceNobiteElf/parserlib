using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserLibTests.Internal
{
	internal static class ComparisonTester
	{
		#region Public API
		public static void AssertEqualsZero<T1, T2>(T1 lhs, T2 rhs) where T1 : IComparable<T2>
			=> Assert.IsTrue(lhs.CompareTo(rhs) == 0);

		public static void AssertGreaterThanZero<T1, T2>(T1 lhs, T2 rhs) where T1 : IComparable<T2>
			=> Assert.IsTrue(lhs.CompareTo(rhs) > 0);

		public static void AssertLessThanZero<T1, T2>(T1 lhs, T2 rhs) where T1 : IComparable<T2>
			=> Assert.IsTrue(lhs.CompareTo(rhs) < 0);
		#endregion
	}
}
