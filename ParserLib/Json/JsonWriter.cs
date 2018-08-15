using System;
using System.Collections.Generic;
using System.Globalization;
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
		public static void WriteToFile(string filePath, JsonElement json)
			=> WriteToFile(filePath, json, null);

		public static void WriteToFile(string filePath, JsonElement json, bool prettyPrint)
			=> WriteToFile(filePath, json, new WriterOptions { PrettyPrint = prettyPrint });

		public static void WriteToFile(string filePath, JsonElement json, WriterOptions options)
			=> Write(new FileWriteControl(filePath, options), json);

		public static string WriteToString(JsonElement json)
			=> WriteToString(json, null);

		public static string WriteToString(JsonElement json, bool prettyPrint)
			=> WriteToString(json, new WriterOptions { PrettyPrint = prettyPrint });

		public static string WriteToString(JsonElement json, WriterOptions options)
		{
			var control = new StringWriteControl(options);

			Write(control, json);

			return control.Result;
		}
		#endregion


		#region Helper Functions
		static void Write(WriteControl control, JsonElement json)
		{
			try
			{
				if (json == null)
				{
					throw new ArgumentNullException(nameof(json));
				}

				if (!typeof(IJsonRoot).IsInstanceOfType(json))
				{
					throw new ArgumentException("The given JSON element must implement the IJsonRoot interface.", nameof(json));
				}

				WriteElement(control, json);
			}
			finally
			{
				control.Dispose();
			}
		}

		static void WriteElement(WriteControl control, JsonElement json)
		{
			if (json == null)
			{
				WriteNull(control, json); 
			}
			else if (WriterLookup.TryGetValue(json.GetType(), out Action<WriteControl, JsonElement> writer))
			{
				writer.Invoke(control, json);
			}
			else
			{
				throw new JsonException("Attempted to write an unsupported JSON type.");
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
					WriteElement(control, pair.Key);

					control.WriteWithSpace(":");
					WriteElement(control, pair.Value);
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
				WriteElement(control, array[0]);

				for (int i = 1; i < array.Count; ++i)
				{
					control.WriteLine(",");

					control.WriteIndentation();
					WriteElement(control, array[i]);
				}

				control.WriteLine();

				control.Unindent();
				control.WriteIndentation();

				control.Write("]");
			}
		}

		static void WriteString(WriteControl control, JsonElement json)
		{
			var value = new StringBuilder();

			foreach (char c in (JsonString)json)
			{
				if (EscapeSequenceLookup.TryGetValue(c, out string sequence))
				{
					value.Append(sequence);
				}
				else if (c < 0x20 || (control.ForceAscii && 0x7E < c))
				{
					value.Append($"\\u{((UInt16)c).ToString("x4")}");
				}
				else
				{
					value.Append(c);
				}
			}

			control.Write($"\"{value}\"");
		}

		static void WriteNumber(WriteControl control, JsonElement json)
			=> control.Write(((JsonNumber)json).ToString(CultureInfo.InvariantCulture));

		static void WriteBool(WriteControl control, JsonElement json)
			=> control.Write((JsonBool)json ? "true" : "false");

		static void WriteNull(WriteControl control, JsonElement json)
			=> control.Write("null");
		#endregion
	}
}
