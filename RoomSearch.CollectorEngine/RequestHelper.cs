using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Specialized;
using HtmlAgilityPack;

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

    }
}
