using System;
using System.Data.SqlClient;

namespace RoomSearch.Common
{
    public abstract partial class Record
    {
        public abstract SqlParameter[] SqlParameters();
    }
}
