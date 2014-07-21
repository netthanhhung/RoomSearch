using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomSearch.Common
{
    public static partial class Factory
    {
        private static void PopulateRecord(Record record, System.Data.IDataReader reader)
        {
            record.Concurrency = Utilities.ToByteArray(reader["Concurrency"]);
            record.DateCreated = Utilities.ToNDateTime(reader["DateCreated"]);
            record.DateUpdated = Utilities.ToNDateTime(reader["DateUpdated"]);
            record.CreatedBy = Utilities.ToString(reader["CreatedBy"]);
            record.UpdatedBy = Utilities.ToString(reader["UpdatedBy"]);
        }

       
    }
}
