using System;
using System.Globalization;
using RideAndTrip.Trip;

namespace RideAndTrip
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var svt = new SaVeTayel();
            var iF = new Family("israel", 3);
            svt.AddTrip(new Trip.Trip(1, "", DateTime.MaxValue, 10.5f));
            svt.AddFamilyToTrip(1, iF);
            Console.WriteLine(svt.GetIncome(1));
            svt.AddTrip(new Trip.Trip(2, "Park", new DateTime(), 30.3f));
            svt.AddFamilyToTrip(2, iF);
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-gb");
            Console.WriteLine($@"{svt.GetIncome(DateTime.MinValue, DateTime.MaxValue):C}");
        }
    }
}
