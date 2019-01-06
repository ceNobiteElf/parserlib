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
		#region Nested Types
		private sealed class ClassAttributes
		{
			public JsonSerializableAttribute SerializationAttributes { get; }
			public bool IsAnonymous { get; }

			public SerializationMode SerializationMode { get; }
			public bool IsOptInOnly { get; }
			public bool IsSerializable { get; }

			public BindingFlags Binding { get; }

			public ClassAttributes(JsonSerializableAttribute serializationAttributes, bool isAnonymous)
			{
				SerializationAttributes = serializationAttributes;
				IsAnonymous = isAnonymous;

				SerializationMode = IsAnonymous ? SerializationMode.All : (SerializationAttributes?.Mode ?? SerializationMode.All);
				IsOptInOnly = !IsAnonymous && SerializationMode == SerializationMode.OptIn;
				IsSerializable = IsAnonymous || SerializationAttributes != null;

				if (IsSerializable)
				{
					Binding = SerializationMode == SerializationMode.Public ? BindingPublic : BindingAll;
				}
			}
		}

		private interface ISerializableMember
		{
			string Name { get; }
			Type Type { get; }

			object GetValue(object source);
			void SetValue(object source, object value);
		}

		private abstract class SerializableMember<T> : ISerializableMember where T : MemberInfo
		{
			protected T BackingMemberInfo { get; set; }

			public string Name { get; protected set; }
			public abstract Type Type { get; }

			protected SerializableMember(T backingMemberInfo)
				: this(backingMemberInfo, GetMemberName(backingMemberInfo)) { }

			protected SerializableMember(T backingMemberInfo, string name)
			{
				BackingMemberInfo = backingMemberInfo;
				Name = name;
			}

			public abstract object GetValue(object source);
			public abstract void SetValue(object source, object value);
		}

		private sealed class FieldMember : SerializableMember<FieldInfo>
		{
			public override Type Type { get => BackingMemberInfo.FieldType; }

			public FieldMember(FieldInfo backingMemberInfo)
				: base(backingMemberInfo) { }

			public FieldMember(FieldInfo backingMemberInfo, string name)
				: base(backingMemberInfo, name) { }

			public override object GetValue(object source)
				=> BackingMemberInfo.GetValue(source);

			public override void SetValue(object source, object value)
				=> BackingMemberInfo.SetValue(source, value);
		}

		private sealed class PropertyMember : SerializableMember<PropertyInfo>
		{
			public override Type Type { get => BackingMemberInfo.PropertyType; }

			public PropertyMember(PropertyInfo backingMemberInfo)
				: base(backingMemberInfo) { }

			public PropertyMember(PropertyInfo backingMemberInfo, string name)
				: base(backingMemberInfo, name) { }

			public override object GetValue(object source)
				=> BackingMemberInfo.GetValue(source);

			public override void SetValue(object source, object value)
				=> BackingMemberInfo.SetValue(source, value);
		}
		#endregion


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
			=> Serialize(source, false);

		public static string Serialize(object source, bool prettyPrint)
			=> Serialize(source, new JsonWriterOptions() { PrettyPrint = prettyPrint });

		public static string Serialize(object source, JsonWriterOptions options)
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

			return JsonWriter.WriteToString(root, options);
		}

		public static T DeserializeFromString<T>(string rawJson)
			=> (T)DeserializeFromString(typeof(T), rawJson);

		public static object DeserializeFromString(Type targetType, string rawJson)
			=> DeserializeFromJsonElement(targetType, JsonParser.ParseFromString(rawJson));

		public static T DeserializeFromFile<T>(string filePath)
			=> (T)DeserializeFromFile(typeof(T), filePath);

		public static object DeserializeFromFile(Type targetType, string filePath)
			=> DeserializeFromJsonElement(targetType, JsonParser.ParseFromFile(filePath));

		public static T DeserializeFromJsonElement<T>(JsonElement json)
			=> (T)DeserializeFromJsonElement(typeof(T), json);

		[Obsolete("Deserialization is not yet production-ready and for now quite error prone.")]
		public static object DeserializeFromJsonElement(Type target, JsonElement json)
			=> Deserialize(target, json);
		#endregion


		#region Helper Functions - General
		static string GetMemberName(MemberInfo member)
			=> member.GetCustomAttribute<JsonPropertyAttribute>()?.Name ?? member.Name;

		static bool IsAnonymous(Type objType)
			=> objType.IsGenericType && objType.IsClass && objType.IsSealed
				&& objType.Attributes.HasFlag(TypeAttributes.NotPublic) && objType.GetCustomAttribute<CompilerGeneratedAttribute>() != null;

		static bool IsAutoProperty(PropertyInfo property)
			=> property.CanRead && property.CanWrite && property.GetGetMethod(true).IsDefined(typeof(CompilerGeneratedAttribute), true);
		#endregion


		#region Helper Functions - Serialization
		static bool IsSerializable(PropertyInfo property, ClassAttributes classAttributes)
		{
			bool result = false;

			if (classAttributes.IsAnonymous)
			{
				result = true;
			}
			else if (classAttributes.IsOptInOnly)
			{
				result = property.IsDefined(typeof(JsonPropertyAttribute), true);
			}
			else
			{
				result = !property.IsDefined(typeof(CompilerGeneratedAttribute)) && property.CanRead;
			}

			return result;
		}

		static bool IsSerializable(FieldInfo field, ClassAttributes classAttributes)
		{
			bool result = false;

			if (!classAttributes.IsAnonymous)
			{
				result = !field.IsDefined(typeof(CompilerGeneratedAttribute));

				if (classAttributes.IsOptInOnly)
				{
					result = result && field.IsDefined(typeof(JsonPropertyAttribute), true);
				}
			}

			return result;
		}

		static IEnumerable<ISerializableMember> GetSerializableMembers(Type objType)
		{
			var classAttributes = new ClassAttributes(objType.GetCustomAttribute<JsonSerializableAttribute>(), IsAnonymous(objType));
			var members = new List<ISerializableMember>();

			if (!classAttributes.IsSerializable)
			{
				return members;
			}

			foreach (PropertyInfo property in objType.GetProperties(classAttributes.Binding))
			{
				if (IsSerializable(property, classAttributes))
				{
					members.Add(new PropertyMember(property));
				}
			}

			foreach (FieldInfo field in objType.GetFields(classAttributes.Binding))
			{
				if (IsSerializable(field, classAttributes))
				{
					members.Add(new FieldMember(field));
				}
			}

			return members;
		}

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
			else if (fieldType.IsEnum)
			{
				result = (JsonNumber)(int)source;
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
			var jsonObject = new JsonObject();

			foreach (ISerializableMember member in GetSerializableMembers(source.GetType()))
			{
				JsonElement json = SerializeField(member.Type, member.GetValue(source));

				if (json != null && !(json is JsonObject obj && obj.Count == 0))
				{
					jsonObject.Add(member.Name, json);
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


		#region Helper Functions - Deserialization
		static object Deserialize(Type target, JsonElement json)
		{
			return DeserializeField(target, json);
		}

		static object DeserializeField(Type target, JsonElement json)
		{
			object result;

			// TODO Target type needs to be used better. And add provisioning for enums.
			if (target.IsSubclassOf(typeof(JsonElement)))
			{
				result = json;
			}
			else
			{
				switch (json)
				{
					case JsonBool jsonBool:
						result = jsonBool.Value;
						break;

					case JsonNumber jsonNumber:
						result = target == typeof(double) ? result = jsonNumber.Value : Convert.ChangeType(jsonNumber.Value, target);
						break;

					case JsonString jsonString:
						result = jsonString.Value;
						break;

					case JsonArray jsonArray:
						result = DeserializeArray(target, jsonArray);
						break;

					case JsonObject jsonObject:
						result = DeserializeObject(target, jsonObject);
						break;

					default:
					case JsonNull _:
						result = null;
						break;
				}
			}

			return result;
		}

		static object DeserializeObject(Type target, JsonObject jsonObject)
		{
			// TODO Get all deserializable members. Not the same as serializable members since a serializable property might not have a setter and we need to rely on a constructor to set this state.
			IEnumerable<ISerializableMember> deserializableMembers = new List<ISerializableMember>();

			// TODO Figure out which constructor to use and add the values to the constructor parameters. Will later add a JsonConstructor attribute to indicate this.
			IDictionary<string, object> constructorParameters = new Dictionary<string, object>();

			object result = Activator.CreateInstance(target, constructorParameters.Values.ToArray());

			foreach (ISerializableMember member in deserializableMembers)
			{
				if (!constructorParameters.ContainsKey(member.Name) && jsonObject.ContainsKey(member.Name))
				{
					member.SetValue(result, DeserializeField(member.Type, jsonObject[member.Name]));
				}
			}

			return result;
		}

		static object DeserializeArray(Type target, JsonArray jsonArray)
		{
			// TODO Finish this implementation. There are a few curveballs with using reflection and figuring out what 
			// the actual type of the IList's elements are, further exasperated by conrecte implementations which might only be implementing IList.
			return null;
		}
		#endregion
	}
}
