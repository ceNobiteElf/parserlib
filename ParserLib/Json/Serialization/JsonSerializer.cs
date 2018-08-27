using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ParserLib.Json.Serialization
{
	public static class JsonSerializer
	{
		#region Constants
		private const BindingFlags BindingAll = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
		private const BindingFlags BindingPublic = BindingFlags.Instance | BindingFlags.Public;
		#endregion


		#region Properties
		private static IDictionary<Type, Func<object, JsonElement>> JsonCastLookup { get; }
		#endregion


		#region Constructors
		static JsonSerializer()
		{
			JsonCastLookup = new Dictionary<Type, Func<object, JsonElement>> {
				{ typeof(string),  obj => (JsonString)(string)obj },

				{ typeof(short),   obj => (JsonNumber)(short)obj },
				{ typeof(int),     obj => (JsonNumber)(int)obj },
				{ typeof(long),    obj => (JsonNumber)(long)obj },
				{ typeof(float),   obj => (JsonNumber)(float)obj },
				{ typeof(double),  obj => (JsonNumber)(double)obj },

				{ typeof(bool),    obj => (JsonBool)(bool)obj }
			};
		}
		#endregion


		#region Public API
		public static string Serialize(object source)
		{
			JsonElement root = null;

			if (typeof(IList).IsInstanceOfType(source))
			{
				root = SerializeArray(source);
			}
			else
			{
				root = SerializeObject(source);
			}

			return JsonWriter.WriteToString(root, true);
		}
		#endregion


		#region Helper Functions
		static bool IsAutoProperty(PropertyInfo property)
			=> property.CanRead && property.CanWrite && property.GetGetMethod(true).IsDefined(typeof(CompilerGeneratedAttribute), true);

		static bool IsAnonymous(Type type)
			=> type.IsGenericType && type.IsClass && type.IsSealed 
				&& type.Attributes.HasFlag(TypeAttributes.NotPublic) && type.GetCustomAttribute<CompilerGeneratedAttribute>() != null;

		static JsonElement SerializeField(Type fieldType, object source)
		{
			JsonElement result = null;

			if (source == null)
			{
				result = JsonNull.Value;
			}
			else if (fieldType.IsSubclassOf(typeof(JsonElement)))
			{
				result = (JsonElement)source;
			}
			else if (JsonCastLookup.TryGetValue(fieldType, out Func<object, JsonElement> castToJson))
			{
				result = castToJson(source);
			}
			else if (typeof(IList).IsInstanceOfType(source))
			{
				result = SerializeArray(source);
			}
			else
			{
				result = SerializeObject(source);
			}

			return result;
		}

		static JsonObject SerializeObject(object source)
		{
			Type objType = source.GetType();
			bool isAnonymous = IsAnonymous(objType);

			var jsonSerializableAttribute = objType.GetCustomAttribute<JsonSerializableAttribute>();
			bool optInOnly = jsonSerializableAttribute?.Mode == SerializationMode.OptIn;

			if (!isAnonymous && jsonSerializableAttribute == null)
			{
				return null;
			}

			BindingFlags bindings = isAnonymous || jsonSerializableAttribute.Mode != SerializationMode.Public ? BindingAll : BindingPublic;
			IEnumerable<MemberInfo> members = objType.GetProperties(bindings);

			if (!isAnonymous)
			{
				members = members.Concat(objType.GetFields(bindings));
			}

			var jsonObject = new JsonObject();

			foreach (MemberInfo member in members)
			{
				var jsonPropertyAttribute = member.GetCustomAttribute<JsonPropertyAttribute>();

				if (member.GetCustomAttribute<CompilerGeneratedAttribute>() != null
					|| (optInOnly && jsonPropertyAttribute == null))
				{
					continue;
				}

				string memberName = jsonPropertyAttribute?.Name ?? member.Name;
				Type memberType = null;
				object value = null;

				if (member is PropertyInfo property)
				{
					if (!isAnonymous && !IsAutoProperty(property))
					{
						continue;
					}

					memberType = property.PropertyType;
					value = property.GetValue(source);
				}
				else if (member is FieldInfo field)
				{
					memberType = field.FieldType;
					value = field.GetValue(source);
				}

				if (memberType != null)
				{
					JsonElement json = SerializeField(memberType, value);

					if (json != null && !(json is JsonObject obj && obj.Count == 0))
					{
						jsonObject.Add(memberName, json);
					}
				}
			}

			return jsonObject;
		}

		static JsonArray SerializeArray(object source)
		{
			var jsonArray = new JsonArray();

			foreach (object element in (IList)source)
			{
				JsonElement json = SerializeField(element?.GetType(), element);

				if (json != null && !(json is JsonObject obj && obj.Count == 0))
				{
					jsonArray.Add(json);
				}
			}
				
			return jsonArray;
		}
		#endregion
	}
}
