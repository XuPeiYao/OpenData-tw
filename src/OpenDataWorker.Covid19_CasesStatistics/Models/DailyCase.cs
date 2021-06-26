using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using OpenDataWorker.Covid19_CasesStatistics.JsonConverters;

namespace OpenDataWorker.Covid19_CasesStatistics.Models
{
    public class DailyCase
    {
        [JsonPropertyName("個案研判日")]
        public string Date { get; set; }

        [JsonPropertyName("縣市")]
        public string County { get; set; }

        [JsonPropertyName("鄉鎮")]
        public string District { get; set; }

        [JsonPropertyName("性別")]
        public Gender Gender { get; set; }

        [JsonPropertyName("是否為境外移入")]
        [JsonConverter(typeof(YesNoJsonConverter))]
        public bool Imported { get; set; }

        [JsonPropertyName("年齡層")]
        public string AgeGroup_Raw { get; set; }

        public string AgeGroup
        {
            get
            {
                if (string.IsNullOrWhiteSpace(AgeGroup_Raw)||
                    AgeGroup_Raw.Length < 2)
                {
                    return "00-04";
                }

                if (AgeGroup_Raw == "5-9") return "05-09";

                return AgeGroup_Raw;
            }
        }

        [JsonPropertyName("確定病例數")]
        [JsonConverter(typeof(StringNumberJsonConverter))]
        public uint Count { get; set; }

    }
}
