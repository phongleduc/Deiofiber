using Deiofiber.Common;
using System;

namespace Deiofiber
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            JobScheduler.Start();
        }
    }
}