using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpacePark_ModelsDB.Database;
using SpacePark_ModelsDB.Models;

namespace SpacePark_API.Controllers
{
    public class TestController : Controller
    {
        private readonly IStarwarsRepository _repository;

        public TestController(IStarwarsRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            var model = _repository.People;
            
            return View(model);
        }

        public IActionResult Create()
        {
            var luke = new Person { Name = "Luke Skywalker" };

            _repository.Add(luke);

            _repository.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
