using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using agl_web.UI.Models;

using agl_web.DataLayer;
using agl_web.DataLayer.Models;

namespace agl_web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //init People Service
            PeopleService ps = new PeopleService("http://agl-developer-test.azurewebsites.net/people.json");

            //Get all cats and then group by the owner's gender
            List<Pet> petArray = ps.GetPetsWithOwnerGender();
            List<GenderGroup> gp = ps.GetPetGenderGroups(petArray);

            //Create a Gender List - by grouping  the people with cats into 2 genders (Male, Female)
            GenderListVM genderList = new GenderListVM() { GenderList = gp };

            return View(genderList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}