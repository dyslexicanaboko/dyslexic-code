using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proofs_UnitTests
{
    public static class BasicConverter
    {
        public static int ConvertToInt32(this object target)
        {
            if (target == DBNull.Value || target == null)
                return 0;

            string str = target as string;

            if (str != null)
            {
                int val = 0;

                int.TryParse(str, out val);

                return val;
            }

            return Convert.ToInt32(target);
        }

        public static bool ConvertToBoolean(this object target)
        {
            if (target == DBNull.Value || target == null)
                return false;

            string str = target as string;

            if (str != null)
            {
                bool val = false;

                bool.TryParse(str, out val);

                return val;
            }

            return Convert.ToBoolean(target);
        }

        public static string ConvertToString(this object target)
        {
            if (target == DBNull.Value || target == null)
                return string.Empty;

            return target as string;
        }
    }
}
