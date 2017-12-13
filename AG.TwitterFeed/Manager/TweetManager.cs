using AG.TwitterFeed.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AG.TwitterFeed.Manager
{
    public sealed class TweetManager
    {
        #region Private variables
        private List<User> _users = new List<User>();
        private static TweetManager _instance = null;
        private string _userFilePath;
        private string _tweetFilePath;
        #endregion

        #region Constructor
        private TweetManager()
        {

        }
        #endregion

        #region Public Properties
        public static TweetManager GetInstance
        {
            get
            {
                if (_instance == null)
                    _instance = new TweetManager();
                return _instance;
            }
        }
        public List<User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
            }
        }
        public string UserFilePath
        {
            get
            {
                return _userFilePath;
            }
            set
            {
                _userFilePath = value;
            }
        }
        public string TweetFilePath
        {
            get
            {
                return _tweetFilePath;
            }
            set
            {
                _tweetFilePath = value;
            }
        }
        #endregion

        #region Public Methods
        public void ImportFiles(string file1, string file2)
        {
            if (!File.Exists(file1))
            {
                throw new Exception(String.Format("Error: Could not find file {0} in folder {1}", file1, Environment.CurrentDirectory));
            }
            if (!File.Exists(file2))
            {
                throw new Exception("Error: The second argument file does not exist.");
            }
            _userFilePath = "";
            _tweetFilePath = "";

            if (IsUserFile(file1))
            {
                _userFilePath = file1;
            }
            else if (IsTweetFile(file1))
            {
                _tweetFilePath = file1;
            }
            if (IsUserFile(file2))
            {
                _userFilePath = file2;
            }
            else if (IsTweetFile(file2))
            {
                _tweetFilePath = file2;
            }
            if (_userFilePath == "")
            {
                throw new Exception("Error: No user valid file added as an argument.");
            }
            if (_tweetFilePath == "")
            {
                throw new Exception("Error: No user tweet file added as an argument.");
            }

            ImportUserFile();

            ImportTweetFile();
        }

        public void PrintUserTweets(bool printName, bool includeFollowerTweets)
        {
            this.Users.Sort();

            foreach (User u in this.Users)
            {
                u.PrintTweets(printName, includeFollowerTweets);
            }
        }

        public void ImportUserFile()
        {
            // Assuming that all users are deleted with file passed in.
            // Assuming that the same Name in the different rows is the same User eg. Alan is Alan no matter which row its on.
            this.Users = new List<User>();
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(this._userFilePath);
            while ((line = file.ReadLine()) != null)
            {
                if (line.ToLower().Contains("follows") && !line.ToLower().Contains(">"))
                {
                    // Process the line
                    string[] parts = line.Split(" follows ");
                    if (parts.Length > 0)
                    {
                        // First part is the User
                        string name = parts[0].Trim();
                        string follows = parts[1].Trim();
                        if (name != "")
                        {
                            User existingUser = DoesUserExist(name);
                            if (existingUser == null)
                            {
                                User user = new User(name);
                                this.Users.Add(user);
                                existingUser = user;
                            }

                            // Get a list of people that the user follows.
                            string[] followerNames = follows.Split(",");
                            // Check if this user exists and add his followers.
                            foreach (string fname in followerNames)
                            {
                                string _followername = fname.Trim();
                                User followerUser = DoesUserExist(_followername);
                                if (followerUser == null)
                                {
                                    User user = new User(_followername);
                                    this.Users.Add(user);
                                    followerUser = user;
                                }

                                // Check if the user is already following the follower user.
                                if (!existingUser.IsFollowing(followerUser.Name))
                                    existingUser.UserFollowers.Add(followerUser);
                            }
                        }
                    }
                }
            }

            file.Close();
        }
        public void ImportTweetFile()
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(this._tweetFilePath);
            while ((line = file.ReadLine()) != null)
            {
                if (line.ToLower().Contains(">"))
                {
                    string[] parts = line.Split(">");
                    if (parts.Length > 0)
                    {
                        string userName = parts[0].Trim();
                        string message = parts[1].Trim();
                        if (userName != "")
                        {
                            User tweetUser = DoesUserExist(userName);
                            if (tweetUser != null)
                            {
                                tweetUser.Tweets.Add(new Tweet { Message = message, UserName = tweetUser.Name });
                            }
                        }
                    }
                }
            }
            file.Close();
        }
        #endregion

        #region Private Methods
        private bool IsUserFile(string filename)
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            while ((line = file.ReadLine()) != null)
            {
                if (line.ToLower().Contains("follows") && !line.ToLower().Contains(">"))
                {
                    file.Close();
                    return true;
                }
            }

            file.Close();
            return false;
        }
        private bool IsTweetFile(string filename)
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            while ((line = file.ReadLine()) != null)
            {
                if (line.ToLower().Contains(">"))
                {
                    file.Close();
                    return true;
                }
            }

            file.Close();
            return false;
        }
        private User DoesUserExist(string Name)
        {
            foreach (User u in this.Users)
            {
                if (u.Name.ToLower() == Name.ToLower())
                {
                    return u;
                }
            }
            return null;
        }
        #endregion
    }
}
