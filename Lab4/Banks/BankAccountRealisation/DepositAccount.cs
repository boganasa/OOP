using System.Globalization;
using Banks.Exceptions;
using Banks.Interfaces;

namespace Banks.BankAccountRealisation;

public class DepositAccount : IBankAccount
{
    public DepositAccount(string id, decimal sum, string termsOfInterestAccrual, int openingPeriodInMonths) // 0-10000=1%,10000-100000=3%,5%
    {
        Sum = sum;
        MonthlyPayment = 0;
        Commission = 0;
        Id = $"id/{Guid.NewGuid().ToString("N")}";
        OpeningPeriodInMonths = openingPeriodInMonths;
        TransactionHistory = new List<string>();
        SetInterestOnTheBalance(termsOfInterestAccrual);
    }

    public string Id { get; }
    public IReadOnlyList<string> TransactionHistory { get; private set; }
    public decimal Sum { get; private set; }
    public decimal MonthlyPayment { get; private set; }
    public float InterestOnTheBalance { get; set; }
    public float Commission { get; }
    public int OpeningPeriodInMonths { get; private set; }

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

    public void SetInterestOnTheBalance(string termsOfInterestAccrual)
    {
        string[] subs = termsOfInterestAccrual.Split(',');

        foreach (string sub in subs)
        {
            if (!sub.Contains('-'))
            {
                InterestOnTheBalance = float.Parse(sub.Substring(0, sub.IndexOf('%')));
                break;
            }

            decimal min = decimal.Parse(sub.Substring(0, sub.IndexOf('-')));
            decimal max = decimal.Parse(sub.Substring(sub.IndexOf('-') + 1, sub.IndexOf('=') - sub.IndexOf('-') - 1));
            if (Sum >= min && Sum < max)
            {
                InterestOnTheBalance = float.Parse(sub.Substring(sub.IndexOf('=') + 1, sub.IndexOf('%') - sub.IndexOf('=') - 1));
                break;
            }
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
        return false;
    }

    public bool CanGoIntoNegative()
    {
        return false;
    }

    public void WithdrawMoney(decimal sum)
    {
        if (OpeningPeriodInMonths > 0)
        {
            throw new CantDoException("Withdraw");
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
        if (OpeningPeriodInMonths > 0)
        {
            throw new CantDoException("Transfer");
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
        OpeningPeriodInMonths--;
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