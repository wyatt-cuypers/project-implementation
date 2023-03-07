using NAUCountryA;
using NAUCountryA.Models;

namespace NAUCountryATest.Tables
{
    public class CommodityTableTest
    {
        [SetUp]
        public void Setup()
        {
            Service.LoadTables();
        }

        [Test]
        public void TestContainsKey1()
        {
            Assert.That(Service.CommodityEntries.ContainsKey(37), Is.True);
        }

        [Test]
        public void TestContainsKey2()
        {
            Assert.That(Service.CommodityEntries.ContainsKey(141), Is.False);
        }

        [Test]
        public void TestCount()
        {
            Assert.That(Service.CommodityEntries.Count,Is.EqualTo(140)); // claiming to be 134 by N Unit
        }

        [Test]
        public void TestGetEnumerator()
        {
            IList<KeyValuePair<int,Commodity>> expectedPairs = new List<KeyValuePair<int,Commodity>>();
            expectedPairs.Add(new Commodity(37, "Raisins", "RAISINS", 'P', 2022, Service.ToDateTime("4/7/2022"), "A00420").Pair);
            expectedPairs.Add(new Commodity(11, "Wheat", "WHEAT", 'A', 2023, Service.ToDateTime("6/17/2022"), "A00420").Pair);
            expectedPairs.Add(new Commodity(12, "Blueberries", "BLUEBERRY", 'P', 2023, Service.ToDateTime("8/9/2022"), "A00420").Pair);
            expectedPairs.Add(new Commodity(13, "Onions", "ONIONS", 'A', 2023, Service.ToDateTime("6/17/2022"), "A00420").Pair);
            expectedPairs.Add(new Commodity(15, "Canola", "CANOLA", 'A', 2023, Service.ToDateTime("6/17/2022"), "A00420").Pair);
            bool same = true;
            int i = 0;
            foreach (KeyValuePair<int,Commodity> pair in Service.CommodityEntries)
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
            Commodity expected = new Commodity(37, "Raisins", "RAISINS", 'P', 2022, Service.ToDateTime("4/7/2022"), "A00420");
            Commodity actual = Service.CommodityEntries[37];
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
            foreach (int key in Service.CommodityEntries.Keys)
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
            Commodity commodity = new Commodity(37, "Raisins", "RAISINS", 'P', 2022, Service.ToDateTime("4/7/2022"), "A00420");
            Assert.That(Service.CommodityEntries.TryGetValue(37, out commodity), Is.True);
        }

        [Test]
        public void TestTryGet2()
        {
            Commodity commodity;
            Assert.That(Service.CommodityEntries.TryGetValue(140, out commodity), Is.False);
        }

        [Test]
        public void TestValues()
        {
            IList<Commodity> expectedValues = new List<Commodity>();
            expectedValues.Add(new Commodity(37, "Raisins", "RAISINS", 'P', 2022, Service.ToDateTime("4/7/2022"), "A00420"));
            expectedValues.Add(new Commodity(11, "Wheat", "WHEAT", 'A', 2023, Service.ToDateTime("6/17/2022"), "A00420"));
            expectedValues.Add(new Commodity(12, "Blueberries", "BLUEBERRY", 'P', 2023, Service.ToDateTime("8/9/2022"), "A00420"));
            expectedValues.Add(new Commodity(13, "Onions", "ONIONS", 'A', 2023, Service.ToDateTime("6/17/2022"), "A00420"));
            expectedValues.Add(new Commodity(15, "Canola", "CANOLA", 'A', 2023, Service.ToDateTime("6/17/2022"), "A00420"));
            bool same = true;
            int i = 0;
            foreach (Commodity value in Service.CommodityEntries.Values)
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
            Commodity type = Service.CommodityEntries[key];
        }
    }
}