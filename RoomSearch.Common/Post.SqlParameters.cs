using System;
using System.Data.SqlClient;

namespace RoomSearch.Common
{
    public partial class Post
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.PostId, NullableRecordId)
                , Utilities.MakeInputParameter(ColumnNames.PostTypeId, PostTypeId)
				, Utilities.MakeInputParameter(ColumnNames.PersonName, PersonName)
                , Utilities.MakeInputParameter(ColumnNames.PhoneNumber, PhoneNumber)
                , Utilities.MakeInputParameter(ColumnNames.Email, Email)
                , Utilities.MakeInputParameter(ColumnNames.RoomTypeId, RoomTypeId)
                , Utilities.MakeInputParameter(ColumnNames.AvailableRooms, AvailableRooms)
                , Utilities.MakeInputParameter(ColumnNames.Description, Description)                
                , Utilities.MakeInputParameter(ColumnNames.MeterSquare, MeterSquare)
                , Utilities.MakeInputParameter(ColumnNames.Floor, Floor)
                , Utilities.MakeInputParameter(ColumnNames.Address, Address)
                , Utilities.MakeInputParameter(ColumnNames.DistrictId, DistrictId)
                , Utilities.MakeInputParameter(ColumnNames.CityId, CityId)
                , Utilities.MakeInputParameter(ColumnNames.CountryId, CountryId)
                , Utilities.MakeInputParameter(ColumnNames.Price, Price)
                , Utilities.MakeInputParameter(ColumnNames.IsLegacy, IsLegacy)
			};
        }
    }
}
