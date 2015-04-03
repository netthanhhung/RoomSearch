using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using System.IO;
using System.Collections.Specialized;
using RoomSearch.Common;
using HtmlAgilityPack;
using System.Globalization;
using RoomSearch.Business;
using System.Net;

namespace RoomSearch.CollectorEngine.Realestate
{
    public class BatDongSanJob : IJob
    {
        public class Constants
        {
            public const string CurrentUser = "Automated:BatDongSanEngine";
        }

        public BatDongSanJob() { }

        #region IJob Members

        public void Execute(JobExecutionContext context)
        {

            Logger.Log.Debug("Start Importing BatDongSan Job...");

            DateTime dateStart = DateTime.Today;
            //if (dateStart.DayOfWeek == DayOfWeek.Monday)
            //{
            //    dateStart = dateStart.AddDays(-2);
            //}
            DateTime dateEnd = DateTime.Today;

            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-cu-chi", 1, "Huyện Củ Chi", 80, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-can-gio", 1, "Huyện Cần Giờ", 80, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-hoc-mon", 1, "Huyện Hóc Môn", 18, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-nha-be", 1, "Huyện Nhà Bè", 20, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-binh-chanh", 1, "Huyện Bình Chánh", 19, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-binh-tan", 1, "Quận Bình Tân", 79, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-thu-duc", 1, "Quận Thủ Đức", 17, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-go-vap", 1, "Quận Gò Vấp", 16, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-tan-phu", 1, "Quận Tân Phú", 15, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-tan-binh", 1, "Quận Tân Bình", 14, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-binh-thanh", 1, "Quận Bình Thạnh", 21, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-phu-nhuan", 1, "Quận Phú Nhuận", 13, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-quan-12", 1, "Quận 12", 12, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-quan-11", 1, "Quận 11", 11, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-quan-10", 1, "Quận 10", 10, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-quan-9", 1, "Quận 9", 9, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-quan-8", 1, "Quận 8", 8, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-quan-6", 1, "Quận 6", 6, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-quan-5", 1, "Quận 5", 5, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-quan-4", 1, "Quận 4", 4, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-quan-3", 1, "Quận 3", 3, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-quan-1", 1, "Quận 1", 1, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-quan-2", 1, "Quận 2", 2, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-quan-7", 1, "Quận 7", 7, dateStart, dateEnd);

            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-cam-le-ddn", 4, "Cam Le", 82, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-hai-chau-ddn", 4, "Hai Chau", 83, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-lien-chieu-ddn", 4, "Lien Chieu", 84, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-ngu-hanh-son-ddn", 4, "Ngu Hanh Son", 85, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-son-tra-ddn", 4, "Son Tra", 86, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-thanh-khe-ddn", 4, "Thanh Khe", 87, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-hoa-vang-ddn", 4, "Hoa Vang", 88, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-hoang-sa-ddn", 4, "Hoang Sa", 89, dateStart, dateEnd);
            GetPostForOneDistrict("http://batdongsan.com.vn/{0}-nha-trang-kh", 6, "Nha Trang", 90, dateStart, dateEnd);

