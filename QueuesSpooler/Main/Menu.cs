using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Functions.AdvancedConsole;
using Functions.Calculator;
using Functions.Extensions;
using RideAndTrip.Trip;
using static Functions.AdvancedConsole.AdvancedConsolePrinter;
using static Functions.AdvancedConsole.ConsoleData;

namespace QueuesSpooler.Main
{
    #region Enums and Structs

    internal enum UserFunction
    {
    }

    internal enum State
    {
        Menu,
        Trip,
        Spooler
    }

    internal struct Function
    {
        public Function(string literalStringFunc, State funcType)
        {
            LiteralStringFunc = literalStringFunc;
            FuncType = funcType;
        }

        public State FuncType { get; set; }
        public string LiteralStringFunc { get; set; }
    }

    public enum Data
    {
        Main,
        Svt,
        Trip,
        Family
    }

    #endregion

    public class Menu
    {
        #region Propeties

        private Commands Commands { get; set; }
        public readonly List<SaVeTayel> Companies = new List<SaVeTayel>();
        private State State { get; set; } = State.Menu;
        private bool Running { get; set; }
        public List<Spooler> Printers { get; set; } = new List<Spooler>();
        private Spooler CurrentPrinter { get; set; }

        #endregion

        #region Constructor(s)

        public Menu()
        {
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
            Commands = new Commands(this);
        }

        #endregion

        #region Main Handlers

        public void Start()
        {
            Running = true;
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-gb");
            var s = new SaVeTayel(1);
            s.AddTrip(new Trip(2, "", DateTime.MinValue, 20));
            Companies.Add(s);
            Run();
        }

        private void Run()
        {
            while (Running)
            {
                try
                {
                    Console.Write("");
                }
                catch
                {
                    return;
                }

                PrintRelativeToState();
            }
        }

