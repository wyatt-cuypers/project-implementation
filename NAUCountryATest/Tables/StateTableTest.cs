using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NAUCountryATest.Tables
{
    public class StateTableTest
    {
        [SetUp]
        public void Setup()
        {
            try
            {
                tableMockup = new StateTable();
            }
            catch (NullReferenceException)
            {
                Service.InitializeUserTo(new User("localhost", 2023, "postgres", "naucountrydev"));
            }
        }

        [Test]
        public void TestContainsKey1()
        {
            bool actual = tableMockup.ContainsKey("06");
            Assert.That(actual, Is.True);
        }

        [Test]
        public void TestContainsKey2()
        {
            bool actual = tableMockup.ContainsKey("98");
            Assert.That(actual, Is.False);
        }

        
    }
}
