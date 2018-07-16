using System;
using System.Collections.Generic;
using System.Data;

namespace F5EverywhereLib.Repository
{
    public abstract class RepositoryBase
    {
        protected List<T> ToList<T>(DataTable dt, Func<DataRow, T> method)
            where T : new()
        {
            var lst = new List<T>(dt.Rows.Count);

            foreach (DataRow dr in dt.Rows)
                lst.Add(method(dr));

            return lst;
        }
    }
}
