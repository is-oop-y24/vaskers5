namespace Banks.Entities
{
    public class BankCommission
    {
        public BankCommission(float commissionValue)
        {
            CommissionValue = commissionValue;
        }

        public float CommissionValue { get; set; }
    }
}