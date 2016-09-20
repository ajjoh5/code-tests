using System;
using System.Collections.Generic;
using System.Linq;
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
        public void GetCats_GroupedByGender()
        {
            PeopleService ps = new PeopleService("http://agl-developer-test.azurewebsites.net/people.json");
            List<Pet> petArray = ps.GetPetsWithOwnerGender();
            List<GenderGroup> gp = ps.GetPetGenderGroups(petArray);

            Assert.AreEqual(7, petArray.Count, "Cats, into Gender groups contained error");
            Assert.AreEqual(2, gp.Count, "Gender groups not = 2 (Male, Female) - contained error");

            //Ensure 4 cats for Male
            Assert.AreEqual(4, gp.SingleOrDefault(x => x.Gender == "Male").PetList.Count, "Male group didn't have 4 cats");

            //Ensure 3 cats for Male
            Assert.AreEqual(3, gp.SingleOrDefault(x => x.Gender == "Female").PetList.Count, "Female group didn't have 3 cats");

        }
    }
}
