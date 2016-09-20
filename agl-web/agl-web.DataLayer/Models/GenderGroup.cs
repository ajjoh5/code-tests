using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agl_web.DataLayer.Models
{
    public class GenderGroup
    {
        public string Gender { get; set; }

        public List<Person> PeopleList { get; set; }

        public List<Pet> PetList { get; set; }
    }
}
