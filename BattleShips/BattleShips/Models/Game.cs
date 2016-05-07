using BattleShips.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BattleShips.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Password { get; set; }
        [Required]
        public virtual Board Board { get; set; }
        public virtual ICollection<Player> Players{ get; set; }
        public virtual ICollection<Shot> Shots { get; set; }

        internal bool IsFinnished()
        {
            var p1ships = Board.Ships.Where(x => x.Player == Players.First()).Any(x => x.Alive);
            var p2ships = Board.Ships.Where(x => x.Player == Players.Last()).Any(x => x.Alive);

            return !(p1ships && p2ships);
        }
    }
}