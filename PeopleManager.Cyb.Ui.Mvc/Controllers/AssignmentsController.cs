using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Services;

namespace PeopleManager.Cyb.Ui.Mvc.Controllers
{
    [Authorize]
    public class AssignmentsController(
        AssignmentService assignmentService,
        PersonService personService) : Controller
    {
        public IActionResult Index()
        {
            var assignments = assignmentService.Find();
            return View(assignments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return AssignmentView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Assignment assignment)
        {
            if (!ModelState.IsValid)
            {
                return AssignmentView(assignment);
            }

            assignmentService.Create(assignment);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute]int id)
        {
            var assignment = assignmentService.Get(id);
            return AssignmentView(assignment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, Assignment assignment)
        {
            if (!ModelState.IsValid)
            {
                return AssignmentView(assignment);
            }

            assignmentService.Update(id, assignment);

            return RedirectToAction("Index");
        }

        private IActionResult AssignmentView(Assignment? assignment = null)
        {
            var people = personService.Find();
            //ViewData["People"] = people;
            ViewBag.People = people;

            if (assignment is null)
            {
                return View();
            }

            return View(assignment);
        }

        [HttpGet]
        public IActionResult Delete([FromRoute]int id)
        {
            var assignment = assignmentService.Get(id);
            return View(assignment);
        }

        [HttpPost("/assignments/delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed([FromRoute]int id)
        {
            assignmentService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
