using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser
{
	public class JsonNode
	{
		#region Properties
		public object Data { get; private set; }
		#endregion


		#region Constructors
		internal JsonNode(object data)
		{
			Data = data;
		}
		#endregion


		#region Public API

		#endregion
	}
}
