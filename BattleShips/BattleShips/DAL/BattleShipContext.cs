using BattleShips.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BattleShips.DAL
{
    public class BattleShipContext : DbContext
    {
        public DbSet<Game> Games{ get; set; }
    }
}