using BattleShips.DAL;
using BattleShips.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BattleShips.Infrastructure
{
    public class ViewModelBuilder
    {
        private readonly GameManager _gm;
        public ViewModelBuilder(GameManager gm)
        {
            _gm = gm;
        }

        private GameViewModel.Field[,] CreateBoard(Game game, int playerId)
        {
            GameViewModel.Field[,] field = new GameViewModel.Field[game.Board.Width, game.Board.Height];
            foreach (var shot in game.Shots.Where(x => x.Player.PlayerId == playerId))
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

        public GameViewModel BuildViewModel(int id, string password, SingleShotViewModel shot = null)
        {
            var game = _gm.GetGame(id);
            if (game == null) return null;
            if (game.Password != password) return null;

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
                PlayerTurnId = turn.PlayerId,
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