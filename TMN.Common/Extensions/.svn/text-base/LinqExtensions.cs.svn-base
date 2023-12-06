using System;
using System.Collections.Generic;
using System.Linq;


namespace TMN
{
    public static class LiquExtensions
    {

        public static string Concatinate<TSource>(this IEnumerable<TSource> source, Func<TSource, string> selector)
        {
            string result = "";
            foreach (var item in source)
            {
                result += selector(item) + ", ";
            }
            return result.TrimEnd(new char[] { ' ', ',' });
        }

        public static bool AllTheSame<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {

            TSource first = source.First();
            foreach (var item in source)
            {
                if (selector(item) == null && selector(first) == null)
                {
                    continue;
                }
                else if ((selector(item) == null && selector(first) != null)
                    || (selector(item) != null && selector(first) == null)
                    || !(selector(item).Equals(selector(first))))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Converts 1,2,3,7,8,9 to 7,8,9,1,2,3
        /// </summary>        
        public static IEnumerable<TSource> SequencialSort<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            if (!source.Any())
            {
                return source;
            }
            var sortedSource = source.OrderBy(s => selector(s));
            int counter = selector(sortedSource.First());
            var firstHalf = sortedSource.TakeWhile(s => selector(s) == counter++).ToArray();
            counter = selector(sortedSource.First());
            var secondHalf = sortedSource.SkipWhile(s => selector(s) == counter++).ToArray();
            return secondHalf.Union(firstHalf);
        }

        public static Exception SubmitChangesTransactional(this System.Data.Linq.DataContext db)
        {
            if (db.Connection.State != System.Data.ConnectionState.Open)
            {
                db.Connection.Open();
            }
            db.Transaction = db.Connection.BeginTransaction();
            try
            {
                db.SubmitChanges();
                db.Transaction.Commit();
                return null;
            }
            catch (Exception ex)
            {
                db.Transaction.Rollback();
                return ex;
            }
            finally
            {
                db.Connection.Close();
                db.Transaction.Dispose();
                db.Transaction = null;
            }
        }


    }
}
