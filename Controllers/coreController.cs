using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Leaf.Controllers
{
    [Route("api/v0.1/[controller]/")]
    [ApiController]
    public class coreController : ControllerBase
    {
        private readonly ILogger<coreController> _logger;
        private readonly IConfiguration _config;

        public coreController(ILogger<coreController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        [Route("getdatabaseversion")]
        public String GetDataBaseVersion()
        {
            return _config.GetValue<string>("Versions:DataBaseVersion");
        }

        [HttpGet]
        [Route("getapiversion")]
        public String GetApiVersion()
        {
            return _config.GetValue<string>("Versions:ApiVersion");
        }
    }
}
