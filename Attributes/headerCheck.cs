using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Diagnostics;


namespace Leaf.Attributes
{
    public class HeaderCheckJWT : ActionFilterAttribute
    {
        public string Parameter { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            try
            {
                base.OnActionExecuting(filterContext);

                string token = string.Empty;
                string app = string.Empty;

                token = filterContext.HttpContext.Request.Headers["token"];
                app = filterContext.HttpContext.Request.Headers["app"];

                string user = Hnet.Util.JWTTools.CheckToken(token, app);
                if (user == null)
                {
                    Debug.WriteLine("Unauthorized for " + user + " in " + app + " [" + filterContext.HttpContext.Request.Path + "]");
                    
                    // TODO: More especific error
                    filterContext.HttpContext.Abort();
                }
                else
                {
                    Debug.WriteLine("Access Granted for " + user + " in " + app + " [" + filterContext.HttpContext.Request.Path + "]");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Invalid token [" + filterContext.HttpContext.Request.Path + "]");
                Debug.WriteLine("  >> Reason -> " + ex.Message);
                
                // TODO: More especific error
                filterContext.HttpContext.Abort();
            }
        }

    }
}
