namespace SeleniumPageObjectsExamples.Models
{
    public class Customer
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Customer()
        {
            Username = "john";
            Password = "demo";
        }
    }
}
