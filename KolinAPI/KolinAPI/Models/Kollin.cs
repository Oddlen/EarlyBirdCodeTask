using KollinAPI.DataValidation;

namespace KollinAPI.Models
{
    public class Kollin
    {
        public string Id { get; set; }
        public int Weight { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public bool IsValid => KollinValidator.ValidateSize(this);
    }
}
