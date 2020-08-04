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
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        private readonly IConfiguration _config;

        /// <summary>
        ///     Class Contructor
        /// </summary>
        /// <param name="logger"> Logguer object for debug </param>
        /// <param name="config"> appsettings.json properties </param>
        public PatientController(ILogger<PatientController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
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
        public ActionResult<object> Save([FromBody] Dictionary<string, string> values)
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

        [HttpGet]
        [Route("search/{neddle}")]
        public ActionResult<object> Search(string neddle)
        {
            try
            {
                using (Leaf.Datalayers.Patient.DataLayer dl = new Leaf.Datalayers.Patient.DataLayer(_config))
                {
                    return dl.Search(neddle);
                }
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(new Dictionary<string, string>() {
                                        { "error" , ex.Message },
                                    }
                                   );
            }
        }

        [HttpGet]
        [Route("{nhc}/notes")]
        public ActionResult<object> get(int nhc)
        {

            try
            {
                using (Leaf.Datalayers.Note.DataLayer dl = new Leaf.Datalayers.Note.DataLayer(_config))
                {
                    return dl.GetNotesByNHC(nhc, _config);
                }
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(new Dictionary<string, string>() {
                                        { "error" , ex.Message },
                                    }
                                   );
            }
        }

        [HttpPost]
        [Route("{nhc}/notes/create")]
        public ActionResult<object> CreateNote(int nhc, [FromBody] Dictionary<string, string> values)
        {

            try
            {

                string content = values["content"];

                Models.Note newNote = new Note(_config);
                newNote.Patient = new Models.Patient(nhc, _config);
                newNote.Content = content;
                newNote.CreationDate = DateTime.Now;


                string token = Request.Headers["token"];
                string user = Tools.JWTTools.CheckToken(token);

                newNote.Owner = new Models.User(user, _config);

                return newNote.Create();

            }
            catch (NullReferenceException ex)
            {
                return BadRequest(new Dictionary<string, string>() {
                                        { "error" , ex.Message },
                                    }
                                   );
            }
        }
    }
}
