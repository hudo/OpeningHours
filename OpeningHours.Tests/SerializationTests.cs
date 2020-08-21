using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace OpeningHours.Tests
{
    public class SerializationTests
    {
        [Test]
        public void Serialize_tuple()
        {
            Time time = 2.5;

            var json = JsonSerializer.Serialize(time);

            var deserialized = JsonSerializer.Deserialize<Time>(json);

            Assert.AreEqual("\"2:30\"", json);
            Assert.IsTrue(deserialized == new Time(2, 30));
        }
    }
}