        private void PrintRelativeToState()
        {
            switch (State)
            {
                case State.Menu:
                    PrintMenu();
                    break;
                case State.Trip:
                    PrintTrip();
                    break;
                case State.Spooler:
                    PrintSpooler();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PrintMenu()
        {
            PrintFormat("====== aMAIN MENUa =====", true, false, new AdvancedFormatter("a",
                ConsoleColor.Magenta, ConsoleColor.Blue, FormatterConstruction.Regular));
            while (true)
            {
                PrintFormat("!What would you like to do?!\n" +
                            "#(#!#1!#)#See ~Trip~ Management.\n" +
                            "#(#!#2!#)#See ~Spooler~ Examples.\n" +
                            "#(#!#2!#)#See <a>GetStringsLength()</a> method Examples.", true, false,
                    new AdvancedFormatter(
                        "a", ConsoleColor.Black, ConsoleColor.Blue, FormatterConstruction.Scrpting));
                var ans = GetSpecificAns("", false, true, s => true, new[] {"quit", "1", "2", "3"},
                    new[] {"Must be a num between 1-3", "'QUIT' Command"});
                switch (ans)
                {
                    case "quit":
                        Running = false;
                        return;
                    case "1":
                        State = State.Trip;
                        return;
                    case "2":
                        State = State.Spooler;
                        return;
                    default:
                        PrintFormat($"'<b>{ans}</b>' is not a recognizable command.\n", true, false);
                        break;
                }
            }
        }

        #endregion

        #region Spooler - Related

        private void PrintSpooler()
        {
            PrintFormat("====== a PRINTER MANAGEMENT a =====", true, false, new AdvancedFormatter("a",
                ConsoleColor.Magenta, ConsoleColor.Blue, FormatterConstruction.Regular));
            while (true)
            {
                var head = CurrentPrinter == null
                    ? "<b>No Printer Logged In</b>\\>"
                    : $"<b>CPC</b>:\\!{CurrentPrinter.Code}!\\>";
                PrintFormat(head, false, false);
                var command = Console.ReadLine()?.Trim().ToLower();
                switch (command, command != null)
                {
                    case ("log out", _):
                    case ("logout", _):
                    case ("log-out", _):
                        PrintFormat(CurrentPrinter == null
                                ? "!Can! <b>not</b> !Log out because System is! <b>not</b> !logged in to any printer!\n"
                                : $"#Successfully# <b>logged out of printer</b> (#code# = !{CurrentPrinter.Code}!)\n",
                            false, false);
                        CurrentPrinter = null;
                        break;
                    case ("log in", _):
                    case ("login", _):
                    case ("log-in", _):
                        PrintBasicFormat("#Printer Code# :");
                        while (true)
                        {
                            var code = Console.ReadLine();
                            if (code is "cancel") break;
                            if (code.IsNumericType() && code != null)
                            {
                                var c = int.Parse(code);
                                var printer = Printers.Find(p => p.Code == c);
                                if (printer == null)
                                {
                                    PrintBasicFormat(
                                        $"!No printer with the code! #{c}# !Has been found, if you wish to stop the log in type! '~CANCEL~'\n");
                                }
                                else
                                {
                                    PrintFormat(
                                        $"#Successfully# <b>logged in to printer</b> (#code# = !{printer.Code}!)\n",
                                        false, false);
                                    CurrentPrinter = printer;
                                    break;
                                }
                            }
                            else
                            {
                                PrintBasicFormat($"!{code} Must be a natural number!\n");
                            }

                            PrintBasicFormat("#Printer Code# :");
                        }
                        break;
                    case ("sign up", _):
                    case ("signup", _):
                    case ("sign-up", _):
                        PrintBasicFormat("#Printer Code# :");
                        while (true)
                        {
                            var code = Console.ReadLine()?.Trim().ToLower();
                            if (code is "cancel") break;
                            if (code.IsNumericType() && code != null)
                            {
                                var c = int.Parse(code);
                                var printer = Printers.Find(p => p.Code == c);
                                if (printer != null)
                                {
                                    PrintBasicFormat(
                                        $"!A printer with the code! #{c}# !Already exists, if you wish to stop the log in type! '~CANCEL~'\n");
                                }
                                else
                                {
                                    CurrentPrinter = new Spooler(c);
                                    Printers.Add(CurrentPrinter);
                                    PrintFormat($"#Successfully# <b>created a printer with the</b> (#code# = !{c}!)\n",
                                        false, false);
                                    break;
                                }
                            }
                            else
                            {
                                PrintBasicFormat($"!{code} Must be a natural number!\n");
                            }

                            PrintBasicFormat("#Printer Code# :");
                        }
                        break;
                    case ("quit", _):
                        State = State.Menu;
                        if (CurrentPrinter != null)
                            CurrentPrinter = GetBoolFromUser($"Do you want to log out from current printer " +
                                                             $"(code = {CurrentPrinter.Code}) before you quit")
                                ? null
                                : CurrentPrinter;
                        return;
                    case ("string", _):
                    case ("print", _):
                        if (CurrentPrinter == null)
                        {
                            PrintFormat(
                                "!Can! <b>not</b> !Print Files because System is! <b>not</b> !logged in to any printer!\n");
                            break;
                        }
                        if (!CurrentPrinter.AnyFilesLeft())
                        {
                            PrintFormat(
                                "!Can! <b>not</b> !Print Files because there are! <b>not</b> !any files to print, Use the! " +
                                "<b>add file</b> !command to! <b>add</b> !a! <b>new</b> #file#\n");
                            break;
                        }
                        var ts = CurrentPrinter.AllFiles.Peek();
                        PrintFormat($"#FILE ABOUT TO PRINT#: {ts}");
                        PrintFormat($"All Files Before printing: \n{CurrentPrinter}\n");
                        var speed = GetNumberFromUser("Speed of printing", f => f.IsInRange(0.01f, 100), "Speed Range 0.01 - 100");
                        CurrentPrinter.Print(speed);
                        PrintFormat($"All Files After: \n{CurrentPrinter}\n");
                        break;
                    case ("add file", _):
                    case ("addfile", _):
                    case ("add-file", _):
                        if (CurrentPrinter == null)
                        {
                            PrintFormat(
                                "!Can! <b>not</b> !Add Files because System is! <b>not</b> !logged in to any printer!\n");
                            break;
                        }

                        FileToPrint ftp = GetFileToPrintFromUser(CurrentPrinter);
						CurrentPrinter.AddFile(ftp);
                        break;
                    default:
                        PrintFormat($"<e>\"{command}\" is not a recognizable command</e>\n", false, false,
                            new AdvancedFormatter("e", ConsoleColor.Black, ConsoleColor.Red,
                                FormatterConstruction.Scrpting));
                        break;
                }
            }
        }

        private FileToPrint GetFileToPrintFromUser(Spooler currentPrinter)
        {
            var code = GetIntFromUser("Code", i => currentPrinter.AllFiles.FirstOrDefault(a => a.Code == i) == null
                , "A file with that code might already exist");
            var ls = GetSpecificAns("Long / Short", false, true, "lONg", "ShORT").ToLower() switch
            {
                "long" => LOrS.Long,
                "short" => LOrS.Short,
                _=> throw new ArgumentException("Must be long or short")
            };
            var ans = GetSpecificAns("Date to Print :(Custom / Auto", false, true, "auto", "custom").ToLower() switch
            {
                "auto" => Answer.Auto,
                "short" => Answer.Custom,
                _ => throw new ArgumentException("Must be long or short")
            };
            var hours = GetIntFromUser("Hours", i => i > 0, "Must be positive");
            var minutes = GetIntFromUser("Minutes", i => i > 0, "Must be positive");
            var seconds = GetIntFromUser("Seconds", i => i > 0, "Must be positive");
            var fileSize = GetLongFromUser("File Size", i => i > 0, "File Size must be positive and greater than 0");
            var name = GetExcludedSpecificAns("Name", false, s => s.Length.IsInRange(3, 10), Array.Empty<string>(),
                new[] {"Name Length Range 3 - 10"});
            return null;
        }

        #endregion

        #region Trip - Related

        private void PrintTrip(string s = "")
        {
            PrintFormat("====== aTRIP MANAGEMENTa =====", true, false, new AdvancedFormatter("a",
                ConsoleColor.Magenta, ConsoleColor.Blue, FormatterConstruction.Regular));
            while (true)
            {
                if (s.Equals("quit"))
                {
                    State = State.Menu;
                    return;
                }

                var command = Console.ReadLine()?.Trim();
                switch (command)
                {
                    case "add":
                        try
                        {
                            PrintBasicFormat("#Source#: ");
                            var srcArr = Console.ReadLine()?.Split(" ");
                            PrintBasicFormat("#Destination#: ");
                            var destArr = Console.ReadLine()?.Split(" ");
                            var dest = Commands.GetDestination(destArr);
                            var src = Commands.GetSource(srcArr, destArr);
                            AddSrcToDest(src, dest);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;
                    case "string":
                        try
                        {
                            PrintBasicFormat("#Source#: ");
                            var srcArr = Console.ReadLine()?.Split(" ");
                            var ans = GetBoolFromUser("Show full directory");
                            PrintSrcStr(Commands.GetDestination(srcArr), ans);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;
                    case "rem":
                        try
                        {
                            PrintBasicFormat("#Target#: ");
                            Commands.RemoveTarget(Commands.GetDestination(Console.ReadLine()?.Split(" ")));
                            PrintFormat("gSuccessfully removedg\n", true, false,
                                new AdvancedFormatter("g", ConsoleColor.White, ConsoleColor.Green,
                                    FormatterConstruction.Regular));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;
                    case "inc":
                        try
                        {
                            PrintBasicFormat("#Source#: ");
                            var src = Commands.GetDestination(Console.ReadLine()?.Split(" "));
                            if (src.Svt == null || src.Family != null || src.Trip != null)
                                throw new ArgumentException("Invalid Source (Source Must Be a SVT)");
                            var ans = (int) GetNumberFromUser("Income From: (1/2)\n(1)From a Date to another " +
                                                              "Date\n(2)From a specified trip (provide code)",
                                f => f.IsInRange(1, 2) && f.IsComplete(),
                                "Must be 1 or 2");
                            switch (ans)
                            {
                                case 1:
                                {
                                    PrintFormat("   <b>From</b>:", false, false);
                                    var start = GetDateFromUser(1);
                                    PrintFormat("   <b>Until</b>:", false, false);
                                    var end = DateTime.MinValue;
                                    while (end < start)
                                        end = GetDateFromUser(1);
                                    var trips = src.Svt.GetTrips(start, end);
                                    var totalFam = trips.SelectMany(trip => trip.Families).Count();
                                    PrintFormat($"Money earned from svt - (code = {src.Svt.Code}):" +
                                                $"\n~From~ #Date#: {start.ToStringPlus()}" +
                                                $"\n~Until~ #Date#: {end.ToStringPlus()}" +
                                                $"{src.Svt.GetIncome(start, end)}\n" +
                                                $"Total Trips Included: {trips.Length}\n" +
                                                $"Total Families Included: {totalFam}\n" +
                                                $"{trips.Aggregate("", (s1, trip) => s1 + "\n#Name#: ~" + trip.Name + "~ #Code#: ~" + trip.Code + "~")}\n",
                                        true, false);
                                    break;
                                }
                                case 2:
                                {
                                    var code = (int) GetNumberFromUser("Trip Code: ",
                                        f => f.IsNatural() && src.Svt.GetTrip((int) f) != null);
                                    var trip = src.Svt.GetTrip(code);
                                    var price = trip.Families.Sum(family => trip.CostPerPerson * family.FamilyMembers);
                                    PrintFormat(
                                        $"Money earned <b>from</b> #svt# - (code = {src.Svt.Code}) <b>in</b> trip - (code = {trip.Code}) is : {price:C}\n"
                                        , true, false);
                                    break;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Print(e.Message, PrintType.Error, true);
                        }

                        break;
                }

                s = command;
            }
        }

        private static void PrintSrcStr(DataSource src, bool fullDir)
        {
            if (src.Family != null)
            {
                PrintFormat("!" + GetFullPath(src, Data.Family) + "!\n" +
                            $"{src.Family}", true, false);
                return;
            }

            if (src.Trip != null)
            {
                PrintFormat("!" + GetFullPath(src, Data.Trip) + "!\n" +
                            $"{src.Trip}", true, false);
                return;
            }

            if (src.Svt == null) throw new ArgumentException("No paths found");
            PrintFormat("!" + GetFullPath(src, Data.Svt) + "!\n" +
                        $"{src.Svt}", true, false);
        }

        private static string GetFullPath(DataSource ds, Data target)
        {
            var f = "";
            var path = GetDisorderedPath(ds, target, ref f).Split("/").ToList();
            path.RemoveAt(path.Count - 1);
            path.Reverse();
            return path.Aggregate("", (s, s1) => s + "/" + s1, s => s + "/");
        }

        private static string GetDisorderedPath(DataSource ds, Data target, ref string final)
        {
            if (target == Data.Main) final += "Main:/";
            if (target == Data.Svt)
            {
                final += $"svt={ds.Svt.Code}/";
                GetDisorderedPath(ds, Data.Main, ref final);
            }

            if (target == Data.Trip)
            {
                final += $"trip={ds.Trip.Code}/";
                GetDisorderedPath(ds, Data.Svt, ref final);
            }

            if (target == Data.Family)
            {
                final += $"trip={ds.Trip.Code}/";
                GetDisorderedPath(ds, Data.Trip, ref final);
            }

            return final;
        }

        private void AddSrcToDest(DataSource src, DataSource dest)
        {
            if (dest.AllNull())
            {
                Companies.Add(src.Svt);
                return;
            }

            if (dest.Svt != null && dest.Trip == null) Companies.Find(d => dest.Svt.Code == d.Code)?.AddTrip(src.Trip);

            if (dest.Svt != null && dest.Trip != null && dest.Family == null)
                Companies.Find(d => dest.Svt.Code == d.Code)?.GetTrip(dest.Trip.Code).Families.Add(src.Family);
        }

        private void StartTrip()
        {
        }

        #endregion

        #region Static Members (Partly Un - Realted)

        public static Trip CreateNewTripFromUser(SaVeTayel target)
        {
            PrintFormat("~args~:", true, true);
            var af = new AdvancedFormatter("/t", ConsoleColor.Cyan,
                ConsoleColor.Black, FormatterConstruction.Regular);
            var code = (int) GetNumberFromUser("code", i => i > 0 && target.GetTrip((int) i) == null &&
                                                            i % 1 < 0.001f, "Code Cannot Be Negative",
                "Trip with the same code already exits");
            var name = GetExcludedSpecificAns("name", false, f => f.Length.IsInRange(3, 10) ||
                                                                  target.GetTrip(a => a.Name.Equals(f)) != null,
                Array.Empty<string>(), new[] {"Name already exists in the current context", "Length Range 3- 10"});
            var cpp = GetNumberFromUser("Cost Per Person", f => f > 0, "Cost cannot Be Negative");
            var dt = GetDateFromUser(1);
            return new Trip(code, name, dt, cpp);
        }

        #endregion
    }
}