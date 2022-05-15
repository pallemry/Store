using System;
using System.Collections.Generic;
using System.Globalization;
using Functions.AdvancedConsole;
using Functions.Calculator;
using Functions.Extensions;
using RideAndTrip.Trip;
using static System.Console;
using static Functions.AdvancedConsole.AdvancedConsolePrinter;

namespace QueuesSpooler.Main
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var menu = new Menu();
            menu.Start();
        }
    }
}