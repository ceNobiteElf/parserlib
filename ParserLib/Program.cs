using System;
using System.Collections.Generic;

using ParserLib.Json;

namespace ParserLib
{
	class Program
	{
		static void Main(string[] args)
		{
			JsonObject obj = JsonParser.ParseFromFile<JsonObject>(@"D:\Libraries\Desktop\JsonTest\cars_test.json");

			Console.WriteLine(obj.Count);

			foreach (KeyValuePair<JsonString, JsonElement> pair in obj)
			{
				Console.WriteLine($"{pair.Key} {pair.Value}");
			}

			Console.WriteLine(obj["cars"][1]["name"]);

			/*var obj = JsonParser.ParseFromString<JsonObject>("{ 'name' : 'Con' }");

			Console.WriteLine(obj.Count);
			Console.WriteLine(obj["name"]);*/

			/*var obj2 = JsonParser.ParseFromString<JsonArray>("[{ 'name' : 'Con' }, { 'name' : 'Kevin' }, { 'name' : 'Stab'}]");

			Console.WriteLine(obj2.Count);
			Console.WriteLine(obj2[0]["name"]);*/

			Console.WriteLine(JsonWriter.WriteToString(obj, true));

			JsonWriter.WriteToFile(@"D:\Libraries\Desktop\JsonTest\cars_test.json", obj, true);

			Console.ReadKey();
		}
	}
}
