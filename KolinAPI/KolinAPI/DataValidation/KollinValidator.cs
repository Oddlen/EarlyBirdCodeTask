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
    }
}
