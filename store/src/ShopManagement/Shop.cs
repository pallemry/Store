using System.Collections.Generic;
using System.Linq;
using store.UserTypes;

namespace store.ShopManagement
{
   public static class Shop
   {
        public static readonly List<Manager> Managers = new List<Manager>();
        public static readonly List<Item> Items = new List<Item>();
        public static readonly List<Client> Clients = new List<Client>();
        public static readonly List<Seller> Sellers = new List<Seller>();
        public static float profits = 0;
        public static Seller GetSeller(int id)
        {
            return Sellers.FirstOrDefault(s => s.Id == id);
        }
        public static Client GetClient(int id)
        {
            return Clients.FirstOrDefault(s => s.Id == id);
        }
        public static Item GetItem(int id)
        {
            return Items.FirstOrDefault(variable => variable.code == id);
        }
        
    }
}
