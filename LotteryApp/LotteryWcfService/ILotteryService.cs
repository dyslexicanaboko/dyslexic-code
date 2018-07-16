using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace LotteryWcfService
{
    [ServiceContract]
    public interface ILotteryService
    {
        [OperationContract]
        [WebGet(
            UriTemplate = "/GetTodaysWinningNumber",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        LotteryInfo GetTodaysWinningNumber();

        [OperationContract]
        [WebGet(
            UriTemplate = "/IsAlive",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        DateTime IsAlive();

        [OperationContract]
        [WebGet(
            UriTemplate = "/Ping",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        string Ping();
    }
}
