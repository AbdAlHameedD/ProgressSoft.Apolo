using Microsoft.AspNetCore.Mvc;
using ProgressSoft.Apolo.Application.Interfaces.Services;
using ProgressSoft.Apolo.Application.Models;

namespace ProgressSoft.Apolor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;

        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] ImageModel image)
        {
            try
            {
                return Ok(_imageService.Add(image));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                return Ok(_imageService.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_imageService.GetById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
