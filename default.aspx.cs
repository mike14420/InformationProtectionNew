using System;
using System.Web;
using System.Web.Mvc;

namespace InformationProtection
{
    public partial class _default : System.Web.UI.Page
    {
        public void Page_Load(object sender, System.EventArgs e)
        {
            // Change the current path so that the Routing handler can correctly interpret
            // the request, then restore the original path so that the OutputCache module
            // can correctly process the response (if caching is enabled).

            string originalPath = Request.Path;
            HttpContext.Current.RewritePath(Request.ApplicationPath, false);
            IHttpHandler httpHandler = new MvcHttpHandler();
            //httpHandler.ProcessRequest(HttpContext.Current);
            HttpContext.Current.RewritePath(originalPath, false);
            String LoginUserName = Request.LogonUserIdentity.Name;
            NameId.InnerText = LoginUserName;
        }
    }
}