using System;
using System.Collections.Generic;
using System.Linq;
using Functions.Calculator;
using Functions.Extensions;
using RideAndTrip.src.Trip;

namespace RideAndTrip.Trip
{
    public class Trip : ISource<Trip>, ISource
    {
        public static Trip Default { get; } = new Trip(0, "", new DateTime(), 0);
        public Trip(int code, string name, DateTime date, float costPerPerson)
        {
            Code = code;
            Name = name;
            Date = date;
            CostPerPerson = costPerPerson;
            Families = new List<Family>();
        }

        public Trip GetSource() => this;
        

        public override string ToString() => $"Name: {Name}, Code: {Code}\nCostPerPerson: {CostPerPerson:C}, \n" +
                                             $"Date: {Date.ToStringPlus()}\n{Families.AllElementsToString(true)}";
        public DateTime Date { get; set; }
        public float CostPerPerson { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public List<Family> Families { get; set; }

    }
}
