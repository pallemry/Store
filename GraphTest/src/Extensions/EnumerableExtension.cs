using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GraphTest.Base_Classes;

namespace GraphTest.Extensions
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
        public static bool AddKeyEvent(this ICollection<Key> list
        , KeyEventArgs e)
        {
            try
            {
                var k = list.FirstOrDefault(key => key.Keytype == e.KeyCode && key.Pressed);
                if (k != null)
                {
                    return false;
                }
                list.Add(new Key(e.KeyCode, true, e));
                return true;
            }
            catch 
            {
                return false;
            }
        }
        /// <summary>
        /// Remove a Potential key in the collection
        /// </summary>
        /// <param name="list">The list to edit</param>
        /// <param name="e">The event to remove</param>
        /// <param name="terminateOrSilent">Whether to remove and delete the object or silent it?</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static bool RemoveKeyEvent(this ICollection<Key> list
            , KeyEventArgs e, TORS terminateOrSilent = TORS.Terminate)
        {
            try
            {
                var k = list.FirstOrDefault(key => key.Keytype == e.KeyCode && key.Pressed);
                if (k == null) return false;
                switch (terminateOrSilent)
                {
                    case TORS.Silent: k.Pressed = false; return true;
                    case TORS.Terminate: list.Remove(k); return true;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(terminateOrSilent), 
                            terminateOrSilent, @"Illegal TORS value");
                }
            }
            catch
            {
                return false;
            }

            
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static bool RemoveKeyEvent(this ICollection<Key> list
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
            , Key k, TORS terminateOrSilent = TORS.Terminate)
        {
            try
            {
                var kk = list.FirstOrDefault(key => key.Equals(k));
                if (kk == null) return false;
                switch (terminateOrSilent)
                {
                    case TORS.Silent: k.Pressed = false; return true;
                    case TORS.Terminate: list.Remove(k); return true;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(terminateOrSilent),
                            terminateOrSilent, @"Illegal TORS value");
                }
            }
            catch
            {
                return false;
            }
        }


    }
}
