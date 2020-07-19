using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System;

namespace Leaf.Attributes
{
    public class HeaderCheckJWT : ActionFilterAttribute
    {
        /// <summary>
        ///     Checks request token to authorize
        /// </summary>
        /// <param name="filterContext"> ActionExecutingContext with the Request </param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            try
            {
                base.OnActionExecuting(filterContext);

                // Init variables
                string token = string.Empty;

                // Get token variables from Headers
                token = filterContext.HttpContext.Request.Headers["token"];

                /* Checks token validity
                 *  - checks token expiration date
                 *  - checks token secretkey
                 *  - checks token user
                 */
                string user = Leaf.Tools.JWTTools.CheckToken(token);

                /* if Hnet.Util.JWTTools.CheckToken returns null
                 * something was wrong and token is invalid
                 */
                if (user == null)
                {
                    /* Logs 401 unauthorized request
                     * - Token is not present
                     */
                    Debug.WriteLine($"Unauthorized for {user} [{filterContext.HttpContext.Request.Path}]");
                    // Returns 401 status and JSON Message for unauthorized request
                    filterContext.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                }
                else
                {
                    // Logs authorized request
                    Debug.WriteLine($"Access Granted for {user} [{filterContext.HttpContext.Request.Path}]");
                }
            }
            catch (Exception ex)
            {
                /* Logs 401 unauthorized request
                 *  - Token is not present
                 *  - 500 error ehen checking token (token is malformed or is not really a token)
                 */
                Debug.WriteLine($"Invalid token [{filterContext.HttpContext.Request.Path}]");
                Debug.WriteLine($"  >> Reason -> {ex.Message}");

                // Returns 401 status and JSON Message for unauthorized request
                filterContext.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
            }
        }
    }
}
