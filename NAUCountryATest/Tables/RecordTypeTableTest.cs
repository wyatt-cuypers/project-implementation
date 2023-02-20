using NAUCountryA.Models;
using NAUCountryA.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NAUCountryATest.Tables
{
    public class RecordTypeTableTest
    {
        private RecordTypeTable tableMockup;
        [SetUp]
        public void Setup()
        {
            User testUser = new User("localhost", 2023, "postgres", "naucountrydev");
            tableMockup = new RecordTypeTable(testUser);
        }

        [Test]
        public void TestContainsKey1()
        {
            bool actual = tableMockup.ContainsKey("A00420");
            Assert.That(actual, Is.True);
        }

        [Test]
        public void TestContainsKey2()
        {
            bool actual = tableMockup.ContainsKey("A0042");
            Assert.That(actual, Is.False);
        }
    }
}