using ECOMap;
using ECOMap.Models;
using ECOMap.Tables;

namespace ECOMapTest.Tables
{
    public class StateTableTest
    {
        // Assigned to Miranda Ryan
        private StateTable tableMockup;
        private ECODataService ECODataService { get; set; }

		[SetUp]
        public async Task Setup()
        {
			ECODataService = await CachedData.GetECODataService();
		}

        [Test]
        public void TestContainsKey1()
        {
            bool actual = tableMockup.ContainsKey(01);
            Assert.That(actual, Is.True);
        }

        [Test]
        public void TestContainsKey2()
        {
            bool actual = tableMockup.ContainsKey(60);
            Assert.That(actual, Is.False);
        }

        [Test]
        public void TestCount()
        {
            Assert.That(tableMockup.Count, Is.EqualTo(5));
        }

        [Test]
        public void TestGetStateByKeyValid()
        {
            State expected = new State(01, "Alabama", "AL", "A00520", ECODataService.RecordTypeEntries["A00520"]);
            State actual = tableMockup[01];
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetStateByKeyInvalid()
        {
            Assert.Throws<KeyNotFoundException>(() => ToDelegate(60));
        }

        private void ToDelegate(int key)
        {
            State type = tableMockup[key];
        }

        [Test]
        public void TestGetKeys()
        {
            IEnumerable<int> expected = new int[] { 01, 02, 04, 05, 06 }.AsEnumerable();
            IEnumerable<int> actual = tableMockup.Keys;
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void TestGetValues()
        {
            IEnumerable<State> expected = new State[]
            {new State(01, "Alabama", "AL", "A00520", ECODataService.RecordTypeEntries["A00520"]),
            new State(02, "Alaska", "AK", "A00520", ECODataService.RecordTypeEntries["A00520"]),
            new State(04, "Arizona", "AZ", "A00520", ECODataService.RecordTypeEntries["A00520"]),
            new State(05, "Arkansas", "AR", "A00520", ECODataService.RecordTypeEntries["A00520"]),
            new State(06, "California", "CA", "A00520", ECODataService.RecordTypeEntries["A00520"])};
            IEnumerable<State> actual = tableMockup.Values;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetEnumerator()
        {
            ICollection<KeyValuePair<int,State>> expected = new HashSet<KeyValuePair<int,State>>();
            expected.Add(new KeyValuePair<int, State>(01, new State(01, "Alabama", "AL", "A00520", ECODataService.RecordTypeEntries["A00520"])));
            expected.Add(new KeyValuePair<int, State>(02, new State(02, "Alaska", "AK", "A00520", ECODataService.RecordTypeEntries["A00520"])));
            expected.Add(new KeyValuePair<int, State>(04, new State(04, "Arizona", "AZ", "A00520", ECODataService.RecordTypeEntries["A00520"])));
            expected.Add(new KeyValuePair<int, State>(05, new State(05, "Arkansas", "AR", "A00520", ECODataService.RecordTypeEntries["A00520"])));
            expected.Add(new KeyValuePair<int, State>(06, new State(06, "California", "CA", "A00520", ECODataService.RecordTypeEntries["A00520"])));
            ICollection<KeyValuePair<int, State>> actual = new HashSet<KeyValuePair<int, State>>();
            foreach (KeyValuePair<int, State> pair in tableMockup)
            {
                actual.Add(pair);
            }
            Assert.That(actual, Is.EqualTo(actual));
        }

        [Test]
        public void TestTryGet1()
        {
            Assert.That(tableMockup.TryGetValue(01, out _), Is.True);
        }

        [Test]
        public void TestTryGet2()
        {
            Assert.That(tableMockup.TryGetValue(60, out _), Is.False);
        }
    }
}