using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using RoomSearch.Common;
using System.Data;
using System.Data.Common;

namespace RoomSearch.Data
{
    public sealed partial class DataLayer
    {
        #region Private Attributes
        private Database _db;
        private const int _extendedTimeout = 5400;

        #endregion

        #region Public Constructors
        public DataLayer()
        {
            //_db = DatabaseFactory.CreateDatabase();
            _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("RoomSearch");
        }
        #endregion

        #region Common
        private void AddParameters(System.Data.Common.DbCommand command, SqlParameter[] sqlParams)
        {
            foreach (SqlParameter param in sqlParams)
            {
                _db.AddParameter(command, param.ParameterName, param.DbType, param.Size, param.Direction, param.IsNullable, param.Precision, param.Scale, param.SourceColumn, param.SourceVersion, param.Value);
            }
        }

        public void SaveRecord(Record record)
        {
            SqlParameter[] sqlParams = record.SqlParameters();
            System.Data.Common.DbCommand cmd = _db.GetStoredProcCommand("procSave" + record.TypeName);
            AddParameters(cmd, sqlParams);
            record.Concurrency = Utilities.ToByteArray(_db.ExecuteScalar(cmd));
            record.RecordId = (long)_db.GetParameterValue(cmd, record.TypeName + "ID");
        }

        public void SaveRecord(Record record, string currentUser)
        {
            SqlParameter[] sqlParams = record.SqlParameters();
            System.Data.Common.DbCommand cmd = _db.GetStoredProcCommand("procSave" + record.TypeName);
            AddParameters(cmd, sqlParams);
            AddParameters(cmd, new SqlParameter[] { new SqlParameter("CurrentUser", currentUser) });
            record.Concurrency = Utilities.ToByteArray(_db.ExecuteScalar(cmd));
            record.RecordId = (long)_db.GetParameterValue(cmd, record.TypeName + "ID");
        }

        public void SaveRecord(Record record, string currentUser, string procName)
        {
            SqlParameter[] sqlParams = record.SqlParameters();
            System.Data.Common.DbCommand cmd = _db.GetStoredProcCommand(procName);
            AddParameters(cmd, sqlParams);
            AddParameters(cmd, new SqlParameter[] { new SqlParameter("CurrentUser", currentUser) });
            record.Concurrency = Utilities.ToByteArray(_db.ExecuteScalar(cmd));
            record.RecordId = (long)_db.GetParameterValue(cmd, record.TypeName + "ID");
        }

        public void SaveRecordExtended(Record record, string procName)
        {
            SqlParameter[] sqlParams = record.SqlParameters();
            System.Data.Common.DbCommand cmd = _db.GetStoredProcCommand(procName);
            AddParameters(cmd, sqlParams);
            _db.ExecuteScalar(cmd);
        }
        
        private DbCommand GetDbCommandWithExtendedTimeout(string storedProcedureName, params object[] parameterValues)
        {
            DbCommand result = this._db.GetStoredProcCommand(storedProcedureName, parameterValues);
            result.CommandTimeout = _extendedTimeout;

            return result;
        }

        private int ExecuteNonQueryWithExtendedTimeout(string storedProcedureName, params object[] parameterValues)
        {
            int result = 0;

            using (DbCommand cmd = this._db.GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                cmd.CommandTimeout = _extendedTimeout;
                result = _db.ExecuteNonQuery(cmd);
            }

            return result;
        }

        public void DeleteRecord(long recordId, string recordType)
        {
            string store = "procDelete" + recordType;
            _db.ExecuteNonQuery(store, recordId);
        }
        #endregion

        #region Country
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Country> ListCountry(int? countryId)
        {
            List<Country> result = new List<Country>();

            using (IDataReader reader = _db.ExecuteReader("procListCountry", countryId))
            {
                Factory.FillCountryList(result, reader);
            }

            return result;
        }
        #endregion

        #region City
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<City> ListCity(int? countryid, int? cityId)
        {
            List<City> result = new List<City>();

            using (IDataReader reader = _db.ExecuteReader("procListCity", countryid, cityId))
            {
                Factory.FillCityList(result, reader);
            }

            return result;
        }
        #endregion

        #region District
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<District> ListDistrict(int? cityId, int? districtId)
        {
            List<District> result = new List<District>();

            using (IDataReader reader = _db.ExecuteReader("procListDistrict", cityId, districtId))
            {
                Factory.FillDistrictList(result, reader);
            }

            return result;
        }
        #endregion

        #region ContactInformation
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<ContactInformation> ListContactInformation(int? contactInfoId)
        {
            List<ContactInformation> result = new List<ContactInformation>();

            using (IDataReader reader = _db.ExecuteReader("procListContactInformation", contactInfoId))
            {
                Factory.FillContactInformationList(result, reader);
            }

            return result;
        }
        #endregion

        #region RoomType
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<RoomType> ListRoomType()
        {
            List<RoomType> result = new List<RoomType>();

            using (IDataReader reader = _db.ExecuteReader("procListRoomType"))
            {
                Factory.FillRoomTypeList(result, reader);
            }
            return result;
        }
        #endregion

