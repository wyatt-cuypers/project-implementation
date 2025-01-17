using ECOMap;
using ECOMap.Models;

namespace ECOMapTest.Models
{
    public class CommodityTest
    {
        private Commodity commodity;
		private ECODataService ECODataService { get; set; }

		[SetUp]
        public async Task Setup()
        {
            ECODataService = await CachedData.GetECODataService();
		}

        [Test]
        public void TestEquals1()
        {
            object other = new Commodity(37, "Raisins", "RAISINS", 'P', 2022, new DateTime(2022, 4, 7), "A00420", ECODataService.RecordTypeEntries["A00420"]);
            Assert.That(commodity.Equals(other), Is.True);
        }

        [Test]
        public void TestEquals2()
        {
            object other = new Commodity(3,"Raisins", "RAISINS", 'P', 2022, new DateTime(2022, 4, 7), "A00420", ECODataService.RecordTypeEntries["A00420"]);
            Assert.That(commodity.Equals(other), Is.False);
        }

        [Test]
        public void TestEquals3()
        {
            object other = 37;
            Assert.That(commodity.Equals(other), Is.False);
        }

        [Test]
        public void TestFormatCommodityCode()
        {
            string expected = "\"0037\"";
            string actual = commodity.FormatCommodityCode();
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestToString()
        {
            string expected = "\"0037\",\"Raisins\",\"RAISINS\",\"P\",\"2022\",\"4/7/2022\",\"A00420\"";
            string actual = commodity.ToString();
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}