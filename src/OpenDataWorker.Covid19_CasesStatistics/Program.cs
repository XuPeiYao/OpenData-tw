using System;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using OpenDataWorker.Covid19_CasesStatistics.JsonConverters;
using OpenDataWorker.Covid19_CasesStatistics.Models;

namespace OpenDataWorker.Covid19_CasesStatistics
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var service   = new Covid19CasesService();
            var dailyCase = await service.GetAllDailyCases();

            #region Daily total

            var dailyTotalCaseCount = dailyCase.GroupBy(x => x.Date).Select(
                x => new
                {
                    date            = x.Key,
                    total           = x.Sum(y => y.Count),
                    imported        = x.Count(y => y.Imported),
                    locallyAcquired = x.Count(y => !y.Imported),
                    gender = new
                    {
                        male   = x.Where(y => y.Gender == Gender.Male).Sum(x => x.Count),
                        female = x.Where(y => y.Gender == Gender.Female).Sum(x => x.Count)
                    }
                });
            OutputJsonFile(
                $"covid19-daily-cases_{DateTime.UtcNow.AddHours(8).AddDays(-1).ToString("yyyy-MM-dd_UTC+8")}.json"
              , dailyTotalCaseCount);
            OutputJsonFile(
                $"covid19-daily-cases_lastDay.json",
                dailyTotalCaseCount.OrderByDescending(x=>x.date).FirstOrDefault());
            #endregion

            #region Daily county cases

            var dailyCountyCaseCount = dailyCase.GroupBy(x => x.Date).Select(
                x => new
                {
                    date = x.Key,
                    county = x.GroupBy(y => y.County)
                              .ToDictionary(
                                  y => y.Key == "空值" ? "境外" : y.Key,
                                  y => new
                                  {
                                      total = y.Sum(z => z.Count),
                                      gender = new
                                      {
                                          male   = y.Where(z => z.Gender == Gender.Male).Sum(z => z.Count),
                                          female = y.Where(z => z.Gender == Gender.Female).Sum(z => z.Count)
                                      }
                                  })
                });
            OutputJsonFile(
                $"covid19-daily-county-cases_{DateTime.UtcNow.AddHours(8).AddDays(-1).ToString("yyyy-MM-dd_UTC+8")}.json"
              , dailyCountyCaseCount);
            OutputJsonFile(
                $"covid19-daily-county-cases_lastDay.json"
              , dailyCountyCaseCount.OrderByDescending(x=>x.date).FirstOrDefault());
            #endregion

            #region Daily cases county age group
            var dailyCountyAgeCaseCount = dailyCase.GroupBy(x => x.Date).Select(
                x => new
                {
                    date = x.Key,
                    county = x.GroupBy(y => y.County)
                              .ToDictionary(
                                  y => y.Key == "空值" ? "境外" : y.Key,
                                  y => new
                                  {
                                      total = y.Sum(z => z.Count),
                                      ages = y.GroupBy(z=>z.AgeGroup).ToDictionary(
                                                 y => y.Key,
                                                 y => new
                                                 {
                                                     total = y.Sum(z => z.Count),
                                                     male = y.Where(z => z.Gender == Gender.Male)
                                                             .Sum(z => z.Count),
                                                     female = y.Where(
                                                                   z => z.Gender == Gender.Female)
                                                               .Sum(z => z.Count)
                                                 })
                                  })
                });
            OutputJsonFile(
                $"covid19-daily-county-age-cases_{DateTime.UtcNow.AddHours(8).AddDays(-1).ToString("yyyy-MM-dd_UTC+8")}.json"
              , dailyCountyAgeCaseCount);
            OutputJsonFile(
                $"covid19-daily-county-age-cases_lastDay.json"
              , dailyCountyAgeCaseCount.OrderByDescending(x=>x.date).FirstOrDefault());
            #endregion
        }

        public static void OutputLog(string message)
        {
            var path = Path.Combine(GetLoggingDirectory(), $"{DateTime.UtcNow.ToString("yyyy-MM-dd")}.log");
            File.AppendAllText(path, $"[{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}]\t" + message + "\r\n");
        }

        public static void OutputJsonFile(string filename, object data)
        {
            var path = Path.Combine(GetDataDirectory(), filename);
            System.IO.File.WriteAllText(
                path, JsonSerializer.Serialize(
                    data, new JsonSerializerOptions()
                    {
                        Encoder       = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                        WriteIndented = true
                    }));
            OutputLog($"Output json file: {filename}");
        }

        public static void OutputTextFile(string filename, string text)
        {
            var path = Path.Combine(GetDataDirectory(), filename);
            System.IO.File.WriteAllText(path, text);
            OutputLog($"Output text file: {filename}");
        }

        public static string GetDataDirectory()
        {
            var workDir = Path.Combine(Directory.GetCurrentDirectory(), "data");
            if (!Directory.Exists(workDir))
            {
                Directory.CreateDirectory(workDir);
            }

            return workDir;
        }

        public static string GetLoggingDirectory()
        {
            var workDir = Path.Combine(Directory.GetCurrentDirectory(), "logs");
            if (!Directory.Exists(workDir))
            {
                Directory.CreateDirectory(workDir);
            }

            return workDir;
        }
    }
}