        #region PostType
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<PostType> ListPostType()
        {
            List<PostType> result = new List<PostType>();

            using (IDataReader reader = _db.ExecuteReader("procListPostType"))
            {
                Factory.FillPostTypeList(result, reader);
            }
            return result;
        }
        #endregion

        #region Post
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Post> SearchPost(int postTypeId,
                                    int? roomTypeId,
                                    int? realestateId,
                                    int? countryId,
                                    int? cityId,
                                    int? districtId,
                                    string personName,
                                    string phoneNumber,
                                    string email,		
                                    int? gender,
                                    decimal? priceFrom,	
                                    decimal? pPriceTo,	
                                    DateTime? dateFrom,
                                    DateTime? dateTo,
                                    decimal? meterSquareFrom,
                                    decimal? meterSquareTo,	
                                    bool showLegacy)
        {
            List<Post> result = new List<Post>();

            using (IDataReader reader = _db.ExecuteReader("procSearchPost", postTypeId, roomTypeId, realestateId, countryId, cityId, districtId, personName, phoneNumber, email, gender,
                priceFrom, pPriceTo, dateFrom, dateTo, meterSquareFrom, meterSquareTo, showLegacy))
            {
                Factory.FillPostList(result, reader);
            }
            
            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public int CountPost(int postTypeId,
                                    int? roomTypeId,
                                    int? realestateTypeId,
                                    int? countryId,
                                    int? cityId,
                                    int? districtId,
                                    string personName,
                                    string phoneNumber,
                                    string email,
                                    int? gender,
                                    decimal? priceFrom,
                                    decimal? pPriceTo,
                                    DateTime? dateFrom,
                                    DateTime? dateTo,
                                    decimal? meterSquareFrom,
                                    decimal? meterSquareTo,
                                    bool showLegacy)
        {
            
            return Utilities.ToInt(_db.ExecuteScalar("procCountPost", postTypeId, roomTypeId, realestateTypeId, countryId, cityId, districtId, personName, phoneNumber, email, gender,
                priceFrom, pPriceTo, dateFrom, dateTo, meterSquareFrom, meterSquareTo, showLegacy));
        }


        public List<Post> SearchPostPaging(int postTypeId,
                                   int? roomTypeId,
                                   int? realestateTypeId,
                                   int? countryId,
                                   int? cityId,
                                   int? districtId,
                                   string personName,
                                   string phoneNumber,
                                   string email,
                                   int? gender,
                                   decimal? priceFrom,
                                   decimal? pPriceTo,
                                   DateTime? dateFrom,
                                   DateTime? dateTo,
                                   decimal? meterSquareFrom,
                                   decimal? meterSquareTo,
                                   bool showLegacy,
                                   int pageSize, int pageNumber, string sortOrder, string sortOrderInvert)
        {
            List<Post> result = new List<Post>();

            string sqlQuery = @"SELECT	{0}
		                        P.PostId
                              ,P.PostTypeId
                              ,P.PersonName
                              ,P.PhoneNumber
                              ,P.Email
                              ,P.Gender
                              ,P.RoomTypeId
                              ,RT.Name as RoomType
                              ,P.RealestateTypeId
                              ,RLT.Name as RealestateType
                              ,P.AvailableRooms
                              ,P.Description
                              ,P.MeterSquare
                              ,P.Floor
                              ,P.Address
                              ,P.DistrictId
                              ,D.Name as District
                              ,P.CityId
                              ,CT.Name as City
                              ,P.CountryId
                              ,C.Name as Country
                              ,P.Price
                              ,P.IsLegacy
                              ,P.Concurrency
                              ,P.DateCreated
                              ,P.DateUpdated
                              ,P.CreatedBy
                              ,P.UpdatedBy
	                        FROM	dbo.Post P
	                        LEFT OUTER JOIN dbo.RoomType RT on P.RoomTypeId = RT.RoomTypeId
	                        LEFT OUTER JOIN dbo.RealestateType RLT on P.RealestateTypeId = RLT.RealestateTypeId
	                        LEFT OUTER JOIN dbo.Country C on P.CountryId = C.CountryId
	                        LEFT OUTER JOIN dbo.City CT on P.CityId = CT.CityId
	                        LEFT OUTER JOIN dbo.District D on P.DistrictId = D.DistrictId
	                        WHERE	P.PostTypeId = @PostTypeId
	                        AND	(@RoomTypeId IS NULL OR P.RoomTypeId = @RoomTypeId)
                            AND	(@RealestateTypeId IS NULL OR P.RealestateTypeId = @RealestateTypeId)
	                        AND	(@CountryId is null OR P.CountryId = @CountryId)
	                        AND	(@CityId is null OR P.CityId = @CityId)
	                        AND	(@DistrictId is null OR P.DistrictId = @DistrictId)
	                        AND	(@PersonName is null  OR (P.PersonName like '%' + @PersonName + '%'))
	                        AND	(@PhoneNumber is null  OR LOWER(P.PhoneNumber) = @PhoneNumber)
	                        AND	(@Email is null  OR LOWER(P.Email) = @Email)
                            AND	(@Gender IS NULL OR P.Gender = @Gender)
	                        AND (@PriceFrom IS NULL OR P.Price IS NULL OR P.Price >= @PriceFrom)
	                        AND (@PriceTo IS NULL OR P.Price IS NULL OR P.Price <= @PriceTo)
	                        AND (@DateFrom IS NULL OR P.DateUpdated >= @DateFrom)
	                        AND (@DateTo IS NULL OR P.DateUpdated <= @DateTo)
	                        AND (@MeterSquareFrom  is null  OR P.MeterSquare IS NULL OR P.MeterSquare >= @MeterSquareFrom) 
	                        AND (@MeterSquareTo  is null  OR P.MeterSquare IS NULL OR P.MeterSquare <= @MeterSquareTo)
	                        AND (@ShowLegacy = 1 OR P.IsLegacy = 0)";
            
            sqlQuery += " order by " + sortOrder;
            sqlQuery = string.Format(sqlQuery, "top " + pageSize * pageNumber + " ");
            sqlQuery = "(select top " + pageSize + " * from \n (" + sqlQuery + " ) As T1 \n"
                    + " Order by " + sortOrderInvert;
            sqlQuery = "select * from \n " + sqlQuery + " ) As T2 \n"
                    + " Order by " + sortOrder;

            DbCommand dbCommand = _db.GetSqlStringCommand(sqlQuery);
            dbCommand.Parameters.Add(BuildSqlParameter("@PostTypeId", SqlDbType.Int, true, postTypeId));
            dbCommand.Parameters.Add(BuildSqlParameter("@RoomTypeId", SqlDbType.Int, roomTypeId.HasValue, roomTypeId));
            dbCommand.Parameters.Add(BuildSqlParameter("@RealestateTypeId", SqlDbType.Int, realestateTypeId.HasValue, realestateTypeId));
            dbCommand.Parameters.Add(BuildSqlParameter("@CountryId", SqlDbType.Int, countryId.HasValue, countryId));
            dbCommand.Parameters.Add(BuildSqlParameter("@CityId", SqlDbType.Int, cityId.HasValue, cityId));
            dbCommand.Parameters.Add(BuildSqlParameter("@DistrictId", SqlDbType.Int, districtId.HasValue, districtId));
            dbCommand.Parameters.Add(BuildSqlParameter("@PersonName", SqlDbType.NVarChar, !string.IsNullOrEmpty(personName), personName));
            dbCommand.Parameters.Add(BuildSqlParameter("@PhoneNumber", SqlDbType.NVarChar, !string.IsNullOrEmpty(phoneNumber), phoneNumber));
            dbCommand.Parameters.Add(BuildSqlParameter("@Email", SqlDbType.NVarChar, !string.IsNullOrEmpty(email), email));
            dbCommand.Parameters.Add(BuildSqlParameter("@Gender", SqlDbType.Int, gender.HasValue, gender));
            dbCommand.Parameters.Add(BuildSqlParameter("@PriceFrom", SqlDbType.Decimal, priceFrom.HasValue, priceFrom));
            dbCommand.Parameters.Add(BuildSqlParameter("@PriceTo", SqlDbType.Decimal, pPriceTo.HasValue, pPriceTo));
            dbCommand.Parameters.Add(BuildSqlParameter("@DateFrom", SqlDbType.DateTime, dateFrom.HasValue, dateFrom));
            dbCommand.Parameters.Add(BuildSqlParameter("@DateTo", SqlDbType.DateTime, dateTo.HasValue, dateTo));
            dbCommand.Parameters.Add(BuildSqlParameter("@MeterSquareFrom", SqlDbType.Decimal, meterSquareFrom.HasValue, meterSquareFrom));
            dbCommand.Parameters.Add(BuildSqlParameter("@MeterSquareTo", SqlDbType.Decimal, meterSquareTo.HasValue, meterSquareTo));
            dbCommand.Parameters.Add(BuildSqlParameter("@ShowLegacy", SqlDbType.Bit, true, showLegacy));
        

            using (IDataReader reader = _db.ExecuteReader(dbCommand))
            {
                Factory.FillPostList(result, reader);
            }

            return result;
        }

        private SqlParameter BuildSqlParameter(string paramName, SqlDbType paramType, bool hasValue, object value)
        {
            SqlParameter result = new SqlParameter(paramName, paramType);
            if (hasValue)
            {
                result.Value = value;
            }
            else
            {
                result.Value = DBNull.Value;
            }
            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Post> GetPost(int postId)
        {
            List<Post> result = new List<Post>();

            using (IDataReader reader = _db.ExecuteReader("procGetPost", postId))
            {
                Factory.FillPostList(result, reader);
            }

            return result;
        }
        #endregion


        #region Image
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Image> ListImage(int? imageId, int? itemId, int? imageTypeId, int loadType)
        {
            List<Image> result = new List<Image>();

            using (IDataReader reader = _db.ExecuteReader("procListImage", imageId, itemId, imageTypeId, loadType))
            {
                Factory.FillImageList(result, reader);
            }
            return result;
        }
        #endregion

    }
}
