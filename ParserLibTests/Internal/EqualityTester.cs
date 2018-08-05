using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserLibTests.Internal
{
	internal class EqualityTester
	{
		#region Public API - Hash Codes
		public static void AssertDifferentHashCodes<T>(T obj1, T obj2)
			=> Assert.IsTrue(obj1.GetHashCode() != obj2.GetHashCode());

		public static void AssertSameHashCodes<T>(T obj1, T obj2)
			=> Assert.IsTrue(obj1.GetHashCode() == obj2.GetHashCode());
		#endregion


		#region Public API - Equals
		public static void AssertEquals<T1, T2>(T1 lhs, T2 rhs) where T1 : IEquatable<T2>
			=> Assert.IsTrue(lhs.Equals(rhs));

		public static void AssertNotEquals<T1, T2>(T1 lhs, T2 rhs) where T1 : IEquatable<T2>
			=> Assert.IsFalse(lhs.Equals(rhs));

		public static void AssertEquals<T>(T lhs, object rhs)
			=> Assert.IsTrue(lhs.Equals(rhs));

		public static void AssertNotEquals<T>(T lhs, object rhs)
			=> Assert.IsFalse(lhs.Equals(rhs));
		#endregion
	}
}
