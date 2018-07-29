using System;

namespace ParserLib.Json.Internal
{
	internal abstract class ReadControl : IControl
	{
		#region Properties
		public char CurrentCharacter { get; protected set; }
		#endregion


		#region Constructors
		public ReadControl() {}
		#endregion


		#region Interface Implementation - IControl
		public virtual void Dispose() { }
		#endregion


		#region Public API
		public abstract char ReadNextCharacter();
		#endregion
	}
}
