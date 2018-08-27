using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ParserLib.Json;
using ParserLib.Json.Serialization;

namespace ParserLibTests.Json.Serialization
{
	[TestClass]
	public class JsonSerializerTests
	{
		[JsonSerializable(SerializationMode.OptIn)]
		public class Tester
		{
			[JsonProperty]
			public string Stab { get; set; }
			public string Help { get => mHelp; set => mHelp = value; }
			public int GateCode { get => mGateCode; }

			public string Murderer;

			[JsonProperty("Help")]
			private string mHelp;

			[JsonProperty("GateCode")]
			private int mGateCode;

			private string[] mStrings = { null, "hello", "world", "nex", "sacramentum" };

			public Tester()
			{
				Stab = "stab";
				Help = "She screamed for help!";

				Murderer = "Nex";

				mGateCode = 1234;
			}
		}

		[JsonSerializable(SerializationMode.All)]
		public class KittyTester : Tester
		{
			public int WitchNumber { get; set; }
			internal string KatType { get; set; }
		}

		[TestMethod]
		public void TestMethod1()
		{
			string json = JsonSerializer.Serialize(new Tester());

			Console.WriteLine(json);

			json = JsonSerializer.Serialize(new {
				Hello = "Hello?",
				World = "This is scary."
			});

			Console.WriteLine(json);

			json = JsonSerializer.Serialize(new KittyTester());

			Console.WriteLine(json);

		}

		[TestMethod]
		public void TestMethod2()
		{
			var root = JsonParser.ParseFromFile<JsonObject>(@"D:\Libraries\Desktop\JsonTest\Rory.json");

			Console.WriteLine(root.Count);
		}
	}
}
