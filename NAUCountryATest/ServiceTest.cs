using NAUCountryA;
namespace NAUCountryATest
{
    public class ServiceTest
    {

        [Test]
        public void TestConnection()
        {
            Assert.That(Service.Connection, Is.Not.Null);
        }
    }
}