using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace TweetServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(TweetService)))
            using (ServiceHost host1 = new ServiceHost(typeof(DispatchedTweetService)))
            {
                host.Open();
                host1.Open();
                
                Console.WriteLine("The tweet service is ready.");
                Console.WriteLine("The dispatched tweet service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();
            }
        }
    }
}
