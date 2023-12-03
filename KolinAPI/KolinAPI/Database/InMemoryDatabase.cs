using KollinAPI.Models;

namespace KollinAPI.Database
{
    public class InMemoryDatabase
    {
        private List<Kollin> Kollins;

        public InMemoryDatabase() 
        {
            Kollins = new List<Kollin> { 
                new Kollin { Height = 1, Length = 1, Weight = 1, Width = 1, Id = "999111111111111111"},
                new Kollin { Height = 99, Length = 1, Weight = 1, Width = 1, Id = "999999999999999999"},
                new Kollin { Height = 99, Length = 1, Weight = 1, Width = 1, Id = "999999999999999999"},
                new Kollin { Height = 2, Length = 2, Weight = 2, Width = 2, Id = "999222222222222222"}};
        }
        public IEnumerable<string> GetKollinIds()
        {
            return Kollins.Select(x => x.Id);
        }
    }
}
