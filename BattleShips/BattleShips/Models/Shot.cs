using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BattleShips.Models
{
    public class Shot
    {
        public int Id { get; set; }
        public Coordinate Pos { get; set; }
        public bool Hit { get; set; }
        public virtual Player Player { get; set; }
        public virtual Game Game { get; set; }
    }
}