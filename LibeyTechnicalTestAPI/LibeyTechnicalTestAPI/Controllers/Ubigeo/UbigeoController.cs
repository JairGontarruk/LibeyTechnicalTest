using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibeyTechnicalTestAPI.Controllers.Ubigeo
{
    [ApiController]
    [Route("[controller]")]
    public class UbigeoController : Controller
    {
        private readonly IUbigeoAggregate _aggregate;

        public UbigeoController(IUbigeoAggregate aggregate)
        {
            _aggregate = aggregate;
        }

        [HttpGet("Region")]
        public IActionResult FindAllRegionResponse()
        {
            var row = _aggregate.FindAllRegionResponse();
            return Ok(row);
        }

        [HttpGet("Province/{regionCode}")] // La URL sería /Province/02 (ejemplo)
        public IActionResult FindAllProvinceResponse(string regionCode)
        {
            var row = _aggregate.FindAllProvinceResponse(regionCode);
            return Ok(row);
        }

        [HttpGet("Ubigeo/{regionCode}/{provinceCode}")] // La URL sería /Ubigeo/01/0101 (ejemplo)
        public IActionResult FindAllUbigeoResponse(string regionCode, string provinceCode)
        {
            var row = _aggregate.FindAllUbigeoResponse(regionCode, provinceCode);
            return Ok(row);
        }

    }
}
