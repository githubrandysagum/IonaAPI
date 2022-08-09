namespace IonaAPI.Dtos
{
    public class BreedDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Temperament { get; set; }

        public string Origin { get; set; }

        public string CountryCode { get; set; }

        public string Description { get; set; }

        public BreedImageDto Image { get; set; }
    }
}
