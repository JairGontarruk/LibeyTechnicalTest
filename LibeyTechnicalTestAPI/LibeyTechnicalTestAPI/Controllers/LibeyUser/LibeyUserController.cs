using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
namespace LibeyTechnicalTestAPI.Controllers.LibeyUser
{
    [ApiController]
    [Route("[controller]")]
    public class LibeyUserController : Controller
    {
        private readonly ILibeyUserAggregate _aggregate;
        public LibeyUserController(ILibeyUserAggregate aggregate)
        {
            _aggregate = aggregate;
        }
        [HttpGet("{documentNumber}")]
        //[Route("{documentNumber}")]
        public IActionResult FindResponse(string documentNumber)
        {
            var row = _aggregate.FindResponse(documentNumber);
            return Ok(row);
        }
        [HttpPost]       
        public IActionResult Create(UserUpdateorCreateCommand command)
        {
             _aggregate.Create(command);
            return Ok(true);
        }

        [HttpGet]
        public IActionResult FindAllResponse([FromQuery] string? documentNumber)
        {
            var row = _aggregate.FindAllResponse(documentNumber);
            return Ok(row);
        }

        [HttpPut("{documentNumber}")]
        public IActionResult Update(string documentNumber, UserUpdateorCreateCommand command)
        {
            bool valor = _aggregate.Update(documentNumber, command);
            return Ok(valor);
        }

        [HttpDelete]
        [Route("{documentNumber}")]
        public IActionResult Remove(string documentNumber)
        {
            bool valor = _aggregate.Remove(documentNumber);
            return Ok(valor);
        }
    }
}