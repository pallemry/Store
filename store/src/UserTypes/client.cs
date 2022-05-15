using store.ShopManagement;
using static store.ShopManagement.Shop;

namespace store.UserTypes
{
   public class Client :Person
    {
        public bool hasClubCard;
        public float Payment;
       
        public Client(int id, string firstName, string lastName, string cell, bool hasClubCard, string pass = "")
            : base(id, firstName, lastName, cell, pass)
        {
            this.hasClubCard = hasClubCard;
        }

        public string HasClubCard { get; set; }

        public bool BoolBuy(int code)
        {
            foreach (var t in Items)
            {
                if (t.code == code)
                {
                    if (t.amount > 0)
                    {
                        var price = hasClubCard ? t.clubPrice : t.price;
                        Payment += price;
                        profits += price;
                        t.amount--;
                        return true;
                    }
                    return false;

                }
            }

            return false;
        }
 
        public float GetPrice(int code)
        {
            for (var i = 0; i < Items.Count; i++)
            {
                var it = Items[i];
                if (it.code == code)
                {
                    return hasClubCard ? it.clubPrice : it.price;
                }
            }
            return 0;
        }
    }
}
