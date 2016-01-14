using System;
using System.Web;

namespace ContC.crosscutting.Authentication
{
    public static class UserIdentity
    {
        public static string Value
        {
            get
            {
                HttpRequest request = HttpContext.Current.Request;
                HttpCookie cookie = request.Cookies["uid"];
                if (cookie != null && !(string.IsNullOrEmpty(cookie.Value)))
                {
                    return cookie.Value;
                }

                return "";
            }
            set
            {
                HttpResponse response = HttpContext.Current.Response;
                HttpCookie cookie = new HttpCookie("uid", value);
                cookie.Expires = DateTime.Now.AddDays(1);
                response.Cookies.Add(cookie);
            }
        }

        public static string RedisValue
        {
            get
            {
                HttpRequest request = HttpContext.Current.Request;
                HttpCookie cookie = request.Cookies["uidrds"];
                if (cookie != null && !(string.IsNullOrEmpty(cookie.Value)))
                {
                    return cookie.Value;
                }

                return "";
            }
        }

        public static void SetRedisValue()
        {
            HttpResponse response = HttpContext.Current.Response;
            HttpCookie cookie = new HttpCookie("uidrds", Guid.NewGuid().ToString());
            response.Cookies.Add(cookie);

        }

    }
}
