using System;
using System.Collections.Generic;
using System.Text;

using ParserLib.Json.Internal;

namespace ParserLib.Json
{
	public static class JsonWriter
	{
		#region Properties
		private static IDictionary<Type, Action<WriteControl, JsonElement>> WriterLookup { get; }

		private static IDictionary<char, string> EscapeSequenceLookup { get; }
		#endregion


		#region Constructors
		static JsonWriter()
		{
			WriterLookup = new Dictionary<Type, Action<WriteControl, JsonElement>> {
				{ typeof(JsonObject), WriteObject },
				{ typeof(JsonArray),  WriteArray },
				{ typeof(JsonString), WriteString },
				{ typeof(JsonNumber), WriteNumber },
				{ typeof(JsonBool),   WriteBool },
				{ typeof(JsonNull),   WriteNull },
			};

			EscapeSequenceLookup = new Dictionary<char, string> {
				{ '\'', @"\'" }, { '\"', @"\""" }, { '\\', @"\\" },
				{ '/', @"\/" },  { '\a', @"\a" },  { '\b', @"\b" },
				{ '\f', @"\f" }, { '\n', @"\n" },  { '\r', @"\r" },
				{ '\t', @"\t" }, { '\v', @"\v" }
			};
		}
		#endregion


		#region Public API
		public static void WriteToFile<T>(string filePath, T json, bool prettyPrint = false) where T : JsonElement, IJsonRoot
			=> WriteToFile(filePath, json, prettyPrint, Environment.NewLine);

		public static void WriteToFile<T>(string filePath, T json, bool prettyPrint, string newLine) where T : JsonElement, IJsonRoot
		{
			var control = new FileWriteControl(filePath, prettyPrint, newLine);

			try
			{
				Write(control, json);
			}
			finally
			{
				control.Dispose();
			}
		}

		public static string WriteToString<T>(T json, bool prettyPrint = false) where T : JsonElement, IJsonRoot
			=> WriteToString(json, prettyPrint, Environment.NewLine);

		public static string WriteToString<T>(T json, bool prettyPrint, string newLine) where T : JsonElement, IJsonRoot
		{
			var control = new StringWriteControl(prettyPrint, newLine);

			try
			{
				Write(control, json);
			}
			finally
			{
				control.Dispose();
			}

			return control.Result;
		}
		#endregion


		#region Helper Functions
		static void Write(WriteControl control, JsonElement json)
		{
			if (WriterLookup.TryGetValue(json.GetType(), out Action<WriteControl, JsonElement> writer))
			{
				writer.Invoke(control, json);
			}
			else
			{
				throw new Exception();
			}
		}

		static void WriteObject(WriteControl control, JsonElement json)
		{
			var obj = (JsonObject)json;

			if (obj.Count == 0)
			{
				control.WriteLine("{}");
			}
			else
			{
				control.WriteLine("{");
				control.Indent();

				bool first = true;

				foreach (KeyValuePair<JsonString, JsonElement> pair in obj)
				{
					if (!first)
					{
						control.WriteLine(",");
					}
					else
					{
						first = false;
					}

					control.WriteIndentation();
					Write(control, pair.Key);

					control.WriteWithSpace(":");
					Write(control, pair.Value);
				}

				control.WriteLine();

				control.Unindent();
				control.WriteIndentation();

				control.Write("}");
			}
		}

		static void WriteArray(WriteControl control, JsonElement json)
		{
			var array = (JsonArray)json;

			if (array.Count == 0)
			{
				control.WriteLine("[]");
			}
			else
			{
				control.WriteLine("[");
				control.Indent();

				control.WriteIndentation();
				Write(control, array[0]);

				for (int i = 1; i < array.Count; ++i)
				{
					control.WriteLine(",");

					control.WriteIndentation();
					Write(control, array[i]);
				}

				control.WriteLine();

				control.Unindent();
				control.WriteIndentation();

				control.Write("]");
			}
		}

		static void WriteString(WriteControl control, JsonElement json)
		{
			var str = (JsonString)json;

			if (str.Value != null)
			{
				var value = new StringBuilder();

				foreach (char c in str)
				{
					if (EscapeSequenceLookup.TryGetValue(c, out string sequence))
					{
						value.Append(sequence);
					}
					else if (c < 0x20 || (true && 0x7E < c)) //TODO replace true with a variable to determine whether we should force ascii or not
					{
						value.Append($"\\u{((ushort)c).ToString("x4")}");
					}
					else
					{
						value.Append(c);
					}
				}

				control.Write($"\"{value}\"");
			}
			else
			{
				WriteNull(control, json);
			}
		}

		static void WriteNumber(WriteControl control, JsonElement json)
			=> control.Write(((JsonNumber)json).ToString());

		static void WriteBool(WriteControl control, JsonElement json)
			=> control.Write((JsonBool)json ? "true" : "false");

		static void WriteNull(WriteControl control, JsonElement json)
			=> control.Write("null");
		#endregion
	}
}
