using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameStart.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GameStart.Controllers
{
    public class GamesController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetGames(string searchQuery)
        {
            string parsedSearch = searchQuery.Replace(" ", "+");
            var result = Game.GetGames("?fields=name&li mit=10&offset=0&order=release_dates.date%3Adesc&search=" + parsedSearch);
            return Json(result);
        }
    }
}
