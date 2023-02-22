using NAUCountryA.Models;
namespace NAUCountryATest.Models
{
    public class UserTest
    {
        
        [Test]
        public void TestConnection()
        {
            User testUser = new User("localhost", 2023, "postgres", "naucountrydev");
            string expected = "Host=localhost;Port=2023;Username=postgres;Database=NAUCountryData";
            Assert.That(testUser.Connection.ConnectionString, Is.EqualTo(expected));
        }
    }
}