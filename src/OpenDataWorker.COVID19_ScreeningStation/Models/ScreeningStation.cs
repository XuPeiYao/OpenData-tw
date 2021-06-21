using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDataWorker.Covid19_ScreeningStation.Models
{
    public class ScreeningStation
    {
        public string Code { get; set; }
        public Information ZhInfo { get; set; }
        public Information EnInfo { get; set; }
        public string Telephone { get; set; }
        public Position Position { get; set; }

        public static ScreeningStation LoadFromRaw(
            RawCsvRecord zh,
            RawCsvRecord en
        )
        {
        }

        public static IEnumerable<ScreeningStation> LoadFromRaw(
            IEnumerable<RawCsvRecord> zh,
            IEnumerable<RawCsvRecord> en
        )
        {
            return zh.Select(x => LoadFromRaw(x, en.FirstOrDefault(y => y.機構代碼 == x.機構代碼)));
        }
    }

    public class Information
    {
        public string Region { get; set; }
        public string County { get; set; }
        public string District { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class Position
    {
        /// <summary>
        /// 經度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 緯度
        /// </summary>
        public double Latitude { get; set; }
    }
}
