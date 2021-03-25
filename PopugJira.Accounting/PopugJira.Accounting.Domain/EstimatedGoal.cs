namespace PopugJira.Accounting.Domain
{
    public class EstimatedGoal
    {
        public EstimatedGoal(string id, decimal assignPrice, decimal completePrice)
        {
            Id = id;
            AssignPrice = assignPrice;
            CompletePrice = completePrice;
        }

        public string Id { get; }
        public decimal AssignPrice { get; }
        public decimal CompletePrice { get; }
    }
}