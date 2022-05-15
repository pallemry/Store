namespace store.ShopManagement
{
   public class Item
    {
        public int code { get; }
        public string name;
        public float price;
        public float clubPrice;
        public int amount;
        public Item(int code, string name, int price, int clubPrice, int amount)
        {
            this.code = code;
            this.name = name;
            this.price = price;
            this.clubPrice = clubPrice;
            this.amount = amount; 
        }
    }



}
