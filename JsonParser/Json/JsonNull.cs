namespace ParserLib.Json
{
	public sealed class JsonNull : JsonElement
	{
		#region Properties
		public static JsonNull Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new JsonNull();
				}

				return mInstance;
			}
		}

		public object Value { get { return null; } }
		#endregion


		#region Variables
		private static JsonNull mInstance;
		#endregion


		#region Constructors
		private JsonNull() {}
		#endregion


		#region Operator Overloads
		public static implicit operator char(JsonNull obj)
		{
			return '\0';
		}

		public static implicit operator string(JsonNull obj)
		{
			return null;
		}

		public static implicit operator bool(JsonNull obj)
		{
			return false;
		}

		public static implicit operator int(JsonNull obj)
		{
			return 0;
		}

		public static implicit operator float(JsonNull obj)
		{
			return 0f;
		}

		public static implicit operator double(JsonNull obj)
		{
			return 0.0;
		}
		#endregion
	}
}
