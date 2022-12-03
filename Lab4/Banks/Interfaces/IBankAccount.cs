namespace Banks.Interfaces;

public interface IBankAccount
{
    string Id { get; }
    IReadOnlyList<string> TransactionHistory { get; }
    decimal Sum { get; }
    decimal MonthlyPayment { get; }
    float InterestOnTheBalance { get; }
    float Commission { get; }

    bool CanWithdrawMoney();
    bool CanGoIntoNegative();
    void WithdrawMoney(decimal sum);
    void PutMoney(decimal sum);
    void TransferMoney(decimal sum, IBankAccount otherAccount);
    void DailyAccrualOfInterestInTheBalance(int days);
    void InterestPayment();
    void CommissionDeduction();
    void SetByIndex(int index, string comment);
    string GetByIndex(int index);
}