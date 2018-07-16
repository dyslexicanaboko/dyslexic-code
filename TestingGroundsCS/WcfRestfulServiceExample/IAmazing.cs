using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiceApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAmazing
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
                   UriTemplate = "/Ping",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json)]
        string Ping();

        [OperationContract]
        [WebInvoke(Method = "POST",
                   UriTemplate = "/AddTwoNumbers?x={x}&y={y}",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json)]
        int AddTwoNumbers(int x, int y);

        [OperationContract]
        [WebInvoke(Method = "POST",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json)]
        int AddThreeNumbers(ThreeNumbers numbers);

        [OperationContract]
        [WebInvoke(Method = "GET",
                   UriTemplate = "/SyncOperation/{jsonString}", 
                   RequestFormat = WebMessageFormat.Json, 
                   ResponseFormat = WebMessageFormat.Json)]
        bool SyncOperation(string jsonString);

        [OperationContract]
        [WebInvoke(Method = "GET",
                   UriTemplate = "/CopyCat/{input}",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json)]
        string CopyCat(string input);
    }
}
