using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Hangfire;
using ToDoList.Notification;

[assembly: OwinStartup(typeof(ToDoList.Web.App_Start.Startup))]

namespace ToDoList.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/User/Login")
            });

            GlobalConfiguration.Configuration
                .UseSqlServerStorage("connectionString");

            app.UseHangfireServer();

            Manager NotfManager = new Manager();
            RecurringJob.AddOrUpdate(() => NotfManager.MakeNotification(), "* * * * *");
        }
    }
}
