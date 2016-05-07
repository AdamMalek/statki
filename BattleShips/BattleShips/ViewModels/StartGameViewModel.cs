using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BattleShips.Models
{
    public class StartGameViewModel
    {
        public bool Refresh { get; set; } = true;
        public string Player1Name { get; set; }
        public string Player2Name { get; set; }
        [RegularExpression(@"\d*")]
        public int GameId { get; set; }
        public string Password { get; set; } = "";
    }
}