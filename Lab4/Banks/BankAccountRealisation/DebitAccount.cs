using System.Globalization;
using Banks.Exceptions;
using Banks.Interfaces;

namespace Banks.BankAccountRealisation;

public class DebitAccount : IBankAccount
{
    public DebitAccount(string id, decimal sum, float interestOnTheBalance)
    {
        Sum = sum;
        InterestOnTheBalance = interestOnTheBalance;
        Commission = 0;
        MonthlyPayment = 0;
        Id = $"id/{Guid.NewGuid().ToString("N")}";
        TransactionHistory = new List<string>();
    }

    public string Id { get; }
    public IReadOnlyList<string> TransactionHistory { get; private set; }
    public decimal Sum { get; private set; }
    public decimal MonthlyPayment { get; private set; }
    public float InterestOnTheBalance { get; set; }
    public float Commission { get; }

    public string this[int index]
    {
        get
        {
            return TransactionHistory[index];
        }

        set
        {
            var newList = new List<string>(TransactionHistory);
            newList[index].Replace("success", value);
            TransactionHistory = newList;
        }
    }

    public void SetByIndex(int index, string comment)
    {
        this[index] = comment;
    }

    public string GetByIndex(int index)
    {
        return this[index];
    }

    public bool CanWithdrawMoney()
    {
        return true;
    }

    public bool CanGoIntoNegative()
    {
        return false;
    }

    public void WithdrawMoney(decimal sum)
    {
        if (Sum - sum < 0)
        {
            throw new GoToNegativeExceptions(Sum - sum);
        }

        Sum -= sum;
        AddTransaction(sum, "Withdraw");
    }

    public void PutMoney(decimal sum)
    {
        Sum += sum;
        AddTransaction(sum, "Put");
    }

    public void TransferMoney(decimal sum, IBankAccount otherAccount)
    {
        if (Sum - sum < 0)
        {
            throw new GoToNegativeExceptions(Sum - sum);
        }

        Sum -= sum;
        AddTransaction(sum, $"Transfer/{otherAccount.Id}");
    }

    public void DailyAccrualOfInterestInTheBalance(int days)
    {
        MonthlyPayment += (decimal)(InterestOnTheBalance / 365 / 100 * (double)Sum * days);
    }

    public void InterestPayment()
    {
        Sum += MonthlyPayment;
        MonthlyPayment = 0;
    }

    public void CommissionDeduction()
    {
    }

    private void AddTransaction(decimal sum, string typeOfTransaction)
    {
        string newTransaction = $"{typeOfTransaction}/{sum.ToString(CultureInfo.InvariantCulture)}/success";
        var newList = new List<string> { newTransaction };
        TransactionHistory = newList;
    }
}