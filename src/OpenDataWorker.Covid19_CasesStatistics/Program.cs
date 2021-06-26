using System;
using System.IO;

namespace OpenDataWorker.Covid19_CasesStatistics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
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
    }
}
