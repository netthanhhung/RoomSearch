using Common.Logging;

namespace RoomSearch.CollectorEngine
{
    public class Logger
    {
        public static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
