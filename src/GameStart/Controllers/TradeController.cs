using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameStart.Models;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GameStart.Controllers
{
    public class TradeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TradeRequest(string newTo, string newMessage)
        {
            Trade newTrade = new Trade();
            newTrade.ToUserName = newTo;
            newTrade.FromUserName = User.Identity.Name;
            newTrade.Message = newMessage;
            newTrade.Accepted = false;
            _db.Trades.Add(newTrade);
            _db.SaveChanges();
            return View();
        }
    }
}
