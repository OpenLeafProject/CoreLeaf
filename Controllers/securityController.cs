using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Leaf.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ILogger<SecurityController> _logger;
        private readonly IConfiguration _config;

        /// <summary>
        ///     Class Contructor
        /// </summary>
        /// <param name="logger"> Logguer object for debug </param>
        /// <param name="config"> appsettings.json properties </param>
        public SecurityController(ILogger<SecurityController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        /// <summary>
        ///     Returns a JWT token if user and password are correct
        ///     No param need, values [user, password] are obtained from Headers
        /// </summary>
        /// <returns>
        ///    Returns string with JWT token
        /// </returns>
        [HttpGet]
        [Route("Login")]
        public ActionResult<Dictionary<string, string>> Login()
        {
            String user = String.Empty;
            String password = String.Empty;

            try
            {
                // Get headers values
                user = Request.Headers["user"];
                password = Request.Headers["password"];

                // If values are null, reurns 400 Error
                if (user == null || password == null)
                {
                    Debug.WriteLine($"Error getting header values");
                    return BadRequest(new Dictionary<string, string>() {
                                    { "error" , "Params not valid" },
                                }
                            );
                }

            }
            // If exception is throwed, reurns 400 Error
            catch (Exception exHeader)
            {
                Debug.WriteLine($"Error getting header values >> {exHeader.Message}");
                return BadRequest(new Dictionary<string, string>() {
                                    { "error" , "Params not valid" },
                                }
                        );
            }

            // Connec to with database
            using (Leaf.Datalayers.Security.DataLayer dl = new Leaf.Datalayers.Security.DataLayer(_config))
            {
                // Checks username and password and generate a token
                string token = dl.Login(user, password);

                // If there is no token returns 401 unauthorized
                if (token == null || token.Contains("Error"))
                {
                    return Unauthorized(new Dictionary<string, string>() {
                                    { "error" , "User or password is incorrect" },
                                }
                            );

                }
                else
                {
                    /* If username and password are correct, returns:
                     * json with user, token and profile
                     */
                    string profile = dl.GetProfileFromToken(token);

                    return Ok(new Dictionary<string, string>() {
                                    { "user" , user },
                                    { "token", token },
                                    { "rol" , profile }
                                }
                            );
                }
            }

        }

        /// <summary>
        ///     Checks if a toke is valid. [token] is obtained from Headers
        /// </summary>
        /// <returns>
        ///    Returns:
        ///     - username if toke is correct
        ///     - null if is not a valid token
        /// </returns>
        [HttpGet]
        [Route("CheckToken")]
        public ActionResult<Dictionary<string, string>> ChekToken()
        {
            String token = String.Empty;

            // Get Headers values
            try
            {
                token = Request.Headers["token"];

                // If values are null, reurns 400 Error
                if (token == null)
                {
                    Debug.WriteLine($"Error getting header values");
                    return BadRequest(new Dictionary<string, string>() {
                                    { "error" , "Params not valid" },
                                }
                            );
                }

            }
            // If exception is throwed, reurns 400 Error
            catch (Exception exHeader)
            {
                Debug.WriteLine($"Error getting header values >> {exHeader.Message}");
                return BadRequest(new Dictionary<string, string>() {
                                    { "error" , "Params not valid" },
                                }
                        );
            }

            // Checks token
            string username = Leaf.Tools.JWTTools.CheckToken(token);

            /* If token es correct, returns username
             * if not, returns 401 Unauthorized
             */
            if (username != null)
            {
                return Ok(username);
            } else
            {
                return Unauthorized(new Dictionary<string, string>() {
                                    { "error" , "Token is not valid" },
                                }
                        );
            }

        }
    }
}
