using System;
using System.Windows.Forms;

using Functions.Extensions;
using QueuesSpooler;
using Functions.AdvancedConsole;
using static Functions.AdvancedConsole.AdvancedConsolePrinter;
using static Functions.Calculator.AdvancedCalculations;

namespace Test
{
    
    public static class TeachingNati
    {
        public static void Print(string s = "") => Console.WriteLine(s);
        public static void Main()
        {
            var i = ConsoleData.GetIntFromUser("your age", i1 =>true );
            var money = ConsoleData.GetNumberFromUser("Your money");
            var longNumber = ConsoleData.GetLongFromUser("seconds you lived", l => true);
            MessageBox.Show($"Age: {i}, Money: {money}, Sec: {longNumber}");
            PrintFormat(null, false, true);
            var id = "123456789";
            PrintFormat("Hello!same line!\n@new\n\t<b>blue</b> + tabbed #g\ng#");
            Console.WriteLine(ValidIsraeliID(id));
            var p = Math.Pow(10, -5);
            var Cin = Math.Pow(10, -2);
            var Cou = Math.Pow(10, -5);
            var ans = -p * (Cin - Cou);
            Console.WriteLine(ans);
            Console.ReadLine();
            Load();
            string[] a = {"first", "second"}, b = {"third forth", "actual fifth"};
            Console.WriteLine(a.JoinTogether(b).AllElementsToString(true));
            var s = new Spooler(1);
            Console.WriteLine(s);
            s.Print(100);
            s.Print(100);
            s.SortFiles();
            Console.WriteLine(s.AllFiles.AllElementsToString(true));
            Console.WriteLine(s);
            var f = new FileToPrint("hi", LOrS.Long, new TimeSpan(10), 5, 31, DateTime.Now);
            Console.WriteLine(f);
        }
    }


    class Dog : ICloneable
    {
        public string Color;

        public Dog(string dudgwiu)
        {
            Color = dudgwiu;
            Console.WriteLine(@"New Dog created");
        }

        public override string ToString()
        {
            return Color;
        }

        public object Clone()
        {
            var k = new Dog(Color);
            k.moneu = moneu;
            k.age = age;
            return k;
        }

        public int age;
        public int moneu;
    }
}
