
using Onion.Model;
using System.Collections.Generic;
using System.Linq;

namespace Onion.Data
{
    public static class SeedData
    {
        public static void Initialize(OnionContext  context)
        {
            context.Database.EnsureCreated();
            if (!context.GetDbSet<Person>().Any())
            {
                GetPersons().ForEach(person => context.GetDbSet<Person>().Add(person));
                context.Commit();
            }
        }

        private static List<Person> GetPersons()
        {
            return new List<Person>
            {
                new Person{firstname = "Nick", lastname="Limnios", fathername = "Xristos" },
                new Person{firstname = "John", lastname="Limnios", fathername = "Xristos" }
            };
        }
    }
}
