namespace store.UserTypes
{
    public class Person
    {
        protected Person(int id, string firstName, string lastName, string cell, string password = "")
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Cell = cell;
            Password = password;
        }

        public string Password { get; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cell { get; set; }
        //public Address Address { get; set; }

    }
}
