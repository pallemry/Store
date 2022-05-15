using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Functions.Extensions;
using static Functions.AdvancedConsole.AdvancedConsolePrinter;
// ReSharper disable MemberCanBePrivate.Global
namespace Functions.AdvancedConsole
{
    public static class ConsoleData
    {
        /// <summary>
        /// Returns a float number While asking the user for an Input
        /// <br><paramref name="nameOfInput"/> is the name of the input to be asked</br>
        /// </summary>
        /// <param name="nameOfInput"></param>
        /// <returns>A float number (floating point allowed) that the user provides</returns>
        public static float GetNumberFromUser(string nameOfInput)
        {
            return GetNumberFromUser(nameOfInput, f => true);
        }
        /// <summary>
        /// Returns a float number While asking the user for an Input that is a<br></br> valid number and matches the requirements specified in the
        /// <paramref name="pre"/> Predicate 
        /// </summary>
        /// <remarks>
        /// <br><paramref name="nameOfInput"/> is the name of the input to be asked</br>
        /// <br><paramref name="pre"/> the predicate to match the float number with</br>
        /// <br><paramref name="predicateReq"/> In case the user doesn't provide a valid number the system displays all of the potential reasons,<br></br>
        /// so that the user can fix his input according to requirements <br>For example: <para>If the following method is called
        /// <see cref="GetNumberFromUser(string,System.Predicate{float},string[])"/></para></br></br>
        /// <example>
        /// <param name="nameOfInput">is 5</param>
        /// </example>
        /// </remarks>
        /// <param name="nameOfInput"></param>
        /// <param name="pre"></param>
        /// <param name="predicateReq"></param>
        /// <returns></returns>
        public static float GetNumberFromUser(string nameOfInput, Predicate<float> pre, params string[] predicateReq)
        {
            var res = 0f;
            var af = new AdvancedFormatter("/t", ConsoleColor.Cyan,
                                           ConsoleColor.Black, FormatterConstruction.Regular);
            while (true)
            {
                PrintFormat($"/t{nameOfInput}/t: ", false, false, af); 
                var ans = Console.ReadLine();
                if (ans.IsNumericType(true)) res = float.Parse(ans);
                else
                {
                    PrintBasicFormat($"!Error! '{ans}' is not a float number");
                }
                if (!pre(res))
                {
                    PrintFormat($"!ERROR!: Wrong Input: Input did <b>not</b> meet the following Requirement(<b>s</b>):\n", true, false, af);
                    for (var i = 0; i < predicateReq.Length; i++)
                    {
                        var preR = predicateReq[i];
                        PrintFormat($"<ae>{preR}</ae>", false, false, new AdvancedFormatter("ae", ConsoleColor.White,
                            ConsoleColor.Red,
                            FormatterConstruction.Scrpting));
                        if (i < predicateReq.Length - 1)
                            PrintFormat("\n!OR!\n", false, false);
                    }
                    Console.WriteLine();
                }
                else if (ans.IsNumericType(true)) break;
            }
            return res;
        }
        /// <summary>
        /// Gets an int from the user
        /// </summary>
        /// <param name="nameOfInput"></param>
        /// <param name="pre"></param>
        /// <param name="predicateReq"></param>
        /// <returns></returns>
        public static int GetIntFromUser(string nameOfInput, Predicate<int> pre, params string[] predicateReq)
        {
            var res = 0;
            var af = new AdvancedFormatter("/t", ConsoleColor.Cyan,
                ConsoleColor.Black, FormatterConstruction.Regular);
            while (true)
            {
                PrintFormat($"/t{nameOfInput}/t: ", false, false, af);
                var ans = Console.ReadLine();
                if (ans.IsNumericType()) res = int.Parse(ans ?? "e");
                else
                {
                    PrintBasicFormat($"!Error! '{ans}' is not an int number");
                }

                if (!pre(res))
                {
                    PrintFormat($"!ERROR!: Wrong Input: Input did <b>not</b> meet the following Requirement(<b>s</b>):",
                                true, false, af);

                    for (var i = 0; i < predicateReq.Length; i++)
                    {
                        var preR = predicateReq[i];
                        PrintFormat($"<ae>{preR}</ae>", false, false, new AdvancedFormatter("ae", ConsoleColor.White,
                                     ConsoleColor.Red,
                                     FormatterConstruction.Scrpting));
                        if (i < predicateReq.Length - 1) PrintFormat("\n!OR!\n", false, false);
                    }

                    Console.WriteLine();
                }
                else if (ans.IsNumericType()) break;
            }
            return res;
        }
        public static long GetLongFromUser(string nameOfInput, Predicate<long> pre, params string[] predicateReq)
        {
            var res = 0L;
            var af = new AdvancedFormatter("/t", ConsoleColor.Cyan,
                ConsoleColor.Black, FormatterConstruction.Regular);
            while (true)
            {
                PrintFormat($"/t{nameOfInput}/t: ", false, false, af);
                var ans = Console.ReadLine();
                if (ans.IsNumericType()) res = long.Parse(ans ?? "e");
                else
                {
                    PrintBasicFormat($"!Error! '{ans}' is not a float number");
                }

                if (!pre(res))
                {
                    PrintFormat($"!ERROR!: Wrong Input: Input did <b>not</b> meet the following Requirement(<b>s</b>):",
                                true, false, af);

                    for (var i = 0; i < predicateReq.Length; i++)
                    {
                        var preR = predicateReq[i];
                        PrintFormat($"<ae>{preR}</ae>", false, false, new AdvancedFormatter("ae", ConsoleColor.White,
                                     ConsoleColor.Red,
                                     FormatterConstruction.Scrpting));
                        if (i < predicateReq.Length - 1) PrintFormat("\n!OR!\n", false, false);
                    }

                }
                else if (ans.IsNumericType()) break;
                Console.WriteLine();
            }
            return res;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="numOfTabs"></param>
        /// <returns></returns>
        public static DateTime GetDateFromUser(int numOfTabs = 0)
        {
            var tabs = string.Empty;
            for (var i = 0; i < numOfTabs -1; i++) { tabs += "   "; }
            PrintFormat($"<b>{tabs}DateTime:</b>\n", false, false);
            tabs += "   ";

            var year = (int) GetNumberFromUser($"{tabs}Year", f => f.IsInRange(2000, 3000),
                "Year Range: 2000 - 3000");
            var month = (int) GetNumberFromUser($"{tabs}Month", f => f.IsInRange(1, 12), 
                "Month Range: 1 - 12");
            var day = (int)GetNumberFromUser($"{tabs}Day", f => f.IsInRange(1, 30), 
                "Day Range: 1 - 30");
            var hour = (int)GetNumberFromUser($"{tabs}Hour", f => f.IsInRange(1, 23),
                "Hour Range: 1 - 23");
            try { return new DateTime(year, month, day, hour, 0, 0); }
            catch { return new DateTime(year, month, 1, hour, 0, 0); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameOfInput"></param>
        /// <param name="caseSensitive"></param>
        /// <param name="overrideColors"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string GetSpecificAns(string nameOfInput, bool caseSensitive, bool overrideColors = true, params string[] options) 
            => GetSpecificAns(nameOfInput, caseSensitive, overrideColors, s => true, options, Array.Empty<string>());
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameOfInput"></param>
        /// <param name="caseSensitive"></param>
        /// <param name="overrideColors"></param>
        /// <param name="pre"></param>
        /// <param name="options"></param>
        /// <param name="requirements"></param>
        /// <param name="includeOptionsWhenError"></param>
        /// <returns></returns>
        public static string GetSpecificAns(string nameOfInput, bool caseSensitive, bool overrideColors, Predicate<string> pre, string[] options, string[] requirements,
            bool includeOptionsWhenError = false)
        {
            #region Color Managment
            var af = new AdvancedFormatter("/t", ConsoleColor.Cyan,
                ConsoleColor.Black, FormatterConstruction.Regular);
            var varOver = overrideColors ? "/t" : "";
            var blueStart = overrideColors ? DefaultFormatters.Find(a => a.ForeColor == ConsoleColor.Blue)?.StartSeparator : "";
            var blueEnd = overrideColors ? DefaultFormatters.Find(a => a.ForeColor == ConsoleColor.Blue)?.EndSeparator : "";
            var red = overrideColors ? DefaultFormatters.Find(a => a.ForeColor == ConsoleColor.Red)?.StartSeparator : "";
            var green = overrideColors ? DefaultFormatters.Find(a => a.ForeColor == ConsoleColor.Green)?.StartSeparator : "";
            #endregion
            while (true)
            {
                PrintFormat(!nameOfInput.Equals(string.Empty) ? $"{varOver}{nameOfInput}{varOver}: " : "", false, false, af);
                var ans = Console.ReadLine();
                if (options.Any(option => caseSensitive ? option.Equals(ans) : option.ToLower().Equals(ans?.ToLower()) && pre(ans))) return ans;
                PrintFormat($"{red}Invalid Input{red} - {blueStart}'{ans}'{blueEnd} does not meet the following {green}requirement{green}({blueStart}s{blueEnd})", true, false);
                var fullReq = includeOptionsWhenError ? options.JoinTogether(requirements).ToArray() : requirements;
                for (var i = 0; i < fullReq.Length; i++)
                {
                    var preR = fullReq[i];
                    PrintFormat($"{(overrideColors ? "<ae>" : "")}{preR}{(overrideColors ? "</ae>" : "")}", false, false,
                        new AdvancedFormatter("ae", ConsoleColor.White,
                        ConsoleColor.Red,
                        FormatterConstruction.Scrpting));
                    if (i < fullReq.Length - 1)
                        PrintFormat($"\n{(overrideColors ? "e" : "")}OR{(overrideColors ?  "e" : "")}\n", false, false, 
                            new AdvancedFormatter("e", ConsoleColor.DarkMagenta, ConsoleColor.Black, "e"));
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameOfInput"></param>
        /// <param name="caseSensitive"></param>
        /// <param name="pre"></param>
        /// <param name="excludedOptions"></param>
        /// <param name="requirements"></param>
        /// <returns></returns>
        public static string GetExcludedSpecificAns(string nameOfInput, bool caseSensitive, Predicate<string> pre, string[] excludedOptions, string[] requirements)
        {
            var options = excludedOptions;
            var af = new AdvancedFormatter("/t", ConsoleColor.Cyan,
                ConsoleColor.Black, FormatterConstruction.Regular);
            while (true)
            {
                var b = false;
                PrintFormat($"/t{nameOfInput}/t: ", false, false, af);
                var ans = Console.ReadLine();
                foreach (var VARIABLE in options)
                {
                    if (caseSensitive ? VARIABLE.Equals(ans) : VARIABLE.ToLower().Equals(ans?.ToLower()) || !pre(ans))
                    {
                        PrintFormat("!Invalid Input!", true, false);
                        var fullreq = options.JoinTogether(requirements).ToArray();
                        for (var i = 0; i < fullreq.Length; i++)
                        {
                            var preR = fullreq[i];
                            PrintFormat($"<ae>{preR}</ae>", true, false, new AdvancedFormatter("ae", ConsoleColor.White,
                                ConsoleColor.Red,
                                FormatterConstruction.Scrpting));
                            if (i < fullreq.Length - 1)
                                PrintFormat("!OR!", true, false);
                            b = true;
                        }
                    }
                }
                if (!b)
                    return ans;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameOfInput"></param>
        /// <param name="caseSensitive"></param>
        /// <param name="excludedOptions"></param>
        /// <returns></returns>
        public static string GetExcludedSpecificAns(string nameOfInput, bool caseSensitive, params string[] excludedOptions)
        {
            var options = excludedOptions;
            var af = new AdvancedFormatter("/t", ConsoleColor.Cyan,
                ConsoleColor.Black, FormatterConstruction.Regular);
            while (true)
            {
                PrintFormat($"/t{nameOfInput}/t: ", true, false, af);
                var ans = Console.ReadLine();
                if (options.Any(option => caseSensitive ? !option.Equals(ans) : !option.ToLower().Equals(ans?.ToLower()))) return ans;
                PrintFormat("!Invalid Input!", true, false);
                for (var i = 0; i < options.Length; i++)
                {
                    var preR = options[i];
                    PrintFormat($"<ae>{preR}</ae>", true, false, new AdvancedFormatter("ae", ConsoleColor.White,
                        ConsoleColor.Red,
                        FormatterConstruction.Scrpting));
                    if (i < options.Length - 1)
                        PrintFormat("!OR!", true, false);
                }
            }
        }
        /// <summary>
        /// Gets a true or false value from the user
        /// </summary>
        /// <remarks>
        /// Asks a user for input as the following syntax <br></br>
        /// "<paramref name="nameOfInput"/>(Y/N): * here goes user input *
        /// </remarks>
        /// <param name="nameOfInput"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool GetBoolFromUser(string nameOfInput)
        {
            var ans = GetSpecificAns(nameOfInput+"(!Y!/!N!)", false, false, s => true,
                new []{ "Y", "N" }, new []{"Answer must be y/n"} ).ToLower();
            return ans switch
            {
                "y" => true,
                "n" => false,
                _ => throw new ArgumentException("Failed to get yes or no"),
            };
        }
	
    }
}
