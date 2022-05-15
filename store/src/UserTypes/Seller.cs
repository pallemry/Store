using System.Linq;
using static store.ShopManagement.Shop;
namespace store.UserTypes
{
   public class Seller : Person , IWorker
    {
        private readonly int _salaryForHour;
       public int workHours;

       public Seller(int id, string firstName, string lastName, string cell, int salaryForHour, int workHours, string pass ="")
                  : base(id, firstName, lastName, cell, pass)
        {
            this._salaryForHour = salaryForHour;
            this.workHours = workHours;
            
        }
        

        public void SetDiscount(int itemCode, float precents = 10)
        {
            foreach (var t in Items.Where(t => t.code == itemCode))
            {
                t.price *= (1 - precents / 100f);
                break;
            }
        }
        public int GetSalary(string WorkerId = null) {
            return  _salaryForHour * workHours;
        }
    }
}
