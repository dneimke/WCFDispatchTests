using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace TweetServiceHost
{
    public class QueryStringDispatcherBehavior : Attribute, IContractBehavior, IDispatchOperationSelector
    {
        public string SelectOperation(ref Message message)
        {
            Console.WriteLine("In QueryStringDispatcherBehavior.SelectOperation");
            Console.WriteLine(message.State + " -- " + message.Headers.Action + " -- " + message.Headers.To);
            var msg = message.ToString();

            var document = new XmlDocument();
            document.LoadXml(msg);
            var bodyElement = document.DocumentElement.ChildNodes[1].FirstChild.LocalName;
            Console.WriteLine(bodyElement);

            return bodyElement;            
        }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            Console.WriteLine("In QueryStringDispatcherBehavior.ApplyDispatchBehavior");
            dispatchRuntime.OperationSelector = new QueryStringDispatcherBehavior();
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
            
        }
    }
}