using lesson1_Simple_Functions___Controller.DTOs.FiltersDtos;
using lesson1_Simple_Functions___Controller.Services.ColorController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lesson1_Simple_Functions___Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService colorService;

        public ColorController(IColorService service)
        {
            colorService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetFilterDto>>> GetColors()
        {
            var result = await colorService.GetColors();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddColor(AddFilterDto newColor)
        {
            await colorService.AddColor(newColor);
            return Ok();
        }
    }
}
