using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamProperties.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProperties()
        {
            var properties = new List<string>
            {
                "property 1",
                "property 2"
            };

            return Ok(properties);
        }
    }
}
