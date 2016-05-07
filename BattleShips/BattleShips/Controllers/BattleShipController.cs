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
        private readonly GameManager _gm;
        private readonly IStoreProvider _cookie;

        public BattleShipController(GameManager gm, IStoreProvider cookie)
        {
            _gm = gm;
            _cookie = cookie;
        }

        // GET: BattleShip
        public ActionResult Game(int id)
        {
            var vm = BuildViewModel(id);
            if (vm == null) return RedirectToAction("Index", "Home");
            return View(vm);
        }

        private GameViewModel.Field[,] CreateBoard(Game game, int playerId)
        {
            GameViewModel.Field[,] field = new GameViewModel.Field[game.Board.Width, game.Board.Height];
            foreach (var shot in game.Shots.Where(x=> x.Player.PlayerId == playerId))
            {
                if (shot.Hit)
                {
                    field[shot.Pos.X, shot.Pos.Y] = GameViewModel.Field.Hit;
                }
                else
                {
                    field[shot.Pos.X, shot.Pos.Y] = GameViewModel.Field.Miss;
                }
            }
            return field;
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
                        var vm2 = BuildViewModel(vm.Shot.GameId, vm.Shot);
                        vm2.Status = "Couldn't shoot at given location!";
                        return View(vm2);
                    }
            }
            return RedirectToAction("Game", new { id = vm.Shot.GameId });
        }
        private GameViewModel BuildViewModel(int id, SingleShotViewModel shot=null)
        {
            var game = _gm.GetGame(id);
            if (game == null) return null;
            if (game.Password != (string)_cookie.Get("password")) return null;

            Player turn;
            if (game.Shots.Count == 0)
            {
                turn = game.Players.OrderBy(x => Guid.NewGuid()).First();
            }
            else
            {
                var lastshot = game.Shots.Last();
                if (lastshot.Hit)
                {
                    turn = game.Players.First(x => x.PlayerId == lastshot.Player.PlayerId);
                }
                else
                {
                    turn = game.Players.First(x => x.PlayerId != lastshot.Player.PlayerId);
                }
            }

            var vm = new GameViewModel
            {
                GameId = id,
                PlayerTurn = turn.PlayerName,
                Players = game.Players.ToArray(),
                Shot = new SingleShotViewModel
                {
                    GameId = game.Id,
                    PlayerId = turn.PlayerId,
                    Shot = new Coordinate()
                }
            };
            if (shot != null) vm.Shot = shot;
            if (game.IsFinnished())
            {
                vm.Status = "Game over! Winner: " + game.Shots.Last().Player.PlayerName;
                vm.end = true;
            }
            vm.Board1 = CreateBoard(game, game.Players.First().PlayerId);
            vm.Board2 = CreateBoard(game, game.Players.Last().PlayerId);
            return vm;
        }
    }
}