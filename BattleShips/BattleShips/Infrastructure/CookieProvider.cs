using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BattleShips.Infrastructure
{
    public class CookieProvider : IStoreProvider
    {
        public object Get(string key)
        {
            var cookie = HttpContext.Current.Request.Cookies[key];

            if (cookie != null)
                return cookie.Value;
            else
                return null;
        }

        public void Set(string key,string value)
        {
            var cookie = HttpContext.Current.Request.Cookies[key];

            if (cookie == null)
            {
                cookie = new HttpCookie(key, value);
                cookie.Expires = DateTime.Now.AddHours(2);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            else
            {
                cookie.Value = value;
                cookie.Expires = DateTime.Now.AddMinutes(15);
                HttpContext.Current.Response.Cookies.Set(cookie);
            }
            
        }

        public void UnSet(string key)
        {
            var cookie = HttpContext.Current.Request.Cookies[key];

            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.Subtract(TimeSpan.FromDays(10));
                HttpContext.Current.Response.Cookies.Set(cookie);
            }            
        }
    }
}
