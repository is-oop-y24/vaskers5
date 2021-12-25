namespace Banks.Entities
{
    public class Transaction
    {
        public Transaction(int id, float moneyBefore, float moneyAfter)
        {
            MoneyBefore = moneyBefore;
            MoneyAfter = moneyAfter;
            Id = id;
            Declined = false;
        }

        public float MoneyBefore { get; }
        public float MoneyAfter { get; }
        public int Id { get; set; }
        public bool Declined { get; set; }
    }
}