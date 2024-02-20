namespace UITemplate.Core.Interfaces
{
    public interface IIncomeService
    {
        void Process();
        void AccruePassiveIncome();

        void ClaimPassiveIncome(float multiplier);
    }
}