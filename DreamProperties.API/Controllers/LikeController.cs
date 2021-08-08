using DreamProperties.API.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamProperties.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILogger<LikeController> _logger;
        private readonly DatabaseContext _databaseContext;

        public LikeController(ILogger<LikeController> logger, DatabaseContext databaseContext)
        {
            _logger = logger;
            _databaseContext = databaseContext;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id)
        {
            try
            {
                var existingProperty = await _databaseContext.Properties.FirstOrDefaultAsync(x => x.Id == id);

                if (existingProperty == null) { return BadRequest(); }

                existingProperty.NumberOfLikes++;
                _databaseContext.Update(existingProperty);
                await _databaseContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Put)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

    }
}
