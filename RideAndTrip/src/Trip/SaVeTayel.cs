using System;
using System.Collections.Generic;
using System.Linq;
using Functions.Extensions;
using RideAndTrip.src.Trip;

namespace RideAndTrip.Trip
{
    public class SaVeTayel : ISource<SaVeTayel>, ISource
    {
        public int Code { get; set; }
        public SaVeTayel(int code = 0)
        {
            Code = code;
            AvailableTrips = new List<Trip>();
        }

        public void RemoveFamilyFromTrip(int tripCode, Family f)
        {
            try
            {
                AvailableTrips.Find(trip => trip.Code == tripCode)?.Families.Remove(f);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        public void AddFamilyToTrip(int tripCode, Family f)
        {
            try
            {
                AvailableTrips.Find(trip => trip.Code == tripCode)?.Families.Add(f);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public float GetIncome(int tripCode)
        {
            return (from trip in from trip in AvailableTrips where trip.Code == tripCode 
                select trip from family in trip.Families 
                select family.FamilyMembers * trip.CostPerPerson).Sum();
        }
        public Trip this[int code]
        {
            get
            {
                try
                {
                    return AvailableTrips.First(t => t.Code == code);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public void AddTrip(Trip t)
        {
            AvailableTrips.Add(t);
        }

        public bool RemoveTrip(Trip t)
        {
            if (t == null)
                throw new ArgumentException("trip can't be null");
            try
            {
                return AvailableTrips.Remove(t);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public bool RemoveTrip(int tripCode)
        {
            try
            {
                var t = AvailableTrips.FirstOrDefault(trip => trip.Code == tripCode);
                return AvailableTrips.Remove(t ?? throw new ArgumentException($"didnt find a trip with the code {tripCode}"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        public float GetIncome(DateTime start, DateTime end)
        {
            return (from v in (from availableTrip in AvailableTrips let Date = availableTrip.Date 
                where Date <= end && Date >= start select availableTrip) from r in v.Families 
                select r.FamilyMembers * v.CostPerPerson).Sum();
        }

        public SaVeTayel GetSource() => this;

        public override string ToString()
        {
            return $"Code: {Code}, {AvailableTrips.AllElementsToString(true)}";
        }

        


        public Trip[] GetTrips(DateTime start, DateTime end) => 
            (from a in AvailableTrips let d = a.Date where start <= d && d <= end select a).ToArray();
        public Trip GetTrip(Predicate<Trip> pre) => AvailableTrips.FirstOrDefault(a => pre(a));
        public Trip GetTrip(int code) => AvailableTrips.FirstOrDefault(a => a.Code == code);
        public Trip GetTrip(int code, Predicate<Trip> p) => AvailableTrips.FirstOrDefault(a => a.Code == code && p(a));
        private List<Trip> AvailableTrips { get; set; }
    }
}
