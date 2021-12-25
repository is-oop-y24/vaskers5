namespace Banks.Entities
{
    public class BalanceInterest
    {
        public BalanceInterest(float interestValue)
        {
            InterestValue = interestValue;
        }

        public float InterestValue { get; set; }
    }
}