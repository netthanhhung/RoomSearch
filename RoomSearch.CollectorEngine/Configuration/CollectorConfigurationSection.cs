using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace RoomSearch.CollectorEngine.Configuration
{
    public class CollectorConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("KenhNhaTroSettings")]
        public KenhNhaTroElement KenhNhaTro
        {
            get { return (KenhNhaTroElement)this["KenhNhaTroSettings"]; }
        }

        [ConfigurationProperty("BatDongSanSettings")]
        public BatDongSanElement BatDongSan
        {
            get { return (BatDongSanElement)this["BatDongSanSettings"]; }
        }
    }
}
