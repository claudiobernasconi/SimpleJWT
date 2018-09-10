using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleJWT.Serialization;
using System;

namespace SimpleJWT.TestCore.Serialization
{
    [TestClass]
    public class NewtonsoftJsonSerializerTest
    {
        [TestMethod]
        public void Serialize()
        {
            var serializer = new NewtonsoftJsonSerializer();

            var person = new TestPerson
            {
                Age = 35,
                Birthdate = new DateTime(1981, 8, 8),
                Name = "Roger Federer"
            };

            var output = serializer.Serialize(person);

            Assert.AreEqual("{\"Age\":35,\"Name\":\"Roger Federer\",\"Birthdate\":\"1981-08-08T00:00:00\"}", output);
        }

        [TestMethod]
        public void Deserialize()
        {
            var serializer = new NewtonsoftJsonSerializer();

            const string input = "{\"Age\":35,\"Name\":\"Roger Federer\",\"Birthdate\":\"1981-08-08T00:00:00\"}";

            var person = serializer.Deserialize<TestPerson>(input);

            Assert.AreEqual(35, person.Age);
            Assert.AreEqual(new DateTime(1981, 8, 8), person.Birthdate);
            Assert.AreEqual("Roger Federer", person.Name);
        }

        public class TestPerson
        {
            public int Age { get; set; }
            public string Name { get; set; }
            public DateTime? Birthdate { get; set; }
        }
    }
}
