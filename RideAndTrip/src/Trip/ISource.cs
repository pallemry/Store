using System;
using System.Collections.Generic;
using System.Text;
using RideAndTrip.Trip;

namespace RideAndTrip.src.Trip
{
    public interface ISource<T> 
    {
        public T GetSource();
    }
    public interface ISource{}
}
