using ListPeoplePost.Data;
using ListPeoplePost.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;



namespace ListPeoplePost.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress; Initial Catalog=PeopleList; Integrated Security=true;";

        public IActionResult Index()
        {
            PeopleDb db = new PeopleDb(_connectionString);
            IndexViewModel vm = new()
            {
                People = db.GetAllPeople()
            };

            if (TempData["message"] != null)
            {
                vm.Message = (string)TempData["message"];
            }

            return View(vm);

        }

        public IActionResult AddView()
        {
            return View();
        }

        public IActionResult Add(List<Person> people)
        {
            PeopleDb db = new PeopleDb(_connectionString);

            db.AddMany(people);

            TempData["message"] = $"{people.Count} {(people.Count == 1 ? "person has" : "people have")} been added!";
            return Redirect("/home/index");
        }

    }
}