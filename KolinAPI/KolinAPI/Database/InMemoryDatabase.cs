using KollinAPI.DataValidation;
using KollinAPI.Models;

namespace KollinAPI.Database
{
    public class InMemoryDatabase
    {
        private List<Kollin> Kollins;

        public InMemoryDatabase() 
        {
            // Seed database for testing
            Kollins = new List<Kollin> { 
                new Kollin { Height = 1, Length = 1, Weight = 1, Width = 1, Id = "999111111111111111"},
                new Kollin { Height = 99, Length = 1, Weight = 1, Width = 1, Id = "999999999999999999"},
                new Kollin { Height = 8, Length = 1, Weight = 80000, Width = 1, Id = "999999999999999998"},
                new Kollin { Height = 2, Length = 2, Weight = 20000, Width = 2, Id = "999222222222222222"}};
        }
        public IEnumerable<string> GetKollinIds()
        {
            return Kollins.Select(x => x.Id);
        }

        public Kollin? GetKolli(string Id)
        {
            return Kollins.SingleOrDefault(x => x.Id == Id);
        }

        public bool AddKolli(Kollin kollin)
        {
            if (kollin is null || !KollinValidator.ValidateId(kollin.Id).Item1 || Kollins.Any(x => x.Id == kollin.Id)) return false;
            Kollins.Add(kollin);
            return true;
        }
    }
}
