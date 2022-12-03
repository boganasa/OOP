using System.Reflection;
using Banks.BankAccountRealisation;
using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class Bank : IObservable
{
    public Bank(string name, float commissionToTransfer, float commissionForCreditAccount, decimal limitForCreditAccount, float interestOnTheBalanceForDebitAccount, string termsOfInterestAccrual, decimal amountLimitingSuspiciousClient, int openingPeriodInMonths)
    {
        Name = name;
        CommissionToTransfer = commissionToTransfer;
        Id = Guid.NewGuid().ToString("N");
        Clients = new List<Client>();
        Observers = new List<Client>();
        Accounts = new List<IBankAccount>();
        InterestOnTheBalanceForDebitAccount = interestOnTheBalanceForDebitAccount;
        CommissionForCreditAccount = commissionForCreditAccount;
        LimitForCreditAccount = limitForCreditAccount;
        TermsOfInterestAccrual = termsOfInterestAccrual;
        AmountLimitingSuspiciousClient = amountLimitingSuspiciousClient;
        OpeningPeriodInMonths = openingPeriodInMonths;
        InfoBank = new Info(this);
    }

    public string Id { get; }
    public string Name { get; }
    public float CommissionToTransfer { get; private set; }
    public IReadOnlyList<Client> Clients { get; private set; }
    public IReadOnlyList<IObserver> Observers { get; private set; }
    public IReadOnlyList<IBankAccount> Accounts { get; private set; }
    public float InterestOnTheBalanceForDebitAccount { get; private set; }
    public float CommissionForCreditAccount { get; private set; }
    public decimal LimitForCreditAccount { get; private set; }
    public string TermsOfInterestAccrual { get; private set; }
    public decimal AmountLimitingSuspiciousClient { get; private set; }
    public int OpeningPeriodInMonths { get; }
    public Info InfoBank { get; private set; }

    public void UpdateInfo()
    {
        var newInfo = new Info(this);
    }

    public void RegisterObserver(IObserver observer)
    {
        var newList = new List<IObserver>(Observers);
        if (newList.Contains(observer))
        {
            throw new ResubscribeException();
        }

        newList.Add(observer);
        Observers = newList;
    }

    public void RemoveObserver(IObserver observer)
    {
        var newList = new List<IObserver>(Observers);
        if (!newList.Contains(observer))
        {
            throw new UnsubscribeException();
        }

        newList.Remove(observer);
        Observers = newList;
    }

    public void NotifyObservers()
    {
        Info oldInfo = new Info(InfoBank);
        UpdateInfo();
        foreach (Client observer in Observers)
        {
            if (observer.Accounts != null && oldInfo.InterestOnTheBalanceForDebitAccountInfo.ToString() != InfoBank.InterestOnTheBalanceForDebitAccountInfo.ToString() && observer.Accounts.OfType<DebitAccount>().Any())
            {
                observer.Update(InfoBank.SendMessage());
            }
            else if (observer.Accounts != null && oldInfo.CommissionForCreditAccountInfo.ToString() != InfoBank.CommissionForCreditAccountInfo.ToString() && oldInfo.LimitForCreditAccountInfo != InfoBank.LimitForCreditAccountInfo && observer.Accounts.OfType<CreditAccount>().Any())
            {
                observer.Update(InfoBank.SendMessage());
            }
            else if (observer.Accounts != null && oldInfo.TermsOfInterestAccrualInfo != InfoBank.TermsOfInterestAccrualInfo && observer.Accounts.OfType<DepositAccount>().Any())
            {
                observer.Update(InfoBank.SendMessage());
            }
            else
            {
                observer.Update(InfoBank.SendMessage());
            }
        }
    }

    public void AddClient(Client client)
    {
        var clients = new List<Client>(Clients);
        if (!clients.Contains(client))
        {
            clients.Add(client);
            Clients = clients;
        }
    }

    public void AddAccount(IBankAccount account)
    {
        var accounts = new List<IBankAccount>(Accounts);
        accounts.Add(account);
        Accounts = accounts;
    }

    public void AddClientWithNewAccount(Client client, decimal sum, string typeOfAccount)
    {
        MethodInfo create = this.GetType().GetMethod($"Create{typeOfAccount}Account") ?? throw new AccountExistException(typeOfAccount);
        object[] param = { sum };
        var newAccount = (IBankAccount)create.Invoke(this, param) !;
        client.AddAccount(newAccount);
        AddClient(client);
        AddAccount(newAccount);
    }

    public void CommissionDeduction()
    {
        foreach (IBankAccount account in Accounts)
        {
            account.CommissionDeduction();
        }
    }

    public void DailyAccrualOfInterestInTheBalance(int days)
    {
        foreach (IBankAccount account in Accounts)
        {
            account.DailyAccrualOfInterestInTheBalance(days);
        }
    }

    public void InterestPayment()
    {
        foreach (IBankAccount account in Accounts)
        {
            account.InterestPayment();
        }
    }

    public void WithdrawMoney(Client client, IBankAccount account, decimal sum)
    {
        if (client.SuspicionOfAttacker() && sum > AmountLimitingSuspiciousClient)
        {
            throw new AttackException(client.Name!);
        }

        account.WithdrawMoney(sum);
    }

    public void PutMoney(Client client, IBankAccount account, decimal sum)
    {
        account.PutMoney(sum);
    }

    public void TransferMoney(Client client, IBankAccount account, decimal sum, IBankAccount otherAccount)
    {
        if (client.SuspicionOfAttacker() && sum > AmountLimitingSuspiciousClient)
        {
            throw new AttackException(client.Name!);
        }

        account.TransferMoney(sum, otherAccount);
    }

    public void CancelingATransaction(int number, IBankAccount account)
    {
        if (account.GetByIndex(number).EndsWith("success"))
        {
            string[] subs = account.GetByIndex(number).Split('/');

            subs[4] = "fail";
            decimal sum = Convert.ToDecimal(subs[3]);
            account.PutMoney(sum);
            account.SetByIndex(number, "fail");
        }
        else
        {
            throw new CancelException();
        }
    }

    public void ActualizeInfoForDebitAccount()
    {
        var debits = Accounts.OfType<DebitAccount>();
        foreach (DebitAccount debit in debits)
        {
            debit.InterestOnTheBalance = InterestOnTheBalanceForDebitAccount;
        }
    }

    public void ActualizeInfoForCreditAccount()
    {
        var debits = Accounts.OfType<CreditAccount>();
        foreach (CreditAccount debit in debits)
        {
            debit.Commission = CommissionForCreditAccount;
            debit.Limit = LimitForCreditAccount;
        }
    }

    public void ActualizeInfoForDepositAccount()
    {
        var debits = Accounts.OfType<DepositAccount>();
        foreach (DepositAccount debit in debits)
        {
            debit.SetInterestOnTheBalance(TermsOfInterestAccrual);
        }
    }

    public void ChangeCommissionToTransfer(float newCommissionToTransfer)
    {
        CommissionToTransfer = newCommissionToTransfer;
        NotifyObservers();
    }

    public void ChangeInterestOnTheBalanceForDebitAccount(float newInterestOnTheBalanceForDebitAccount)
    {
        InterestOnTheBalanceForDebitAccount = newInterestOnTheBalanceForDebitAccount;
        ActualizeInfoForDebitAccount();
        NotifyObservers();
    }

    public void ChangeCommissionForCreditAccount(float newCommissionForCreditAccount)
    {
        CommissionForCreditAccount = newCommissionForCreditAccount;
        ActualizeInfoForCreditAccount();
        NotifyObservers();
    }

    public void ChangeLimitForCreditAccount(decimal newLimitForCreditAccount)
    {
        LimitForCreditAccount = newLimitForCreditAccount;
        ActualizeInfoForCreditAccount();
        NotifyObservers();
    }

    public void ChangeTermsOfInterestAccrual(string newTermsOfInterestAccrual)
    {
        TermsOfInterestAccrual = newTermsOfInterestAccrual;
        ActualizeInfoForDepositAccount();
        NotifyObservers();
    }

    public void ChangeAmountLimitingSuspiciousClient(decimal newAmountLimitingSuspiciousClient)
    {
        AmountLimitingSuspiciousClient = newAmountLimitingSuspiciousClient;
        NotifyObservers();
    }

    public CreditAccount CreateCreditAccount(decimal sum)
    {
        var newAccount = new CreditAccount(Id, sum, CommissionForCreditAccount, LimitForCreditAccount);
        return newAccount;
    }

    public DebitAccount CreateDebitAccount(decimal sum)
    {
        var newAccount = new DebitAccount(Id, sum, InterestOnTheBalanceForDebitAccount);
        return newAccount;
    }

    public DepositAccount CreateDepositAccount(decimal sum)
    {
        var newAccount = new DepositAccount(Id, sum, TermsOfInterestAccrual, OpeningPeriodInMonths);
        return newAccount;
    }

    public class Info
    {
        public Info(Bank bank)
        {
            Bank = bank;
            CommissionToTransferInfo = bank.CommissionToTransfer;
            InterestOnTheBalanceForDebitAccountInfo = bank.InterestOnTheBalanceForDebitAccount;
            CommissionForCreditAccountInfo = bank.CommissionForCreditAccount;
            LimitForCreditAccountInfo = bank.LimitForCreditAccount;
            TermsOfInterestAccrualInfo = bank.TermsOfInterestAccrual;
            AmountLimitingSuspiciousClientInfo = bank.AmountLimitingSuspiciousClient;
        }

        public Info(Info info)
        {
            Bank = info.Bank;
            CommissionToTransferInfo = info.CommissionToTransferInfo;
            InterestOnTheBalanceForDebitAccountInfo = info.InterestOnTheBalanceForDebitAccountInfo;
            CommissionForCreditAccountInfo = info.CommissionForCreditAccountInfo;
            LimitForCreditAccountInfo = info.LimitForCreditAccountInfo;
            TermsOfInterestAccrualInfo = info.TermsOfInterestAccrualInfo;
            AmountLimitingSuspiciousClientInfo = info.AmountLimitingSuspiciousClientInfo;
        }

        public Bank Bank { get; }
        public float CommissionToTransferInfo { get; private set; }
        public float InterestOnTheBalanceForDebitAccountInfo { get; private set; }
        public float CommissionForCreditAccountInfo { get; private set; }
        public decimal LimitForCreditAccountInfo { get; private set; }
        public string TermsOfInterestAccrualInfo { get; private set; }
        public decimal AmountLimitingSuspiciousClientInfo { get; private set; }

        public string InfoToString()
        {
            return $"CommissionToTransferInfo - {CommissionToTransferInfo}\nInterestOnTheBalanceForDebitAccountInfo - {InterestOnTheBalanceForDebitAccountInfo}\nCommissionForCreditAccountInfo - {CommissionForCreditAccountInfo}\nLimitForCreditAccountInfo - {LimitForCreditAccountInfo}\nTermsOfInterestAccrualInfo - {TermsOfInterestAccrualInfo}\nAmountLimitingSuspiciousClientInfo - {AmountLimitingSuspiciousClientInfo}";
        }

        public string SendMessage()
        {
            return
                $"The bank's {Bank.Name} ({Bank.Id}) policy has changed. Check out the new terms of use of accounts:\n{InfoToString()}";
        }
    }
}