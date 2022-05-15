using System;
using System.Collections.Generic;
using Functions.AdvancedConsole;
using RideAndTrip.src.Trip;

namespace RideAndTrip.Trip
{
    public class Family : ISource<Family>, ISource
    {
        public Family(string name, int familyMembers)
        {
            Name = name;
            FamilyMembers = familyMembers;
            AdvancedConsolePrinter.PrintBasicFormat($"`CC{name}CC` family was created\n" +
                                               $"#Family Members#: `CC{FamilyMembers}CC`" +
                                               $"\n#Time# of creation `CC{DateTime.Now:HH:mm:ss tt zz}CC`!(GMT)! `CC{DateTime.Now:yyyy MMMM dd}CC`", true,
                ConsoleColor.Blue, true);
        }

        public Family GetSource() => this;

        public override string ToString() => $"Name: {Name}, Family Members: {FamilyMembers}";
        
        public string Name { get; set; }
        public int FamilyMembers { get; set; }
    }
}
