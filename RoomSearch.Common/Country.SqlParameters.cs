using System;
using System.Data.SqlClient;

namespace RoomSearch.Common
{
    public partial class Country
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.CountryId, NullableRecordId),
				Utilities.MakeInputParameter(ColumnNames.Name, Name)
			};
        }
    }
}
