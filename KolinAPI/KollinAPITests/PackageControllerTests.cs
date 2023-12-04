using KollinAPI.Controllers;
using KollinAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KollinAPITests
{
    public class PackageControllerTests
    {
        private PackageController _controller;
        private List<Kollin> _testKolins = new List<Kollin> {
                new Kollin { Height = 1, Length = 1, Weight = 1, Width = 1, Id = "999111111111111111"},
                new Kollin { Height = 99, Length = 1, Weight = 1, Width = 1, Id = "999999999999999999"},
                new Kollin { Height = 8, Length = 1, Weight = 80000, Width = 1, Id = "999999999999999998"},
                new Kollin { Height = 2, Length = 2, Weight = 20000, Width = 2, Id = "999222222222222222"}};

        [SetUp]
        public void Setup()
        {
            _controller = new PackageController(new KollinAPI.Database.InMemoryDatabase());
        }
        
        [Test]
        public void GetAllKolliIdsTest()
        {
            var ids = _controller.Package();
            Assert.That(ids.Count(), Is.EqualTo(4));
            Assert.Multiple(() =>
            {
                Assert.That(ids, Does.Contain("999111111111111111"));
                Assert.That(ids, Does.Contain("999999999999999999"));
                Assert.That(ids, Does.Contain("999999999999999998"));
                Assert.That(ids, Does.Contain("999222222222222222"));
            });
        }
        
        [TestCase(null, "KolliId needs to be set.")]
        [TestCase("89999999999999999", "KolliId needs to be 18 digits long.")]
        [TestCase("89999999999999999a", "KolliId needs to be only digits.")]
        [TestCase("899999999999999999", "KolliId needs to start with \"999\"")]
        public void GetKolliInvalidIdTest(string input, string expected)
        {
            var result = _controller.Package(input);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            Assert.That(((BadRequestObjectResult) result).Value, Is.EqualTo(expected));
        }
        
        [TestCase("999111111111111111", 0)]
        [TestCase("999999999999999999", 1)]
        [TestCase("999999999999999998", 2)]
        [TestCase("999222222222222222", 3)]
        public void GetKolliTest(string input, int expectedIndex)
        {
            var result = _controller.Package(input);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var kollin = (Kollin)((OkObjectResult)result).Value;
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
        public void PostPackageNullTest()
        {
            var result = _controller.Package((Kollin)null);
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }
        
        [TestCase("89999999999999999", "KolliId needs to be 18 digits long.")]
        [TestCase("89999999999999999a", "KolliId needs to be only digits.")]
        [TestCase("899999999999999999", "KolliId needs to start with \"999\"")]
        public void PostPackageInvalidIdTest(string input, string expected)
        {
            var result = _controller.Package(new Kollin { Height = 1, Length = 1, Weight = 1, Width = 1, Id = input});
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo(expected));
        }
        
        [TestCase(61, 1, 1, 1)]
        [TestCase(1, 61, 1, 1)]
        [TestCase(1, 1, 20001, 1)]
        [TestCase(1, 1, 1, 61)]
        [TestCase(-1, 1, 1, 1)]
        [TestCase(1, -1, 1, 1)]
        [TestCase(1, 1, -1, 1)]
        [TestCase(1, 1, 1, -1)]
        public void PostPackageInvalidPackageSizeTest(int height, int length, int weight, int width)
        {
            var result = _controller.Package(new Kollin { Height = height, Length = length, Weight = weight, Width = width, Id = "999999999999999100" });
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("Package size is not allowed."));
        }
        
        [Test]
        public void PostPackageTest()
        {
            var result = _controller.Package(new Kollin { Height = 1, Length = 1, Weight = 1, Width = 1, Id = "999999999999999100" });
            Assert.That(result, Is.InstanceOf<OkResult>());
        }
        
        [Test]
        public void PostExistingPackageTest()
        {
            var result = _controller.Package(new Kollin { Height = 1, Length = 1, Weight = 1, Width = 1, Id = "999111111111111111" });
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("Package already exists."));
        }
    }
}