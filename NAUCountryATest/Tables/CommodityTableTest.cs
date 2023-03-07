using NAUCountryA;
using NAUCountryA.Models;

namespace NAUCountryATest.Tables
{
    public class CommodityTableTest
    {
        [SetUp]
        public void Setup()
        {
            Service.InitializeUserTo(ServiceTest.TEST_USER);
        }

        [Test]
        public void TestCount()
        {
            Assert.That(Service.CommodityEntries.Count,Is.EqualTo(140));
        }
        /*
        "RECORD_TYPE_CODE","RECORD_CATEGORY_CODE","REINSURANCE_YEAR","COMMODITY_YEAR","COMMODITY_CODE","COMMODITY_NAME","COMMODITY_ABBREVIATION","ANNUAL_PLANTING_CODE","LAST_RELEASED_DATE","RELEASED_DATE","DELETED_DATE"
        "A00420","01","2023","2022","0037","Raisins","RAISINS","P","","4/7/2022",""
        "A00420","01","2023","2023","0011","Wheat","WHEAT","A","","6/17/2022",""
        "A00420","01","2023","2023","0012","Blueberries","BLUEBERRY","P","","8/9/2022",""
        "A00420","01","2023","2023","0013","Onions","ONIONS","A","","6/17/2022",""
        "A00420","01","2023","2023","0015","Canola","CANOLA","A","","6/17/2022",""
        */

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
                same = expectedKeys[i] == key;
                if (!same || i >= 5)
                {
                    break;
                }
                i++;
            }
            Assert.That(same, Is.True);
        }
    }
}