using System.Text;

namespace ParserLib.Json
{
	public sealed class ReaderOptions
	{
		#region Properties
		/// <summary>
		/// If set to <c>true</c> the parser will not propogate exceptions and instead return <c>null</c>.
		/// </summary>
		public bool NullOnExceptions { get; set; }

		/// <summary>
		/// The encoding to use when reading from file. This defaults to UTF8, but can be set to <c>null</c> to tell the reader to try to detect the encoding automatically.
		/// </summary>
		public Encoding FileEncoding { get; set; }

		/// <summary>
		/// The size of the underlying buffer that will be used when reading from file. This defaults to 1024 characters.
		/// </summary>
		public int BufferSize { get; set; }
		#endregion


		#region Constructors
		public ReaderOptions()
		{
			NullOnExceptions = false;

			FileEncoding = Encoding.UTF8;
			BufferSize = 1024;
		}
		#endregion
	}
}
