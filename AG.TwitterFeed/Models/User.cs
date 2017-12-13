using System;
using System.Collections.Generic;
using System.Text;

namespace AG.TwitterFeed.Models
{
    public class User : IComparable<User>
    {
        #region Private variables
        private string _name;
        private List<User> _userfollowers = new List<User>();
        private List<Tweet> _tweets = new List<Tweet>();
        #endregion

        #region Constructors
        public User()
        {

        }
        public User(string name)
        {
            Name = name;
        }
        #endregion

        #region Public Properties
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public bool IsFollowing(string name)
        {
            foreach (User u in UserFollowers)
            {
                if (u.Name.ToLower() == name.ToLower())
                    return true;
            }
            return false;
        }
        public List<User> UserFollowers
        {
            get
            {
                return _userfollowers;
            }
            set
            {
                _userfollowers = value;
            }
        }

        public List<Tweet> Tweets
        {
            get
            {
                return _tweets;
            }
            set
            {
                _tweets = value;
            }
        }

        #endregion

        #region Public Methods
        public void SortTweets()
        {
            Tweets.Sort();
        }
        public int CompareTo(User other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                return Name.CompareTo(other.Name);
            }
        }
        public void PrintTweets(bool printName, bool includeFollowerTweets)
        {
            Tweets.Sort();
            if (printName)
                Console.WriteLine(this.Name);

            List<Tweet> tweetList = new List<Tweet>();

            // Add My Tweets
            foreach(Tweet t in this.Tweets)
            {
                tweetList.Add(t);
            }

            if (includeFollowerTweets)
            {
                // Add all the tweets of the people the user follows
                foreach (User f in this.UserFollowers)
                {
                    foreach (Tweet ft in f.Tweets)
                    {
                        tweetList.Add(ft);
                    }
                }
            }
            // Sort the order
            tweetList.Sort();

            foreach (Tweet tweet in tweetList)
            {
                Console.WriteLine("\t@{0}: {1}", tweet.UserName, tweet.Message);
            }
        }
        #endregion
    }
}
