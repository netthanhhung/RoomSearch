using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace RoomSearch.CollectorEngine.Realestate.Configuration
{
    public class BatDongSanElement : ConfigurationElement
    {
        [ConfigurationProperty("BatDongSanCronTriggerExpression")]
        public string BatDongSanCronTriggerExpression
        {
            get { return (string)this["BatDongSanCronTriggerExpression"]; }
        }
        
    }
}
