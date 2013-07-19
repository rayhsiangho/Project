using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterTweetApp.DO
{
    public class TwitterDO
    {
        private String created_at;
        private String id;
        private String text;
        private String accountname;

        public String AccountName
        {
            get { return accountname; }
            set { accountname = value; }
        }
        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        private String screen_name;

        public String Screen_name
        {
            get { return screen_name; }
            set { screen_name = value; }
        }

        private String description;

        public String Description
        {
            get { return description; }
            set { description = value; }
        }
        private String url;

        public String Url
        {
            get { return url; }
            set { url = value; }
        }
        public String Text
        {
            get { return text; }
            set { text = value; }
        }
        public String Created_at
        {
            get { return created_at; }
            set { created_at = value; }
        }
        public String Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}