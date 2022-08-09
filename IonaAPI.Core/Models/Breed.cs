namespace IonaAPI.Core.Models
{
    public class Breed
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Temperament { get; set; }

        public string Origin { get; set; }

        public string CountryCode { get; set; }

        public string Description { get; set; }

        public BreedImage Image { get; set; }
    }
}
