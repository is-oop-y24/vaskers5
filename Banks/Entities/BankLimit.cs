namespace Banks.Entities
{
    public class BankLimit
    {
        public BankLimit(float limitValue)
        {
            LimitValue = limitValue;
        }

        public float LimitValue { get; set; }
    }
}