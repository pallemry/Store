using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;

using static Functions.Calculator.BasicCalculations;

namespace Functions.Extensions
{
    public static class BasicTypesExtensions
    {
        // int 
        public static bool IsInRange(this int i, float min, float max) 
            => i >= min && i <= max;
        // float
        public static bool IsInRange(this float i, float min, float max)
            => i >= min && i <= max;
        // double 
        public static bool IsInRange(this double i, float min, float max) 
            => i >= min && i <= max;
        // decimal 
        public static bool IsInRange(this decimal i, float min, float max) 
            => i >=(decimal) min && i <=(decimal) max;

        public static bool IsComplete(this float i) => i % 1 == 0;
        public static bool IsComplete(this decimal i) => i % 1 == 0;
        public static bool IsComplete(this double i) => i % 1 == 0;
        public static bool IsNatural(this float i) => i.IsComplete() && i > 0;
        public static bool IsNatural(this int i) => i > 0;
        public static bool IsNatural(this double i) => i.IsComplete() && i > 0;
        public static bool IsNatural(this decimal i) => i.IsComplete() && i > 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="allowFloatingPoint"></param>
        /// <returns></returns>
        public static bool IsNumericType(this string s, bool allowFloatingPoint = false)
        {
            s = s.Trim();
            try
            {
                var a =allowFloatingPoint ? float.Parse(s) : ulong.Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string LimitSizeStart(this string s, int n)
        {
            return n < 1 ? throw new ArgumentOutOfRangeException(nameof(n), n, @"Minimum length is 1") :
                n >= s.Length ? GetEmptyString(n - s.Length)+s : s[..n];
        }
        public static string LimitSizeEnd(this string s, int n)
        {
            return n < 1 ? throw new ArgumentOutOfRangeException(nameof(n), n, @"Minimum length is 1") :
                n >= s.Length ? s + GetEmptyString(n - s.Length) : s[..n];
        }
        public static string LimitSizeMiddle(this string s, int n)
        {
            if (n < 1) throw new ArgumentOutOfRangeException(nameof(n), n, @"Minimum length is 1");
            if (n < s.Length)
                return s[..n];
            var total = n - s.Length;
            var r = total / 2.0 % 1 != 0 ? 1 : 0;
            return GetEmptyString(total / 2) + s + GetEmptyString(total / 2 + r);
        }

        public static long SumOfDigits(string s)
        {
            try
            {
                var ind = 1;
                return s.Sum(c =>
                {
                    ind++;
                    var num = int.Parse(c.ToString()) * (ind % 2 + 1);
                    if (!num.IsInRange(0, 9))
                        num = num / 10 + num % 10;
                    return num;
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
    
    
}
