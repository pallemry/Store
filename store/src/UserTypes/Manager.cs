using System.Linq;
using static store.ShopManagement.Shop;

namespace store.UserTypes
{
    public class Manager : Person
    {
        public readonly int salaryForHour;
        public readonly int workHours;


        public Manager(int id, string firstName, string lastName, string cell, int salaryForHour, int workHours, string pass = "")
            : base(id, firstName, lastName, cell, pass)
        {
            this.salaryForHour = salaryForHour;
            this.workHours = workHours;
        }

        
        public static void SetDiscount(int itemCode, float precents = 10)
        {
            foreach (var t in  
                     from t in Items 
                     let code = t.code
                     where code == itemCode
                     select t)
            {
                t.price *= (1 - precents/100f);
                t.clubPrice *= (1 - precents / 100f);
                break;

            }
        }
        public int GetSalary(string WorkerId = null)
        {
            return salaryForHour * workHours;
        }


    }
}