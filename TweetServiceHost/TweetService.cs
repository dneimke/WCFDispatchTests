using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TweetServiceHost.Contracts;

namespace TweetServiceHost
{
    [QueryStringDispatcherBehavior]
    public class DispatchedTweetService : ITweetService
    {
        public TweetResponse SendTweet(SendTweetRequest tweet)
        {
            Console.WriteLine("In DispatchedTweetService.SendTweet");
            return new TweetResponse { Message = "SendTweet" };
        }

        public TweetResponse DeleteTweet(DeleteTweetRequest tweet)
        {
            Console.WriteLine("In DispatchedTweetService.DeleteTweet");
            return new TweetResponse { Message = "DeleteTweet" };
        }
    }


    public class TweetService : ISendTweetService, IDeleteTweetService
    {
        public TweetResponse SendTweet(SendTweetRequest tweet)
        {
            Console.WriteLine("In TweetService.SendTweet");
            return new TweetResponse { Message = "SendTweet" };
        }

        public TweetResponse DeleteTweet(DeleteTweetRequest tweet)
        {
            Console.WriteLine("In TweetService.DeleteTweet");
            return new TweetResponse { Message = "DeleteTweet" };
        }
    }

    
}
