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
    public class RongBayJob : IJob
    {
        public class Constants
        {
            public const string CurrentUser = "Automated:RongBayEngine";
        }

        public RongBayJob() { }

        #region IJob Members

        public void Execute(JobExecutionContext context)
        {

            Logger.Log.Debug("Start Importing RongBay Job...");

            DateTime dateStart = DateTime.Today;
            //if (dateStart.DayOfWeek == DayOfWeek.Monday)
            //{
            //    dateStart = dateStart.AddDays(-2);
            //}
            DateTime dateEnd = DateTime.Today;

            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d116.html", "H.Củ Chi", 80, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d119.html", "H.Nhà Bè", 20, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d120.html", "H.Cần Giờ", 80, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d118.html", "H.Bình Chánh", 19, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d117.html", "H.Hóc Môn", 18, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d114.html", "Q.Thủ Đức", 17, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d115.html", "Q.Bình Tân", 79, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d110.html", "Q.Tân Bình", 14, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d111.html", "Q.Tân Phú", 15, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d109.html", "Q.Gò Vấp", 16, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d113.html", "Q.Phú Nhuận", 13, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d112.html", "Q.Bình Thạnh", 21, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d107.html", "Quận 12", 12, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d106.html", "Quận 11", 11, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d105.html", "Quận 10", 10, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d104.html", "Quận 9", 9, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d103.html", "Quận 8", 8, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d101.html", "Quận 6", 6, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d100.html", "Quận 5", 5, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d99.html", "Quận 4", 4, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d98.html", "Quận 3", 3, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d97.html", "Quận 2", 2, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d96.html", "Quận 1", 1, dateStart, dateEnd);
            GetPostForOneDistrict("http://rongbay.com/TP-HCM/Cho-thue-Nha-o-Nha-tro-Thue-va-cho-thue-nha-c272-t634-n242-d102.html", "Quận 7", 7, dateStart, dateEnd);

            Logger.Log.Debug("End Importing RongBay Job...");
        }

        private void GetPostForOneDistrict(string url, string districtName, int districtId, DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                List<Post> result = new List<Post>();
                List<string> allURLs = GetDetailsURLForOneDistrict(url, districtName, dateStart, dateEnd);
                foreach (string urlDetail in allURLs)
                {

                    Post post = GetDetailPost(urlDetail, districtId);
                    if (post != null)
                    {
                        if (string.IsNullOrEmpty(post.Address))
                        {
                            post.Address = districtName;
                        }
                        result.Add(post);
                    }
                }
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
                    url = url.Replace(".html", string.Format("-trang{0}.html", page));
                }
                
                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument document = web.Load(url);

                HtmlNode matrixResultsNode = RequestHelper.FindElementNodeById(document.DocumentNode, "search_area");
                if (matrixResultsNode != null)
                {
                    List<HtmlNode> itemNodes = RequestHelper.FindAllElementNodeByType(matrixResultsNode, "tr");
                    foreach (HtmlNode itemNode in itemNodes)
                    {
                        List<HtmlNode> tdNodes = RequestHelper.FindAllElementNodeByType(itemNode, "td");
                        if (tdNodes != null && tdNodes.Count == 5)
                        {
                            string district = RequestHelper.GetCleanString(tdNodes[1].InnerText);
                            DateTime parsedDate;
                            if (district == districtName
                                && DateTime.TryParseExact(RequestHelper.GetCleanString(tdNodes[3].InnerText), "dd-MM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                            {
                                if (parsedDate >= dateStart && parsedDate <= dateEnd)
                                {
                                    HtmlNode aNodes = RequestHelper.FindFirstElementNodeByType(tdNodes[0], "a");
                                    if (aNodes != null && aNodes.Attributes["href"] != null)
                                    {
                                        string urlDetail = aNodes.Attributes["href"].Value;
                                        if (!urlDetail.Contains("http:"))
                                        {
                                            urlDetail = "http://rongbay.com" + urlDetail;
                                        }
                                        result.Add(urlDetail);
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
                result.CreatedBy = "RongBayCollector";
                result.ImageList = new List<Image>();

                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument document = web.Load(urlDetail);

                HtmlNode authorNode = RequestHelper.FindElementNodeById(document.DocumentNode, "NewsAuthor");
                if (authorNode != null)
                {
                    HtmlNode mobileNode = RequestHelper.FindElementNodeByClassName(authorNode, "mobilecall");
                    if (mobileNode != null)
                    {
                        HtmlNode spanNode = RequestHelper.FindFirstElementNodeByType(mobileNode, "span");
                        if (spanNode != null)
                        {
                            result.PhoneNumber = RequestHelper.GetStringFromAsciiHex(RequestHelper.GetCleanString(spanNode.InnerText));
                        }
                    }

                    HtmlNode personNode = RequestHelper.FindElementNodeByClassName(authorNode, "detail_personal");
                    if (personNode != null)
                    {
                        HtmlNode aNode = RequestHelper.FindFirstElementNodeByType(personNode, "a");
                        if (aNode != null)
                        {
                            result.PersonName = RequestHelper.GetCleanString(aNode.InnerText);
                        }
                    }
                }

                HtmlNode descriptionNode = RequestHelper.FindElementNodeById(document.DocumentNode, "NewsContent");
                if (descriptionNode != null)
                {
                    string description = string.Empty;
                    List<HtmlNode> pNodes = RequestHelper.FindAllElementNodeByType(descriptionNode, "p");
                    if (pNodes.Count > 0)
                    {
                        foreach (HtmlNode pNode in pNodes)
                        {
                            string paragraph = RequestHelper.GetCleanString(pNode.InnerText);
                            if (!string.IsNullOrEmpty(paragraph))
                            {
                                description += paragraph + "\n";
                            }
                        }
                    }
                    else
                    {
                        description = RequestHelper.GetCleanString(descriptionNode.InnerText);
                    }
                    description = RequestHelper.SplitLongStringIntoMultilines(description);
                    result.Description = description;

                    result.Price = RequestHelper.ParsePrice(description);
                    if (result.Price >= 10)
                    {
                        result.RoomTypeId = (int)RoomTypes.Exclusive;
                    }

                    if (!result.Price.HasValue)
                    {
                        return null;
                    }
                }

                HtmlNode imgNode = RequestHelper.FindElementNodeById(document.DocumentNode, "show_galley_d");
                if (imgNode != null)
                {
                    List<HtmlNode> aNodes = RequestHelper.FindAllElementNodeByType(imgNode, "a");
                    foreach (HtmlNode aNode in aNodes)
                    {
                        if (aNode.Attributes["href"] != null)
                        {
                            string imgLink = aNode.Attributes["href"].Value;
                            if (!imgLink.Contains("http:"))
                            {
                                imgLink = "http://rongbay.com" + imgLink;
                            }
                            using (var client = new WebClient())
                            {
                                byte[] imageFile = client.DownloadData(imgLink);
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
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }
            return result;
        }
        #endregion

    }
}
