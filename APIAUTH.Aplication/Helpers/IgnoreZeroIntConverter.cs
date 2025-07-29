using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Helpers
{
    partial class IgnoreZeroIntConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
       => reader.GetInt32();

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            // No escribir nada si el valor es 0
            if (value != 0)
                writer.WriteNumberValue(value);
        }
    }
}
