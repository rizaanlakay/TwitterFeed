using System;
using System.Collections.Generic;
using System.Text;

namespace AG.TwitterFeed.Models
{
    public class Tweet : IComparable<Tweet>
    {
        #region Private variables
        private DateTime _createdDate;
        private string _userName;
        private string _message;
        #endregion

        #region Constructors
        public Tweet()
        {
            _createdDate = DateTime.Now;
        }
        public Tweet(string userName, string message)
        {
            UserName = userName;
            Message = message;
            _createdDate = DateTime.Now;
        }
        #endregion

        #region Public Properties
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }
        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }
        }
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                // Assuming messages greater 140 characters get truncated.
                if (value.Length > 140)
                {
                    _message = value.Substring(0, 137) + "...";
                }
                else
                {
                    _message = value;
                }
            }
        }

        public int CompareTo(Tweet other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                return this.CreatedDate.CompareTo(other.CreatedDate);
            }
        }
        #endregion
    }
}
