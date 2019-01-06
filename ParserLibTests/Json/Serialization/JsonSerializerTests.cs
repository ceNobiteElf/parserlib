using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ParserLib.Json.Serialization;

namespace ParserLibTests.Json.Serialization
{
	[TestClass]
	public class JsonSerializerTests
	{
		#region Nested Types
		public enum TrainingStatus
		{
			Untrained,
			InTraining,
			Trained
		}

		[JsonSerializable]
		public sealed class Pet
		{
			public bool IsVaccinated { get; set; }

			public string Name { get; set; }
			public TrainingStatus TrainingStatus { get; set; }
		}

		[JsonSerializable]
		public sealed class Tester
		{
			public bool IsActive { get; set; }

			public string Name { get; set; }
			public string Surname { get; set; }

			public int Age { get; set; }

			public List<Pet> Pets { get; set; } = new List<Pet>();
		}

		[JsonSerializable]
		public sealed class Settings
		{
			public double MusicVolume { get; set; } = 0.7;
			public double EffectsVolume { get; set; } = 0.8;

			public double InterfaceVolume { get; set; } = 0.5;

			public bool AllowNotifications { get; set; } = true;
		}
		#endregion


		#region Tests - Serialization
		[TestMethod, TestCategory("JsonSerializer - Serialization")]
		public void SerializeObject_SimpleObject_DefaultValues()
		{
			var serializableObject = new Settings();

			string result = JsonSerializer.Serialize(serializableObject);

			Assert.AreEqual("{\"MusicVolume\":0.7,\"EffectsVolume\":0.8,\"InterfaceVolume\":0.5,\"AllowNotifications\":true}", result);
		}

		[TestMethod, TestCategory("JsonSerializer - Serialization")]
		public void SerializeObject_SimpleObject_WithEnum()
		{
			var serializableObject = new Pet {
				IsVaccinated = true,
				Name = "Fluffy",
				TrainingStatus = TrainingStatus.Trained
			};

			string result = JsonSerializer.Serialize(serializableObject);

			Assert.AreEqual("{\"IsVaccinated\":true,\"Name\":\"Fluffy\",\"TrainingStatus\":2}", result);
		}

		[TestMethod, TestCategory("JsonSerializer - Serialization")]
		public void SerializeObject_ComplexObject_DefaultValues()
		{
			var serializableObject = new Tester();

			string result = JsonSerializer.Serialize(serializableObject);

			Assert.AreEqual("{\"IsActive\":false,\"Name\":null,\"Surname\":null,\"Age\":0,\"Pets\":[]}", result);
		}

		[TestMethod, TestCategory("JsonSerializer - Serialization")]
		public void SerializeObject_ComplexObject_NonDefaultValues()
		{
			var serializableObject = new Tester {
				IsActive = false,
				Name = "Tester",
				Surname = "McTester",
				Age = 25,
				Pets = new List<Pet> {
					new Pet { Name = "Pet1" },
					new Pet { Name = "Pet2", TrainingStatus = TrainingStatus.InTraining }
				} 
			};

			string result = JsonSerializer.Serialize(serializableObject);

			Assert.AreEqual(
				"{\"IsActive\":false,\"Name\":\"Tester\",\"Surname\":\"McTester\",\"Age\":25,\"Pets\":[{\"IsVaccinated\":false,\"Name\":\"Pet1\",\"TrainingStatus\":0},{\"IsVaccinated\":false,\"Name\":\"Pet2\",\"TrainingStatus\":1}]}",
				result
			);
		}

		// TODO Write more tests as the above tests only test "best cases" where the the API of a class is nice and public without any transient weirdness.
		#endregion
	}
}
