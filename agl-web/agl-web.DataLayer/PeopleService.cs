using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using Newtonsoft.Json;

using agl_web.DataLayer.Models;

namespace agl_web.DataLayer
{
    public class PeopleService
    {
        public string apiURL = "";

        public PeopleService(string url)
        {
            apiURL = url;
        }

        public List<Person> GetPeople()
        {
            List<Person> retval = new List<Person>();

            try
            {
                var client = new WebClient();
                string json = client.DownloadString(apiURL);
                //string json = "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]},{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"}]},{\"name\":\"Steve\",\"gender\":\"Male\",\"age\":45,\"pets\":null},{\"name\":\"Fred\",\"gender\":\"Male\",\"age\":40,\"pets\":[{\"name\":\"Tom\",\"type\":\"Cat\"},{\"name\":\"Max\",\"type\":\"Cat\"},{\"name\":\"Sam\",\"type\":\"Dog\"},{\"name\":\"Jim\",\"type\":\"Cat\"}]},{\"name\":\"Samantha\",\"gender\":\"Female\",\"age\":40,\"pets\":[{\"name\":\"Tabby\",\"type\":\"Cat\"}]},{\"name\":\"Alice\",\"gender\":\"Female\",\"age\":64,\"pets\":[{\"name\":\"Simba\",\"type\":\"Cat\"},{\"name\":\"Nemo\",\"type\":\"Fish\"}]}]";
                retval = JsonConvert.DeserializeObject<List<Person>>(json);
            }
            catch (Exception ex)
            {
                //Log exception here...
            }
            

            return retval;
        }

        public List<Person> GetPeopleWithCats()
        {
            List<Person> retval = new List<Person>();

            try
            {
                //Get all people to filter on
                var allPeople = this.GetPeople();

                //filter by all people who have 1 or more pets, and contain at least 1 cat
                //then remove all other pets (not a cat) and order them alphabetically

                retval =
                (
                    from p in allPeople
                    where p.Pets != null && p.Pets.Any(x => x.Type.ToUpper() == "CAT")
                    select new Person()
                    {
                        Age = p.Age,
                        Gender = p.Gender,
                        Name = p.Name,
                        Pets = p.Pets.Where(x => x.Type.ToUpper() == "CAT").OrderBy(x => x.Name).ToList()
                    }
                ).ToList();
                
            }
            catch (Exception ex)
            {
                //Log exception here...
            }

            return retval;
        }

        public List<Pet> GetPetsWithOwnerGender()
        {
            List<Pet> retval = new List<Pet>();

            try
            {
                //Get all people to filter on
                var allPeople = this.GetPeople();

                //Get all people with cats
                var peopleCatList =
                (
                    from p in allPeople
                    where p.Pets != null && p.Pets.Any(x => x.Type.ToUpper() == "CAT")
                    select new Person()
                    {
                        Age = p.Age,
                        Gender = p.Gender,
                        Name = p.Name,
                        Pets = (
                                    from pet in p.Pets
                                    where pet.Type.ToUpper() == "CAT"
                                    select new Pet()
                                    {
                                        Name = pet.Name,
                                        Type = pet.Type,
                                        OwnerGender = p.Gender
                                    }
                                ).ToList()
                    }
                );

                //Add each owners pets together into a Pet List
                foreach (Person p in peopleCatList)
                {
                    retval.AddRange(p.Pets);
                }
                
                //Order pet list alphabetically
                retval = retval.OrderBy(x => x.Name).ToList();

            }
            catch (Exception ex)
            {
                //Log exception here...
            }

            return retval;
        }

        public List<GenderGroup> GetPeopleGenderGroups(List<Person> p)
        {
            List<GenderGroup> retval = new List<GenderGroup>();

            try
            {
                retval =
                    p.GroupBy(x => x.Gender, (key, g) => new GenderGroup
                    {
                        Gender = key,
                        PeopleList = g.ToList()
                    }).ToList();

            }
            catch (Exception ex)
            {
                //Log exception here...
            }

            return retval;
        }

        public List<GenderGroup> GetPetGenderGroups(List<Pet> p)
        {
            List<GenderGroup> retval = new List<GenderGroup>();

            try
            {
                retval =
                    p.GroupBy(x => x.OwnerGender, (key, g) => new GenderGroup
                    {
                        Gender = key,
                        PetList = g.ToList()
                    }).ToList();

            }
            catch (Exception ex)
            {
                //Log exception here...
            }

            return retval;
        }
    }
}
