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
    public class KenhNhaTroJob : IJob
    {
        public class Constants
        {
            public const string CurrentUser = "Automated:KenhNhaTroEngine";
        }

        public KenhNhaTroJob() { }

        #region IJob Members

        public void Execute(JobExecutionContext context)
        {

            Logger.Log.Debug("Start Importing KenhNhaTro Job...");

            DateTime dateStart = DateTime.Today;
            //if (dateStart.DayOfWeek == DayOfWeek.Monday)
            //{
            //    dateStart = dateStart.AddDays(-2);
            //}
            DateTime dateEnd = DateTime.Today;

            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/huyen-hoc-mon/tp-ho-chi-minh.html", 1, "Huyện Hóc Môn", 18, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/huyen-can-gio/tp-ho-chi-minh.html", 1, "Huyện Cần Giờ", 80, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/huyen-nha-be/tp-ho-chi-minh.html", 1, "Huyện Nhà Bè", 20, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/huyen-cu-chi/tp-ho-chi-minh.html", 1, "Huyện Củ Chi", 80, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/huyen-binh-chanh/tp-ho-chi-minh.html", 1, "Huyện Bình Chánh", 19, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-binh-tan/tp-ho-chi-minh.html", 1, "Quận Bình Tân", 79, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-thu-duc/tp-ho-chi-minh.html", 1, "Quận Thủ Đức", 17, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-go-vap/tp-ho-chi-minh.html", 1, "Quận Gò Vấp", 16, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-tan-phu/tp-ho-chi-minh.html", 1, "Quận Tân Phú", 15, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-tan-binh/tp-ho-chi-minh.html", 1, "Quận Tân Bình", 14, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-binh-thanh/tp-ho-chi-minh.html", 1, "Quận Bình Thạnh", 21, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-phu-nhuan/tp-ho-chi-minh.html", 1, "Quận Phú Nhuận", 13, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-12/tp-ho-chi-minh.html", 1, "Quận 12", 12, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-11/tp-ho-chi-minh.html", 1, "Quận 11", 11, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-10/tp-ho-chi-minh.html", 1, "Quận 10", 10, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-9/tp-ho-chi-minh.html", 1, "Quận 9", 9, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-8/tp-ho-chi-minh.html", 1, "Quận 8", 8, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-7/tp-ho-chi-minh.html", 1, "Quận 7", 7, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-6/tp-ho-chi-minh.html", 1, "Quận 6", 6, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-5/tp-ho-chi-minh.html", 1, "Quận 5", 5, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-4/tp-ho-chi-minh.html", 1, "Quận 4", 4, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-3/tp-ho-chi-minh.html", 1, "Quận 3", 3, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-2/tp-ho-chi-minh.html", 1, "Quận 2", 2, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-1/tp-ho-chi-minh.html", 1, "Quận 1", 1, dateStart, dateEnd);

            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-cam-le/da-nang.html", 4, "Cam Le", 82, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-hai-chau/da-nang.html", 4, "Hai Chau", 83, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-lien-chieu/da-nang.html", 4, "Lien Chieu", 84, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-ngu-hanh-son/da-nang.html", 4, "Ngu Hanh Son", 85, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-son-tra/da-nang.html", 4, "Son Tra", 86, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-thanh-khe/da-nang.html", 4, "Thanh Khe", 87, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-hoa-vang/da-nang.html", 4, "Hoa Vang", 88, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/quan-hoang-sa/da-nang.html", 4, "Hoang Sa", 89, dateStart, dateEnd);
            GetPostForOneDistrict("http://kenhnhatro.com/cho-thue-phong-tro-nha-tro/thanh-pho-nha-trang/khanh-hoa.html", 6, "Nha Trang", 90, dateStart, dateEnd);

            Logger.Log.Debug("End Importing KenhNhaTro Job...");
        }

        private void GetPostForOneDistrict(string url, int cityId, string districtName, int districtId, DateTime dateStart, DateTime dateEnd)
        {

            try
            {
            List<Post> result = new List<Post>();
            List<string> allURLs = GetDetailsURLForOneDistrict(url, districtName, dateStart, dateEnd);
            foreach (string urlDetail in allURLs)
            {
                Post post = GetDetailPost(urlDetail, cityId, districtId);
                if (post != null)
                {
                    result.Add(post);
                }
            }

            if (result.Count > 0)
            {
                RoomSearchServiceHelper.SavePostList(result);
            }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
            }
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
                    url = url.Substring(0, url.Length - 5) + "/page/" + page;
                }
                
                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument document = web.Load(url);

                HtmlNode matrixResultsNode = RequestHelper.FindElementNodeByClassName(document.DocumentNode, "wraplist_ariticle");
                if (matrixResultsNode != null)
                {
                    foreach (HtmlNode divParent in matrixResultsNode.ChildNodes)
                    {
                        if (divParent.Name != "div")
                        {
                            continue;
                        }
                        if (divParent.Attributes["class"] != null
                            && divParent.Attributes["class"].Value == "title")
                        {
                        }
                        else if (divParent.Attributes["id"] != null
                            && divParent.Attributes["id"].Value == "paging")
                        {

                        }
                        else
                        {
                            foreach (HtmlNode ulItem in divParent.ChildNodes)
                            {
                                if (ulItem.Name == "ul")
                                {
                                    foreach (HtmlNode listItem in ulItem.ChildNodes)
                                    {
                                        if (listItem.Name == "li")
                                        {
                                            HtmlNode dateNode = RequestHelper.FindElementNodeByClassName(listItem, "red");
                                            DateTime parsedDate;
                                            if (dateNode != null && DateTime.TryParseExact(dateNode.InnerText, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                                            {
                                                if (parsedDate >= dateStart && parsedDate <= dateEnd)
                                                {
                                                    HtmlNode noidungNode = RequestHelper.FindElementNodeByClassName(listItem, "lnoidung lfloat");
                                                    if (noidungNode != null)
                                                    {
                                                        HtmlNode districtNode = RequestHelper.FindElementNodeByClassName(noidungNode, "clearfix");
                                                        if (districtNode != null && districtNode.ChildNodes != null && districtNode.ChildNodes.Count == 5)
                                                        {
                                                            string district = districtNode.ChildNodes[3].InnerText;
                                                            if (!string.IsNullOrEmpty(district))
                                                            {
                                                                string[] abc = district.Split(',');
                                                                if (abc.Length == 2 && abc[0] == districtName)
                                                                {
                                                                    HtmlNode realURLNode = RequestHelper.FindElementNodeByClassName(noidungNode, "tomtat");
                                                                    if (realURLNode != null)
                                                                    {
                                                                        HtmlNode aHref = RequestHelper.FindFirstElementNodeByType(realURLNode, "a");
                                                                        if (aHref != null && aHref.Attributes["href"] != null)
                                                                        {
                                                                            string urlDetail = aHref.Attributes["href"].Value;
                                                                            result.Add(urlDetail);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
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

        private Post GetDetailPost(string urlDetail, int cityId, int districtId)
        {
            Post result = null;
            try
            {
                result = new Post();
                result.PostTypeId = (int)PostTypes.Room;
                result.CityId = cityId;
                result.CountryId = 232;
                result.DistrictId = districtId;
                result.RoomTypeId = (int)RoomTypes.Standard;
                result.AvailableRooms = 1;
                result.CreatedBy = "KenhNhaTroCollector";

                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument document = web.Load(urlDetail);

                HtmlNode matrixResultsNode = RequestHelper.FindElementNodeByClassName(document.DocumentNode, "detail");
                if (matrixResultsNode != null)
                {
                    HtmlNode titleNode = RequestHelper.FindElementNodeByClassName(matrixResultsNode, "dtitle");
                    if (titleNode != null)
                    {
                        result.Description = titleNode.InnerText;
                    }

                    HtmlNode personNode = RequestHelper.FindElementNodeByClassName(matrixResultsNode, "dtnguoidang");
                    if (personNode != null)
                    {
                        foreach (HtmlNode child in personNode.ChildNodes)
                        {
                            if (child.Name == "span")
                            {
                                result.PersonName = child.InnerText;
                                break;
                            }
                        }
                    }

                    HtmlNode emailNode = RequestHelper.FindElementNodeByClassName(matrixResultsNode, "dtemail");
                    if (emailNode != null)
                    {
                        foreach (HtmlNode child in emailNode.ChildNodes)
                        {
                            if (child.Name == "a")
                            {
                                result.Email = child.InnerText;
                                break;
                            }
                        }
                    }

                    HtmlNode addressNode = RequestHelper.FindElementNodeByClassName(matrixResultsNode, "direction");
                    if (addressNode != null)
                    {
                        HtmlNode parent = addressNode.ParentNode;
                        foreach (HtmlNode child in parent.ChildNodes)
                        {
                            if (child.Name == "span" && child.Attributes["class"].Value == "green")
                            {
                                result.Address = child.InnerText;
                                break;
                            }
                        }
                    }

                    HtmlNode phoneNode = RequestHelper.FindElementNodeByClassName(matrixResultsNode, "dphone info");
                    if (phoneNode != null)
                    {
                        foreach (HtmlNode child in phoneNode.ChildNodes)
                        {
                            if (child.Name == "span")
                            {
                                result.PhoneNumber = child.InnerText;
                                break;
                            }
                        }
                    }

                    HtmlNode priceNode = RequestHelper.FindElementNodeByClassName(matrixResultsNode, "dgia info");
                    if (priceNode != null)
                    {
                        foreach (HtmlNode child in priceNode.ChildNodes)
                        {
                            if (child.Name == "span")
                            {
                                string price = child.InnerText;
                                string[] array = price.Split(' ');
                                if (array.Length > 0 && !string.IsNullOrEmpty(array[0]))
                                {
                                    string priceReal = array[0].Replace(".", "");
                                    decimal priceDec;
                                    if (decimal.TryParse(priceReal, out priceDec))
                                    {
                                        result.Price = priceDec / 1000000;
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
                                break;
                            }
                        }
                    }

                    HtmlNode squreNode = RequestHelper.FindElementNodeByClassName(matrixResultsNode, "ddientich infos");
                    if (squreNode != null)
                    {
                        foreach (HtmlNode child in squreNode.ChildNodes)
                        {
                            if (child.Name == "span")
                            {
                                string price = child.InnerText;
                                string[] array = price.Split(' ');
                                if (array.Length > 0 && !string.IsNullOrEmpty(array[0]))
                                {
                                    string priceReal = array[0].Replace(".", "");
                                    decimal priceDec;
                                    if (decimal.TryParse(priceReal, out priceDec))
                                    {
                                        result.MeterSquare = priceDec / 100;
                                    }
                                    else
                                    {
                                        result.MeterSquare = 20;
                                    }
                                }
                                break;
                            }
                        }
                    }

                    HtmlNode desNode = RequestHelper.FindElementNodeByClassName(matrixResultsNode, "dnoidung");
                    if (desNode != null)
                    {
                        string description = desNode.InnerText;
                        int index1 = description.IndexOf("tags:");
                        int index2 = description.IndexOf("quang cao adsense");
                        if (!string.IsNullOrEmpty(description) && (index1 > 0 || index2 > 0))
                        {
                            if (index1 > 0)
                            {
                                description = description.Substring(0, index1 - 1);
                            }
                            else if (index2 > 5)
                            {
                                description = description.Substring(0, index2 - 5);
                            }
                        }
                        description = RequestHelper.SplitLongStringIntoMultilines(description);
                        result.Description += description;
                        HtmlNode imageNode = RequestHelper.FindElementNodeByClassName(matrixResultsNode, "imgnhatro");
                        if (imageNode != null)
                        {
                            result.ImageList = new List<Image>();
                            foreach (HtmlNode child in imageNode.ChildNodes)
                            {
                                if (child.Name == "p")
                                {
                                    foreach (HtmlNode imgItem in child.ChildNodes)
                                    {
                                        if (imgItem.Name == "img" && !string.IsNullOrEmpty(imgItem.Attributes["src"].Value))
                                        {
                                            string source = imgItem.Attributes["src"].Value;
                                            using (var client = new WebClient())
                                            {
                                                byte[] imageFile = client.DownloadData(source);
                                                Image newImage = new Image();
                                                newImage.ImageSmallContent = RequestHelper.ResizeImageByteArray(100, imageFile);
                                                newImage.ImageContent = RequestHelper.ResizeImageByteArray(400, imageFile);
                                                newImage.FileName = "noname";
                                                newImage.ImageTypeId = (int)ImageType.Room;
                                                newImage.CreatedBy = result.CreatedBy;
                                                result.ImageList.Add(newImage);
                                            }
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
        #endregion

    }
}
