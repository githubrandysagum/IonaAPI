using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IonaAPI.Infrastructure.Services.Extensions
{
    public static class StreamExtension
    {
        public static T ReadAndDeserializeFromJson<T>(this Stream stream)
        {
            if(stream == null)
            {
               throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new NotSupportedException("Can't read from a stream.");
            }

            using var streamReader = new StreamReader(stream);
            using var jsonTextReader = new JsonTextReader(streamReader);

            var jsonSerializer = new JsonSerializer();
            return jsonSerializer.Deserialize<T>(jsonTextReader);


        }
    }
}
