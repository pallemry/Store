using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Functions.Extensions;
using static Functions.AdvancedConsole.AdvancedConsolePrinter;
namespace Functions.Calculator
{
    /// <summary>
    /// Used For More advanced calculations than <see cref="BasicCalculations"/> class, such as complex
    /// <c>algorithms</c> or any other type of advanced calculations
    /// </summary>
    public static class AdvancedCalculations
    {
        /// <summary>
        /// This is a test for the 1.0.21 version of this NuGet (Functions) .csporj
        /// </summary>
        /// <param name="idToCheck"></param>
        /// <returns></returns>
        public static bool ValidIsraeliID(string idToCheck)
        {
            var ind = 1;
            idToCheck = idToCheck.Trim();
            // Is the ID actually a 9 digits number?
            try
            {
                if (idToCheck.Length > 9)
                    return false;
                if (idToCheck.Length < 9)
                {
                    var idt = "000000000" + idToCheck;
                    for (var index = 0; index < idt.Length; index++)
                    {
                        var c = idt[index];
                        Print($"[{index}]: " + c + "\n");
                    }
                    idToCheck = idt.Substring(idt.Length - 8,8 );
                    
                }
                var id = int.Parse(idToCheck);
            }
            catch
            {
                return false;
            }
    
            // Calculate The Sum of the digits
            var results = new int[9];

            for (var i = 0; i < idToCheck.Length; i++)
            {
                var num = int.Parse(idToCheck[i].ToString()) * (i % 2 + 1);
                if (!num.IsInRange(0, 9))
                    num = num / 10 + num % 10;
                results[i] = num;
            }

            
            var x = idToCheck.Sum(c =>
            {
                ind++;
                var num = int.Parse(c.ToString()) * (ind % 2 + 1);
                if (!num.IsInRange(0, 9))
                    num = num / 10 + num % 10;
                return num;
            });
            PrintFormat($"!{x}! {results.Sum()}");
            // Is The Sum of the numbers actually divisible by 10?
            return results.Sum() % 10 == 0;
        }
    
        public static bool ValidIsraeliID(int idToCheck) => ValidIsraeliID(idToCheck.ToString());
    }
}
