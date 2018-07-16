using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingGroundsCS
{
    public class LoopingCollection
    {
        private List<string> _lstString;
        private int _iCount = 0;
        private int i = 0;
        private string _nextItem;

        public LoopingCollection(List<string> lstString)
        {
            _lstString = lstString;
            _iCount = _lstString.Count - 1;
        }

        public string Next()
        {
            _nextItem = _lstString[i];

            if (_iCount == i)
                i = 0;
            else
                i++;

            return _nextItem;
        }
    }
}
