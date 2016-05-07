using BattleShips.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShips.Models
{
    public class Board
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Id { get; set; }
        public virtual ICollection<Ship> Ships { get; set; }
        public virtual Game Game { get; set; }
    }
}
