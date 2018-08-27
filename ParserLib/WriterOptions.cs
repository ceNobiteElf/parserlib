using System.Text;

namespace ParserLib
{
	public class WriterOptions
	{
		#region Properties
		/// <summary>
		/// If set to <c>true</c> the writer will attempt to add spacing and indentation to the output.
		/// </summary>
		public bool PrettyPrint { get; set; }

		/// <summary>
		/// The newline sequence that will be used by the writer. This defaults to Unix-like newlines (<c>\n</c>).
		/// </summary>
		public string NewLine { get; set; }

		/// <summary>
		/// If <see cref="UseTabCharacter"/> is set to <c>false</c> this will be used to determine how many spaces should be added per indentation level.
		/// </summary>
		public int TabWidth { get; set; }

		/// <summary>
		/// If set to <c>true</c> the tabulation character (<c>\t</c>) will be used instead of spaces and <see cref="TabWidth"/> will be ignored completely.
		/// </summary>
		public bool UseTabCharacter { get; set; }

		/// <summary>
		/// If set to <c>true</c> the writer will escape any Unicode characters which are not in the ASCII-printable range.
		/// </summary>
		public bool ForceAscii { get; set; }

		/// <summary>
		/// The encoding to use when writing to file. This defaults to UTF8.
		/// </summary>
		public Encoding FileEncoding { get; set; }

		/// <summary>
		/// The size of the underlying buffer that will be used when writing to file. This defaults to 1024 characters.
		/// </summary>
		public int BufferSize { get; set; }
		#endregion


		#region Constructors
		public WriterOptions()
		{
			PrettyPrint = false;

			NewLine = "\n";
			TabWidth = 2;
			UseTabCharacter = false;

			ForceAscii = false;
			FileEncoding = Encoding.UTF8;
			BufferSize = 1024;
		}
		#endregion
	}
}
