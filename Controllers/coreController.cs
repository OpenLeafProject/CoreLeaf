using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Leaf.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class CoreController : ControllerBase
    {
        private readonly ILogger<CoreController> _logger;
        private readonly IConfiguration _config;

        /// <summary>
        ///     Class Contructor
        /// </summary>
        /// <param name="logger"> Logguer object for debug </param>
        /// <param name="config"> appsettings.json properties </param>
        public CoreController(ILogger<CoreController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        /// <summary>
        ///     Returns database version
        ///     No JWT validation is required
        /// </summary>
        /// <returns>
        ///     Returns database version in JSON Format
        /// </returns>
        [HttpGet]
        [Route("getdatabaseversion")]
        public String GetDataBaseVersion()
        {
           return Newtonsoft.Json.JsonConvert.SerializeObject(_config.GetValue<string>("Versions:DataBaseVersion"));
        }

        /// <summary>
        ///     Returns api version
        ///     No JWT validation is required
        /// </summary>
        /// <returns>
        ///     Returns api version in JSON Format
        /// </returns>
        [HttpGet]
        [Route("getapiversion")]
        public String GetApiVersion()
        {
            return _config.GetValue<string>("Versions:ApiVersion");
        }
    }
}
