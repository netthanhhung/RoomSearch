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


        #region Country
        public static Country Country(System.Data.IDataReader reader)
        {
            Country result = null;

            if (null != reader && reader.Read())
            {
                result = new Country();
                PopulateCountry(result, reader);
            }

            return result;
        }

        public static void PopulateCountry(Country input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.CountryId = Utilities.ToInt(reader[RoomSearch.Common.Country.ColumnNames.CountryId]);
            input.Name = Utilities.ToString(reader[RoomSearch.Common.Country.ColumnNames.Name]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillCountryList(List<Country> list, System.Data.IDataReader reader)
        {
            list.Clear();
            Country item;
            while (true)
            {
                item = Factory.Country(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region City
        public static City City(System.Data.IDataReader reader)
        {
            City result = null;

            if (null != reader && reader.Read())
            {
                result = new City();
                PopulateCity(result, reader);
            }

            return result;
        }

        public static void PopulateCity(City input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.CityId = Utilities.ToInt(reader[RoomSearch.Common.City.ColumnNames.CityId]);
            input.CountryId = Utilities.ToInt(reader[RoomSearch.Common.City.ColumnNames.CountryId]);
            input.Name = Utilities.ToString(reader[RoomSearch.Common.City.ColumnNames.Name]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillCityList(List<City> list, System.Data.IDataReader reader)
        {
            list.Clear();
            City item;
            while (true)
            {
                item = Factory.City(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region District
        public static District District(System.Data.IDataReader reader)
        {
            District result = null;

            if (null != reader && reader.Read())
            {
                result = new District();
                PopulateDistrict(result, reader);
            }

            return result;
        }

        public static void PopulateDistrict(District input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.DistrictId = Utilities.ToInt(reader[RoomSearch.Common.District.ColumnNames.DistrictId]);
            input.CityId = Utilities.ToInt(reader[RoomSearch.Common.District.ColumnNames.CityId]);
            input.Name = Utilities.ToString(reader[RoomSearch.Common.District.ColumnNames.Name]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillDistrictList(List<District> list, System.Data.IDataReader reader)
        {
            list.Clear();
            District item;
            while (true)
            {
                item = Factory.District(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region ContactInformation
        public static ContactInformation ContactInformation(System.Data.IDataReader reader)
        {
            ContactInformation result = null;

            if (null != reader && reader.Read())
            {
                result = new ContactInformation();
                PopulateContactInformation(result, reader);
            }

            return result;
        }

        public static void PopulateContactInformation(ContactInformation input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = Utilities.ToInt(reader[RoomSearch.Common.ContactInformation.ColumnNames.ContactInformationId]);
            input.ContactTypeId = Utilities.ToInt(reader[RoomSearch.Common.ContactInformation.ColumnNames.ContactTypeId]);
            input.FirstName = Utilities.ToString(reader[RoomSearch.Common.ContactInformation.ColumnNames.FirstName]);
            input.LastName = Utilities.ToString(reader[RoomSearch.Common.ContactInformation.ColumnNames.LastName]);
            input.Address = Utilities.ToString(reader[RoomSearch.Common.ContactInformation.ColumnNames.Address]);
            input.Address2 = Utilities.ToString(reader[RoomSearch.Common.ContactInformation.ColumnNames.Address2]);
            input.District = Utilities.ToString(reader[RoomSearch.Common.ContactInformation.ColumnNames.District]);
            input.DistrictId = Utilities.ToNInt(reader[RoomSearch.Common.ContactInformation.ColumnNames.DistrictId]);
            input.City = Utilities.ToString(reader[RoomSearch.Common.ContactInformation.ColumnNames.City]);
            input.CityId = Utilities.ToNInt(reader[RoomSearch.Common.ContactInformation.ColumnNames.CityId]);
            input.State = Utilities.ToString(reader[RoomSearch.Common.ContactInformation.ColumnNames.State]);
            input.Postcode = Utilities.ToString(reader[RoomSearch.Common.ContactInformation.ColumnNames.Postcode]);
            input.CountryId = Utilities.ToNInt(reader[RoomSearch.Common.ContactInformation.ColumnNames.CountryId]);
            input.PhoneNumber = Utilities.ToString(reader[RoomSearch.Common.ContactInformation.ColumnNames.PhoneNumber]);
            input.FaxNumber = Utilities.ToString(reader[RoomSearch.Common.ContactInformation.ColumnNames.FaxNumber]);
            input.Email = Utilities.ToString(reader[RoomSearch.Common.ContactInformation.ColumnNames.Email]);
            input.DoB = Utilities.ToNDateTime(reader[RoomSearch.Common.ContactInformation.ColumnNames.DoB]);
            input.Visa = Utilities.ToString(reader[RoomSearch.Common.ContactInformation.ColumnNames.Visa]);
            input.VisaValidFrom = Utilities.ToNDateTime(reader[RoomSearch.Common.ContactInformation.ColumnNames.VisaValidFrom]);
            input.VisaValidTo = Utilities.ToNDateTime(reader[RoomSearch.Common.ContactInformation.ColumnNames.VisaValidTo]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillContactInformationList(List<ContactInformation> list, System.Data.IDataReader reader)
        {
            list.Clear();
            ContactInformation item;
            while (true)
            {
                item = Factory.ContactInformation(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region RoomType

        public static RoomType RoomType(System.Data.IDataReader reader)
        {
            RoomType result = null;

            if (null != reader && reader.Read())
            {
                result = new RoomType();
                PopulateRoomType(result, reader);
            }

            return result;
        }

        public static void PopulateRoomType(RoomType input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.RoomTypeId = Utilities.ToInt(reader[RoomSearch.Common.RoomType.ColumnNames.RoomTypeId]);
            input.Name = Utilities.ToString(reader[RoomSearch.Common.RoomType.ColumnNames.Name]);
            input.Description = Utilities.ToString(reader[RoomSearch.Common.RoomType.ColumnNames.Description]);            
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillRoomTypeList(List<RoomType> list, System.Data.IDataReader reader)
        {
            list.Clear();
            RoomType item;
            while (true)
            {
                item = Factory.RoomType(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region RealestateType

        public static RealestateType RealestateType(System.Data.IDataReader reader)
        {
            RealestateType result = null;

            if (null != reader && reader.Read())
            {
                result = new RealestateType();
                PopulateRealestateType(result, reader);
            }

            return result;
        }

        public static void PopulateRealestateType(RealestateType input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.RealestateTypeId = Utilities.ToInt(reader[RoomSearch.Common.RealestateType.ColumnNames.RealestateTypeId]);
            input.Name = Utilities.ToString(reader[RoomSearch.Common.RealestateType.ColumnNames.Name]);
            input.Description = Utilities.ToString(reader[RoomSearch.Common.RealestateType.ColumnNames.Description]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillRealestateTypeList(List<RealestateType> list, System.Data.IDataReader reader)
        {
            list.Clear();
            RealestateType item;
            while (true)
            {
                item = Factory.RealestateType(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region PostType

        public static PostType PostType(System.Data.IDataReader reader)
        {
            PostType result = null;

            if (null != reader && reader.Read())
            {
                result = new PostType();
                PopulatePostType(result, reader);
            }

            return result;
        }

        public static void PopulatePostType(PostType input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.PostTypeId = Utilities.ToInt(reader[RoomSearch.Common.PostType.ColumnNames.PostTypeId]);
            input.Name = Utilities.ToString(reader[RoomSearch.Common.PostType.ColumnNames.Name]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillPostTypeList(List<PostType> list, System.Data.IDataReader reader)
        {
            list.Clear();
            PostType item;
            while (true)
            {
                item = Factory.PostType(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region Image

        public static Image Image(System.Data.IDataReader reader)
        {
            Image result = null;

            if (null != reader && reader.Read())
            {
                result = new Image();
                PopulateImage(result, reader);
            }

            return result;
        }

        public static void PopulateImage(Image input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.ImageId = Utilities.ToInt(reader[RoomSearch.Common.Image.ColumnNames.ImageId]);
            input.ImageTypeId = Utilities.ToInt(reader[RoomSearch.Common.Image.ColumnNames.ImageTypeId]);
            input.ItemId = Utilities.ToInt(reader[RoomSearch.Common.Image.ColumnNames.ItemId]);
            input.FileName = Utilities.ToString(reader[RoomSearch.Common.Image.ColumnNames.FileName]);
            input.ImageContent = Utilities.ToByteArray(reader[RoomSearch.Common.Image.ColumnNames.ImageContent]);
            input.ImageSmallContent = Utilities.ToByteArray(reader[RoomSearch.Common.Image.ColumnNames.ImageSmallContent]);
            input.DisplayIndex = Utilities.ToNInt(reader[RoomSearch.Common.Image.ColumnNames.DisplayIndex]);
            input.Description = Utilities.ToString(reader[RoomSearch.Common.Image.ColumnNames.Description]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillImageList(List<Image> list, System.Data.IDataReader reader)
        {
            list.Clear();
            Image item;
            while (true)
            {
                item = Factory.Image(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region Post

        public static Post Post(System.Data.IDataReader reader)
        {
            Post result = null;

            if (null != reader && reader.Read())
            {
                result = new Post();
                PopulatePost(result, reader);
            }

            return result;
        }

        public static void PopulatePost(Post input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.PostId = Utilities.ToInt(reader[RoomSearch.Common.Post.ColumnNames.PostId]);
            input.PostTypeId = Utilities.ToInt(reader[RoomSearch.Common.Post.ColumnNames.PostTypeId]);
            input.PersonName = Utilities.ToString(reader[RoomSearch.Common.Post.ColumnNames.PersonName]);
            input.PhoneNumber = Utilities.ToString(reader[RoomSearch.Common.Post.ColumnNames.PhoneNumber]);
            input.Email = Utilities.ToString(reader[RoomSearch.Common.Post.ColumnNames.Email]);
            input.Gender = Utilities.ToNInt(reader[RoomSearch.Common.Post.ColumnNames.Gender]);
            input.Floor = Utilities.ToNInt(reader[RoomSearch.Common.Post.ColumnNames.Floor]);
            input.Address = Utilities.ToString(reader[RoomSearch.Common.Post.ColumnNames.Address]);
            input.District = Utilities.ToString(reader[RoomSearch.Common.Post.ColumnNames.District]);
            input.DistrictId = Utilities.ToNInt(reader[RoomSearch.Common.Post.ColumnNames.DistrictId]);
            input.City = Utilities.ToString(reader[RoomSearch.Common.Post.ColumnNames.City]);
            input.CityId = Utilities.ToNInt(reader[RoomSearch.Common.Post.ColumnNames.CityId]);
            input.Country = Utilities.ToString(reader[RoomSearch.Common.Post.ColumnNames.Country]);
            input.CountryId = Utilities.ToNInt(reader[RoomSearch.Common.Post.ColumnNames.CountryId]);
            input.RoomTypeId = Utilities.ToNInt(reader[RoomSearch.Common.Post.ColumnNames.RoomTypeId]);
            input.RealestateTypeId = Utilities.ToNInt(reader[RoomSearch.Common.Post.ColumnNames.RealestateTypeId]);
            input.RoomType = Utilities.ToString(reader[RoomSearch.Common.Post.ColumnNames.RoomType]);
            input.RealestateType = Utilities.ToString(reader[RoomSearch.Common.Post.ColumnNames.RealestateType]);
            input.AvailableRooms = Utilities.ToNInt(reader[RoomSearch.Common.Post.ColumnNames.AvailableRooms]);
            input.Description = Utilities.ToString(reader[RoomSearch.Common.Post.ColumnNames.Description]);
            input.MeterSquare = Utilities.ToNDecimal(reader[RoomSearch.Common.Post.ColumnNames.MeterSquare]);
            input.Price = Utilities.ToNDecimal(reader[RoomSearch.Common.Post.ColumnNames.Price]);
            input.IsLegacy = Utilities.ToBool(reader[RoomSearch.Common.Post.ColumnNames.IsLegacy]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillPostList(List<Post> list, System.Data.IDataReader reader)
        {
            list.Clear();
            Post item;
            while (true)
            {
                item = Factory.Post(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

    }
}
