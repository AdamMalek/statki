using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BattleShips.Models
{
    public class GameViewModel
    {
        public string Status { get; set; }
        public bool end { get; set; } = false;
        public int GameId { get; set; }
        public string PlayerTurn { get; set; }
        public Player[] Players { get; set; }
        public Field[,] Board1 { get; set; }
        public Field[,] Board2 { get; set; }
        public SingleShotViewModel Shot { get; set; }

        public enum Field
        {
            Covered = 0,
            Hit = 1,
            Miss = 2
        }
    }
}