using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagingSwitchLibrary.PagingSwitch
{
    //All PagerController server classes must implement these
    //two methods
    public interface IPagerController
    {
        bool ConnectToPagingSwitch();
        void CloseConnection();
        string SendPage(string subscriber, string message);
    }
}
