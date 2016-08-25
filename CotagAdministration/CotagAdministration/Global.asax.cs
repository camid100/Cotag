using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace CotagAdministration
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {
            //if (HttpContext.Current.Response.Status.StartsWith("401"))
            //{
            //    HttpContext.Current.Response.ClearContent();
            //    Response.Redirect("~/AccessDenied.aspx");
            //}

            HttpContext context = HttpContext.Current;
            if (context.Response.Status.Substring(0, 3).Equals("401"))
            {
                context.Response.ClearContent();
                context.Response.Write(@"<script language=''javascript''>" + "self.location='AccessDenied.aspx';</script>");
            } 
        }

        void Application_Error(object sender, EventArgs e)
        {
            DataLayer.ExceptionHandler.write(Server.GetLastError());
            Response.Redirect("~/ErrorPage.aspx");
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
