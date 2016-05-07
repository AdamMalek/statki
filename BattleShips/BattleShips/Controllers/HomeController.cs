using BattleShips.DAL;
using BattleShips.Infrastructure;
using BattleShips.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BattleShips.Controllers
{
    public class HomeController : Controller
    {

        private readonly GameManager _gm;
        private readonly IStoreProvider _cookie;

        public HomeController(GameManager gm, IStoreProvider cookie)
        {
            _gm = gm;
            _cookie = cookie;
        }
        public ActionResult Game(int id)
        {
            return View(id);
        }

        public ActionResult Index()
        {
            StartGameViewModel vm = new StartGameViewModel();
            return View(vm);
        }

        [HttpPost]
        public ActionResult NewGame(StartGameViewModel vm)
        {
            _gm.Width = 5;
            _gm.Height = 5;
            _gm.ShipCounter = 5;
            var game = _gm.CreateGame(vm.Player1Name, vm.Player2Name, vm.Password);
            _cookie.Set("password", vm.Password ?? "");
            if (vm.Refresh)
            {
                return RedirectToAction("Game", "BattleShip", routeValues: new { id = game.Id });
            }
            else
            {
                return RedirectToAction("Game", "Home", routeValues: new { id = game.Id });
            }
        }
        
        [HttpPost]
        public ActionResult Index(StartGameViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var game = _gm.GetGame(vm.GameId);
                if (game == null)
                {
                    ModelState.AddModelError("Password", "Złe hasło lub ID.");
                    return View(vm);
                }
                else
                {
                    var pass = vm.Password ?? "";
                    if (game.Password != pass)
                    {
                        ModelState.AddModelError("Password", "Złe hasło lub ID");
                        return View(vm);
                    }
                    else if (game.IsFinnished())
                    {
                        ModelState.AddModelError("Password", "Podana gra została już zakończona");
                        return View(vm);
                    }
                    else
                    {
                        _cookie.Set("password", vm.Password ?? "");
                        if (vm.Refresh)
                        {
                            return RedirectToAction("Game", "BattleShip", routeValues: new { id = game.Id });
                        }
                        else
                        {
                            return RedirectToAction("Game", "Home", routeValues: new { id = game.Id });
                        }
                    }
                }
            }
            else
            {
                return View(vm);
            }
        }
    }
}
