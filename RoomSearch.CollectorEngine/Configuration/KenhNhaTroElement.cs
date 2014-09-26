using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace RoomSearch.CollectorEngine.Configuration
{
    public class KenhNhaTroElement : ConfigurationElement
    {
        [ConfigurationProperty("KenhNhaTroCronTriggerExpression")]
        public string KenhNhaTroCronTriggerExpression
        {
            get { return (string)this["KenhNhaTroCronTriggerExpression"]; }
        }
        
    }
}
