using KollinAPI.Models;

namespace KollinAPI.DataValidation
{
    public static class KollinValidator
    {
        public static (bool, string) ValidateId(string id)
        {
            bool isValid = true;
            string errorMessage = "";

            if (id == null) 
            {
                isValid = false;
                errorMessage = "KolliId needs to be set.";
            }
            else if (id.Length != 18)
            {
                isValid = false;
                errorMessage = "KolliId needs to be 18 digits long.";
            }
            else if (!id.All(char.IsDigit))
            {
                isValid = false;
                errorMessage = "KolliId needs to be only digits.";
            }
            else if (!id.StartsWith("999"))
            {
                isValid = false;
                errorMessage = "KolliId needs to start with \"999\"";
            }

            return (isValid, errorMessage);
        }

        public static bool ValidateSize(Kollin kollin) => !(kollin is null 
                || kollin.Width < 1 || kollin.Width > 60
                || kollin.Height < 1 || kollin.Height > 60
                || kollin.Length < 1 || kollin.Length > 60
                || kollin.Weight < 1 || kollin.Weight > 20000);
    }
}
