using TwitterTweetsApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using TwitterTweetApp.DO;
using System.Collections.Generic;

namespace TwitterTweetsApp.Tests
{


    /// <summary>
    ///This is a test class for TwitterTweetModelTest and is intended
    ///to contain all TwitterTweetModelTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TwitterTweetModelTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion



        [TestMethod()]
        public void StoreTwitterTweetListTest()
        {
            TwitterTweetModel target = new TwitterTweetModel();
            List<TwitterDO> twittertweetlist = new List<TwitterDO>();
            TwitterDO twitter = new TwitterDO();
            twitter.AccountName = "@pay_by_phone";
            twitter.Created_at = "Wed Jul 17 07:22:05 +0000 2013";
            twitter.Text = "Après #Issy, #Boulogne, #lehavre, au tour de ";
            twitter.Id = "355451785655234560";
            twittertweetlist.Add(twitter);
            target.StoreTwitterTweetList(twittertweetlist);
            Assert.AreEqual(1, target.TwitterTweetList.Count);
        }


        [TestMethod()]
        public void GetTwitterTweetTotalTest()
        {
            TwitterTweetModel target = new TwitterTweetModel();
            List<TwitterDO> twittertweetlist = new List<TwitterDO>();
            TwitterDO twitter = new TwitterDO();
            twitter.AccountName = "@pay_by_phone";
            twitter.Created_at = "Wed Jul 17 07:22:05 +0000 2013";
            twitter.Text = "Après #Issy, #Boulogne, #lehavre, au tour de ";
            twitter.Id = "355451785655234560";
            twittertweetlist.Add(twitter);
            target.StoreTwitterTweetList(twittertweetlist);
            target.GetTwitterTweetTotal("@pay_by_phone");
            Assert.AreEqual(1, target.TwitterTweetTotalList.Count);
        }

        [TestMethod()]
        public void NumberOfMeniontedinTweetsTest()
        {
            TwitterTweetModel target = new TwitterTweetModel();
            List<TwitterDO> twittertweetlist = new List<TwitterDO>();
            TwitterDO twitter = new TwitterDO();
            twitter.AccountName = "@pay_by_phone";
            twitter.Created_at = "Wed Jul 17 07:22:05 +0000 2013";
            twitter.Text = "Après #Issy, #Boulogne, #lehavre, au tour de @pay_by_phone ";
            twitter.Id = "355451785655234560";
            twittertweetlist.Add(twitter);
            target.StoreTwitterTweetList(twittertweetlist);
            int number = target.NumberOfMeniontedinTweets("@pay_by_phone");
            Assert.AreEqual(1, number);
        }

        [TestMethod()]
        public void NumberOfTweetsTest()
        {
            TwitterTweetModel target = new TwitterTweetModel();
            List<TwitterDO> twittertweetlist = new List<TwitterDO>();
            TwitterDO twitter = new TwitterDO();
            twitter.AccountName = "@pay_by_phone";
            twitter.Created_at = "Wed Jul 17 07:22:05 +0000 2013";
            twitter.Text = "Après #Issy, #Boulogne, #lehavre, au tour de ";
            twitter.Id = "355451785655234560";
            twittertweetlist.Add(twitter);
            target.StoreTwitterTweetList(twittertweetlist);
            int number = target.NumberOfTweets("@pay_by_phone");

            Assert.AreEqual(1, number);
        }
    }
}
