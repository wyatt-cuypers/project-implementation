using NAUCountryA;
using NAUCountryA.Models;

namespace NAUCountryATest.Models
{
    public class CommodityTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestFormatCommodityCode()
        {
            while (true)
            {
                try
                {
                    Commodity commodity = new Commodity(37, "Raisins", "RAISINS", 'P', 2022, new DateTime(2022, 4, 7), "A00420");
                    string expected = "\"0037\"";
                    string actual = commodity.FormatCommodityCode();
                    Assert.That(actual, Is.EqualTo(expected));
                }
                catch (NullReferenceException)
                {
                    Service.InitializeUserTo(new NAUUser("localhost", 2023, "postgres", "naucountrydev"));
                }
            }
        }
    }
}