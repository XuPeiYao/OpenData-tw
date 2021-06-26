using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenDataWorker.Covid19_CasesStatistics.JsonConverters
{
    public class YesNoJsonConverter:JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type   typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString() == "是";
        }

        public override void Write(Utf8JsonWriter      writer, bool value,         JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
