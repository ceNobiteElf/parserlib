using System;
using System.Collections.Generic;

using ParserLib.Json;

namespace ParserLib
{
	class Program
	{
		static void Main(string[] args)
		{
			/*JsonObject obj = Parser.ParseFile(@"D:\Libraries\Desktop\JsonTest\cars.json");

			Console.WriteLine(obj.Count);

			foreach (KeyValuePair<JsonString, JsonElement> pair in obj)
			{
				Console.WriteLine($"{pair.Key} {pair.Value}");
			}

			Console.WriteLine(obj["cars"][1]["name"]);*/

			JsonObject obj = Parser.ParseString("{ 'name' : 'Con' }");

			Console.WriteLine(obj.Count);
			Console.WriteLine(obj["name"]);

			 Console.ReadKey();
		}
	}
}
