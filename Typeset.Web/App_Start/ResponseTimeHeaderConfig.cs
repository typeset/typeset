using System;
using System.Diagnostics;
using System.Web;

namespace Typeset.Web
{
    public class ResponseTimeHeaderConfig
    {
        private const string ContextKey = "ResponseTimeHeaderKey";

        public static void RegisterEvents(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
            context.EndRequest += new EventHandler(context_EndRequest);
        }

        static void context_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                var context = ((HttpApplication)sender).Context;
                context.Items.Add(ContextKey, Stopwatch.StartNew());
            }
            catch { }
        }

        static void context_EndRequest(object sender, EventArgs e)
        {
            try
            {
                var context = ((HttpApplication)sender).Context;
                var stopwatch = context.Items[ContextKey] as Stopwatch;
                stopwatch.Stop();
                context.Response.AddHeader("X-ResponseTime", stopwatch.ElapsedMilliseconds.ToString());
            }
            catch { }
        }
    }
}