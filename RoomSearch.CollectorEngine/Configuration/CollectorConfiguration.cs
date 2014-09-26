using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace RoomSearch.CollectorEngine.Configuration
{
    public class CollectorConfiguration
    {
        private static CollectorConfigurationSection instance = null;

        private static CollectorConfigurationSection GetInstance()
        {
            if (instance == null)
            {
                instance = (CollectorConfigurationSection)ConfigurationManager.GetSection("Collector");
            }
            return instance;
        }

        public static KenhNhaTroElement KenhNhaTro
        {
            get { return GetInstance().KenhNhaTro; }
        }

        public static BatDongSanElement BatDongSan
        {
            get { return GetInstance().BatDongSan; }
        }
    }
}
