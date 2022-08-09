using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IonaAPI.Infrastructure.Services.Result
{
    [DataContract]
    public class ImagesResult
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "width")]
        public int? Width { get; set; }

        [DataMember(Name = "height")]
        public int? Height { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }
    }
}
