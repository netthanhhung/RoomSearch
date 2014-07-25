using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoomSearch.Data;
using RoomSearch.Common;
using System.Security.Cryptography;

namespace RoomSearch.Business
{
    public static class BusinessMethods
    {
        #region Common
        /// <summary>
        /// Calls the data layer to save the record object.
        /// The record's RecordId is used to determine if the save operation is an insert or an update.
        /// </summary>
        /// <param name="record">The record object containing the data to be saved.</param>
        public static void SaveRecord(Record record)
        {
            new DataLayer().SaveRecord(record, (record.NullableRecordId == null) ? record.CreatedBy : record.UpdatedBy);
        }       

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public static void DeleteRecord(Record record)
        {
            DataLayer dl = new DataLayer();
            String type = record.GetType().ToString();
            dl.DeleteRecord(Utilities.ToLong(record.RecordId), type.Substring(type.LastIndexOf(".") + 1, (type.Length - (type.LastIndexOf(".") + 1))));
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public static void DeleteRecord(long id, Type recordType)
        {
            DataLayer temp = new DataLayer();
            String type = recordType.ToString();
            temp.DeleteRecord(id, type.Substring(type.LastIndexOf(".") + 1, (type.Length - (type.LastIndexOf(".") + 1))));
        }
       
        #endregion

        #region Contact Information
        public static List<Country> ListCountry(int? countryId)
        {
            return new DataLayer().ListCountry(countryId);
        }

        public static List<City> ListCity(int? countryid, int? cityId)
        {
            return new DataLayer().ListCity(countryid, cityId);
        }

        public static List<District> ListDistrict(int? cityId, int? districtId)
        {
            return new DataLayer().ListDistrict(cityId, districtId);
        }

        public static List<ContactInformation> ListContactInformation(int? contactInfoId)
        {
            return new DataLayer().ListContactInformation(contactInfoId);
        }

        public static void SaveContactInformation(List<ContactInformation> saveList)
        {
            if (saveList != null)
            {
                foreach (ContactInformation item in saveList)
                {
                    if (item.IsDeleted && item.NullableRecordId != null)
                    {
                        DeleteRecord((Record)item);
                    }
                    else if (item.IsChanged)
                    {
                        SaveRecord((Record)item);
                    }
                }
            }
        }
        #endregion

        #region RoomType
        public static List<RoomType> ListRoomType()
        {
            return new DataLayer().ListRoomType();
        }

        public static void SaveRoomType(List<RoomType> saveList)
        {
            if (saveList != null)
            {
                foreach (RoomType item in saveList)
                {
                    if (item.IsDeleted && item.NullableRecordId != null)
                    {
                        DeleteRecord((Record)item);
                    }
                    else if (item.IsChanged)
                    {
                        SaveRecord((Record)item);
                    }
                }
            }
        }
        #endregion

        #region PostType
        public static List<PostType> ListPostType()
        {
            return new DataLayer().ListPostType();
        }

        public static void SavePostType(List<PostType> saveList)
        {
            if (saveList != null)
            {
                foreach (PostType item in saveList)
                {
                    if (item.IsDeleted && item.NullableRecordId != null)
                    {
                        DeleteRecord((Record)item);
                    }
                    else if (item.IsChanged)
                    {
                        SaveRecord((Record)item);
                    }
                }
            }
        }
        #endregion

        #region Image
        public static List<Image> ListImage(int? imageId, int? itemId, int? imageTypeId, int loadType)
        {
            return new DataLayer().ListImage(imageId, itemId, imageTypeId, loadType);
        }

        public static void SaveImage(List<Image> saveList)
        {
            if (saveList != null)
            {
                foreach (Image item in saveList)
                {
                    if (item.IsDeleted && item.NullableRecordId != null)
                    {
                        DeleteRecord((Record)item);
                    }
                    else if (item.IsChanged)
                    {
                        SaveRecord((Record)item);
                    }
                }
            }
        }
        #endregion

        #region Post
        public static List<Post> SearchPost(int postTypeId,
                                    int? roomTypeId,
                                    int? countryId,
                                    int? cityId,
                                    int? districtId,
                                    string personName,
                                    string phoneNumber,
                                    string email,
                                    decimal? priceFrom,
                                    decimal? pPriceTo,
                                    DateTime? dateFrom,
                                    DateTime? dateTo,
                                    decimal? meterSquareFrom,
                                    decimal? meterSquareTo,
                                    bool showLegacy)
        {
            return new DataLayer().SearchPost(postTypeId, roomTypeId, countryId, cityId, districtId, personName, phoneNumber, email,
                priceFrom, pPriceTo, dateFrom, dateTo, meterSquareFrom, meterSquareTo, showLegacy);
        }

        public static void SavePost(Post saveItem)
        {
            if (saveItem != null)
            {
                SaveRecord(saveItem);
                if (saveItem.ImageList != null && saveItem.ImageList.Count > 0)
                {
                    foreach (Image image in saveItem.ImageList)
                    {
                        image.ItemId = saveItem.PostId = Convert.ToInt32(saveItem.RecordId);
                        SaveRecord(image);
                    }
                }
            }
        }

        public static int CountPost(int postTypeId,
                                    int? roomTypeId,
                                    int? countryId,
                                    int? cityId,
                                    int? districtId,
                                    string personName,
                                    string phoneNumber,
                                    string email,
                                    decimal? priceFrom,
                                    decimal? pPriceTo,
                                    DateTime? dateFrom,
                                    DateTime? dateTo,
                                    decimal? meterSquareFrom,
                                    decimal? meterSquareTo,
                                    bool showLegacy)
        {
            return new DataLayer().CountPost(postTypeId, roomTypeId, countryId, cityId, districtId, personName, phoneNumber, email,
                priceFrom, pPriceTo, dateFrom, dateTo, meterSquareFrom, meterSquareTo, showLegacy);
        }

        public static List<Post> SearchPostPaging(int postTypeId,
                                   int? roomTypeId,
                                   int? countryId,
                                   int? cityId,
                                   int? districtId,
                                   string personName,
                                   string phoneNumber,
                                   string email,
                                   decimal? priceFrom,
                                   decimal? pPriceTo,
                                   DateTime? dateFrom,
                                   DateTime? dateTo,
                                   decimal? meterSquareFrom,
                                   decimal? meterSquareTo,
                                   bool showLegacy,
                                   int pageSize, int pageNumber, string sortOrder, string sortOrderInvert)
        {
            return new DataLayer().SearchPostPaging(postTypeId, roomTypeId, countryId, cityId, districtId, personName, phoneNumber, email,
                priceFrom, pPriceTo, dateFrom, dateTo, meterSquareFrom, meterSquareTo, showLegacy,
                pageSize, pageNumber, sortOrder, sortOrderInvert);
        }
        #endregion

    }
}

