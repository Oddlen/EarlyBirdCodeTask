using KollinAPI.DataValidation;
using KollinAPI.Models;

namespace KollinAPITests
{
    public class KollinValidatorTests
    {
        [TestCase(null, "KolliId needs to be set.", false)]
        [TestCase("89999999999999999", "KolliId needs to be 18 digits long.", false)]
        [TestCase("89999999999999999a", "KolliId needs to be only digits.", false)]
        [TestCase("899999999999999999", "KolliId needs to start with \"999\"", false)]
        [TestCase("999999999999999999", "", true)]
        public void ValidateIdTest(string input, string expectedMessage, bool expectedValidResult)
        {
            (var isValid, var message) = KollinValidator.ValidateId(input);
            Assert.Multiple(() =>
            {
                Assert.That(isValid, Is.EqualTo(expectedValidResult));
                Assert.That(message, Is.EqualTo(expectedMessage));
            });
        }

        [TestCase(61, 1, 1, 1, false)]
        [TestCase(1, 61, 1, 1, false)]
        [TestCase(1, 1, 20001, 1, false)]
        [TestCase(1, 1, 1, 61, false)]
        [TestCase(-1, 1, 1, 1, false)]
        [TestCase(1, -1, 1, 1, false)]
        [TestCase(1, 1, -1, 1, false)]
        [TestCase(1, 1, 1, -1, false)]
        [TestCase(1, 1, 20000, 1, true)]
        [TestCase(60, 60, 20000, 60, true)]
        [TestCase(1, 1, 1, 1, true)]
        public void ValidateSizeTest(int height, int length, int weight, int width, bool expectedResult)
        {
            var result = KollinValidator.ValidateSize( new Kollin { Height = height, Length = length, Weight = weight, Width = width, Id = "999999999999999100" });
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}