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
        public static void CleanupDatabase()
        {
            new DataLayer().CleanupDatabase();
        }

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

        public static List<District> ListDistrict(int? cityId, int? districtId, bool insertAllItem)
        {
            List<District> result = new DataLayer().ListDistrict(cityId, districtId);
            if (insertAllItem)
            {
                District rt = new District();
                rt.Name = "Tất cả";
                result.Insert(0, rt);
            }
            return result;
            //return new DataLayer().ListDistrict(cityId, districtId);
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

        #region RealestateType
        public static List<RealestateType> ListRealestateType(bool insertAllItem)
        {
            List<RealestateType> result = new DataLayer().ListRealestateType();
            if (insertAllItem)
            {
                RealestateType rt = new RealestateType();
                rt.Name = "Tất cả";
                result.Insert(0, rt);
            }
            return result;
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
            return new DataLayer().SearchPost(postTypeId, roomTypeId, realestateId, countryId, cityId, districtId, personName, phoneNumber, email, gender,
                priceFrom, pPriceTo, dateFrom, dateTo, meterSquareFrom, meterSquareTo, showLegacy);
        }

        public static void SavePostList(List<Post> postList)
        {            
            if (postList != null && postList.Count > 0)
            {
                DataLayer dataLayer = new DataLayer();
                List<Post> oldPosts = dataLayer.SearchPost(postList[0].PostTypeId, null, null, null, null, null, null, null, null, null, null, null, 
                    DateTime.Today, DateTime.Now, null, null, false);
                foreach (Post saveItem in postList)
                {
                    if (oldPosts.Count(i => i.PersonName == saveItem.PersonName
                                            && i.PhoneNumber == saveItem.PhoneNumber
                                            && i.Address == saveItem.Address
                                            && i.Price == saveItem.Price
                                            && i.PostTypeId == saveItem.PostTypeId) == 0)
                    {
                        SavePost(saveItem);
                    } 
                }
            }
        }

        public static void SavePost(Post saveItem)
        {
            if (saveItem != null)
            {
                bool isUpdate = saveItem.PostId > 0;
                SaveRecord(saveItem);

                if (!isUpdate && saveItem.ImageList != null && saveItem.ImageList.Count > 0)
                {
                    foreach (Image image in saveItem.ImageList)
                    {
                        image.ItemId = saveItem.PostId = Convert.ToInt32(saveItem.RecordId);
                        SaveRecord(image);
                    }
                }
                else
                {
                    List<Image> oldImages = ListImage(null, saveItem.PostId, (int)ImageType.Room, 0);
                    foreach (Image oldItem in oldImages)
                    {
                        if (saveItem.ImageList == null || saveItem.ImageList.Count(i => i.ImageId == oldItem.ImageId) == 0)
                        {
                            DeleteRecord(oldItem);
                        }
                    }

                    if (saveItem.ImageList != null)
                    {
                        foreach (Image image in saveItem.ImageList)
                        {
                            if (image.ImageId <= 0)
                            {
                                image.ItemId = saveItem.PostId = Convert.ToInt32(saveItem.RecordId);
                                SaveRecord(image);
                            }
                        }
                    }

                }

            }
        }

        public static int CountPost(int postTypeId,
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
                                   string keywords,
                                    bool showLegacy)
        {
            return new DataLayer().CountPost(postTypeId, roomTypeId, realestateTypeId, countryId, cityId, districtId, personName, phoneNumber, email, gender,
                priceFrom, pPriceTo, dateFrom, dateTo, meterSquareFrom, meterSquareTo, keywords, showLegacy);
        }

        public static List<Post> SearchPostPaging(int postTypeId,
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
                                   string keywords,
                                   bool showLegacy,
                                   int pageSize, int pageNumber, string sortOrder, string sortOrderInvert)
        {
            return new DataLayer().SearchPostPaging(postTypeId, roomTypeId, realestateTypeId, countryId, cityId, districtId, personName, phoneNumber, email, gender,
                priceFrom, pPriceTo, dateFrom, dateTo, meterSquareFrom, meterSquareTo, keywords, showLegacy,
                pageSize, pageNumber, sortOrder, sortOrderInvert);
        }

        public static Post GetPost(int postId, int loadType)
        {
            DataLayer dataLayer = new DataLayer();
            List<Post> result = dataLayer.GetPost(postId);
            if (result != null && result.Count > 0)
            {
                result[0].ImageList = dataLayer.ListImage(null, result[0].PostId, (int)ImageType.Room, loadType);
                return result[0];
            }
            return null;
        }
        #endregion

        #region NoPrice
        public static int CountNoPricePost()
        {
            return new DataLayer().CountNoPricePost();
        }

        public static List<Post> SearchNoPricePostPaging(int pageSize, int pageNumber, string sortOrder, string sortOrderInvert)
        {
            return new DataLayer().SearchNoPricePostPaging(pageSize, pageNumber, sortOrder, sortOrderInvert);
        }
        #endregion
    }
}

