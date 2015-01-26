using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Specialized;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.Drawing;

namespace RoomSearch.CollectorEngine
{
    public class RequestHelper
    {
        public static HtmlNode FindElementNodeByClassName(HtmlNode dtElement, string className)
        {
            HtmlNode result = null;
            if (dtElement.Attributes["class"] != null && dtElement.Attributes["class"].Value == className)
            {
                result = dtElement;
            }
            else
            {
                foreach (HtmlNode node in dtElement.ChildNodes)
                {
                    HtmlNode foundNode = FindElementNodeByClassName(node, className);
                    if (foundNode != null)
                    {
                        result = foundNode;
                        break;
                    }
                }
            }

            return result;
        }

        public static List<HtmlNode> FindAllElementNodeByClassName(HtmlNode dtElement, string className)
        {
            List<HtmlNode> result = new List<HtmlNode>();
            if (dtElement.Attributes["class"] != null && dtElement.Attributes["class"].Value == className)
            {
                result.Add(dtElement);
            }
            
            foreach (HtmlNode node in dtElement.ChildNodes)
            {
                List<HtmlNode> foundNodes = FindAllElementNodeByClassName(node, className);
                if (foundNodes != null)
                {
                    result.AddRange(foundNodes);
                }
            }            

            return result;
        }

        public static HtmlNode FindElementNodeById(HtmlNode dtElement, string id)
        {
            HtmlNode result = null;
            if (dtElement.Id == id)
            {
                result = dtElement;
            }
            else
            {
                foreach (HtmlNode node in dtElement.ChildNodes)
                {
                    HtmlNode foundNode = FindElementNodeById(node, id);
                    if (foundNode != null)
                    {
                        result = foundNode;
                        break;
                    }
                }
            }

            return result;
        }

        public static HtmlNode FindFirstElementNodeByType(HtmlNode dtElement, string type)
        {
            HtmlNode result = null;
            if (dtElement.Name == type)
            {
                result = dtElement;
            }
            else
            {
                foreach (HtmlNode node in dtElement.ChildNodes)
                {
                    HtmlNode foundNode = FindFirstElementNodeByType(node, type);
                    if (foundNode != null)
                    {
                        result = foundNode;
                        break;
                    }
                }
            }

            return result;
        }

        public static List<HtmlNode> FindAllElementNodeByType(HtmlNode dtElement, string type)
        {
            List<HtmlNode> result = new List<HtmlNode>();
            if (dtElement.Name == type)
            {
                result.Add(dtElement);
            }

            foreach (HtmlNode node in dtElement.ChildNodes)
            {
                List<HtmlNode> foundNodes = FindAllElementNodeByType(node, type);
                if (foundNodes != null)
                {
                    result.AddRange(foundNodes);
                }
            }

            return result;
        }

        public static string SubmitGetRequest(string url, NameValueCollection headers, bool autoRedirect)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            httpWebRequest.Method = "GET";
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:16.0) Gecko/20100101 Firefox/16.0";
            httpWebRequest.KeepAlive = true;

            httpWebRequest.Headers.Add("Accept-Encoding", "gzip");
            httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;

            if (headers != null)
            {
                httpWebRequest.Headers.Add(headers);
            }
            httpWebRequest.AllowAutoRedirect = autoRedirect;

