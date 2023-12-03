using KollinAPI.Database;
using Microsoft.AspNetCore.Mvc;

namespace KollinAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PackageController : ControllerBase
    {
        private InMemoryDatabase _database;
        public PackageController(InMemoryDatabase database) 
        {
            _database = database;
        }

        [HttpGet]
        public IEnumerable<string> Package()
        {
            return _database.GetKollinIds();
        }
    }
}
