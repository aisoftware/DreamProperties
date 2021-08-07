using DreamProperties.API.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DreamProperties.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly DatabaseContext _databaseContext;

        public ImageController(ILogger<ImageController> logger,
                               IWebHostEnvironment environment,
                               DatabaseContext databaseContext)
        {
            _logger = logger;
            _environment = environment;
            _databaseContext = databaseContext;
        }

        [HttpGet("{image}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get([FromRoute] string image)
        {
            try
            {
                var imageFile = System.IO.File.OpenRead(Path.Combine(_environment.WebRootPath, image));
                return File(imageFile, image.EndsWith("png") ? "image/png" : "image/jpeg");
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
        public async Task<IActionResult> Post([FromQuery] int property, IFormFile formFile)
        {
            try
            {
                var existingProperty = await _databaseContext.Properties.FirstOrDefaultAsync(x => x.Id == property);

                if (existingProperty == null) { return BadRequest(); }

                string fileName = UploadImage(formFile);

                existingProperty.ImageUrl = fileName;
                _databaseContext.Update(existingProperty);
                await _databaseContext.SaveChangesAsync();

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Post)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        private string UploadImage(IFormFile image)
        {
            string filename = string.Empty;

            if (image == null)
            {
                return filename;
            }

            filename = $"{Guid.NewGuid()}_{image.FileName}";
            var filePath = Path.Combine(_environment.WebRootPath, filename);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return filename;
        }
    }
}
