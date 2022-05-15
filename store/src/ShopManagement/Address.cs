namespace store.ShopManagement
{
    class Address
    { 

        string city;
        string street;
        private int numhome;

        public Address(string city ,string street, int numhome)
        {
            this.city= city;
            this.street = street;
            this.numhome = numhome;
        }

        public string City { get; set; }
        public string Street { get; set; }
        public string Numhome { get; set; }
    }

}
