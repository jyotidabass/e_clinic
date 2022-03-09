﻿using System;
using System.Configuration;
using System.Web;

namespace AyurvedOnCall.Helpers
{
    public static class CookieHelper
    {
        private const string CookieDurationSettingName = "CookieHelper:Duration";
        private const string CookieIshttpSettingName = "CookieHelper:IsHttp";

        static CookieHelper()
        {
            Duration = GetDefaultDurationValue();
            IsHttp = GetDefaultIsHttpValue();
        }

        private static HttpContext Context => HttpContext.Current;

        private static int Duration { get; }

        private static bool IsHttp { get; }

        public static void Set(string key, string value)
        {
            var c = new HttpCookie(key)
            {
                Value = value,
                Expires = DateTime.UtcNow.AddDays(Duration),
                HttpOnly = IsHttp
            };

            Context.Response.Cookies.Add(c);
        }

        public static void Set(string key, string value, bool isHttp, int expireDays)
        {
            var c = new HttpCookie(key)
            {
                Value = value,
                Expires = DateTime.UtcNow.AddDays(expireDays),
                HttpOnly = isHttp
            };

            Context.Response.Cookies.Add(c);
        }

        public static void Set(string key, string value, bool isHttp)
        {
            var c = new HttpCookie(key)
            {
                Value = value,
                Expires = DateTime.UtcNow.AddDays(Duration),
                HttpOnly = isHttp
            };

            Context.Response.Cookies.Add(c);
        }

        public static void Set(string key, string value, int expireDays)
        {
            var c = new HttpCookie(key)
            {
                Value = value,
                Expires = DateTime.UtcNow.AddDays(expireDays),
                HttpOnly = IsHttp
            };

            Context.Response.Cookies.Add(c);
        }

        public static string Get(string key)
        {
            var value = string.Empty;

            var c = Context.Request.Cookies[key];

            return c != null
                   ? Context.Server.HtmlEncode(c.Value)?.Trim()
                   : value;
        }

        public static bool Exists(string key)
        {
            return Context.Request.Cookies[key] != null;
        }

        public static void Delete(string key)
        {
            if (!Exists(key))
            {
                return;
            }

            var c = new HttpCookie(key)
            {
                Expires = DateTime.UtcNow.AddDays(-10),
                Value = null
            };

            Context.Response.Cookies.Add(c);
        }
         
        public static void DeleteAll(bool deleteServerCookies = false)
        {
            for (var i = 0; i <= Context.Request.Cookies.Count - 1; i++)
            {
                if (Context.Request.Cookies[i] != null)
                {
                    Delete(Context.Request.Cookies[i].Name);
                }
            }

            if (deleteServerCookies)
            {
                Context.Request.Cookies.Clear();
            }
        }

        private static int GetDefaultDurationValue()
        {
            //default
            var duration = 30;

            var setting = ConfigurationManager.AppSettings[CookieDurationSettingName];
            if (!string.IsNullOrEmpty(setting))
            {
                int.TryParse(setting, out duration);
            }

            return duration;
        }

        private static bool GetDefaultIsHttpValue()
        {
            //default
            var isHttp = true;

            var setting = ConfigurationManager.AppSettings[CookieIshttpSettingName];
            if (!string.IsNullOrEmpty(setting))
            {
                bool.TryParse(setting, out isHttp);
            }

            return isHttp;
        }
    }
}