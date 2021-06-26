using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenDataWorker.Covid19_CasesStatistics.JsonConverters
{
    public class StringNumberJsonConverter : JsonConverter<uint>
    {
        public override uint Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return uint.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter    writer, uint value,         JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
