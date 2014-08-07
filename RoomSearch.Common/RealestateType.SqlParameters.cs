using System;
using System.Data.SqlClient;

namespace RoomSearch.Common
{
    public partial class RealestateType
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.RealestateTypeId, NullableRecordId)
                , Utilities.MakeInputParameter(ColumnNames.Name, Name)
                , Utilities.MakeInputParameter(ColumnNames.Description, Description)
			};
        }
    }
}
