using KollinAPI.Database;
using KollinAPI.DataValidation;
using KollinAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
    }
}
