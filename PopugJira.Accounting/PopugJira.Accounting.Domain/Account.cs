namespace PopugJira.Accounting.Domain
{
    public class Account
    {
        public Account(string id, string name, decimal balance)
        {
            Id = id;
            Name = name;
            Balance = balance;
        }

        public string Id { get; }
        public string Name { get; }
        public decimal Balance { get; }
    }
}