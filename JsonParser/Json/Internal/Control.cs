using System;

namespace ParserLib.Json.Internal
{
	internal abstract class Control : IDisposable
	{
		#region Properties
		public char CurrentCharacter { get; protected set; }
		#endregion


		#region Constructors
		public Control() {}
		#endregion


		#region Interface Implementation - IDisposable
		public virtual void Dispose() { }
		#endregion


		#region Public API
		public abstract char ReadNextCharacter();
		#endregion
	}
}
