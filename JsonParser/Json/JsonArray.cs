using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserLib.Json
{
	public class JsonArray : JsonElement
	{
		#region Properties
		public IList<JsonElement> Values { get; private set; }
		#endregion


		#region Constructors
		internal JsonArray(JsonObject value) { }
		#endregion


		#region Public API

		#endregion
	}
}
