using BattleShips.Infrastructure;
using BattleShips.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BattleShips.DAL
{
    public class GameManager
    {
        private readonly IShipProvider _provider;

        public int ShipCounter { get; set; } = 5;
        public int Width { get; set; } = 10;
        public int Height { get; set; } = 10;

        public GameManager(IShipProvider provider)
        {
            _provider = provider;
        }

        public Game CreateGame(string player1, string player2, string password)
        {
            using (BattleShipContext db = new BattleShipContext())
            {
                Game g = new Game();
                g.Players = new List<Player>();
                Player p1 = CreatePlayer(player1);
                Player p2 = CreatePlayer(player2);
                g.Players.Add(p1);
                g.Players.Add(p2);
                g.Shots = new List<Shot>();
                g.Board = GenerateBoard(p1, p2);
                g.Board.Game = g;
                g.Password = password ?? "";
                db.Games.Add(g);
                db.SaveChanges();

                return g;
            }
        }

        public Game GetGame(int id)
        {
            using (var db = new BattleShipContext())
            {
                return db.Games.Include("Board").Include("Board.Ships").Include("Players").Include("Shots").Include("Shots.Player").FirstOrDefault(x => x.Id == id);
            }
        }

        public HitState Shot(int gameId, int playerId, Coordinate coord)
        {
            using (var db = new BattleShipContext())
            {
                var game = db.Games.FirstOrDefault(x => x.Id == gameId);
                if (game == null) return HitState.Error;
                if (game.Board.Width <= coord.X || game.Board.Height <= coord.Y) return HitState.Error;
                var player = game.Players.First(x => x.PlayerId == playerId);
                if (player == null || (game.Shots.Count > 0 && game.Shots.Last().Player == player && !game.Shots.Last().Hit)) return HitState.Error;
                if (game.Shots.FirstOrDefault(x => x.Player == player && x.Pos.X == coord.X && x.Pos.Y == coord.Y) != null) return HitState.Error;

                var opponent = game.Players.First(x => x.PlayerId != playerId);
                var ship = game.Board.Ships.FirstOrDefault(x => x.Player == opponent && x.Pos.X == coord.X && x.Pos.Y == coord.Y);
                if (ship != null) ship.Alive = false;
                Shot shot = new Shot
                {
                    Game = game,
                    Player = player,
                    Pos = new Coordinate
                    {
                        X = coord.X,
                        Y = coord.Y,
                    },
                    Hit = (ship != null)
                };
                game.Shots.Add(shot);
                db.SaveChanges();

                return shot.Hit ? HitState.Hit : HitState.Miss;
            }
        }


        private Board GenerateBoard(Player player1, Player player2)
        {
            Board b = new Board();
            b.Width = Width;
            b.Height = Height;
            var p1 = _provider.Generate(b, player1, Width, Height, ShipCounter);
            var p2 = _provider.Generate(b, player2, Width, Height, ShipCounter);

            p1.AddRange(p2);
            b.Ships = p1;

            return b;
        }

        private Player CreatePlayer(string playername)
        {
            Player p = new Player();
            p.PlayerName = playername;

            return p;
        }

        public enum HitState
        {
            Hit,
            Miss,
            Error
        };
    }
}