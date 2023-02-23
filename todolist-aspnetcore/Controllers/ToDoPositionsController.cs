using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Text.Json;
using todolistaspnetcore.DAL;
using todolistaspnetcore.Models;
using todolistaspnetcore.Services;

namespace todolistaspnetcore.Controllers
{
    public class ToDoPositionsController : Controller
    {
        private readonly IRepo repo;
        private readonly ICacheService cacheService;
        private readonly ILogger<ToDoPositionsController> log;

        public ToDoPositionsController(IRepo repo, ICacheService cacheService,
            ILogger<ToDoPositionsController> log)
        {
            this.repo = repo;
            this.cacheService = cacheService;
            this.log = log;
        }

        public async Task<ActionResult> Index()
        {
            RedisValue[] tasksFromCache = await cacheService.GetAllFromListAsync("redis", 6379, "tasks");

            if (tasksFromCache != null)
            {
                log.LogInformation($"tasksFromCache len = {tasksFromCache.Length}");

                foreach (RedisValue taskFromCache in tasksFromCache)
                {
                    log.LogInformation($"tasksFromCache: {taskFromCache}");
                }
            }

            List<ToDoPosition> positions = new List<ToDoPosition>();
            if (tasksFromCache == null || tasksFromCache.Length == 0)
            {
                positions = repo.DbContext.Positions.ToList();

                foreach (ToDoPosition position in positions)
                {
                    log.LogInformation($"adding to cache task = {position.Id}");

                    string serializedTask = JsonSerializer.Serialize(position);
                    await cacheService.AddToListAsync("redis", 6379, "tasks", serializedTask);
                }
            }
            else
            {
                foreach (RedisValue taskFromCache in tasksFromCache)
                {
                    ToDoPosition deserializedTask = JsonSerializer.Deserialize<ToDoPosition>(taskFromCache);

                    log.LogInformation($"restoring from cache task = {deserializedTask.Id}");

                    positions.Add(deserializedTask);
                }
            }

            await cacheService.KeyExpireAsync("redis", 6379, "tasks", 5);

            return View(positions);
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