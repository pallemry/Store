using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Functions.Extensions;
using RideAndTrip.src.Trip;
using RideAndTrip.Trip;
using static Functions.AdvancedConsole.AdvancedConsolePrinter;
using static Functions.AdvancedConsole.ConsoleData;
using static QueuesSpooler.Main.Menu;

namespace QueuesSpooler.Main
{
    public class Commands
    {
        public Menu Menu { get; set; }

        public Commands(Menu menu)
        {
            Menu = menu;
        }

        //public void AddCommandWithGet(string[] command)
        //{
        //    SaVeTayel tempSvt = null;
        //    Trip tempTrip = null;
        //    Family tempFamily = null;
        //    if (command[1].StartsWith("svt"))
        //    {
        //        if (command.Length == 3 && command[2].Equals("main"))
        //        {
        //            PrintBasicFormat("new svt code: ");
        //            var b =(int) GetNumberFromUser("new svt code");
        //            if (Menu.Companies.Find(a => a.Code == b) == null)
        //                Menu.Companies.Add(new SaVeTayel(b));
        //            else throw new ArgumentException($"svt {b} alr exists");
        //            return;
        //        }
        //        tempSvt = Menu.Companies.Find(a => a.Code == int.Parse(command[1].Replace("svt", "")));
        //        if (tempSvt == null) throw new ArgumentException($"Couldn't find svt {int.Parse(command[1][3..])}");
        //    }
        //    if (command[2].StartsWith("trip"))
        //    {
        //        if (tempSvt == null) throw new ArgumentException($"Trip {command[2]} is not associated with any SVT ");
        //        if (command[2].Equals("trip"))
        //        {
        //            tempSvt.AddTrip(CreateNewTripFromUser(tempSvt));
        //        }
        //        command[2] = command[2].Replace("trip", "");
        //        tempTrip = tempSvt.GetTrip(int.Parse(command[2]));
        //        var s = command[2];
        //        if (tempTrip == null) throw new ArgumentException($"Couldn't find trip {s}");
        //    }

        //    if (command[3].StartsWith("family"))
        //    {
        //        if (tempTrip == null) throw new ArgumentException($"Family {command[3]} is not associated with any Trip ");
        //        if (command[3].Equals("family"))
        //        {
        //            var fam = CreateFamilyFromUser();
        //            if (!tempTrip.Families.Exists(f => f.Name.Equals(fam.Name)))
        //            {
        //                tempTrip.Families.Add(fam);
        //                PrintBasicFormat($"family #{fam.Name}# with #{fam.FamilyMembers}# succ created");
        //            }
        //            PrintBasicFormat($"family #{fam.Name}# alr exists");
        //        }
        //        command[3] = command[3].Replace("family", "");
        //        tempFamily = tempTrip.Families.Find(int.Parse(command[2]));
        //        var s = command[2];
        //        if (tempTrip == null) throw new ArgumentException($"Couldn't find trip {s}");
        //    }


        //}

        internal DataSource GetDestination(string[] command)
        {
            GetCommand(ref command);
            var dest = new DataSource();
            if (command[0].Equals("main"))
            {
                dest.Main = true;
                return dest;
            }

            for (var i = 0; i < command.Length; i++)
                if (command[i].StartsWith("svt"))
                {
                    dest.Svt = Menu.Companies.Find(a => a.Code == int.Parse(command[i].Replace("svt", "")));
                    if (dest.Svt == null) throw new ArgumentException($"Couldn't find svt {int.Parse(command[i])}");
                }
                else if (command[i].StartsWith("trip"))
                {
                    if (dest.Svt == null)
                        throw new ArgumentException($"Trip {command[i]} is not associated with any SVT ");
                    command[i] = command[i].Replace("trip", "");
                    dest.Trip = dest.Svt.GetTrip(int.Parse(command[i])) ??
                                throw new ArgumentNullException($"didnt find trip {command[i]}");
                }
                else if (command[i].StartsWith("family"))
                {
                    if (dest.Trip == null)
                        throw new ArgumentException($"Family {command[i]} is not associated with any Trip ");
                    command[i] = command[i].Replace("family", "");
                    dest.Family = dest.Trip.Families.Find(a => a.Name.Equals(command[i]));
                }

            return dest;
        }

