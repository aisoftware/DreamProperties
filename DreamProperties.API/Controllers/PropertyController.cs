using AutoMapper;
using DreamProperties.API.Database;
using DreamProperties.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DreamProperties.API.Models;
using System.Linq.Expressions;

namespace DreamProperties.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogger<PropertyController> _logger;
        private readonly IMapper _mapper;

        public PropertyController(DatabaseContext databaseContext,
                                  ILogger<PropertyController> logger,
                                  IMapper mapper)
        {
            _databaseContext = databaseContext;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var properties = await _databaseContext.Properties.ToListAsync();

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

        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var property = await _databaseContext.Properties.FirstOrDefaultAsync(x => x.Id == id);

                if (property == null)
                {
                    return BadRequest();
                }

                var result = _mapper.Map<PropertyDTO>(property);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Get)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}
