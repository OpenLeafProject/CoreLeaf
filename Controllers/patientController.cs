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
    public class patientController : ControllerBase
    {
        private readonly ILogger<patientController> _logger;
        private readonly IConfiguration _config;

        /// <summary>
        ///     Class Contructor
        /// </summary>
        /// <param name="logger"> Logguer object for debug </param>
        /// <param name="config"> appsettings.json properties </param>
        public patientController(ILogger<patientController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        /// <summary>
        ///     Test method to check DB connection
        /// </summary>
        /// <returns>
        ///     Returns api version in JSON Format
        /// </returns>
        [HttpGet]
        [Route("test")]
        public System.Data.DataTable Test()
        {
            using (Leaf.Datalayers.Patient.DataLayer dl = new Leaf.Datalayers.Patient.DataLayer(_config))
            {
                return dl.Test("1");

            }
        }
         
        [HttpGet]
        [Route("{nhc}")]
        public ActionResult<object> Get(int nhc)
        {
            try
            {
                return new Patient(nhc, _config);
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
               return new Patient(values,_config).Create();
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
        public ActionResult<object> save([FromBody] Dictionary<string, string> values)
        {
            try
            {
                return new Patient(values, _config).Save();

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