        internal DataSource GetSource(string[] command, string[] dest)
        {
            SaVeTayel tempSvt = null;
            Trip tempTrip = null;
            Family tempFamily = null;
            var ans = new DataSource();
            GetCommand(ref command);
            GetCommand(ref dest);
            var dsSource = GetDestination(dest);
            if (command[0].StartsWith("new"))
            {
                switch (command[1])
                {
                    case "trip":
                        tempTrip = CreateNewTripFromUser(dsSource.Svt);
                        break;
                    case "family":
                        tempFamily = CreateFamilyFromUser();
                        break;
                    case "svt":
                        tempSvt = CreateSvtFromUser(Menu);
                        break;
                }

                var ds = new DataSource(tempSvt, tempTrip, tempFamily, dsSource.Main, true);
                if (!ds.AllNull()) return ds;
            }

            for (var i = 0; i < command.Length; i++)
                if (command[i].StartsWith("svt"))
                {
                    tempSvt = Menu.Companies.Find(a => a.Code == int.Parse(command[i].Replace("svt", "")));
                    if (tempSvt == null) throw new ArgumentException($"Couldn't find svt {int.Parse(command[i])}");
                }
                else if (command[i].StartsWith("trip"))
                {
                    if (tempSvt == null)
                        throw new ArgumentException($"Trip {command[i]} is not associated with any SVT ");
                    command[i] = command[i].Replace("trip", "");
                    tempTrip = tempSvt.GetTrip(int.Parse(command[i])) ??
                               throw new ArgumentNullException($"didnt find trip {command[i]}");
                }
                else if (command[i].StartsWith("family"))
                {
                    if (tempTrip == null)
                        throw new ArgumentException($"Family {command[i]} is not associated with any Trip ");
                    command[i] = command[i].Replace("family", "");
                    tempFamily = tempTrip.Families.Find(a => a.Name.Equals(command[i]));
                }

            return new DataSource(tempSvt, tempTrip, tempFamily, dsSource.Main);
        }

        public bool GetCommand(ref string[] command)
        {
            if (command.Last().Equals(string.Empty))
            {
                var t = command.ToList();
                t.RemoveAt(command.Length - 1);
                command = t.ToArray();
            }

            var b = false;
            var c = command.ToList();
            for (var i = 0; i < c.Count; i++)
            {
                c[i] = c[i].ToLower();
                if (c[i].ToLower().Equals("get") && i < c.Count - 2)
                {
                    b = true;
                    c[i] = c[i + 1].ToLower() switch
                    {
                        "f" => "family",
                        "t" => "trip",
                        "s" => "svt",
                        _ => throw new ArgumentException()
                    };
                    c[i] += c[i + 2].IsNumericType() ? c[i + 2] : "";
                    c.RemoveAt(i + 1);
                    c.RemoveAt(i + 1);
                }
            }

            command = c.ToArray();
            return b;
        }

        public static Family CreateFamilyFromUser()
        {
            PrintFormat("!name!:", false, false);
            var name = Console.ReadLine();
            PrintFormat("!family members!:", false, false);
            var fm = (int) GetNumberFromUser("family members", f => f.IsInRange(1, 20), "range: 1-20");
            return new Family(name, fm);
        }

        public static SaVeTayel CreateSvtFromUser(Menu m)
        {
            PrintFormat("!!:", true, false);
            var c = (int) GetNumberFromUser("new svt code",
                f => f > 0 && m.Companies.Find(a => a.Code == (int) f) == null,
                "Svt already exists", "Can't be negative");
            return new SaVeTayel(c);
        }

        public void RemoveTarget(DataSource trg)
        {
            bool succ;
            if (trg.AllNull()) throw new ArgumentException("All null therefore cant remove");
            if (trg.Family != null)
            {
                if (!trg.Trip.Families.Remove(trg.Family))
                    throw new ArgumentException($"Could not Remove {trg.Trip}");
                return;
            }

            if (trg.Trip != null)
            {
                if (!trg.Svt.RemoveTrip(trg.Trip.Code))
                    throw new ArgumentException($"Could not Remove {trg.Trip}");
                return;
            }

            if (trg.Svt == null) throw new ArgumentException("All null therefore cant remove");
            if (!Menu.Companies.Remove(trg.Svt)) throw new ArgumentException($"Could not Remove {trg.Trip}");
        }
    }

    public struct DataSource
    {
        public DataSource(SaVeTayel svt = null, Trip trip = null, Family family = null, bool main = false,
            bool usesNewKw = false)
        {
            UsesNewKw = usesNewKw;
            Main = main;
            Trip = trip;
            Svt = svt;
            Family = family;
        }

        public bool UsesNewKw { get; internal set; }

        public bool AllNull()
        {
            return Trip == null && Svt == null && Family == null;
        }

        public bool Main { get; internal set; }
        public Trip Trip { get; set; }
        public SaVeTayel Svt { get; set; }
        public Family Family { get; set; }
    }
}