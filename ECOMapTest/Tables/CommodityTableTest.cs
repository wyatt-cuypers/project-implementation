using ECOMap;
using ECOMap.Models;

namespace ECOMapTest.Tables
{
    public class CommodityTableTest
    {
		private ECODataService ECODataService { get; set; }

		[SetUp]
        public async Task Setup()
        {
			ECODataService = await CachedData.GetECODataService();
		}

        [Test]
        public void TestContainsKey1()
        {
            Assert.That(ECODataService.CommodityEntries.ContainsKey(37), Is.True);
        }

        [Test]
        public void TestContainsKey2()
        {
            Assert.That(ECODataService.CommodityEntries.ContainsKey(141), Is.False);
        }

        [Test]
        public void TestCount()
        {
            Assert.That(ECODataService.CommodityEntries.Count,Is.EqualTo(140)); // claiming to be 134 by N Unit
        }

        [Test]
        public void TestGetEnumerator()
        {
            IList<KeyValuePair<int,Commodity>> expectedPairs = new List<KeyValuePair<int,Commodity>>();
            expectedPairs.Add(new Commodity(37, "Raisins", "RAISINS", 'P', 2022, CsvUtility.ToDateTime("4/7/2022"), "A00420", ECODataService.RecordTypeEntries["A00420"]).Pair);
            expectedPairs.Add(new Commodity(11, "Wheat", "WHEAT", 'A', 2023, CsvUtility.ToDateTime("6/17/2022"), "A00420", ECODataService.RecordTypeEntries["A00420"]).Pair);
            expectedPairs.Add(new Commodity(12, "Blueberries", "BLUEBERRY", 'P', 2023, CsvUtility.ToDateTime("8/9/2022"), "A00420", ECODataService.RecordTypeEntries["A00420"]).Pair);
            expectedPairs.Add(new Commodity(13, "Onions", "ONIONS", 'A', 2023, CsvUtility.ToDateTime("6/17/2022"), "A00420", ECODataService.RecordTypeEntries["A00420"]).Pair);
            expectedPairs.Add(new Commodity(15, "Canola", "CANOLA", 'A', 2023, CsvUtility.ToDateTime("6/17/2022"), "A00420", ECODataService.RecordTypeEntries["A00420"]).Pair);
            bool same = true;
            int i = 0;
            foreach (KeyValuePair<int,Commodity> pair in ECODataService.CommodityEntries)
            {
                try
                {
                    same = expectedPairs[i].Key == pair.Key && expectedPairs[i++].Value == pair.Value;
                    if (!same)
                    {
                        break;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }
            Assert.That(same, Is.True);
        }

        [Test]
        public void TestGetRecordTypeByKeyInvalid()
        {
            Assert.Throws<KeyNotFoundException>(() => ToDelegate(140));
        }

        [Test]
        public void TestGetRecordTypeByKeyValid()
        {
            Commodity expected = new Commodity(37, "Raisins", "RAISINS", 'P', 2022, CsvUtility.ToDateTime("4/7/2022"), "A00420", ECODataService.RecordTypeEntries["A00420"]);
            Commodity actual = ECODataService.CommodityEntries[37];
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestKeys()
        {
            IList<int> expectedKeys = new List<int>();
            expectedKeys.Add(37);
            expectedKeys.Add(11);
            expectedKeys.Add(12);
            expectedKeys.Add(13);
            expectedKeys.Add(15);
            bool same = true;
            int i = 0;
            foreach (int key in ECODataService.CommodityEntries.Keys)
            {
                try
                {
                    same = expectedKeys[i++] == key;
                    if (!same)
                    {
                        break;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }

            }
            Assert.That(same, Is.True);
        }

        [Test]
        public void TestTryGet1()
        {
            Commodity commodity = new Commodity(37, "Raisins", "RAISINS", 'P', 2022, CsvUtility.ToDateTime("4/7/2022"), "A00420", ECODataService.RecordTypeEntries["A00420"]);
            Assert.That(ECODataService.CommodityEntries.TryGetValue(37, out commodity), Is.True);
        }

        [Test]
        public void TestTryGet2()
        {
            Assert.That(ECODataService.CommodityEntries.TryGetValue(140, out _), Is.False);
        }

        [Test]
        public void TestValues()
        {
            IList<Commodity> expectedValues = new List<Commodity>();
            expectedValues.Add(new Commodity(37, "Raisins", "RAISINS", 'P', 2022, CsvUtility.ToDateTime("4/7/2022"), "A00420", ECODataService.RecordTypeEntries["A00420"]));
            expectedValues.Add(new Commodity(11, "Wheat", "WHEAT", 'A', 2023, CsvUtility.ToDateTime("6/17/2022"), "A00420", ECODataService.RecordTypeEntries["A00420"]));
            expectedValues.Add(new Commodity(12, "Blueberries", "BLUEBERRY", 'P', 2023, CsvUtility.ToDateTime("8/9/2022"), "A00420", ECODataService.RecordTypeEntries["A00420"]));
            expectedValues.Add(new Commodity(13, "Onions", "ONIONS", 'A', 2023, CsvUtility.ToDateTime("6/17/2022"), "A00420", ECODataService.RecordTypeEntries["A00420"]));
            expectedValues.Add(new Commodity(15, "Canola", "CANOLA", 'A', 2023, CsvUtility.ToDateTime("6/17/2022"), "A00420", ECODataService.RecordTypeEntries["A00420"]));
            bool same = true;
            int i = 0;
            foreach (Commodity value in ECODataService.CommodityEntries.Values)
            {
                try
                {
                    same = expectedValues[i++] == value;
                    if (!same)
                    {
                        break;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }
            Assert.That(same, Is.True);
        }

        private void ToDelegate(int key)
        {
            Commodity type = ECODataService.CommodityEntries[key];
        }
    }
}