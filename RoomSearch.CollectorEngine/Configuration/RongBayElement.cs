using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace RoomSearch.CollectorEngine.Configuration
{
    public class RongBayElement : ConfigurationElement
    {
        [ConfigurationProperty("RongBayCronTriggerExpression")]
        public string RongBayCronTriggerExpression
        {
            get { return (string)this["RongBayCronTriggerExpression"]; }
        }
        
    }
}
