﻿using System.Text;

namespace ParserLib
{
	public class ReaderOptions
	{
		#region Properties
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
			FileEncoding = Encoding.UTF8;
			BufferSize = 1024;
		}
		#endregion
	}
}
