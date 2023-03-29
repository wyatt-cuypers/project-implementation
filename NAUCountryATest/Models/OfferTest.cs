using System;
using NAUCountryA;
using NAUCountryA.Models;

namespace NAUCountryATest.Models
{
    public class OfferTest
    {
        // Assigned to Miranda Ryan
        private Offer offer;
        [SetUp]
        public void Setup()
        {
            Service.LoadTables();
            string line = "\"25770489\",\"06\",\"053\",\"132\",\"350\",\"002\"";
            offer = new Offer(line);
        }

        [Test]
        public void TestValid()
        {
            Assert.That(offer.Valid, Is.False);
        }
    }
}