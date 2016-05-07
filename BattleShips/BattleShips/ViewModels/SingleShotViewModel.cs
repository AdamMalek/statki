using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BattleShips.Models
{
    public class SingleShotViewModel
    {
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public Coordinate Shot { get; set; }
    }
}