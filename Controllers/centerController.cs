﻿using System;
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
    public class centerController : ControllerBase
    {
        private readonly ILogger<patientController> _logger;
        private readonly IConfiguration _config;

        /// <summary>
        ///     Class Contructor
        /// </summary>
        /// <param name="logger"> Logguer object for debug </param>
        /// <param name="config"> appsettings.json properties </param>
        public centerController(ILogger<patientController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }


        [HttpGet]
        [Route("{id}")]
        public ActionResult<object> getCenter(string id)
        {

            try
            {
                if (Int32.TryParse(id, out int code))
                {
                    return new Center(code, _config);
                }
                else
                {
                    return new Center(id, _config);
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
        public ActionResult<object> createCenter([FromBody] Dictionary<string, string> values)
        {
            try
            {
               return new Center(values,_config).Create();
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
        public ActionResult<object> saveCenter([FromBody] Dictionary<string, string> values)
        {
            try
            {
                return new Center(values, _config).Save();

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