            Logger.Log.Debug("End Importing BatDongSan Job...");
        }

        private void GetPostForOneDistrict(string url, int cityId, string districtName, int districtId, DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                List<Post> result = new List<Post>();
                string realURL = string.Format(url, "ban-can-ho-chung-cu");
                result.AddRange(GetPostForOneDistrictOneType(realURL, (int)RealestateTypes.NhaChungCu, cityId, districtName, districtId, dateStart, dateEnd));
                realURL = string.Format(url, "ban-nha-rieng");
                result.AddRange(GetPostForOneDistrictOneType(realURL, (int)RealestateTypes.NhaPho, cityId, districtName, districtId, dateStart, dateEnd));
                realURL = string.Format(url, "ban-nha-mat-pho");
                result.AddRange(GetPostForOneDistrictOneType(realURL, (int)RealestateTypes.NhaPho, cityId, districtName, districtId, dateStart, dateEnd));
                realURL = string.Format(url, "ban-nha-biet-thu-lien-ke");
                result.AddRange(GetPostForOneDistrictOneType(realURL, (int)RealestateTypes.NhaBietThu, cityId, districtName, districtId, dateStart, dateEnd));

                realURL = string.Format(url, "ban-dat");
                result.AddRange(GetPostForOneDistrictOneType(realURL, (int)RealestateTypes.DatThocu, cityId, districtName, districtId, dateStart, dateEnd));
                realURL = string.Format(url, "ban-dat-nen-du-an");
                result.AddRange(GetPostForOneDistrictOneType(realURL, (int)RealestateTypes.DatNenDuAn, cityId, districtName, districtId, dateStart, dateEnd));
                realURL = string.Format(url, "ban-trang-trai-khu-nghi-duong");
                result.AddRange(GetPostForOneDistrictOneType(realURL, (int)RealestateTypes.DatTrangTrai, cityId, districtName, districtId, dateStart, dateEnd));
                realURL = string.Format(url, "ban-kho-nha-xuong");
                result.AddRange(GetPostForOneDistrictOneType(realURL, (int)RealestateTypes.DatTrangTrai, cityId, districtName, districtId, dateStart, dateEnd));

                if (result.Count > 0)
                {
                    BusinessMethods.SavePostList(result);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
            }


        }

        private List<Post> GetPostForOneDistrictOneType(string url, int realestateTypeId, int cityId, string districtName, int districtId, DateTime dateStart, DateTime dateEnd)
        {
            List<Post> result = new List<Post>();
            List<string> allURLs = GetDetailsURLForOneDistrict(url, districtName, dateStart, dateEnd);
            foreach (string urlDetail in allURLs)
            {
                Post post = GetDetailPost(urlDetail, realestateTypeId, cityId, districtId);
                if (post != null)
                {
                    result.Add(post);
                }
            }
            return result;
        }



        private List<string> GetDetailsURLForOneDistrict(string url, string districtName, DateTime dateStart, DateTime dateEnd)
        {
            List<string> result = new List<string>();
            int page = dateEnd.Subtract(dateStart).Days + 1;
            for (int i = 1; i < page + 1; i++)
            {
                result.AddRange(GetDetailsURLForOnePage(url, districtName, dateStart, dateEnd, i));
            }

            return result;
        }

        private List<string> GetDetailsURLForOnePage(string url, string districtName, DateTime dateStart, DateTime dateEnd, int page)
        {
            List<string> result = new List<string>();
            try
            {
                if (page > 1)
                {
                    url = url + "/p" + page;
                }
                
                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument document = web.Load(url);

                HtmlNode matrixResultsNode = RequestHelper.FindElementNodeByClassName(document.DocumentNode, "Main");
                if (matrixResultsNode != null)
                {
                    foreach (HtmlNode divParent in matrixResultsNode.ChildNodes)
                    {
                        if (divParent.Name != "div")
                        {
                            continue;
                        }
                        if (divParent.Attributes["class"] != null
                            && divParent.Attributes["class"].Value.Contains("vip"))
                        {
                            HtmlNode titleNode = RequestHelper.FindElementNodeByClassName(divParent, "p-title");
                            HtmlNode dateNode = RequestHelper.FindElementNodeByClassName(divParent, "floatright mar-right-10");
                            DateTime parsedDate;
                            if (titleNode != null && dateNode != null
                                && DateTime.TryParseExact(dateNode.InnerText, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                            {
                                if (parsedDate >= dateStart && parsedDate <= dateEnd)
                                {
                                    foreach (HtmlNode linkItem in titleNode.ChildNodes)
                                    {
                                        if (linkItem.Name == "a" && linkItem.Attributes["href"] != null)
                                        {
                                            string urlDetail = linkItem.Attributes["href"].Value;
                                            if (!urlDetail.Contains("http:"))
                                            {
                                                urlDetail = "http://batdongsan.com.vn" + urlDetail;
                                            }
                                            result.Add(urlDetail);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }
            return result;
        }

        private Post GetDetailPost(string urlDetail, int realestateTypeId, int cityId, int districtId)
        {
            Post result = null;
            try
            {
                result = new Post();
                result.PostTypeId = (int)PostTypes.House;
                result.Gender = 0; //bán
                result.CityId = cityId;
                result.CountryId = 232;
                result.DistrictId = districtId;
                result.RealestateTypeId = realestateTypeId;
                result.AvailableRooms = 1;
                result.CreatedBy = "BatDongSanCollector";

                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument document = web.Load(urlDetail);

                HtmlNode matrixResultsNode = RequestHelper.FindElementNodeById(document.DocumentNode, "product-detail");
                if (matrixResultsNode != null)
                {
                    HtmlNode titleNode = RequestHelper.FindElementNodeByClassName(matrixResultsNode, "pm-title");
                    if (titleNode != null)
                    {
                        result.Description = titleNode.InnerText;
                    }

                    HtmlNode chitietNode = RequestHelper.FindElementNodeByClassName(matrixResultsNode, "kqchitiet");
                    if (chitietNode != null)
                    {
                        List<HtmlNode> nodes = RequestHelper.FindAllElementNodeByType(chitietNode, "strong");
                        if (nodes != null && nodes.Count >= 2)
                        {
                            string price = RequestHelper.GetCleanString(nodes[0].InnerText);
                            string[] array = price.Split(' ');
                            if (array.Length > 0 && !string.IsNullOrEmpty(array[0]))
                            {
                                string priceReal = array[0];
                                decimal priceDec;
                                if (decimal.TryParse(priceReal, out priceDec))
                                {
                                    if (!string.IsNullOrEmpty(array[1]) && array[1].Contains("tỷ"))
                                    {
                                        priceDec = priceDec * 1000;
                                    }
                                    result.Price = priceDec;
                                    if (result.Price >= 10)
                                    {
                                        result.RoomTypeId = (int)RoomTypes.Exclusive;
                                    }
                                }
                                else
                                {
                                    result.Price = 2000;
                                }
                            }

                            string square = RequestHelper.GetCleanString(nodes[1].InnerText);
                            string[] arrayS = square.Split('m');
                            if (array.Length > 0 && !string.IsNullOrEmpty(arrayS[0]))
                            {
                                string squareReal = arrayS[0];
                                decimal squareDec;
                                if (decimal.TryParse(squareReal, out squareDec))
                                {
                                    result.MeterSquare = squareDec;
                                }
                                else
                                {
                                    result.MeterSquare = 20;
                                }
                            }
                        }
                    }


                    HtmlNode desNode = RequestHelper.FindElementNodeByClassName(matrixResultsNode, "pm-content stat");
                    if (desNode != null)
                    {
                        string description = desNode.InnerText;
                        int index1 = description.IndexOf("Tìm kiếm theo từ khóa");
                        if (!string.IsNullOrEmpty(description) && (index1 > 0))
                        {
                            description = description.Substring(0, index1 - 1);                            
                        }
                        description = RequestHelper.SplitLongStringIntoMultilines(description);
                        result.Description += description;
                    }

                    //HtmlNode imgNode = RequestHelper.FindElementNodeByClassName(matrixResultsNode, "list-img");
                    //if (imgNode != null)
                    //{
                    //    result.ImageList = new List<Image>();
                    //    List<HtmlNode> nodes = RequestHelper.FindAllElementNodeByType(imgNode, "img");
                    //    foreach (HtmlNode imgItem in nodes)
                    //    {
                    //        if (!string.IsNullOrEmpty(imgItem.Attributes["src"].Value))
                    //        {
                    //            string source = imgItem.Attributes["src"].Value;
                    //            source = source.Replace("guestthumb80x60", "guestthumb745x510");
                    //            using (var client = new WebClient())
                    //            {
                    //                byte[] imageFile = client.DownloadData(source);
                    //                Image newImage = new Image();
                    //                newImage.ImageSmallContent = RequestHelper.ResizeImageByteArray(100, imageFile);
                    //                newImage.ImageContent = RequestHelper.ResizeImageByteArray(400, imageFile);
                    //                newImage.FileName = "noname";
                    //                newImage.ImageTypeId = (int)ImageType.Room;
                    //                newImage.CreatedBy = result.CreatedBy;
                    //                result.ImageList.Add(newImage);
                    //            }
                    //        }
                    //    }
                    //}

                    HtmlNode detailNode = RequestHelper.FindElementNodeByClassName(matrixResultsNode, "pm-content-detail");
                    if (detailNode != null)
                    {
                        HtmlNode leftDetailNode = RequestHelper.FindElementNodeByClassName(detailNode, "left-detail");
                        if (leftDetailNode != null)
                        {
                            HtmlNode addressNode = RequestHelper.FindElementNodeByClassName(leftDetailNode, "right");
                            if (addressNode != null)
                            {
                                result.Address = RequestHelper.GetCleanString(addressNode.InnerText);
                            }
                        }
                    }

                    HtmlNode cusInfoNode = RequestHelper.FindElementNodeById(matrixResultsNode, "divCustomerInfo");
                    if (cusInfoNode != null)
                    {
                        List<HtmlNode> nodes = RequestHelper.FindAllElementNodeByClassName(cusInfoNode, "right-content");
                        foreach (HtmlNode item in nodes)
                        {
                            HtmlNode leftNode = RequestHelper.FindElementNodeByClassName(item, "normalblue left");
                            HtmlNode rightNode = RequestHelper.FindElementNodeByClassName(item, "right");
                            if (leftNode != null && rightNode != null)                            
                            {
                                if (RequestHelper.GetCleanString(leftNode.InnerText) == "Tên liên lạc")
                                {
                                    result.PersonName = RequestHelper.GetCleanString(rightNode.InnerText);
                                }
                                if (RequestHelper.GetCleanString(leftNode.InnerText) == "Điện thoại"
                                    || RequestHelper.GetCleanString(leftNode.InnerText) == "Mobile")
                                {
                                    result.PhoneNumber = RequestHelper.GetCleanString(rightNode.InnerText);
                                }
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }
            return result;
        }
        #endregion

    }
}
