using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace RoomSearch.CollectorEngine.Realestate.Configuration
{
    public class CollectorConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("BatDongSanSettings")]
        public BatDongSanElement BatDongSan
        {
            get { return (BatDongSanElement)this["BatDongSanSettings"]; }
        }

    }
}
