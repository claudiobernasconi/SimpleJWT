using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleJWT.Serialization;

namespace SimpleJWT.TestCore.Serialization
{
    [TestClass]
    public class SystemTextJsonSerializerTest
    {
        [TestMethod]
        public void Serialize()
        {
            var serializer = new SystemTextJsonSerializer();

            var person = new TestPerson(35, "Roger Federer", new DateTime(1981, 8, 8));
            var output = serializer.Serialize(person);

            Assert.AreEqual("{\"Age\":35,\"Name\":\"Roger Federer\",\"DateOfBirth\":\"1981-08-08T00:00:00\"}", output);
        }

        [TestMethod]
        public void Deserialize()
        {
            var serializer = new SystemTextJsonSerializer();

            const string input = "{\"Age\":35,\"Name\":\"Roger Federer\",\"DateOfBirth\":\"1981-08-08T00:00:00\"}";

            var person = serializer.Deserialize(input);

            Assert.AreEqual(35, person["Age"]);
            Assert.AreEqual(new DateTime(1981, 8, 8), person["DateOfBirth"]);
            Assert.AreEqual("Roger Federer", person["Name"]);
        }

        public record TestPerson(int Age, string Name, DateTime DateOfBirth);
    }
}
