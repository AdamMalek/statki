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
            var vm = _builder.BuildViewModel(id, (string)_store.Get("password"));
            return Json(vm);
        }
        
        public void Post([FromBody]string value)
        {
        }
    }
}