using NAUCountryA;
using NAUCountryA.Models;
using NAUCountryA.Tables;

namespace NAUCountryATest.Tables
{
    public class CommodityTableTest
    {
        private IReadOnlyDictionary<int,Commodity> commodityEntries;
        [SetUp]
        public void Setup()
        {
            while (true)
            {
                try
                {
                    commodityEntries = new CommodityTable();
                    break;
                }
                catch (NullReferenceException)
                {
                    Service.InitializeUserTo(new NAUUser("localhost", 2023, "postgres", "naucountrydev"));
                }
            }
        }

        [Test]
        public void TestCount()
        {
            Assert.That(commodityEntries.Count,Is.EqualTo(140));
        }
    }
}