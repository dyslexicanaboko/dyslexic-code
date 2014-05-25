using System;
using System.Collections.Specialized;
using System.Web;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace WCFServicesSecurity
{
    public class AuthorizationMessageInspector : IDispatchMessageInspector, IClientMessageInspector
    {
        #region Dispatch Message Inspector
        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            //The goal here is to inspect the request for the proper header value.
            //The Header value must be a very long stupid key which we will use for
            //securing access. No one can access this service without this key.

            //For the rest scenario - since WCF won't allow me to send over HTTP Headers manually
            //Use the Query String Parameters to validate the Rest Requests
            if (ValidateRestRequest(request.Headers.To))
                return null;

            // Look for my custom header in the request
            int headerPosition = request.Headers.FindHeader(Config.HeaderName, Config.HeaderNamespace);

            if(headerPosition == -1)
                throw new UnauthorizedAccessException("E0: Unauthorized access. 0x201403260001");

            // Get an XmlDictionaryReader to read the header content
            XmlDictionaryReader reader = request.Headers.GetReaderAtHeader(headerPosition);

            // Read it through its static method ReadHeader
            AuthorizationHeader header = AuthorizationHeader.ReadHeader(reader);

            if (header == null)
                throw new UnauthorizedAccessException("E1: Unauthorized access. 0x201403191126");

            if (!header.IsAuthorized())
                throw new UnauthorizedAccessException("E2: Unauthorized access. 0x201403211809");

            return null;
        }

        private bool ValidateRestRequest(Uri to)
        {
            NameValueCollection nvc = HttpUtility.ParseQueryString(to.Query);

            return nvc.Get(Config.HeaderName) == Config.HeaderNamespace && 
                   nvc.Get(Config.AuthorizationTokenName) == Config.AuthorizationTokenValue;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            //Don't do anything here for now
        }
        #endregion

        #region Client Message Inspector
        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            //Don't do anything
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);
            request = buffer.CreateMessage();

            request.Headers.Add(new AuthorizationHeader(Config.AuthorizationTokenValue));

            return null;
        }
        #endregion
    }
}
