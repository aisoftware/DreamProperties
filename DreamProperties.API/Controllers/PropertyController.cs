using AutoMapper;
using DreamProperties.API.Database;
using DreamProperties.API.Models;
using DreamProperties.Common;
using DreamProperties.Common.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DreamProperties.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogger<PropertyController> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public PropertyController(DatabaseContext databaseContext,
                                  ILogger<PropertyController> logger,
                                  IMapper mapper,
                                  IWebHostEnvironment environment)
        {
            _databaseContext = databaseContext;
            _logger = logger;
            _mapper = mapper;
            _environment = environment;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var properties = await _databaseContext.Properties.ToListAsync();

                properties.ForEach(x => 
                {
                    x.ImageUrl = $"{Constants.API_URL}Image/{x.ImageUrl}";
                });

                var result = _mapper.Map<List<PropertyDTO>>(properties);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Get)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet("Query")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] string city, [FromQuery] string type)
        {
            try
            {
                Expression<Func<Property, bool>> predicate = GetFilter(city, type);

                List<Property> properties = await _databaseContext
                .Properties
                .Where(predicate)
                .ToListAsync();

                var result = _mapper.Map<List<PropertyDTO>>(properties);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Get)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreatePropertyDTO propertyDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var newProperty = _mapper.Map<Property>(propertyDTO);

                _databaseContext.Add(newProperty);
                await _databaseContext.SaveChangesAsync();

                var result = _mapper.Map<PropertyDTO>(newProperty);

                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Post)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        private Expression<Func<Property, bool>> GetFilter(string city, string type)
        {
            if (!string.IsNullOrEmpty(city))
            {
                return x => x.City.ToUpper().Contains(city.ToUpper());
            }

            if (type == "Commercial")
            {
                return x => x.PropertyType == PropertyType.Commercial.ToString();
            }

            bool forSale = type == "Buy";
            return x => x.ForSale == forSale;
        }
    }
}
