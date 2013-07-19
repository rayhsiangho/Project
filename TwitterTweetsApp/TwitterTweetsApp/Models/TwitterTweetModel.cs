using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwitterTweetApp.DO;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace TwitterTweetsApp.Models
{
    public class TwitterTweetModel
    {
        public List<TwitterDO> TwitterTweetList;
        public List<TwitterTotalDO> TwitterTweetTotalList;
        public TwitterTweetModel()
        {
            TwitterTweetList = new List<TwitterDO>();
            TwitterTweetTotalList = new List<TwitterTotalDO>();
        }
        public void GetTwitterTweetTotal(String accountName)
        {
            if (TwitterTweetTotalList == null)
                TwitterTweetTotalList = new List<TwitterTotalDO>();
            else
            {
                TwitterTotalDO twittertotal = new TwitterTotalDO();
                twittertotal.AccountName = accountName;
                twittertotal.TotalNumberOfMentionedInTweetForTwoWeek = NumberOfMeniontedinTweets(accountName);
                twittertotal.TotalNumberOfTweetForTwoWeek = NumberOfTweets(accountName);
                TwitterTweetTotalList.Add(twittertotal);
            }
        }
        public void StoreTwitterTweetList(List<TwitterDO> twittertweetlist)
        {
            if (TwitterTweetList == null)
                TwitterTweetList = twittertweetlist;
            else
            {
                TwitterTweetList.AddRange(twittertweetlist);
            }
        }
        public int NumberOfTweets(String accountName)
        {
            if (TwitterTweetList != null && accountName != String.Empty)
            {
                return TwitterTweetList.Where(x => x.AccountName.ToUpper() == accountName.ToUpper()).Count();
            }
            return 0;
        }
        public int NumberOfMeniontedinTweets(String accountName)
        {
            if (TwitterTweetList != null && accountName != String.Empty)
            {
                return TwitterTweetList.Where(x => x.Text.ToUpper().Contains(accountName.ToUpper())).Count();
            }
            return 0;
        }
    }
}