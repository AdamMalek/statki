using BattleShips.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShips.Models
{
    public class Ship
    {
        public int ShipId { get; set; }
        public bool Alive { get; set; } = true;
        public Coordinate Pos { get; set; }
        public virtual Board Board { get; set; }
        public virtual Player Player { get; set; }
    }
}
