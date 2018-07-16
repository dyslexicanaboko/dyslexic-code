using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Serialization;
using ServerOps.EntityFramework;

namespace WorkLogApp.DataAccess
{
    public class DAO
    {
        protected static void Insert<T>(DbContext context, T entityObject) where T : class
        {
            context.Insert(entityObject);
        }

        protected static void Update<T>(DbContext context, T entityObject, params string[] properties) where T : class
        {
            context.Update(entityObject, properties);
        }

        protected static void SoftDelete<T>(DbContext context, T entityObject) where T : class
        {
            context.SoftDelete(entityObject);
        }

        protected static void SoftDelete<T>(DbContext context, T entityObject, params string[] properties) where T : class
        {
            context.SoftDelete(entityObject, properties);
        }

        protected static void HardDelete<T>(DbContext context, T entityObject) where T : class
        {
            context.HardDelete(entityObject);
        }

        /// <summary>
        /// Translate a list of object type E (Entity Framework Generated Objects) into a list of object 
        /// type P (POCO Objects) using the responsible DAO method to handle the individual object
        /// conversion.
        /// </summary>
        /// <typeparam name="P">POCO Object</typeparam>
        /// <typeparam name="E">Entity Framework Object</typeparam>
        /// <param name="target">list of Entity Framework Object</param>
        /// <param name="loadFunction">the </param>
        /// <returns>List of converted POCO objects</returns>
        protected static List<P> Translate<P, E>(IEnumerable<E> target, Func<E, P> loadFunction)
        {
            List<P> lst = new List<P>();

            foreach (E t in target)
                lst.Add(loadFunction(t));

            return lst;
        }

        public static string AddString(string target)
        {
            return "'" + target + "'";
        }

        public static string AddString(DateTime target)
        {
            return AddString(target.ToString("u"));
        }
    }
}
