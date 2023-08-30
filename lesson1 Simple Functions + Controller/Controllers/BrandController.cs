using lesson1_Simple_Functions___Controller.DTOs.FiltersDtos;
using lesson1_Simple_Functions___Controller.Services.BrandService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Reflection;

namespace lesson1_Simple_Functions___Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService brandService;

        public BrandController(IBrandService service)
        {
            brandService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetFilterDto>>> GetBrands()
        {
            var result = await brandService.GetBrands();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddBrand(AddFilterDto newBrand)
        {
            await brandService.AddBrand(newBrand);
            return Ok();
        }
    }
}
