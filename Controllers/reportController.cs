using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
using Microsoft.Extensions.Configuration;
using Leaf.Models;
using System.Collections.Generic;

namespace Leaf.Controllers
{
    [Route("api/v0.1/[controller]")]
    [ApiController]
    // Enables JWT Auth Security for all methods in controller
    [Leaf.Attributes.HeaderCheckJWT]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IConfiguration _config;

        /// <summary>
        ///     Class Contructor
        /// </summary>
        /// <param name="logger"> Logguer object for debug </param>
        /// <param name="config"> appsettings.json properties </param>
        public ReportController(ILogger<ReportController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }


        [HttpGet]
        [Route("{id}")]
        public ActionResult<object> get(string id)
        {

            try
            {
                if (Int32.TryParse(id, out int code))
                {
                    return new Report(code, _config);
                }
                else
                {
                    return new Report(id, _config);
                }
            } catch(NullReferenceException ex)
            {
                return BadRequest(new Dictionary<string, string>() {
                                        { "error" , ex.Message },
                                    }
                                   );
            }
        }


        [HttpPost]
        [Route("create")]
        public ActionResult<object> Create([FromBody] Dictionary<string, string> values)
        {
            try
            {
               return new Report(values,_config).Create();
            }
            catch (Exception ex)
            {
                return BadRequest(new Dictionary<string, string>() {
                                        { "error" , ex.Message },
                                    }
                                   );
            }
        }

        [HttpPost]
        [Route("save")]
        public ActionResult<object> Save([FromBody] Dictionary<string, string> values)
        {
            try
            {
                return new Report(values, _config).Save();

            }
            catch (Exception ex)
            {
                return BadRequest(new Dictionary<string, string>() {
                                        { "error" , ex.Message },
                                    }
                                   );
            }
        }
    }
}
