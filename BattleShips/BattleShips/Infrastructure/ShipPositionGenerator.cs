using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleShips.Models;

namespace BattleShips.Infrastructure
{
    class ShipPositionGenerator : IShipProvider
    {
        public List<Ship> Generate(Board b, Player player, int width, int height, int ships)
        {
            List<Ship> shipList;
            do
            {
              shipList = Enumerable.Range(0, ships * 10)
                                 .Select(c => new Coordinate
                                 {
                                     X = Enumerable.Range(0, width).OrderBy(x => Guid.NewGuid()).First(),
                                     Y = Enumerable.Range(0, width).OrderBy(y => Guid.NewGuid()).First()
                                 })
                                 .ToList()
                                 .Distinct(new PositionComparer())
                                 .Take(ships)
                                 .Select(x => new Ship
                                 {
                                     Pos = x,
                                     Board = b,
                                     Player = player
                                 })
                                 .ToList();
            } while (shipList.Count != ships);
            return shipList;
        }
    }

    class PositionComparer : IEqualityComparer<Coordinate>
    {
        public bool Equals(Coordinate x, Coordinate y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;
            return (x.X == y.X) && (x.Y == y.Y);
        }

        public int GetHashCode(Coordinate obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;

            int hashProductName = obj.X.GetHashCode();

            int hashProductCode = obj.Y.GetHashCode();

            return hashProductName ^ hashProductCode;
        }
    }
}
