using Microsoft.AspNetCore.Mvc;
using todolistaspnetcore.DAL;
using todolistaspnetcore.Models;

namespace todolistaspnetcore.Controllers
{
    public class ToDoPositionsController : Controller
    {
        private readonly IRepo repo;

        public ToDoPositionsController(IRepo repo)
        {
            this.repo = repo;
        }

        public ActionResult Index()
        {
            return View(repo.DbContext.Positions);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ToDoPosition position)
        {
            try
            {
                repo.AddPosition(position);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View(repo.DbContext.Positions.Single(c => c.Id == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ToDoPosition newPosition)
        {
            try
            {
                var oldPosition = repo.DbContext.Positions.Single(c => c.Id == id);

                repo.EditPosition(oldPosition, newPosition);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View(repo.DbContext.Positions.Single(c => c.Id == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var position = repo.DbContext.Positions.Single(c => c.Id == id);

                repo.DeletePosition(position);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}