using Microsoft.EntityFrameworkCore;
using PeopleManager.Core;
using PeopleManager.Model;

namespace PeopleManager.Services
{
    public class PersonService(PeopleManagerDbContext dbContext)
    {
        public IList<Person> Find()
        {
            return dbContext.People
                .ToList();
        }

        public Person? Get(int id)
        {
            return dbContext.People
                .FirstOrDefault(p => p.Id == id);
        }

        public Person? Create(Person person)
        {
            dbContext.People.Add(person);
            dbContext.SaveChanges();

            return person;
        }

        public Person? Update(int id, Person person)
        {
            var dbPerson = dbContext.People.FirstOrDefault(p => p.Id == id);

            if (dbPerson is null)
            {
                return null;
            }

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;

            dbContext.SaveChanges();

            return person;
        }

        public void Delete(int id)
        {
            var dbPerson = dbContext.People.FirstOrDefault(p => p.Id == id);

            if (dbPerson is null)
            {
                return;
            }

            dbContext.People.Remove(dbPerson);

            dbContext.SaveChanges();
        }
    }
}
