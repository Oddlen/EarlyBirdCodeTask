using KollinAPI.Database;
using KollinAPI.DataValidation;
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
    }
}
