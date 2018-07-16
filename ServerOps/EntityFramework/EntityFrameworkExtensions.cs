using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using ServerOps.Database;

namespace ServerOps.EntityFramework
{
    public static class EntityFrameworkExtensions
    {
        public static DataTable ExecuteDataTable(this ObjectContext context, string sqlString)
        {
            return ExecuteDataTable(new SqlConnection(context.GetConnectionString()), sqlString);
        }

        public static DataTable ExecuteDataTable(this IDbConnection con, string sqlString)
        {
            DataTable dt = new DataTable("Table1"); //The Table name MUST be set in order to pass this over WCF

            using (con)
            {
                con.OpenConnection();

                using (IDbCommand cmd = new SqlCommand(sqlString, (SqlConnection)con))
                {
                    cmd.CommandTimeout = 0;

                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = (SqlCommand)cmd;
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataSet ExecuteDataSet(this ObjectContext context, string sqlString)
        {
            return ExecuteDataSet(new SqlConnection(context.GetConnectionString()), sqlString);
        }

        public static DataSet ExecuteDataSet(this IDbConnection con, string sqlString)
        {
            DataSet ds = new DataSet();

            using (con)
            {
                con.OpenConnection();

                using (IDbCommand cmd = new SqlCommand(sqlString, (SqlConnection)con))
                {
                    cmd.CommandTimeout = 0;

                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = (SqlCommand)cmd;
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public static void ExecuteNonQuery(this ObjectContext context, string sqlString)
        {
            ExecuteNonQuery(new SqlConnection(context.GetConnectionString()), sqlString);
        }

        public static void ExecuteNonQuery(this IDbConnection con, string sqlString)
        {
            using (con)
            {
                con.OpenConnection();

                using (IDbCommand cmd = new SqlCommand(sqlString, (SqlConnection)con))
                {
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static string GetConnectionString(this ObjectContext context)
        {
            return ((EntityConnection)context.Connection).StoreConnection.ConnectionString;
        }

        //TODO: This method is untested as I couldn't get it to compile properly for Linq to SQL/EF. 
        //The idea behind this method has merit - it is to set properties of the selected object as 
        //it is being selected in a linq to object query.
        /// <summary>
        /// Used to modify properties of an object returned from a LINQ to Object query
        /// </summary>
        /// <param name="target">target object to modify</param>
        /// <param name="updater">delegate that modifies the target object</param>
        /// <returns>the updated object</returns>
        /// <remarks>
        /// http://robvolk.com/linq-select-an-object-but-change-some-properties-without-creating-a-new-object/
        /// http://stackoverflow.com/questions/807797/linq-select-an-object-and-change-some-properties-without-creating-a-new-object
        /// </remarks>
        public static T Set<T>(this T target, Action<T> updater) where T : class
        {
            updater(target);

            return target;
        }

        /* I added this class because (I didn't try it yet) I don't think that I can put these methods into
         * the KawaUtilLib because it is 3.5 only. I am pretty sure this requires 4.0.
         * 
         * I got the solution for this here:
         * http://stackoverflow.com/questions/1101841/linq-how-to-perform-max-on-a-property-of-all-objects-in-a-collection-and-ret
         * 
         * I pulled the code for this from here:
         * http://code.google.com/p/morelinq/source/browse/MoreLinq/MaxBy.cs
         * http://code.google.com/p/morelinq/source/browse/MoreLinq/ThrowHelper.cs
         */

        /// <summary>
        /// Returns the maximal element of the given sequence, based on
        /// the given projection.
        /// </summary>
        /// <remarks>
        /// If more than one element has the maximal projected value, the first
        /// one encountered will be returned. This overload uses the default comparer
        /// for the projected type. This operator uses immediate execution, but
        /// only buffers a single result (the current maximal element).
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">Selector to use to pick the results to compare</param>
        /// <returns>The maximal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector)
        {
            return source.MaxBy(selector, Comparer<TKey>.Default);
        }

        /// <summary>
        /// Returns the maximal element of the given sequence, based on
        /// the given projection and the specified comparer for projected values. 
        /// </summary>
        /// <remarks>
        /// If more than one element has the maximal projected value, the first
        /// one encountered will be returned. This overload uses the default comparer
        /// for the projected type. This operator uses immediate execution, but
        /// only buffers a single result (the current maximal element).
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">Selector to use to pick the results to compare</param>
        /// <param name="comparer">Comparer to use to compare projected values</param>
        /// <returns>The maximal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="selector"/> 
        /// or <paramref name="comparer"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector, IComparer<TKey> comparer)
        {
            source.ThrowIfNull("source");
            selector.ThrowIfNull("selector");
            comparer.ThrowIfNull("comparer");
            using (IEnumerator<TSource> sourceIterator = source.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence was empty");
                }
                TSource max = sourceIterator.Current;
                TKey maxKey = selector(max);
                while (sourceIterator.MoveNext())
                {
                    TSource candidate = sourceIterator.Current;
                    TKey candidateProjected = selector(candidate);
                    if (comparer.Compare(candidateProjected, maxKey) > 0)
                    {
                        max = candidate;
                        maxKey = candidateProjected;
                    }
                }
                return max;
            }
        }

        internal static void ThrowIfNull<T>(this T argument, string name) where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        //Got this method from stack overflow - user Jon Rea posted it:
        //http://stackoverflow.com/questions/10632776/fastest-way-to-remove-duplicate-value-from-a-list-by-lambda
        /// <summary>
        /// Get a distinct list of a complicated element by using a specific property for comparison
        /// </summary>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var knownKeys = new HashSet<TKey>();

            return source.Where(element => knownKeys.Add(keySelector(element)));
        }

        /* I cannot put these methods into the KawaUtilLibrary because it is in 3.5 which does not support
         * System.Data.Entity.DbContext. These methods are going to have to stay here until we upgrade. */

        #region EF 4.3.1 Extention Methods
        public static DataTable ExecuteDataTable(this DbContext context, string sqlString)
        {
            using (SqlConnection con = new SqlConnection(context.GetConnectionString()))
            {
                return con.ExecuteDataTable(sqlString);
            }
        }

        public static DataSet ExecuteDataSet(this DbContext context, string sqlString)
        {
            using (SqlConnection con = new SqlConnection(context.GetConnectionString()))
            {
                return con.ExecuteDataSet(sqlString);
            }
        }

        public static void ExecuteNonQuery(this DbContext context, string sqlString)
        {
            using (SqlConnection con = new SqlConnection(context.GetConnectionString()))
            {
                con.ExecuteNonQuery(sqlString);
            }
        }

        /// <summary>
        /// Get the connection string from the provided DbContext
        /// </summary>
        /// <param name="context">the target context</param>
        /// <returns>the connection string this context is using</returns>
        public static string GetConnectionString(this DbContext context)
        {
            return context.Database.Connection.ConnectionString;
        }

        /// <summary>
        /// Insert the entity of type T into the appropriate entity collection of type T
        /// </summary>
        /// <typeparam name="T">an entity of type T</typeparam>
        /// <param name="context"></param>
        /// <param name="entityObject">an entity of type T</param>
        public static void Insert<T>(this DbContext context, T entityObject) where T : class
        {
            context.Set<T>().Add(entityObject);
            context.SaveChanges();
        }

        /// <summary>
        /// Update an entity of type T and only update the specified properties
        /// </summary>
        /// <typeparam name="T">an entity of type T</typeparam>
        /// <param name="context"></param>
        /// <param name="entityObject">an entity of type T</param>
        /// <param name="properties">a strict list of properties to update</param>
        public static void Update<T>(this DbContext context, T entityObject, params string[] properties) where T : class
        {
            context.Set<T>().Attach(entityObject); //Attach to the context

            var entry = context.Entry(entityObject); //Get the Entry for this entity

            //Mark each property provided as modified. All properties are initially 
            //assumed to be false - further more properties cannot be marked as false, 
            //this is sadly a limitation of EF 4.3.1
            foreach (string name in properties)
                entry.Property(name).IsModified = true;

            context.SaveChanges();
        }

        public static void SoftDelete<T>(this DbContext context, T entityObject) where T : class
        {
            SoftDelete(context, entityObject, null);
        }

        /// <summary>
        /// Mark an entity of type T as deleted. This is lazy deletion aka soft deletion. This method should only be used
        /// if the entity has a Boolean Property named "Deleted".
        /// </summary>
        /// <typeparam name="T">an entity of type T</typeparam>
        /// <param name="context"></param>
        /// <param name="entityObject">an entity of type T</param>
        /// <param name="properties">A list of other properties to update during the soft delete</param>
        public static void SoftDelete<T>(this DbContext context, T entityObject, params string[] properties) where T : class
        {
            //This method is the equivalent of setting the "Deleted" property to true and saving changes. 
            //Essentially this is a single property update.
            context.Set<T>().Attach(entityObject);

            var entry = context.Entry(entityObject);

            DbPropertyEntry p = entry.Property("Deleted");

            p.CurrentValue = true; //Mark this as deleted
            p.IsModified = true; //Mark this as modified

            if (properties != null)
            {
                foreach (string name in properties)
                    entry.Property(name).IsModified = true;
            }

            context.SaveChanges();
        }

        /// <summary>
        /// Permanently delete/remove an entity of Type T from it's corresponding entity collection.
        /// </summary>
        /// <typeparam name="T">an entity of type T</typeparam>
        /// <param name="context"></param>
        /// <param name="entityObject">an entity of type T</param>
        public static void HardDelete<T>(this DbContext context, T entityObject) where T : class
        {
            context.Set<T>().Attach(entityObject);
            context.Set<T>().Remove(entityObject);
            context.SaveChanges();
        }
        #endregion
    }
}
