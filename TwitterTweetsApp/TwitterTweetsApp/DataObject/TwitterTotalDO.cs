using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterTweetApp.DO
{
    public class TwitterTotalDO
    {
        private int totalnumberoftweetfortwoweek;
        private int totalnumberofmentionedintweetfortwoweek;
        private String accountname;

        public String AccountName
        {
            get { return accountname; }
            set { accountname = value; }
        }

        public int TotalNumberOfMentionedInTweetForTwoWeek
        {
            get { return totalnumberofmentionedintweetfortwoweek; }
            set { totalnumberofmentionedintweetfortwoweek = value; }
        }
        public int TotalNumberOfTweetForTwoWeek
        {
            get { return totalnumberoftweetfortwoweek; }
            set { totalnumberoftweetfortwoweek = value; }
        }
    }
}