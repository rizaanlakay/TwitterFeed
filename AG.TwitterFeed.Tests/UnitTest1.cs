using Microsoft.VisualStudio.TestTools.UnitTesting;
using AG.TwitterFeed.Models;
using System;

namespace AG.TwitterFeed.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestImportFiles()
        {
            try
            {
                string userFileName = "user.txt";
                string tweetFileName = "tweet.txt";
                AG.TwitterFeed.Manager.TweetManager manager = AG.TwitterFeed.Manager.TweetManager.GetInstance;
                manager.ImportFiles(userFileName, tweetFileName);
                if (manager.Users.Count != 3)
                    Assert.Fail();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestAddTweetToUser()
        {
            try
            {
                User user = new User("Alan");
                user.Tweets.Add(new Tweet { Message = "Tweet 1", UserName = "Alan" });
                user.Tweets.Add(new Tweet { Message = "Tweet 2", UserName = "Alan" });
                user.Tweets.Add(new Tweet { Message = "Tweet 3", UserName = "Alan" });

                if (user.Tweets.Count != 3)
                {
                    Console.WriteLine("User tweet count is incorrect.");
                    Assert.Fail();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestSortUsers()
        {
            try
            {
                AG.TwitterFeed.Manager.TweetManager manager = AG.TwitterFeed.Manager.TweetManager.GetInstance;
                manager.Users.Add(new User { Name = "Alan" });
                manager.Users.Add(new User { Name = "Ward" });
                manager.Users.Add(new User { Name = "Martin" });

                if (manager.Users[0].Name != "Alan")
                {
                    Assert.Fail();
                }
                if (manager.Users[1].Name != "Martin")
                {
                    Assert.Fail();
                }
                if (manager.Users[2].Name != "Ward")
                {
                    Assert.Fail();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestImportUserFile()
        {
            try
            {
                AG.TwitterFeed.Manager.TweetManager manager = AG.TwitterFeed.Manager.TweetManager.GetInstance;
                manager.UserFilePath = "user.txt";
                manager.ImportUserFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestImportTweetFile()
        {
            try
            {
                AG.TwitterFeed.Manager.TweetManager manager = AG.TwitterFeed.Manager.TweetManager.GetInstance;
                manager.UserFilePath = "tweet.txt";
                manager.ImportUserFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Fail();
            }
        }
    }
}
