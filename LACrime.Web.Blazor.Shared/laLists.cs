using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;

namespace LACrimes.Web.Blazor.Shared
{
    public class laLists
    {
        public IList<Crime> Crimes = new List<Crime>();
        public IList<Area> Areas = new List<Area>();
        public IList<Premis> Premises = new List<Premis>();
        public IList<Status> Statuses = new List<Status>();
        public IList<SubArea> SubAreas = new List<SubArea>();
        public IList<Victim> Victims = new List<Victim>();
        public IList<Weapon> Weapons = new List<Weapon>();
        public IList<Street> Streets = new List<Street>();
        public IList<Coordinates> Coordinates = new List<Coordinates>();
    }
}
