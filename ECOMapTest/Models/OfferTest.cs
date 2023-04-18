using ECOMap;
using ECOMap.Models;

namespace ECOMapTest.Models
{
    public class OfferTest
    {
        // Assigned to Miranda Ryan
        private Offer offer;

        [SetUp]
        public async Task Setup()
        {
			ECODataService service = await CachedData.GetECODataService();

			string line = "\"25770489\",\"06\",\"053\",\"132\",\"350\",\"002\"";
            offer = new Offer(service, line);
        }

        [Test]
        public void TestValid()
        {
            Assert.That(offer.Valid, Is.False);
        }
    }
}