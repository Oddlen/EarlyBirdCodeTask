using KollinAPI.Database;
using KollinAPI.DataValidation;
using KollinAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KollinAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PackageController(InMemoryDatabase database) : ControllerBase
    {
        private readonly InMemoryDatabase _database = database;

        [HttpGet]
        public IEnumerable<string> Package()
        {
            return _database.GetKollinIds();
        }

        [HttpGet]
        [Route("{kolliId}")]
        public IActionResult Package(string kolliId)
        {
            (var isValid, var Message) = KollinValidator.ValidateId(kolliId);
            
            if (!isValid) return BadRequest(Message);
            
            var kolli = _database.GetKolli(kolliId);

            if (kolli is null) return NotFound();

            return Ok(kolli);
        }

        [HttpPost]
        public IActionResult Package(Kollin kollin)
        {
            if (kollin is null) return BadRequest();

            (var isValid, var Message) = KollinValidator.ValidateId(kollin.Id);

            if (!isValid) return BadRequest(Message);

            if (!KollinValidator.ValidateSize(kollin)) return BadRequest("Package size is not allowed.");

            if (_database.AddKolli(kollin)) return Ok();

            return BadRequest("Package already exists.");
        }
    }
}
