using System.Text.Json.Serialization;
using OpenDataWorker.Covid19_CasesStatistics.JsonConverters;

namespace OpenDataWorker.Covid19_CasesStatistics.Models
{
    [JsonConverter(typeof(GenderJsonConverter))]
    public enum Gender
    {
        Male,
        Female
    }
}
