
using Microsoft.EntityFrameworkCore;
using PeopleManager.Core;
using PeopleManager.Model;

namespace PeopleManager.Services
{
    public class AssignmentService(PeopleManagerDbContext dbContext)
    {
        public IList<Assignment> Find()
        {
            return dbContext.Assignments
                .Include(a => a.AssignedTo)
                .ToList();
        }

        public Assignment? Get(int id)
        {
            return dbContext.Assignments
                .Include(a => a.AssignedTo)
                .FirstOrDefault(p => p.Id == id);
        }

        public Assignment? Create(Assignment assignment)
        {
            dbContext.Assignments.Add(assignment);
            dbContext.SaveChanges();

            return assignment;
        }

        public Assignment? Update(int id, Assignment assignment)
        {
            var dbAssignment = dbContext.Assignments.FirstOrDefault(p => p.Id == id);

            if (dbAssignment is null)
            {
                return null;
            }

            dbAssignment.Name = assignment.Name;
            dbAssignment.Description = assignment.Description;
            dbAssignment.AssignedToId = assignment.AssignedToId;

            dbContext.SaveChanges();

            return assignment;
        }

        public void Delete(int id)
        {
            var dbAssignment = dbContext.Assignments.FirstOrDefault(p => p.Id == id);

            if (dbAssignment is null)
            {
                return;
            }

            dbContext.Assignments.Remove(dbAssignment);

            dbContext.SaveChanges();
        }
    }
}
