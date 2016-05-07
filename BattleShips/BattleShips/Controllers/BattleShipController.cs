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
    public class BattleShipController : Controller
    {
        private readonly ViewModelBuilder _builder;
        private readonly GameManager _gm;
        private readonly IStoreProvider _store;

        public BattleShipController(GameManager gm,ViewModelBuilder builder, IStoreProvider cookie)
        {
            _gm = gm;
            _builder = builder;
            _store = cookie;
        }

        // GET: BattleShip
        public ActionResult Game(int id)
        {
            var vm = _builder.BuildViewModel(id,(string)_store.Get("password"));
            if (vm == null) return RedirectToAction("Index", "Home");
            return View(vm);
        }       

        [HttpPost]
        public ActionResult Game(GameViewModel vm)
        {
            var res = _gm.Shot(vm.Shot.GameId, vm.Shot.PlayerId, vm.Shot.Shot);
            switch (res)
            {
                case GameManager.HitState.Hit:
                    {
                        
                        break;
                    }
                case GameManager.HitState.Miss:
                    {
                        break;
                    }
                case GameManager.HitState.Error:
                    {
                        var vm2 = _builder.BuildViewModel(vm.Shot.GameId, (string)_store.Get("password"), vm.Shot);
                        vm2.Status = "Couldn't shoot at given location!";
                        return View(vm2);
                    }
            }
            return RedirectToAction("Game", new { id = vm.Shot.GameId });
        }        
    }
}