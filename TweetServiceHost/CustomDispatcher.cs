using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Xml.Linq;

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


    public class ConsoleMessageTracer : IDispatchMessageInspector, IClientMessageInspector, IDispatchOperationSelector 
    { 
        private Message TraceMessage(MessageBuffer buffer) 
        { 
            Message msg = buffer.CreateMessage(); 
            Console.WriteLine("\n{0}\n", msg); 
            return buffer.CreateMessage(); 
        }

        NameValueCollection actionMap = new NameValueCollection { { "a", "SendTweet" }, { "b", "DeleteTweet" } };

        public string SelectOperation(ref Message message)
        {
            Console.WriteLine("In QueryStringDispatcherBehavior.SelectOperation");
            Uri uri = new Uri(message.Headers.Action);
            string query = uri.Query;

            Console.WriteLine(query);

            //string methodName = actionMap[message.Headers.Action.ToString()];

            //return methodName;

            return "DeleteTweet";
        }
        
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext) 
        { 
            request = TraceMessage(request.CreateBufferedCopy(int.MaxValue)); 
            return null; 
        } 
        
        public void BeforeSendReply(ref Message reply, object correlationState) 
        { 
            reply = TraceMessage(reply.CreateBufferedCopy(int.MaxValue)); 
        } 
        
        public void AfterReceiveReply(ref Message reply, object correlationState) 
        { 
            reply = TraceMessage(reply.CreateBufferedCopy(int.MaxValue)); 
        } 
        
        public object BeforeSendRequest(ref Message request, IClientChannel channel) 
        { 
            request = TraceMessage(request.CreateBufferedCopy(int.MaxValue)); 
            return null; 
        } 
    }


    public class ConsoleMessageTracing : Attribute, IEndpointBehavior, IServiceBehavior
    {
        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new ConsoleMessageTracer());
        }

        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new ConsoleMessageTracer());
        }

        // remaining methods empty 
        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription desc, ServiceHostBase host)
        {
            foreach (ChannelDispatcher cDispatcher in host.ChannelDispatchers)
                foreach (EndpointDispatcher eDispatcher in cDispatcher.Endpoints)
                    eDispatcher.DispatchRuntime.MessageInspectors.Add(new ConsoleMessageTracer());
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            
        }
    }
}