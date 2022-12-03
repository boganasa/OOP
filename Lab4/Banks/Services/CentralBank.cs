using System.ComponentModel;
using Banks.Entities;
using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Services;

public class CentralBank
{
    public CentralBank()
    {
        Banks = new List<Bank>();
        DaysPassed = 0;
    }

    public IReadOnlyList<Bank> Banks { get; private set; }
    public int DaysPassed { get; private set; }

    public Bank CreateBank(string name, float commissionToTransfer, float commissionForCreditAccount, decimal limitForCreditAccount, float interestOnTheBalanceForDebitAccount, string termsOfInterestAccrual, decimal amountLimitingSuspiciousClient, int openingPeriodInMonths)
    {
        var newBank = new Bank(name, commissionToTransfer, commissionForCreditAccount, limitForCreditAccount, interestOnTheBalanceForDebitAccount, termsOfInterestAccrual, amountLimitingSuspiciousClient, openingPeriodInMonths);
        var banks = new List<Bank>(Banks);
        banks.Add(newBank);
        Banks = banks;
        return newBank;
    }

    public bool ExistBank(string? name)
    {
        foreach (Bank bank in Banks)
        {
            if (bank.Name == name)
            {
                return true;
            }
        }

        return false;
    }

    public Bank FindBank(string? name)
    {
        foreach (Bank bank in Banks)
        {
            if (bank.Name == name)
            {
                return bank;
            }
        }

        throw new Exception();
    }

    public void TransferToAnotherBank(IBankAccount thisAccount, Bank thisBank, Bank otherBank, IBankAccount otherAccount, decimal sum)
    {
        float commission = 0;
        if (thisBank.Name != otherBank.Name)
        {
            commission = thisBank.CommissionToTransfer;
        }

        if (!thisAccount.CanWithdrawMoney())
        {
            throw new Exception();
        }

        thisAccount.TransferMoney(sum + (sum * (decimal)commission / 100), otherAccount);
        otherAccount.PutMoney(sum);
    }

    public void TimeAccelerationMechanism(int year, int month, int day)
    {
        int allDay = day + (month * 30) + (year * 365) + DaysPassed;
        int allMonth = allDay / 30;
        for (int i = 0; i < allMonth; i++)
        {
            DailyAccrualOfInterestInTheBalance(30);
            AccrualOfCommission();
            ChargeInterestOnTheBalance();
        }

        DailyAccrualOfInterestInTheBalance(allDay - (allMonth * 30));
        DaysPassed = allDay - (allMonth * 30);
    }

    public void ChargeInterestOnTheBalance()
    {
        foreach (Bank bank in Banks)
        {
            bank.InterestPayment();
        }
    }

    public void AccrualOfCommission()
    {
        foreach (Bank bank in Banks)
        {
            bank.CommissionDeduction();
        }
    }

    public void DailyAccrualOfInterestInTheBalance(int days)
    {
        foreach (Bank bank in Banks)
        {
            bank.DailyAccrualOfInterestInTheBalance(days);
        }

        DaysPassed++;
        if (DaysPassed == 30)
        {
            AccrualOfCommission();
            ChargeInterestOnTheBalance();
            DaysPassed = 0;
        }
    }

    public Bank FindBankById(string id)
    {
        foreach (Bank bank in Banks)
        {
            if (id == bank.Id)
            {
                return bank;
            }
        }

        throw new ExistingException(id);
    }

    public void CancelTransaction(Client client, IBankAccount account, int number)
    {
        string idBank = account.Id.Substring(0, account.Id.IndexOf('/'));
        Bank bank = FindBankById(idBank);
        foreach (Client suspiciousClient in bank.Clients)
        {
            if (suspiciousClient.Accounts != null && suspiciousClient.Accounts.Contains(account))
            {
                if (!suspiciousClient.SuspicionOfAttacker())
                {
                    throw new AttackerException(suspiciousClient.Name!);
                }

                if (account.GetByIndex(number).Equals("Transfer"))
                {
                    string[] subs = account.GetByIndex(number).Split('/');
                    bank.CancelingATransaction(number, account);
                    Bank bankAttacked = FindBankById(subs[1]);
                    foreach (IBankAccount accountThatWasAttacked in bankAttacked.Accounts)
                    {
                        if (accountThatWasAttacked.Id == $"{subs[1]}{subs[2]}")
                        {
                            int index = 0;
                            foreach (string history in accountThatWasAttacked.TransactionHistory)
                            {
                                if (history.Contains(account.Id))
                                {
                                    bankAttacked.CancelingATransaction(index, accountThatWasAttacked);
                                    return;
                                }

                                index++;
                            }
                        }
                    }
                }

                break;
            }
        }

        throw new CancelException();
    }
}