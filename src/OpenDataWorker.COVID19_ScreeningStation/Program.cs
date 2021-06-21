using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using OpenDataWorker.Covid19_ScreeningStation.Models;

namespace OpenDataWorker.Covid19_ScreeningStation
{
    class Program
    {
        private const string zhCSV = "http://od.cdc.gov.tw/icb/指定採檢醫院清單.csv";
        private const string enCSV = "http://od.cdc.gov.tw/icb/指定採檢醫院清單(英文版).csv";
        public static async Task Main(string[] args)
        {
            var zh = DownloadCsvRecord(zhCSV);
            var en = DownloadCsvRecord(enCSV);
            await Task.WhenAll(zh, en);


            
            Console.WriteLine("Hello World!");
        }


        static async Task<IEnumerable<RawCsvRecord>> DownloadCsvRecord(string url)
        {
            HttpClient client = new HttpClient();

            var stream = await client.GetStreamAsync(url);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
            };

            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<RawCsvRecord>();
            }
        }
    }
}
