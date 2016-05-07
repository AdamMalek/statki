using BattleShips.DAL;
using BattleShips.Infrastructure;
using BattleShips.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace BattleShips.Controllers
{
    public class GameController : ApiController
    {
        private readonly ViewModelBuilder _builder;
        private readonly GameManager _gm;
        private readonly IStoreProvider _store;

        public GameController(GameManager gm, ViewModelBuilder builder, IStoreProvider cookie)
        {
            _gm = gm;
            _builder = builder;
            _store = cookie;
        }

        public JsonResult<GameViewModel> Get(int id)
        {
            var pass = (string)_store.Get("password");
            var vm = _builder.BuildViewModel(id, pass);
            if (vm.end) _store.UnSet("password");
            var ret = Json(vm);
            return ret;
        }

        public JsonResult<GameViewModel> Post(SingleShotViewModel vm)
        {
            var res = _gm.Shot(vm.GameId, vm.PlayerId, vm.Shot);
            var vm2 = _builder.BuildViewModel(vm.GameId, (string)_store.Get("password"),vm);
            if (res == GameManager.HitState.Error)
            {
                vm2.Status = "Couldn't shoot at given location!";
            }
            if (vm2.end) _store.UnSet("password");
            var ret = Json(vm2);
            return ret;
        }
    }
}