using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser
{
	public class Parser
	{
		#region Internal Types
		[Flags]
		private enum Controls
		{
			None    = 0x00,
			Quoted  = 0x01,
			Escaped = 0x02,
			Negated = 0x04,
			IsArray	= 0x08
		}
		#endregion


		#region Constants
		public const int DefaultBlockSize = 4096;
		#endregion


		#region Constructors

		#endregion


		#region Public API
		public static JsonNode Parse(string filePath, int blockSize = DefaultBlockSize)
		{
			var controls = Controls.None;

			var buffer = new Char[blockSize];
			int currentPosition = 0;
			int bytesRead;

			var stringBuilder = new StringBuilder();

			using (FileStream stream = File.OpenRead(filePath))
			{
				using (StreamReader reader = new StreamReader(stream))
				{
					while (true)
					{
						bytesRead = reader.Read(buffer, currentPosition, blockSize);
						currentPosition += bytesRead;

						if (bytesRead > 0)
						{
							foreach (char c in buffer)
							{
								switch (c)
								{
									case '{':
										// TODO create temporary node
										break;

									case '}':
										// TODO collection as value to node
										// TODO add temporary node to the tree
										break;

									case '"':
										if (!controls.HasFlag(Controls.Quoted))
										{
											controls |= Controls.Quoted;
										}
										else
										{
											controls &= ~Controls.Quoted;
										}
										break;

									case ':':
										// TODO assign key
										break;

									case ',':
										// TODO add key/pair or value to collection
										break;

									case '\\':
										controls |= Controls.Escaped;
										break;

									case '[':
										controls |= Controls.IsArray;
										break;

									case ']':
										controls &= ~Controls.IsArray;
										break;

									case '-':
										controls |= Controls.Negated;
										break;

									default:
										if (!controls.HasFlag(Controls.Quoted) && Char.IsWhiteSpace(c))
										{
											continue;
										}

										stringBuilder.Append(c);
										break;
								}
							}
						}
						else
						{
							break;
						}
					}
				}
			}


			return null;
		}
		#endregion
	}
}
