namespace ParserLib.Json
{
	public sealed class JsonReaderOptions : ReaderOptions
	{
		#region Properties
		/// <summary>
		/// If set to <c>true</c> the parser will not propogate exceptions and instead return <c>null</c>.
		/// </summary>
		public bool NullOnExceptions { get; set; }

		/// <summary>
		/// This will determine the behaviour of the parser if multiple root elements are detected. By default an exception will be raised.
		/// </summary>
		public MultipleRootsBehaviour MultipleRootsBehaviour { get; set; }

		/// <summary>
		/// This determines the behaviour of the parser when a duplicate key is found within a JSON object. By default an exception will be raised.
		/// </summary>
		public DuplicateKeyBehaviour DuplicateKeyBehaviour { get; set; }
		#endregion


		#region Constructors
		public JsonReaderOptions()
		{
			NullOnExceptions = false;

			MultipleRootsBehaviour = MultipleRootsBehaviour.ThrowException;
			DuplicateKeyBehaviour = DuplicateKeyBehaviour.ThrowException;
		}
		#endregion
	}
}
