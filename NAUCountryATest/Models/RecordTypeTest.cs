using NAUCountryA;
using NAUCountryA.Models;

namespace NAUCountryATest.Models
{
    public class RecordTypeTest
    {
        private RecordType recordType;
        [SetUp]
        public void Setup()
        {
            recordType = new RecordType("A00420", 1, 2023);
        }

        [Test]
        public void TestToString()
        {
            string expected = "\"A00420\",\"01\",\"2023\"";
            string actual = recordType.ToString();
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}