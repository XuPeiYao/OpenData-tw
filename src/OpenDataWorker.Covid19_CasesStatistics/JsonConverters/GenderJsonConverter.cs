using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using OpenDataWorker.Covid19_CasesStatistics.Models;

namespace OpenDataWorker.Covid19_CasesStatistics.JsonConverters
{
    public class GenderJsonConverter:JsonConverter<Gender>
    {
        public override Gender Read(ref Utf8JsonReader reader, Type   typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.GetString())
            {
                case "男":
                    return Gender.Male;
                case "女":
                    return Gender.Female;
                default:
                    throw new NotSupportedException($"Other genders are not currently supported.");
            }
        }

        public override void Write(Utf8JsonWriter      writer, Gender value,         JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
