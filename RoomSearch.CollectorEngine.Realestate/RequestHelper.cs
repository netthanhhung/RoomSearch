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

namespace RoomSearch.CollectorEngine.Realestate
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

        public static decimal ParsePrice(string description)
        {
            decimal price = 2.0M;
            try
            {
                int index1 = description.IndexOf("giá:");
                int index2 = description.IndexOf("giá :");
                int index3 = description.IndexOf("triệu/tháng");
                int index4 = description.IndexOf("trieu/thang");
                int index5 = description.IndexOf("usd/thang");

                if (!string.IsNullOrEmpty(description)
                    && (index1 > 0 || index2 > 0 || index3 > 0 || index4 > 0))
                {
                    string subDescription = string.Empty;
                    if (index1 > 0)
                    {
                        subDescription = description.Substring(index1 + 4);
                    }
                    if (index2 > 0)
                    {
                        subDescription = description.Substring(index2 + 4);
                    }
                    else if (index3 > 0)
                    {
                        subDescription = description.Substring(index3 - 4, 12);
                    }
                    else if (index4 > 0)
                    {
                        subDescription = description.Substring(index3 - 4, 12);
                    }
                    else if (index5 > 0)
                    {
                        subDescription = description.Substring(index5 - 4, 10);
                    }

                    if (subDescription.Length > 30)
                    {
                        subDescription = subDescription.Substring(0, 30);
                    }
                    if (Regex.IsMatch(subDescription, @"\d+tr\d+", RegexOptions.IgnoreCase))
                    {
                        Match match = Regex.Match(subDescription, @"\d+tr\d*", RegexOptions.IgnoreCase);
                        string[] stringSeparators = new string[] { "tr" };
                        string[] values = match.Value.Split(stringSeparators, StringSplitOptions.None);
                        if (values.Length > 0)
                        {
                            price = decimal.Parse(values[0]);
                            if (values.Length > 1)
                            {
                                price = price + decimal.Parse(values[1].Substring(0, 1)) / 10;
                            }
                        }
                    }
                    else if (Regex.IsMatch(subDescription, @"\d+.\d+tr", RegexOptions.IgnoreCase))
                    {
                        Match match = Regex.Match(subDescription, @"\d+.\d+tr", RegexOptions.IgnoreCase);
                        string value = match.Value;
                        price = decimal.Parse(value.Substring(0, value.Length - 2));
                    }
                    else if (Regex.IsMatch(subDescription, @"\d+.\d+ triệu", RegexOptions.IgnoreCase))
                    {
                        Match match = Regex.Match(subDescription, @"\d+.\d+ triệu", RegexOptions.IgnoreCase);
                        string value = match.Value;
                        price = decimal.Parse(value.Substring(0, value.Length - 6));
                    }
                    else if (Regex.IsMatch(subDescription, @"\d+.\d+ trieu", RegexOptions.IgnoreCase))
                    {
                        Match match = Regex.Match(subDescription, @"\d+.\d+ trieu", RegexOptions.IgnoreCase);
                        string value = match.Value;
                        price = decimal.Parse(value.Substring(0, value.Length - 6));
                    }
                    else if (Regex.IsMatch(subDescription, @"\d+ triệu", RegexOptions.IgnoreCase))
                    {
                        Match match = Regex.Match(subDescription, @"\d+ triệu", RegexOptions.IgnoreCase);
                        string value = match.Value;
                        price = decimal.Parse(value.Substring(0, value.Length - 6));
                    }
                    else if (Regex.IsMatch(subDescription, @"\d+ trieu", RegexOptions.IgnoreCase))
                    {
                        Match match = Regex.Match(subDescription, @"\d+ trieu", RegexOptions.IgnoreCase);
                        string value = match.Value;
                        price = decimal.Parse(value.Substring(0, value.Length - 6));
                    }
                    else if (Regex.IsMatch(subDescription, @"\d+ usd", RegexOptions.IgnoreCase))
                    {
                        Match match = Regex.Match(subDescription, @"\d+ usd", RegexOptions.IgnoreCase);
                        string value = match.Value;
                        price = decimal.Parse(value.Substring(0, value.Length - 4));
                        price = price * 0.022M;
                    }
                    else if (Regex.IsMatch(subDescription, @"\d+usd", RegexOptions.IgnoreCase))
                    {
                        Match match = Regex.Match(subDescription, @"\d+usd", RegexOptions.IgnoreCase);
                        string value = match.Value;
                        price = decimal.Parse(value.Substring(0, value.Length - 3));
                        price = price * 0.022M;
                    }
                    else if (Regex.IsMatch(subDescription, @"\d+.\d{3}.\d{3}đ", RegexOptions.IgnoreCase))
                    {
                        Match match = Regex.Match(subDescription, @"\d+.\d{3}.\d{3}đ", RegexOptions.IgnoreCase);
                        string value = match.Value;
                        string[] array = value.Split('.');
                        price = decimal.Parse(array[0]);
                        price = price + decimal.Parse(array[1]) / 1000;
                    }
                    else if (Regex.IsMatch(subDescription, @"\d+.\d{3}.\d{3}d", RegexOptions.IgnoreCase))
                    {
                        Match match = Regex.Match(subDescription, @"\d+.\d{3}.\d{3}d", RegexOptions.IgnoreCase);
                        string value = match.Value;
                        string[] array = value.Split('.');
                        price = decimal.Parse(array[0]);
                        price = price + decimal.Parse(array[1]) / 1000;
                    }
                    else if (Regex.IsMatch(subDescription, @"\d+tr", RegexOptions.IgnoreCase))
                    {
                        Match match = Regex.Match(subDescription, @"\d+tr", RegexOptions.IgnoreCase);
                        string value = match.Value;
                        price = decimal.Parse(value.Substring(0, value.Length - 2));
                    }
                }
            }
            catch (Exception ex)
            {
                price = 2.0M;
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
