using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OpenDataWorker.Covid19_CasesStatistics.Models;

namespace OpenDataWorker.Covid19_CasesStatistics
{
    public class Covid19CasesService
    {
        public const string WELL_KNOWN_OPENDATA_URL =
            @"https://od.cdc.gov.tw/eic/Day_Confirmation_Age_County_Gender_19CoV.json";

        private readonly HttpClient _http;

        public Covid19CasesService()
        {
            _http = new HttpClient();
        }

        public async Task<IEnumerable<DailyCase>> GetAllDailyCases()
        {
            var response = await _http.GetAsync(WELL_KNOWN_OPENDATA_URL);

            Program.OutputLog("Http Get daily cases from OpenData API");
            Program.OutputLog("Daily case file ETag: " + response.Headers.ETag);
            Program.OutputLog("Daily cases file lastModfiyTime: " +
                              response.Content.Headers.LastModified);

            var jsonText = await response.Content.ReadAsStringAsync();
            Program.OutputTextFile($"raw_covid19-daily-cases_{DateTime.UtcNow.ToString("yyyy-MM-dd(HH-mm-ss)")}.json", jsonText);

            return JsonSerializer.Deserialize<IEnumerable<DailyCase>>(
                jsonText);
        }
    }
}
