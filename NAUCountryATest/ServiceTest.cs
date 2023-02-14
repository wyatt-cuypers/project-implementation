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

        [Test]
        public void TestInitialPathLocation()
        {
            string expectedPath = "C:\\Users\\zakme\\OneDrive - North Dakota University System\\Documents\\project-implementation";
            Assert.That(Service.InitialPathLocation, Is.EqualTo(expectedPath));
        }

        [Test]
        public void TestToCollectionOfA22_INSURANCE_OFFERCount()
        {
            int expected = 2393564;
            Assert.That(Service.ToCollection("A22_INSURANCE_OFFER"), Has.Count.EqualTo(expected));
        }

        [Test]
        public void TestToCollectionOfA22_INSURANCE_OFFERHeaders()
        {
            string expectedHeaders = "\"ADM_INSURANCE_OFFER_ID\",\"STATE_CODE\",\"COUNTY_CODE\",\"TYPE_CODE\",\"PRACTICE_CODE\",\"IRRIGATION_PRACTICE_CODE\"";
            Assert.That(Service.ToCollection("A22_INSURANCE_OFFER").First(), Is.EqualTo(expectedHeaders));
        }

        [Test]
        public void TestToCollectionOfA22_INSURANCE_OFFERFirstEntry()
        {
            string expectedFirstEntry = "\"25360764\",\"06\",\"019\",\"997\",\"092\",\"997\"";
            IEnumerable<string> lines = Service.ToCollection("A22_INSURANCE_OFFER");
            IEnumerator<string> lineEnumerator = lines.GetEnumerator();
            string actualFirstEntry = "";
            if (lineEnumerator.MoveNext() && lineEnumerator.MoveNext()) 
            {
                actualFirstEntry = lineEnumerator.Current;
            }
            Assert.That(actualFirstEntry, Is.EqualTo(expectedFirstEntry));
        }

        [Test]
        public void TestToCollectionOfA23_CountyCount()
        {
            int expected = 3192;
            Assert.That(Service.ToCollection("A23_County"), Has.Count.EqualTo(expected));
        }

        [Test]
        public void TestToCollectionOfA23_CountyHeaders()
        {
            string expectedHeaders = "\"RECORD_TYPE_CODE\",\"RECORD_CATEGORY_CODE\",\"REINSURANCE_YEAR\",\"STATE_CODE\",\"COUNTY_CODE\",\"COUNTY_NAME\",\"LAST_RELEASED_DATE\",\"RELEASED_DATE\",\"DELETED_DATE\"";
            Assert.That(Service.ToCollection("A23_County").First(), Is.EqualTo(expectedHeaders));
        }

        [Test]
        public void TestToCollectionOfA23_CountyFirstEntry()
        {
            string expectedFirstEntry = "\"A00440\",\"01\",\"2023\",\"01\",\"001\",\"Autauga\",\"\",\"4/28/2022\",\"\"";
            IEnumerable<string> lines = Service.ToCollection("A23_County");
            IEnumerator<string> lineEnumerator = lines.GetEnumerator();
            string actualFirstEntry = "";
            if (lineEnumerator.MoveNext() && lineEnumerator.MoveNext()) 
            {
                actualFirstEntry = lineEnumerator.Current;
            }
            Assert.That(actualFirstEntry, Is.EqualTo(expectedFirstEntry));
        }
    }
}