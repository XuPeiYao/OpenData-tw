using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDataWorker.Covid19_ScreeningStation.Models
{
    public class RawCsvRecord
    {
        public string 機構代碼 { get; set; }
        public string 區域 { get; set; }
        public string 縣市 { get; set; }
        public string 行政區 { get; set; }
        public string 機構名稱 { get; set; }
        public string 地址 { get; set; }
        public string 電話 { get; set; }
        public string 經度 { get; set; }
        public string 緯度 { get; set; }
    }
}
