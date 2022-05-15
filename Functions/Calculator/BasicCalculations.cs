using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Functions.Extensions;

namespace Functions.Calculator
{
    public class BasicCalculations
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string GetEmptyString(int n)
        {
            var res = "";
            for (int i = 0; i < n; i++)
            {
                res += " ";
            }
            return res;
        }
        public static bool ReachedMax(int val, ref int max)
        {
            var g = val > max;
            max = g ? val : max;
            return g;
        }

        public static bool ReachedMin(int val, ref int min)
        {
            var g = val < min;
            min = g ? val : min;
            return g;
        }
        public static string GetHtmlRepresentation(string s, bool close = false) => "<" + (close ? "/" : "") + s + ">";
        public static string ReverseSpecifiedChar(string s, char separator = '@', bool separateWords = false)
        {
            var result = "";
            var strings = s.Split(separator).ToStack();
            if (strings.Length() % 2 == 0)
            {
                throw new ArgumentException();
            }
            while (strings.Length() > 0)
            {
                var temp = strings.Pop();
                result += strings.Length() % 2 != 0 ? temp.Reverse().Aggregate("", (s1, c) => s1 + c) : temp;
                result += separateWords ? " " : "";
            }

            result = separateWords ? result[..^1] : result;
            return result;
        }
    }
    public static class ScreenCalculate
    {
        public class a
        {
            public a(int x)
            {
                
            }

            a p()
            {
                return new(2);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Point GetMiddleFormPoint(this Form f)
        {
            var w =  f.Width / 2;
            var h =  f.Height/ 2;
            return new Point(w, h);
        }
        public static Point GetMiddleScreenPointRelativeToForm(this Form f)
        {
            var s = Screen.PrimaryScreen.Bounds;
            var w = (s.Width - f.Width )/2;
            var h = (s.Height - f.Height) / 2;
            return new Point(w,h);
        }
        /// <summary>
        /// Get the middle screen point For example :
        /// If the your screen width is <c>50px</c> and height is <c>30px</c>
        /// than the returned <c>Point</c> is <c>(25, 15)</c>
        /// </summary>
        /// <returns></returns>
        public static Point GetMiddleScreen()
        {
            var s = Screen.PrimaryScreen.Bounds;
            var w = s.Width / 2;
            var h = s.Height / 2;
            return new Point(w, h);
        }
    }
    

    /// <summary>
    /// 
    /// </summary>
    public static class DateManipulations
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToStringPlus(this DateTime dt) =>
            $"{dt:HH:mm:ss tt zz}({GetCurrentGmt()} GMT) {dt:yyyy MMMM dd}";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentGmt()
        {
            var gmt = (DateTime.UtcNow - DateTime.Now).Hours;
            return $"{ (gmt > 0 ? "+" : gmt == 0 ? string.Empty : "-") + (gmt.ToString().Length == 2 ? gmt.ToString() : "0" + gmt)}";
        }
    }
    
}
