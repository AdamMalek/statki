using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Infrastructure
{
    public interface IStoreProvider
    {
        object Get(string key);
        void Set(string key,string value);
        void UnSet(string key);
    }
}
