using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ParserLib.Json;

namespace ParserLib
{
	class Program
	{
		static void Main(string[] args)
		{
			JsonObject obj = Parser.Parse(@"D:\Libraries\Desktop\JsonTest\cars.json");

			Console.WriteLine(obj.Count);

			foreach (KeyValuePair<JsonString, JsonElement> pair in obj)
			{
				Console.WriteLine($"{pair.Key} {pair.Value}");
			}

			Console.WriteLine(((JsonArray)obj["cars"])[1]);

			Console.ReadKey();
		}
	}
}
