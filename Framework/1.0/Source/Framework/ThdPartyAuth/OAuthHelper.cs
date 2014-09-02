using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Cdts.Framework.ThdPartyAuth
{
    public class OAuthHelper : OAuthBase
    {
        #region Properties

        public enum Method { GET, POST, PUT, DELETE };

        public string RequestToken { get; private set; }
        public string Authorize { get; private set; }
        public string AccessToken { get; private set; }

        public string ConsumerKey { get; private set; }
        public string ConsumerSecret { get; private set; }
        public string Token { get; private set; }
        public string TokenSecret { get; set; }
        public string CallbackUrl { get; private set; }
        Action<NameValueCollection> getExtensionData;
        #endregion
        public OAuthHelper(string consumerKey, string consumerSecret, string callbackUrl, string requestToken, string authorize, string accessToken)
            : this(consumerKey, consumerSecret, callbackUrl, requestToken, authorize, accessToken, null)
        {
        }
        public OAuthHelper(string consumerKey, string consumerSecret, string callbackUrl, string requestToken, string authorize, string accessToken, Action<NameValueCollection> getExtensionData)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            CallbackUrl = callbackUrl;
            RequestToken = requestToken;
            Authorize = authorize;
            AccessToken = accessToken;
            this.getExtensionData = getExtensionData;
        }
        /// <summary>
        /// 获取未授权的Request Token，并请求用户授权Request Token
        /// </summary>
        public string AuthorizationGet()
        {
            string ret = null;
            //获取未授权的Request Token
            string response = oAuthWebRequest(Method.GET, RequestToken, String.Empty);
            if (response.Length > 0)
            {
                NameValueCollection qs = HttpUtility.ParseQueryString(response);
                if (qs["oauth_token"] != null)
                {
                    this.Token = qs["oauth_token"];
                    this.TokenSecret = qs["oauth_token_secret"];
                    //请求用户授权Request Token
                    ret = Authorize + "?oauth_token=" + this.Token + "&oauth_callback=" + UrlEncode(this.CallbackUrl);
                }
            }
            return ret;
        }

        /// <summary>
        /// 使用授权后的Request Token换取Access Token
        /// </summary>
        public void AccessTokenGet(string authToken)
        {
            this.Token = authToken;

            string response = oAuthWebRequest(Method.GET, AccessToken, string.Empty);

            if (response.Length > 0)
            {
                NameValueCollection qs = HttpUtility.ParseQueryString(response);
                if (qs["oauth_token"] != null)
                {
                    this.Token = qs["oauth_token"];
                }
                if (qs["oauth_token_secret"] != null)
                {
                    this.TokenSecret = qs["oauth_token_secret"];
                }
                if (getExtensionData != null)
                {
                    getExtensionData.Invoke(qs);
                }
            }
        }
        /// <summary>
        /// Submit a web request using oAuth.
        /// </summary>
        /// <param name="method">GET or POST</param>
        /// <param name="url">The full url, including the querystring.</param>
        /// <param name="postData">Data to post (querystring format)</param>
        /// <returns>The web server response.</returns>
        public string oAuthWebRequest(Method method, string url, string postData)
        {
            string outUrl = "";
            string querystring = "";
            string ret = "";

            //Setup postData for signing.
            //Add the postData to the querystring.
            if (method == Method.POST || method == Method.PUT)
            {
                if (postData.Length > 0)
                {
                    //Decode the parameters and re-encode using the oAuth UrlEncode method.
                    NameValueCollection qs = HttpUtility.ParseQueryString(postData);
                    postData = "";
                    foreach (string key in qs.AllKeys)
                    {
                        if (postData.Length > 0)
                        {
                            postData += "&";
                        }
                        qs[key] = HttpUtility.UrlDecode(qs[key]);
                        qs[key] = this.UrlEncode(qs[key]);
                        postData += key + "=" + qs[key];

                    }
                    if (url.IndexOf("?") > 0)
                    {
                        url += "&";
                    }
                    else
                    {
                        url += "?";
                    }
                    url += postData;
                }
            }

            Uri uri = new Uri(url);

            string nonce = this.GenerateNonce();
            string timeStamp = this.GenerateTimeStamp();

            //Generate Signature
            string sig = this.GenerateSignature(uri,
                this.ConsumerKey,
                this.ConsumerSecret,
                this.Token,
                this.TokenSecret,
                method.ToString(),
                timeStamp,
                nonce,
                out outUrl,
                out querystring);


            querystring += "&oauth_signature=" + HttpUtility.UrlEncode(sig);

            //Convert the querystring to postData
            if (method == Method.POST)
            {
                postData = querystring;
                querystring = "";
            }

            if (querystring.Length > 0)
            {
                outUrl += "?";
            }

            if (method == Method.POST || method == Method.GET)
                ret = WebRequest(method, outUrl + querystring, postData);
            //else if (method == Method.PUT)
            //ret = WebRequestWithPut(Method.PUT,outUrl + querystring, postData);
            return ret;
        }

        /// <summary>
        /// Web Request Wrapper
        /// </summary>
        /// <param name="method">Http Method</param>
        /// <param name="url">Full url to the web resource</param>
        /// <param name="postData">Data to post in querystring format</param>
        /// <returns>The web server response.</returns>
        public string WebRequestWithPut(Method method, string url, string postData)
        {
            Uri uri = new Uri(url);
            string nonce = this.GenerateNonce();
            string timeStamp = this.GenerateTimeStamp();
            string outUrl, querystring;

            //Generate Signature
            string sig = this.GenerateSignatureBase(uri,
                this.ConsumerKey,
                this.ConsumerSecret,
                this.Token,
                this.TokenSecret,
                "GET",
                timeStamp,
                nonce,
                out outUrl,
                out querystring);

            querystring += "&oauth_signature=" + HttpUtility.UrlEncode(sig);
            NameValueCollection qs = HttpUtility.ParseQueryString(querystring);

            HttpWebRequest webRequest = null;
            string responseData = "";

            webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.ContentType = "text/xml";
            webRequest.Method = "PUT";
            webRequest.ServicePoint.Expect100Continue = false;

            webRequest.Headers.Add("Authorization", "OAuth realm=\"\"");
            webRequest.Headers.Add("oauth_consumer_key", this.ConsumerKey);
            webRequest.Headers.Add("oauth_token", this.Token);
            webRequest.Headers.Add("oauth_signature_method", "HMAC-SHA1");
            webRequest.Headers.Add("oauth_signature", sig);
            webRequest.Headers.Add("oauth_timestamp", timeStamp);
            webRequest.Headers.Add("oauth_nonce", nonce);
            webRequest.Headers.Add("oauth_verifier", this.Verifier);
            webRequest.Headers.Add("oauth_version", "1.0");

            //webRequest.KeepAlive = true;

            StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream());
            try
            {
                requestWriter.Write(postData);
            }
            catch
            {
                throw;
            }
            finally
            {
                requestWriter.Close();
                requestWriter = null;
            }


            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            string returnString = response.StatusCode.ToString();

            webRequest = null;

            return responseData;

        }


        /// <summary>
        /// Web Request Wrapper
        /// </summary>
        /// <param name="method">Http Method</param>
        /// <param name="url">Full url to the web resource</param>
        /// <param name="postData">Data to post in querystring format</param>
        /// <returns>The web server response.</returns>
        public string WebRequest(Method method, string url, string postData)
        {
            HttpWebRequest webRequest = null;
            StreamWriter requestWriter = null;
            string responseData = "";

            webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = method.ToString();
            webRequest.ServicePoint.Expect100Continue = false;
            //webRequest.UserAgent  = "Identify your application please.";
            //webRequest.Timeout = 20000;

            if (method == Method.POST || method == Method.PUT)
            {
                if (method == Method.PUT)
                {
                    webRequest.ContentType = "text/xml";
                    webRequest.Method = "PUT";
                }
                else
                    webRequest.ContentType = "application/x-www-form-urlencoded";
                //webRequest.ContentType = "multipart/form-data";

                //POST the data.
                requestWriter = new StreamWriter(webRequest.GetRequestStream());
                try
                {
                    requestWriter.Write(postData);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    requestWriter.Close();
                    requestWriter = null;
                }
            }

            responseData = WebResponseGet(webRequest);

            webRequest = null;

            return responseData;

        }

        /// <summary>
        /// Process the web response.
        /// </summary>
        /// <param name="webRequest">The request object.</param>
        /// <returns>The response data.</returns>
        public string WebResponseGet(HttpWebRequest webRequest)
        {
            StreamReader responseReader = null;
            string responseData = "";

            try
            {
                responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                responseData = responseReader.ReadToEnd();
            }
            catch
            {
                throw;
            }
            finally
            {
                webRequest.GetResponse().GetResponseStream().Close();
                responseReader.Close();
                responseReader = null;
            }

            return responseData;
        }
        public string ParseHtml(string html)
        {
            Regex htmlRegex = new Regex("<b>[0-9]{6}</b>");
            Match m = htmlRegex.Match(html);
            Regex pinRegex = new Regex("[0-9]{6}");
            Match m1 = pinRegex.Match(m.Value);
            return m1.Value;
        }
    }
}
