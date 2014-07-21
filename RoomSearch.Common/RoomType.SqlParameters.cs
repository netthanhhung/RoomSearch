using System;
using System.Data.SqlClient;

namespace RoomSearch.Common
{
    public partial class RoomType
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.RoomTypeId, NullableRecordId)
                , Utilities.MakeInputParameter(ColumnNames.Name, Name)
                , Utilities.MakeInputParameter(ColumnNames.OrganisationId, OrganisationId)
				, Utilities.MakeInputParameter(ColumnNames.SiteId, SiteId)
                , Utilities.MakeInputParameter(ColumnNames.Description, Description)
                , Utilities.MakeInputParameter(ColumnNames.IsLegacy, IsLegacy)
			};
        }
    }
}
