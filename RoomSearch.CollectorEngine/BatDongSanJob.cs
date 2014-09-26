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

namespace RoomSearch.CollectorEngine
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

            try
            {
                DateTime dateStart = DateTime.Today;
                if (dateStart.DayOfWeek == DayOfWeek.Monday)
                {
                    dateStart = dateStart.AddDays(-2);
                }
                DateTime dateEnd = DateTime.Today;
                
                List<Post> result = new List<Post>();
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-quan-1", "Quận 1", 1, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-quan-2", "Quận 2", 2, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-quan-3", "Quận 3", 3, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-quan-4", "Quận 4", 4, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-quan-5", "Quận 5", 5, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-quan-6", "Quận 6", 6, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-quan-7", "Quận 7", 7, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-quan-8", "Quận 8", 8, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-quan-9", "Quận 9", 9, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-quan-10", "Quận 10", 10, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-quan-11", "Quận 11", 11, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-quan-12", "Quận 12", 12, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-phu-nhuan", "Quận Phú Nhuận", 13, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-binh-thanh", "Quận Bình Thạnh", 21, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-tan-binh", "Quận Tân Bình", 14, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-tan-phu", "Quận Tân Phú", 15, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-go-vap", "Quận Gò Vấp", 16, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-thu-duc", "Quận Thủ Đức", 17, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-binh-tan", "Quận Bình Tân", 79, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-binh-chanh", "Huyện Bình Chánh", 19, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-cu-chi", "Huyện Củ Chi", 80, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-nha-be", "Huyện Nhà Bè", 20, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-can-gio", "Huyện Cần Giờ", 80, dateStart, dateEnd));
                result.AddRange(GetPostForOneDistrict("http://batdongsan.com.vn/cho-thue-nha-tro-phong-tro-hoc-mon", "Huyện Hóc Môn", 18, dateStart, dateEnd));

                if (result.Count > 0)
                {
                    BusinessMethods.SavePostList(result);                    
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
            }

            //Logger.Log.Debug("End Importing BatDongSan Job...");
        }

        private List<Post> GetPostForOneDistrict(string url, string districtName, int districtId, DateTime dateStart, DateTime dateEnd)
        {
            List<Post> result = new List<Post>();
            List<string> allURLs = GetDetailsURLForOneDistrict(url, districtName, dateStart, dateEnd);
            foreach (string urlDetail in allURLs)
            {
                Post post = GetDetailPost(urlDetail, districtId);
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

        private Post GetDetailPost(string urlDetail, int districtId)
        {
            Post result = null;
            try
            {
                result = new Post();
                result.PostTypeId = (int)PostTypes.Room;
                result.CityId = 1;
                result.CountryId = 232;
                result.DistrictId = districtId;
                result.RoomTypeId = (int)RoomTypes.Standard;
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
                                    if (priceDec > 100)
                                    {
                                        priceDec = priceDec / 1000;
                                    }
                                    result.Price = priceDec;
                                    if (result.Price >= 10)
                                    {
                                        result.RoomTypeId = (int)RoomTypes.Exclusive;
                                    }
                                }
                                else
                                {
                                    result.Price = 2;
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
                        description = description.Replace("   ", "\n");
                        result.Description += description;
                    }

                    HtmlNode imgNode = RequestHelper.FindElementNodeByClassName(matrixResultsNode, "list-img");
                    if (imgNode != null)
                    {
                        result.ImageList = new List<Image>();
                        List<HtmlNode> nodes = RequestHelper.FindAllElementNodeByType(imgNode, "img");
                        foreach (HtmlNode imgItem in nodes)
                        {
                            if (!string.IsNullOrEmpty(imgItem.Attributes["src"].Value))
                            {
                                string source = imgItem.Attributes["src"].Value;
                                source = source.Replace("guestthumb80x60", "guestthumb745x510");
                                using (var client = new WebClient())
                                {
                                    byte[] imageFile = client.DownloadData(source);
                                    Image newImage = new Image();
                                    newImage.ImageSmallContent = imageFile;
                                    newImage.ImageContent = imageFile;
                                    newImage.FileName = "noname";
                                    newImage.ImageTypeId = (int)ImageType.Room;
                                    newImage.CreatedBy = result.CreatedBy;
                                    result.ImageList.Add(newImage);
                                }
                            }
                        }
                    }

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
