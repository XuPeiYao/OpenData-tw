using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
            return new ScreeningStation()
            {
                Code      = zh.機構代碼,
                Telephone = zh.電話,
                ZhInfo = Information.Load(zh),
                EnInfo = Information.Load(en),
                Position = new Position()
                {
                    Longitude = double.Parse(zh.經度),
                    Latitude = double.Parse(zh.緯度)
                }
            };
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

        public static Information Load(RawCsvRecord raw)
        {
            return new Information()
            {
                Region   = Regex.Unescape(raw.區域),
                County   = Regex.Unescape(raw.縣市),
                District = Regex.Unescape(raw.行政區),
                Name     = Regex.Unescape(raw.機構名稱),
                Address  = Regex.Unescape(raw.地址)
            };
        }
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
