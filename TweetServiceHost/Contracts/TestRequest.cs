using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace TweetServiceHost.Contracts
{
    [ServiceContract]
    public interface ISendTweetService
    {
        [OperationContract(Action = "*")]
        TweetResponse SendTweet(SendTweetRequest tweet);
    }

    [ServiceContract]
    public interface IDeleteTweetService
    {
        [OperationContract(Action = "*")]
        TweetResponse DeleteTweet(DeleteTweetRequest tweet);
    }

    [ServiceContract]
    public interface ITweetService
    {
        [OperationContract(Action = "*")]
        TweetResponse SendTweet(SendTweetRequest tweet);

        [OperationContract]
        TweetResponse DeleteTweet(DeleteTweetRequest tweet);
    }

    [DataContract]
    public class SendTweetRequest
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public DateTime Created { get; set; }
        [DataMember]
        public string Text { get; set; }
    }

    [DataContract]
    public class DeleteTweetRequest
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public DateTime Created { get; set; }
        [DataMember]
        public string Text { get; set; }
    }

    [DataContract]
    public class TweetResponse
    {
        [DataMember]
        public string Message { get; set; }
    }

}
