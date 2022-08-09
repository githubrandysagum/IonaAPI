using System.Runtime.Serialization;

namespace IonaAPI.Infrastructure.Services.Result
{
    [DataContract]
    public class BreedResult
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "temperament")]
        public string Temperament { get; set; }

        [DataMember(Name = "origin")]
        public string Origin { get; set; }

        [DataMember(Name = "country_code")]
        public string CountryCode { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "image")]
        public BreedImageResult Image { get; set; }
    }
}
