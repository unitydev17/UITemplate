namespace UITemplate.Core.Interfaces
{
    public interface IIncomeService
    {
        void Process();
        (int, double) AccruePassiveIncome();
    }
}