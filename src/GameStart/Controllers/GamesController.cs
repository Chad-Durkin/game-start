using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameStart.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GameStart.Controllers
{
    public class GamesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Basic User Account Info here...
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public GamesController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetGames(string searchQuery)
        {
            string parsedSearch = searchQuery.Replace(" ", "+");
            var result = Game.GetGames("?fields=name,screenshots&limit=10&offset=0&order=release_dates.date%3Adesc&search=" + parsedSearch);
            return Json(result);
        }

        public IActionResult Details(int id)
        {
            var result = Game.GetGames("/" + id + "?fields=*");
            return Json(result);
        }

        [HttpPost]
        public IActionResult ClaimGame(int id)
        {
            Game newGame = new Game { ApiId = id };
            newGame.UserName = User.Identity.Name;
            newGame.Tradeable = false;
            _db.Games.Add(newGame);
            _db.SaveChanges();
            return Json(newGame);
        }

        public IActionResult SetTradeable(int id)
        {
            Game foundGame = _db.Games.FirstOrDefault(g => g.ApiId == id);
            foundGame.Tradeable = true;
            _db.Entry(foundGame).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Management", "Account");
        }

        public IActionResult FindTrades(int id)
        {
            ViewBag.Id = id;
            return View(_db.Games.Where(g => g.ApiId == id).ToList());
        }
    }
}
