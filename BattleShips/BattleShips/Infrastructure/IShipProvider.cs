using BattleShips.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShips.Infrastructure
{
    public interface IShipProvider
    {
        List<Ship> Generate(Board b, Player player, int width, int height, int ships);
    }
}