            Task<WebResponse> responseTask = Task.Factory.FromAsync<WebResponse>(httpWebRequest.BeginGetResponse, httpWebRequest.EndGetResponse, null);
            using (var responseStream = responseTask.Result.GetResponseStream())
            {
                var reader = new StreamReader(responseStream);
                return reader.ReadToEnd();
            }
        }

        public static string SubmitJsonPostRequest(string url, NameValueCollection headers, string jsonData)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";

            if (headers == null)
            {
                headers = new NameValueCollection();
            }
            headers.Add("Cache-Control", "no-cache, no-cache");
            headers.Add("Pragma", "no-cache");

            httpWebRequest.Accept = "application/json, text/javascript, */*; q=0.01";
            headers.Add("Accept-Encoding", "gzip");
            httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;

            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:16.0) Gecko/20100101 Firefox/16.0";
            httpWebRequest.Headers.Add(headers);

            if (!string.IsNullOrEmpty(jsonData))
            {
                byte[] requestBytes = Encoding.UTF8.GetBytes(jsonData);
                httpWebRequest.ContentLength = requestBytes.Length;

                using (var requestStream = httpWebRequest.GetRequestStream())
                {
                    requestStream.Write(requestBytes, 0, requestBytes.Length);
                    requestStream.Close();
                }
            }
            else
            {
                httpWebRequest.ContentLength = 0;
            }

            Task<WebResponse> responseTask = Task.Factory.FromAsync<WebResponse>(httpWebRequest.BeginGetResponse, httpWebRequest.EndGetResponse, null);
            using (var responseStream = responseTask.Result.GetResponseStream())
            {
                var reader = new StreamReader(responseStream);
                return reader.ReadToEnd();
            }
        }

        public static string SubmitPostRequest(string url, NameValueCollection headers, NameValueCollection parameters, bool expect100Continue)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Method = "POST";

            if (headers == null)
            {
                headers = new NameValueCollection();
            }
            //headers.Add("Cache-Control", "no-cache");
            //headers.Add("Pragma", "no-cache");

            httpWebRequest.Accept = "application/json, text/javascript, */*; q=0.01";
            headers.Add("Accept-Encoding", "gzip");
            httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;

            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:16.0) Gecko/20100101 Firefox/16.0";

            httpWebRequest.KeepAlive = true;
            httpWebRequest.ServicePoint.Expect100Continue = expect100Continue;
            httpWebRequest.Headers.Add(headers);

            if (parameters != null)
            {
                var sb = new StringBuilder();
                foreach (var key in parameters.AllKeys)
                {
                    sb.Append(key + "=" + parameters[key] + "&");
                }
                sb.Length = sb.Length - 1;

                byte[] requestBytes = Encoding.UTF8.GetBytes(sb.ToString());
                httpWebRequest.ContentLength = requestBytes.Length;

                using (var requestStream = httpWebRequest.GetRequestStream())
                {
                    requestStream.Write(requestBytes, 0, requestBytes.Length);
                    requestStream.Close();
                }
            }
            else
            {
                httpWebRequest.ContentLength = 0;
            }

            Task<WebResponse> responseTask = Task.Factory.FromAsync<WebResponse>(httpWebRequest.BeginGetResponse, httpWebRequest.EndGetResponse, null);
            using (var responseStream = responseTask.Result.GetResponseStream())
            {
                var reader = new StreamReader(responseStream);
                return reader.ReadToEnd();
            }
        }

        public static string GetCleanString(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return text.Replace("/r", "").Replace("/n", "").Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        public static decimal? ParsePrice(string descriptionOrg)
        {
            string description = descriptionOrg.ToLower();
            decimal? price = null;
            try
            {
                int index1 = description.IndexOf("giá:");
                int index2 = description.IndexOf("giá :");
                int index20 = description.IndexOf("giá thuê");
                int index21 = description.IndexOf("giá cho thuê");
                int index3 = description.IndexOf("/tháng");
                int index4 = description.IndexOf("/thang");
                int index5 = description.IndexOf("/ tháng");
                int index6 = description.IndexOf("/ thang");
                int index7 = description.IndexOf("/ 1 tháng");
                int index8 = description.IndexOf("/ 1 thang");
                int index9 = description.IndexOf("/th ");
                int index10 = description.IndexOf("/ th ");
                int index11 = description.IndexOf("/1th ");
                int index12 = description.IndexOf("/ 1th ");                
                int index100 = description.IndexOf("giá ");
                int index101 = description.IndexOf("gía ");

                if (!string.IsNullOrEmpty(description)
                    && (index1 > 0 || index2 > 0 || index21 > 0 || index3 > 0 || index4 > 0
                    || index5 > 0 || index6 > 0 || index7 > 0 || index8 > 0
                    || index9 > 0 || index10 > 0 || index11 > 0 || index12 > 0))
                {
                    string subDescription = string.Empty;
                    if (index1 > 0)
                    {
                        subDescription = description.Substring(index1, 30);
                    }
                    else if (index2 > 0)
                    {
                        subDescription = description.Substring(index2, 30);
                    }
                    else if (index20 > 0)
                    {
                        subDescription = description.Substring(index20, 30);
                    }
                    else if (index21 > 0)
                    {
                        subDescription = description.Substring(index21, 30);
                    }
                    else if (index3 > 0)
                    {
                        subDescription = description.Substring(index3 - 15, 15);
                    }
                    else if (index4 > 0)
                    {
                        subDescription = description.Substring(index4 - 15, 15);
                    }
                    else if (index5 > 0)
                    {
                        subDescription = description.Substring(index5 - 15, 15);
                    }
                    else if (index6 > 0)
                    {
                        subDescription = description.Substring(index6 - 15, 15);
                    }
                    else if (index7 > 0)
                    {
                        subDescription = description.Substring(index7 - 15, 15);
                    }
                    else if (index8 > 0)
                    {
                        subDescription = description.Substring(index8 - 15, 15);
                    }
                    else if (index9 > 0)
                    {
                        subDescription = description.Substring(index9 - 15, 15);
                    }
                    else if (index10 > 0)
                    {
                        subDescription = description.Substring(index10 - 15, 15);
                    }
                    else if (index11 > 0)
                    {
                        subDescription = description.Substring(index11 - 15, 15);
                    }
                    else if (index12 > 0)
                    {
                        subDescription = description.Substring(index12 - 15, 15);
                    }
                    else if (index100 > 0)
                    {
                        subDescription = description.Substring(index100, 30);
                    }
                    else if (index101 > 0)
                    {
                        subDescription = description.Substring(index101, 30);
                    }
                    

                    if (subDescription.Length > 30)
                    {
                        subDescription = subDescription.Substring(0, 30);
                    }

                    price = ParseNumberOnly(subDescription);

                    //if (Regex.IsMatch(subDescription, @"\d+tr\d+", RegexOptions.IgnoreCase))
                    //{
                    //    Match match = Regex.Match(subDescription, @"\d+tr\d*", RegexOptions.IgnoreCase);
                    //    string[] stringSeparators = new string[] { "tr" };
                    //    string[] values = match.Value.Split(stringSeparators, StringSplitOptions.None);
                    //    if (values.Length > 0)
                    //    {
                    //        price = decimal.Parse(values[0]);
                    //        if (values.Length > 1)
                    //        {
                    //            price = price + decimal.Parse(values[1].Substring(0, 1)) / 10;
                    //        }
                    //    }
                    //}
                    //else if (Regex.IsMatch(subDescription, @"\d+.\d+tr", RegexOptions.IgnoreCase))
                    //{
                    //    Match match = Regex.Match(subDescription, @"\d+.\d+tr", RegexOptions.IgnoreCase);
                    //    string value = match.Value;
                    //    price = decimal.Parse(value.Substring(0, value.Length - 2));
                    //}
                    //else if (Regex.IsMatch(subDescription, @"\d+.\d+ triệu", RegexOptions.IgnoreCase))
                    //{
                    //    Match match = Regex.Match(subDescription, @"\d+.\d+ triệu", RegexOptions.IgnoreCase);
                    //    string value = match.Value;
                    //    price = decimal.Parse(value.Substring(0, value.Length - 6));
                    //}
                    //else if (Regex.IsMatch(subDescription, @"\d+.\d+ trieu", RegexOptions.IgnoreCase))
                    //{
                    //    Match match = Regex.Match(subDescription, @"\d+.\d+ trieu", RegexOptions.IgnoreCase);
                    //    string value = match.Value;
                    //    price = decimal.Parse(value.Substring(0, value.Length - 6));
                    //}
                    //else if (Regex.IsMatch(subDescription, @"\d+ triệu", RegexOptions.IgnoreCase))
                    //{
                    //    Match match = Regex.Match(subDescription, @"\d+ triệu", RegexOptions.IgnoreCase);
                    //    string value = match.Value;
                    //    price = decimal.Parse(value.Substring(0, value.Length - 6));
                    //}
                    //else if (Regex.IsMatch(subDescription, @"\d+ trieu", RegexOptions.IgnoreCase))
                    //{
                    //    Match match = Regex.Match(subDescription, @"\d+ trieu", RegexOptions.IgnoreCase);
                    //    string value = match.Value;
                    //    price = decimal.Parse(value.Substring(0, value.Length - 6));
                    //}
                    //else if (Regex.IsMatch(subDescription, @"\d+ usd", RegexOptions.IgnoreCase))
                    //{
                    //    Match match = Regex.Match(subDescription, @"\d+ usd", RegexOptions.IgnoreCase);
                    //    string value = match.Value;
                    //    price = decimal.Parse(value.Substring(0, value.Length - 4));
                    //    price = price * 0.022M;
                    //}
                    //else if (Regex.IsMatch(subDescription, @"\d+usd", RegexOptions.IgnoreCase))
                    //{
                    //    Match match = Regex.Match(subDescription, @"\d+usd", RegexOptions.IgnoreCase);
                    //    string value = match.Value;
                    //    price = decimal.Parse(value.Substring(0, value.Length - 3));
                    //    price = price * 0.022M;
                    //}
                    //else if (Regex.IsMatch(subDescription, @"\d+.\d{3}.\d{3}đ", RegexOptions.IgnoreCase))
                    //{
                    //    Match match = Regex.Match(subDescription, @"\d+.\d{3}.\d{3}đ", RegexOptions.IgnoreCase);
                    //    string value = match.Value;
                    //    string[] array = value.Split('.');
                    //    price = decimal.Parse(array[0]);
                    //    price = price + decimal.Parse(array[1]) / 1000;
                    //}
                    //else if (Regex.IsMatch(subDescription, @"\d+.\d{3}.\d{3}d", RegexOptions.IgnoreCase))
                    //{
                    //    Match match = Regex.Match(subDescription, @"\d+.\d{3}.\d{3}d", RegexOptions.IgnoreCase);
                    //    string value = match.Value;
                    //    string[] array = value.Split('.');
                    //    price = decimal.Parse(array[0]);
                    //    price = price + decimal.Parse(array[1]) / 1000;
                    //}
                    //else if (Regex.IsMatch(subDescription, @"\d+tr", RegexOptions.IgnoreCase))
                    //{
                    //    Match match = Regex.Match(subDescription, @"\d+tr", RegexOptions.IgnoreCase);
                    //    string value = match.Value;
                    //    price = decimal.Parse(value.Substring(0, value.Length - 2));
                    //}
                }
            }
            catch (Exception ex)
            {
                price = null;
            }
            return price;
        }

        public static decimal? ParseNumberOnly(string description)
        {
            char[] array = description.ToArray();
            string result = string.Empty;
            decimal? price = null;
            for (int i = 0; i < description.Length; i++)
            {
                char character = array[i];
                if (char.IsDigit(character))
                {
                    result += character;
                }
                else if (character == '.' && !result.Contains("."))
                {
                    result += character;
                }
                else if (character == ',' && !result.Contains("."))
                {
                    result += ".";
                }
                else if (!string.IsNullOrEmpty(result))
                {
                    break;
                }
            }
            if (!string.IsNullOrEmpty(result))
            {
                price = decimal.Parse(result);
                if (description.Contains("$") || description.Contains("usd"))
                {
                    if (price < 100)
                    {
                        price = price * 1000;
                    }
                    price = price * 0.022M;
                }
            }
            return price;
        }

        public static string GetStringFromAsciiHex(string input)
        {
            string result = string.Empty;

            try
            {
                string[] array = input.Split(';');
                foreach (string item in array)
                {
                    result += GetNumberFromHexCode(item);
                }
            }
            catch (Exception ex)
            {
                
            }
            return result;
        }

        public static string GetNumberFromHexCode(string input)
        {
            string result = string.Empty;            
            if (input == "&#x20")
            {
                result = string.Empty;
            }
            else if (input == "&#x30")
            {
                result = "0"; 
            }
            else if (input == "&#x31")
            {
                result = "1";
            }
            else if (input == "&#x32")
            {
                result = "2";
            }
            else if (input == "&#x33")
            {
                result = "3";
            }
            else if (input == "&#x34")
            {
                result = "4";
            }
            else if (input == "&#x35")
            {
                result = "5";
            }
            else if (input == "&#x36")
            {
                result = "6";
            }
            else if (input == "&#x37")
            {
                result = "7";
            }
            else if (input == "&#x38")
            {
                result = "8";
            }
            else if (input == "&#x39")
            {
                result = "9";
            }

            return result;
        }

        public static MemoryStream ResizeFromStream(int maxSideSize, Stream inputBuffer)
        {
            int intNewWidth;
            int intNewHeight;
            System.Drawing.Image imgInput = System.Drawing.Image.FromStream(inputBuffer);

            // GET IMAGE FORMAT
            ImageFormat fmtImageFormat = imgInput.RawFormat;

            // GET ORIGINAL WIDTH AND HEIGHT
            int intOldWidth = imgInput.Width;
            int intOldHeight = imgInput.Height;

            // IS LANDSCAPE OR PORTRAIT ?? 
            int intMaxSide;

            if (intOldWidth >= intOldHeight)
            {
                intMaxSide = intOldWidth;
            }
            else
            {
                intMaxSide = intOldHeight;
            }


            if (intMaxSide > maxSideSize)
            {
                // SET NEW WIDTH AND HEIGHT
                double dblCoef = maxSideSize / (double)intMaxSide;
                intNewWidth = Convert.ToInt32(dblCoef * intOldWidth);
                intNewHeight = Convert.ToInt32(dblCoef * intOldHeight);
            }
            else
            {
                intNewWidth = intOldWidth;
                intNewHeight = intOldHeight;
            }

            MemoryStream outputStream = new MemoryStream();
            using (System.Drawing.Image img = System.Drawing.Image.FromStream(inputBuffer))
            {
                using (Bitmap bitmap = new Bitmap(img, intNewWidth, intNewHeight))
                {
                    bitmap.Save(outputStream, ImageFormat.Jpeg);
                }
            }

            return outputStream;
        }


        public static byte[] ResizeImageByteArray(int maxSideSize, byte[] originalImage)
        {
            MemoryStream stream = new MemoryStream(originalImage);
            MemoryStream resizeedStream = ResizeFromStream(maxSideSize, stream);
            return resizeedStream.ToArray();
        }

        public static string SplitLongStringIntoMultilines(string description)
        {
            if (!string.IsNullOrEmpty(description))
            {
                description = description.Replace("   ", "\n");
                description = description.Replace(". ", ". \n");
                description = description.Replace("-", "\n -");
                description = description.Replace("+", "\n +");
            }
            return description;
        }
    }
}
