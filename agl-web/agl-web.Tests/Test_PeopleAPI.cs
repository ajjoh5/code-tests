using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Net;

using agl_web.DataLayer;
using agl_web.DataLayer.Models;

namespace agl_web.Tests
{
    [TestClass]
    public class Test_PeopleAPI
    {
        [TestMethod]
        public void GetPeople()
        {
            PeopleService ps = new PeopleService("http://agl-developer-test.azurewebsites.net/people.json");
            List<Person> pArray = ps.GetPeople();

            Assert.IsTrue(pArray.Count > 0, "People Service did not return any people with pets");
        }

        [TestMethod]
        public void GetPeopleWithCats()
        {
            PeopleService ps = new PeopleService("http://agl-developer-test.azurewebsites.net/people.json");
            List<Person> pArray = ps.GetPeopleWithCats();

            Assert.AreEqual(5, pArray.Count, "People Service did not return any people with cats");
        }

        [TestMethod]
        public void GetPeopleWithCats_GroupedByGender()
        {
            PeopleService ps = new PeopleService("http://agl-developer-test.azurewebsites.net/people.json");
            List<Person> pArray = ps.GetPeopleWithCats();
            List<GenderGroup> gp = ps.GetGenderGroups(pArray);

            Assert.AreEqual(2, gp.Count, "Gender groups did not group by correctly");
        }
    }
}
