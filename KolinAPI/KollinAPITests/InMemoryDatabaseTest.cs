using KollinAPI.Database;
using KollinAPI.Models;

namespace KollinAPITests
{
    public class InMemoryDatabaseTest
    {
        private InMemoryDatabase _database;
        private List<Kollin> _testKolins = new List<Kollin> {
                new Kollin { Height = 1, Length = 1, Weight = 1, Width = 1, Id = "999111111111111111"},
                new Kollin { Height = 99, Length = 1, Weight = 1, Width = 1, Id = "999999999999999999"},
                new Kollin { Height = 8, Length = 1, Weight = 80000, Width = 1, Id = "999999999999999998"},
                new Kollin { Height = 2, Length = 2, Weight = 20000, Width = 2, Id = "999222222222222222"}};

        [SetUp]
        public void Setup()
        {
            _database = new InMemoryDatabase();
        }

        [Test]
        public void GetKollinIdsTest()
        {
            var ids = _database.GetKollinIds();
            Assert.That(ids.Count(), Is.EqualTo(4));
            Assert.Multiple(() =>
            {
                Assert.That(ids, Does.Contain("999111111111111111"));
                Assert.That(ids, Does.Contain("999999999999999999"));
                Assert.That(ids, Does.Contain("999999999999999998"));
                Assert.That(ids, Does.Contain("999222222222222222"));
            });
        }

        [TestCase("999111111111111111", 0)]
        [TestCase("999999999999999999", 1)]
        [TestCase("999999999999999998", 2)]
        [TestCase("999222222222222222", 3)]
        public void GetKolliTest(string id, int expectedIndex)
        {
            var kollin = _database.GetKolli(id);
            Assert.Multiple(() =>
            {
                Assert.That(kollin.IsValid, Is.EqualTo(_testKolins.ElementAt(expectedIndex).IsValid));
                Assert.That(kollin.Id, Is.EqualTo(_testKolins.ElementAt(expectedIndex).Id));
                Assert.That(kollin.Length, Is.EqualTo(_testKolins.ElementAt(expectedIndex).Length));
                Assert.That(kollin.Width, Is.EqualTo(_testKolins.ElementAt(expectedIndex).Width));
                Assert.That(kollin.Height, Is.EqualTo(_testKolins.ElementAt(expectedIndex).Height));
                Assert.That(kollin.Weight, Is.EqualTo(_testKolins.ElementAt(expectedIndex).Weight));
            });
        }

        [Test]
        public void GetKolliNullTest()
        {
            var kollin = _database.GetKolli(null);
            Assert.That(kollin, Is.Null);
        }

        [Test]
        public void GetKolliMissingTest()
        {
            var kollin = _database.GetKolli("999211111111111111");
            Assert.That(kollin, Is.Null);
        }

        [Test]
        public void AddKolliTest()
        {
            var kollin = new Kollin { Height = 1, Length = 1, Weight = 1, Width = 1, Id = "999111111171111111" };
            var numberOfKollin = _database.GetKollinIds().Count();
            var result = _database.AddKolli(kollin);
            var newNumberOfKollin = _database.GetKollinIds().Count();
            var newKollin = _database.GetKolli("999111111171111111");
            Assert.That(result, Is.True);
            Assert.That(newNumberOfKollin, Is.EqualTo(numberOfKollin + 1));
            Assert.Multiple(() =>
            {
                Assert.That(kollin.IsValid, Is.EqualTo(newKollin.IsValid));
                Assert.That(kollin.Id, Is.EqualTo(newKollin.Id));
                Assert.That(kollin.Length, Is.EqualTo(newKollin.Length));
                Assert.That(kollin.Width, Is.EqualTo(newKollin.Width));
                Assert.That(kollin.Height, Is.EqualTo(newKollin.Height));
                Assert.That(kollin.Weight, Is.EqualTo(newKollin.Weight));
            });
        }

        [Test]
        public void AddKolliExistingTest()
        {
            var kollin = new Kollin { Height = 1, Length = 1, Weight = 1, Width = 1, Id = "999111111111111111" };
            var numberOfKollin = _database.GetKollinIds().Count();
            var result = _database.AddKolli(kollin);
            var newNumberOfKollin = _database.GetKollinIds().Count();
            Assert.That(result, Is.False);
            Assert.That(newNumberOfKollin, Is.EqualTo(numberOfKollin));
        }
    }
}