using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using TwitterTweetApp.DO;
using System.Globalization;

namespace TwitterTweetsApp.Controllers
{

    enum TwitterUsers
    {
        @pay_by_phone,
        @PayByPhone,
        @PayByPhone_UK
    }
    [HandleError]
    public class HomeController : Controller
    {
        const string format = "ddd MMM dd HH:mm:ss zzzz yyyy";
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ViewTweetList()
        {
            try
            {
                String[] names = Enum.GetNames(typeof(TwitterUsers));
                if (names != null && names.Count() > 0)
                {
                    var model = new Models.TwitterTweetModel();
                    foreach (var user in names)
                    {
                        WebResponse response = SendRequest(user);
                        model.StoreTwitterTweetList(ParseRequest(response, user));
                    }
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    if (model.TwitterTweetList.Count > 0)
                    {
                        model.TwitterTweetList.RemoveAll(x => DateTime.ParseExact(x.Created_at, format, CultureInfo.InvariantCulture) < DateTime.Now.AddDays(-14));
                        string jsonresult = jss.Serialize(model.TwitterTweetList.OrderByDescending(x => DateTime.ParseExact(x.Created_at, format, CultureInfo.InvariantCulture)).ToList());
                        return Json(jsonresult);
                    }
                    else
                        return Json(null);
                }
                else
                    return Json(null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult ViewTweetTotal()
        {
            try
            {
                String[] names = Enum.GetNames(typeof(TwitterUsers));
                if (names != null && names.Count() > 0)
                {
                    var model = new Models.TwitterTweetModel();
                    foreach (var user in names)
                    {
                        WebResponse response = SendRequest(user);
                        model.StoreTwitterTweetList(ParseRequest(response, user));
                    }
                    model.TwitterTweetList.RemoveAll(x => DateTime.ParseExact(x.Created_at, format, CultureInfo.InvariantCulture) < DateTime.Now.AddDays(-14));
                    model.TwitterTweetList.OrderByDescending(x => DateTime.ParseExact(x.Created_at, format, CultureInfo.InvariantCulture)).ToList();

                    foreach (var user in names)
                    {
                        model.GetTwitterTweetTotal(user);
                    }
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    if (model.TwitterTweetTotalList.Count > 0)
                    {
                        string jsonresult = jss.Serialize(model.TwitterTweetTotalList);
                        return Json(jsonresult);
                    }
                    else
                        return Json(null);
                }
                else
                    return Json(null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TwitterDO> ParseRequest(WebResponse response, String accountname)
        {
            try
            {
                if (response != null)
                {
                    string jsonResponse = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    JavaScriptSerializer parser = new JavaScriptSerializer();
                    List<TwitterDO> tweetList = parser.Deserialize<List<TwitterDO>>(jsonResponse);
                    tweetList.ForEach(x => x.AccountName = accountname);
                    return tweetList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new List<TwitterDO>();
        }
        public WebResponse SendRequest(String twitterSreenName)
        {
            try
            {
                var oauth_token = "1594158649-FN2fYJKW8aLJopq81cD0HZbUylDtpmIVbOU9KJR";
                var oauth_token_secret = "0t14X8BvUJmyCu5loN5itfH97QejFjzS0xDTaptu8";
                var oauth_consumer_key = "ui7oDxGwLbcoPzQmUuA";
                var oauth_consumer_secret = "Xe68RKvQ77NpXbmcGvUTvcScLT2wWQTFxrp6ckxUzM";

                var oauth_version = "1.0";
                var oauth_signature_method = "HMAC-SHA1";
                var oauth_nonce = Convert.ToBase64String(
                                                  new System.Text.ASCIIEncoding().GetBytes(
                                                       DateTime.Now.Ticks.ToString()));
                var timeSpan = DateTime.UtcNow
                                                  - new DateTime(1970, 1, 1, 0, 0, 0, 0,
                                                       DateTimeKind.Utc);
                var oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();
                //var slug = "libraries-and-news-sites";
                var owner_screen_name = twitterSreenName;
                var resource_url = "https://api.twitter.com/1.1/statuses/user_timeline.json";
                var baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" +
                     "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&screen_name={6}";

                var baseString = string.Format(baseFormat,
                                            oauth_consumer_key,
                                            oauth_nonce,
                                            oauth_signature_method,
                                            oauth_timestamp,
                                            oauth_token,
                                            oauth_version,
                                            Uri.EscapeDataString(owner_screen_name)

                                            );

                baseString = string.Concat("GET&", Uri.EscapeDataString(resource_url),
                             "&", Uri.EscapeDataString(baseString));

                var compositeKey = string.Concat(Uri.EscapeDataString(oauth_consumer_secret),
                                        "&", Uri.EscapeDataString(oauth_token_secret));

                string oauth_signature;
                using (System.Security.Cryptography.HMACSHA1 hasher = new System.Security.Cryptography.HMACSHA1(System.Text.ASCIIEncoding.ASCII.GetBytes(compositeKey)))
                {
                    oauth_signature = Convert.ToBase64String(
                        hasher.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(baseString)));
                }

                var headerFormat = "OAuth oauth_consumer_key=\"{0}\", oauth_nonce=\"{1}\", oauth_signature=\"{2}\", oauth_signature_method=\"{3}\", oauth_timestamp=\"{4}\", oauth_token=\"{5}\", oauth_version=\"{6}\"";

                var authHeader = string.Format(headerFormat,
                                        Uri.EscapeDataString(oauth_consumer_key),
                                        Uri.EscapeDataString(oauth_nonce),
                                        Uri.EscapeDataString(oauth_signature),
                                        Uri.EscapeDataString(oauth_signature_method),
                                        Uri.EscapeDataString(oauth_timestamp),
                                        Uri.EscapeDataString(oauth_token),
                                        Uri.EscapeDataString(oauth_version)
                                );

                ServicePointManager.Expect100Continue = false;

                var getBody = "?screen_name=" + Uri.EscapeDataString(owner_screen_name);
                resource_url += getBody;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resource_url);

                request.Headers.Add("Authorization", authHeader);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                WebResponse response = request.GetResponse();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
