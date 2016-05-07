using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BattleShips.Models;

namespace BattleShips.Infrastructure
{
    public class NumberedOrderShipProvider : IShipProvider
    {
        public List<Ship> Generate(Board b, Player player, int width, int height, int shipsCounter)
        {
            var ships = Enumerable.Range(0, (height * width) - 1)
                               .OrderBy(x => Guid.NewGuid())
                               .Take(shipsCounter)
                               .Select(x=> new Ship
                               {
                                   Player = player,
                                   Board = b,
                                   Alive = true,
                                   Pos = new Coordinate
                                   {
                                       X = 3,
                                       Y = 3
                                   }
                               }).ToList();

            return ships;
        }
    }
}