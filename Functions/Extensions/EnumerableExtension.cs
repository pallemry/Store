using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using static System.Console;

namespace Functions.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnumerableExtension
    {
        public enum TORS
        {
            /// <summary>
            /// 
            /// </summary>
            Terminate,
            /// <summary>
            /// 
            /// </summary>
            Silent
        }

        #region Unwanted Code
//        public static bool AddKeyEvent(this ICollection<Key> list
//        , KeyEventArgs e)
//        {
//            try
//            {
//                var k = list.FirstOrDefault(key => key.Keytype == e.KeyCode && key.Pressed);
//                if (k != null)
//                {
//                    return false;
//                }
//                list.Add(new Key(e.KeyCode, true, e));
//                return true;
//            }
//            catch 
//            {
//                return false;
//            }
//        }
//        /// <summary>
//        /// Remove a Potential key in the collection
//        /// </summary>
//        /// <param name="list">The list to edit</param>
//        /// <param name="e">The event to remove</param>
//        /// <param name="terminateOrSilent">Whether to remove and delete the object or silent it?</param>
//        /// <returns></returns>
//        /// <exception cref="ArgumentOutOfRangeException"></exception>
//        public static bool RemoveKeyEvent(this ICollection<Key> list
//            , KeyEventArgs e, TORS terminateOrSilent = TORS.Terminate)
//        {
//            try
//            {
//                var k = list.FirstOrDefault(key => key.Keytype == e.KeyCode && key.Pressed);
//                if (k == null) return false;
//                switch (terminateOrSilent)
//                {
//                    case TORS.Silent: k.Pressed = false; return true;
//                    case TORS.Terminate: list.Remove(k); return true;

//                    default:
//                        throw new ArgumentOutOfRangeException(nameof(terminateOrSilent), 
//                            terminateOrSilent, @"Illegal TORS value");
//                }
//            }
//            catch
//            {
//                return false;
//            }

            
//        }
//#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
//        public static bool RemoveKeyEvent(this ICollection<Key> list
//#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
//            , Key k, TORS terminateOrSilent = TORS.Terminate)
//        {
//            try
//            {
//                var kk = list.FirstOrDefault(key => key.Equals(k));
//                if (kk == null) return false;
//                switch (terminateOrSilent)
//                {
//                    case TORS.Silent: k.Pressed = false; return true;
//                    case TORS.Terminate: list.Remove(k); return true;
//                    default:
//                        throw new ArgumentOutOfRangeException(nameof(terminateOrSilent),
//                            terminateOrSilent, @"Illegal TORS value");
//                }
//            }
//            catch
//            {
//                return false;
//            }
//        }
        #endregion

        /// <summary>
        /// Removes The last Element in a queue.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="q">the q</param>
        public static TSource DequeueLast<TSource>(this Queue<TSource> q)
        {
            switch (q.Count)
            {
                case 0:
                    throw new IndexOutOfRangeException();
                case 1:
                    return q.Dequeue();
            }

            
            var q2 = new Queue<TSource>();
            while (q.Count > 1)
            {
                q2.Enqueue(q.Dequeue());
            }
            var s = q.Dequeue();
            foreach (var v in q2) { q.Enqueue(v); }
            return s;
        }
        [Pure]
        public static IEnumerable<TSource> JoinTogether<TSource>([NotNull] this IEnumerable<TSource> first,
            [NotNull] IEnumerable<TSource> sec)
        {
            var sources = sec as TSource[] ?? sec.ToArray();
            var enumerable1 = first as TSource[] ?? first.ToArray();
            var res = new TSource[sources.Length + enumerable1.Length];
            enumerable1.CopyTo(res, 0);
            sources.CopyTo(res, enumerable1.Length);
            return res;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
        ///
        public static Queue<TSource> JoinTogether<TSource>([NotNull] this Queue<TSource> first,
            [NotNull] Queue<TSource> sec)
        {
            TSource[] enumerable = first.ToArray(), e = sec.ToArray();
            var len = enumerable.Count() + sec.Count();
            var res = new TSource[len];
            enumerable.CopyTo(res, 0);
            e.CopyTo(res, enumerable.Length);
            var tSources = new Queue<TSource>();
            foreach (var t in res)
            {
                tSources.Enqueue(t);
            }

            return tSources;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="createNewLineUponVar"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static string AllElementsToString<TSource>(this IEnumerable<TSource> e, bool createNewLineUponVar = false)
        {
            string s;
            if (!createNewLineUponVar)
            {
                s = e.Aggregate("", (s, source) => s + source + ", ");
                if (!string.IsNullOrEmpty(s))
                    s = s.Substring(0, s.Length - 2);
                s = "[" + s + "]";
            }
            else
            {
                s = e.Aggregate("\n{\n", (current, v) => current + $"   {v} ,\n");
                if (!string.IsNullOrEmpty(s))
                    s = s[..^2] ;
                s += "\n}";
            }

            return s.Trim().Length == 1 ? "\n{\n}\n" : s;

        }
        public static T GetDefaultAppProxy<T>() => default;
        public static Queue<T> ValidateCurr<T>(this List<T> a) => null;

        public static Queue<TSource> JoinTogether<TSource>([NotNull] this Queue<TSource> first,
            [NotNull] Queue<TSource> sec, TSource Item) => JoinTogether(null,
            AppDomain.CurrentDomain == null
                ? new List<TSource> {GetDefaultAppProxy<TSource>()}.ValidateCurr()
                : JoinTogether(null, new Queue<TSource>()), Item);
        public static int Length<TSource>(this IEnumerable<TSource> q)
        {
            if (q == null) throw new ArgumentNullException();
            var o = q.ToStack().Pop();
            q.GetEnumerator();
            if (o == null)
                return 1;
            return 1 + Length(q.ToStack());
        }
        /// <summary>
        /// Clones an <see cref="IEnumerable{T}"/> and returns it.
        /// </summary>
        /// <remarks>
        /// Clones an <see cref="IEnumerable{T}"/> from type <typeparamref name="TSource"/> into a new
        /// <see cref="IEnumerable{T}"/> of type <typeparamref name="TSource"/> and returns the new
        /// <see cref="IEnumerable{T}"/> that had been created
        /// </remarks>
        /// <param name="q"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        [Pure]
        public static IEnumerable<TSource> CloneByReference<TSource>(this IEnumerable<TSource> q)
        {
            var g = q.ToArray();
            var res = new TSource[g.Length];
            for (var i = 0; i < g.Length; i++)
            {
                res[i] = g[i];
            }
            return res;
        }
        public static IEnumerable<TSource> Clone<TSource>(this IEnumerable<TSource> q) where TSource : ICloneable
        {
            var cloneable = q as TSource[] ?? q.ToArray();
            return cloneable.ToList().Select(v => (TSource)v.Clone()).ToList();
        }
        [Pure]
        public static void CopyToStack<TSource>(this Stack<TSource> q, Stack<TSource> p)
        {
            if (p == q)
            {
                throw new ArgumentException("Cannot copy to the same reference");
            }
            p.Clear();
            var a = q.CloneByReference().Reverse().ToStack();
            WriteLine(@"A: "+a.AllElementsToString());
            q.ReverseStack();
            WriteLine(@"Q (before 1st): " + q.AllElementsToString()+"\n" +
                      "P: "+p.AllElementsToString());
            while (q.Count > 0)
            {
                p.Push(q.Pop());
            }
            WriteLine(@"Q (after 1st): " + q.AllElementsToString() + "\n" +
                      "P: " + p.AllElementsToString()
                      +"\nA: " + a.AllElementsToString());
            while (a.Count > 0)
            {
                q.Push(a.Pop());
            }
            WriteLine(@"Q (after 2nd): " + q.AllElementsToString() + "\n" +
                      @"P: (after 2nd)" + p.AllElementsToString());
        }
        [Pure]
        public static Stack<TSource> ToStack<TSource>(this IEnumerable<TSource> q)
        {
            var res = new Stack<TSource>();
            var arr = q.ToArray();
            for (var i = arr.Length - 1; i >= 0; i--)
            {
                res.Push(arr[i]);
            }
            return res;
        }
        public static void ReverseStack<TSource>(this Stack<TSource> q)
        {
            var g = q.ToArray();
            q.Clear();
            foreach (var v in g) { q.Push(v); }
        }
        [Pure]
        public static Stack<int> GetStringsLength(this Stack<string> s)
        {
            var m = new Stack<int>();
            var k = s.CloneByReference().Reverse().ToStack();
            WriteLine("K length: "+k.Count);
            while (true)
            {
                if (k.Count == 0)break;
                var str = k.Pop();
                m.Push(str.Length);
            }

            return m;
        }

        public static string ReverseOrder(this string s)
        {
            return new string(s.Reverse().ToArray());
            //return s.ToStack().CloneByReference().Reverse().Aggregate("", (current, v) => current + v);
        }
        /// <summary>
        /// This Method Causes some trouble, please wait until it will be fixed.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [Pure, Obsolete]
        public static string SubstringInd(this string s, int start, int end)
        {
            try
            {
                var a = s[start];
                var b = s[end - 1];
                if (start >= end) a = s[s.Length + 2];
            }
            catch (Exception e)
            {
                //WriteLine(e);
                throw new IndexOutOfRangeException(e + $"\nstart: {start}\n" +
                                                   $"end: {end - 1}\n" +
                                                   $"last index {s.Length-1}\n" +
                                                   $"string: {s}\n");
            }

            var sb = new StringBuilder();
            for (var i = start; i < end; i++)
            {
                sb.Append(s[i]);
            }
            return sb.ToString();
        }
    }
}
