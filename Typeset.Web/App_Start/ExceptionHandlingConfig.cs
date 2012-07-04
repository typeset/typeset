using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Typeset.Web
{
    public class ExceptionHandlingConfig
    {
        public static void RegisterEvents(HttpApplication context)
        {
            context.Error += new EventHandler(context_Error);
            TaskScheduler.UnobservedTaskException += new EventHandler<UnobservedTaskExceptionEventArgs>(TaskScheduler_UnobservedTaskException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            GlobalConfiguration.Configuration.MessageHandlers.Add(new WebApiMessageHandler());
        }

        private static void context_Error(object sender, EventArgs e)
        {
            if (sender is HttpApplication)
            {
                var context = sender as HttpApplication;
                var exception = context.Server.GetLastError().GetBaseException();
                context.Server.ClearError();
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;

            if (e.IsTerminating)
            {
            }
            else
            {
            }
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
        }

        private class WebApiMessageHandler : DelegatingHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return base.SendAsync(request, cancellationToken)
                    .ContinueWith(r =>
                    {
                        // inspect result for status code and handle accordingly
                        return r.Result;
                    });
            }
        }
    }
}