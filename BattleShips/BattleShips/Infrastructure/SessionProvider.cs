using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BattleShips.Infrastructure
{
    public class SessionProvider : IStoreProvider
    {
        public object Get(string key)
        {
            try
            {
                return HttpContext.Current.Session[key];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Set(string key, string value)
        {
            var item = HttpContext.Current.Session[key];
            if (item != null)
            {
                HttpContext.Current.Session.Remove(key);
            }
            HttpContext.Current.Session.Add(key, value);
        }

        public void UnSet(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }
    }
}