using System;
using System.Runtime.Serialization;
using System.Threading;
using LotteryBaseLogic;

namespace LotteryWcfService
{
    [DataContract]
    public class LotteryInfo
    {
        public LotteryInfo()
        {
            Number = new LotteryNumbers().ToString();
        }

        public LotteryInfo(LotteryNumbers lotteryNumbers)
        {
            Number = lotteryNumbers.ToString();
            Status = lotteryNumbers.Status;
            Jackpot = lotteryNumbers.Jackpot;
        }

        [DataMember]
        public string Number { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string Jackpot { get; set; }
    }

    // Simple async result implementation.
    class CompletedAsyncResult<T> : IAsyncResult
    {
        T data;

        public CompletedAsyncResult(T data)
        { this.data = data; }

        public T Data
        { get { return data; } }

        #region IAsyncResult Members
        public object AsyncState
        { get { return (object)data; } }

        public WaitHandle AsyncWaitHandle
        { get { throw new Exception("The method or operation is not implemented."); } }

        public bool CompletedSynchronously
        { get { return true; } }

        public bool IsCompleted
        { get { return true; } }
        #endregion
    }
}
