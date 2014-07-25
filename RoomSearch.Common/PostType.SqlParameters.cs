using System;
using System.Data.SqlClient;

namespace RoomSearch.Common
{
    public partial class PostType
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.PostTypeId, NullableRecordId)
                , Utilities.MakeInputParameter(ColumnNames.Name, Name)
			};
        }
    }
}
