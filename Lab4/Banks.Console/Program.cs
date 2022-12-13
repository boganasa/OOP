using System.Diagnostics;
using Banks.Entities;
using Banks.Models;
using Banks.Services;

namespace Banks.Console;

public class Program
{
    private static void Main(string[] args)
    {
        var centralBank = new CentralBank();
        var clients = new List<Client>();
        int key = 0;
        while (key != 8)
        {
            System.Console.WriteLine(
                "What do you want to do?\n1 - Create bank\n2 - Create client\n3 - Add information about existing client");
            System.Console.WriteLine(
                "4 - Add account to client\n5 - Put money\n6 - Withdraw money\n7 - Transfer money\n8 - Exit");
            key = Convert.ToInt32(System.Console.ReadLine());
            Receiver receiver = new Receiver(key);
            PaymentHandler bankPaymentHandler = new BankPaymentHandler();
            PaymentHandler moneyPaymentHadler = new MoneyPaymentHandler();
            PaymentHandler paypalPaymentHandler = new PayPalPaymentHandler();
            bankPaymentHandler.Successor = paypalPaymentHandler;
            paypalPaymentHandler.Successor = moneyPaymentHadler;
            bankPaymentHandler.Handle(receiver);
        }
    }
}

public class Receiver
{
    public Receiver(int key)
    {
        Key = key;
    }

    public int Key { get; }
}

public abstract class PaymentHandler
{
    public PaymentHandler Successor { get; set; } = null!;
    public abstract void Handle(Receiver receiver);
}

public class BankPaymentHandler : PaymentHandler
{
    public override void Handle(Receiver receiver)
    {
        if (receiver.Key == 1)
            System.Console.WriteLine("Выполняем банковский перевод");
        else if (Successor != null)
            Successor.Handle(receiver);
    }
}

public class PayPalPaymentHandler : PaymentHandler
{
    public override void Handle(Receiver receiver)
    {
        if (receiver.PayPalTransfer == true)
            Console.WriteLine("Выполняем перевод через PayPal");
        else if (Successor != null)
            Successor.Handle(receiver);
    }
}

public class MoneyPaymentHandler : PaymentHandler
{
    public override void Handle(Receiver receiver)
    {
        if (receiver.MoneyTransfer == true)
            Console.WriteLine("Выполняем перевод через системы денежных переводов");
        else if (Successor != null)
            Successor.Handle(receiver);
    }
}

/*public class Program
{
    public static void Main(string[] args)
    {
        var centralBank = new CentralBank();
        var clients = new List<Client>();
        string? key = " ";
        while (key != "8")
        {
            System.Console.WriteLine(
                "What do you want to do?\n1 - Create bank\n2 - Create client\n3 - Add information about existing client");
            System.Console.WriteLine(
                "4 - Add account to client\n5 - Put money\n6 - Withdraw money\n7 - Transfer money\n8 - Exit");
            key = System.Console.ReadLine();
            switch (key)
            {
                case "1":
                    System.Console.WriteLine(
                        "Input: name, commission to transfer, commission for credit account, limit for credit account, interest on balance for debit account, terms of interest accrual, limit for suspicious, opening period");
                    string? name = System.Console.ReadLine();
                    float commissionToTransfer = (float)Convert.ToDouble(System.Console.ReadLine());
                    float interestOnTheBalanceForDebitAccount = (float)Convert.ToDouble(System.Console.ReadLine());
                    float commissionForCreditAccount = (float)Convert.ToDouble(System.Console.ReadLine());
                    decimal limitForCreditAccount = Convert.ToDecimal(System.Console.ReadLine());
                    string? termsOfInterestAccrual = System.Console.ReadLine();
                    decimal amountLimitingSuspiciousClient = Convert.ToDecimal(System.Console.ReadLine());
                    int openingPeriodInMonths = Convert.ToInt32(System.Console.ReadLine());
                    if (name != null && termsOfInterestAccrual != null)
                    {
                        Bank newBank = centralBank.CreateBank(name, commissionToTransfer, commissionForCreditAccount, limitForCreditAccount, interestOnTheBalanceForDebitAccount, termsOfInterestAccrual, amountLimitingSuspiciousClient, openingPeriodInMonths);
                        System.Console.WriteLine(newBank.InfoBank.SendMessage());
                    }

                    break;
                case "2":
                    System.Console.WriteLine("Input name, sername, address, passport");
                    name = System.Console.ReadLine();
                    string? sername = System.Console.ReadLine();
                    string? address = null;
                    int passport = 0;
                    System.Console.WriteLine("Do you want to input address? Y/N");
                    string? flag = System.Console.ReadLine();
                    if (flag == "Y")
                    {
                        address = System.Console.ReadLine();
                    }

                    System.Console.WriteLine("Do you want to input passport? Y/N");
                    flag = System.Console.ReadLine();
                    if (flag == "Y")
                    {
                        passport = Convert.ToInt32(System.Console.ReadLine());
                    }

                    if (name != null && sername != null)
                    {
                        var client = Client.Builder
                            .WithName(name)
                            .WithSurname(sername)
                            .WithAddress(address)
                            .WithPassportNumber(passport)
                            .Build();
                        clients.Add(client);
                    }

                    break;
                case "3":
                    System.Console.WriteLine("Input name and sername client");
                    name = System.Console.ReadLine();
                    sername = System.Console.ReadLine();
                    foreach (Client client in clients)
                    {
                        if (client.Name == name && client.Surname == sername)
                        {
                            System.Console.WriteLine("Do you want to input address? Y/N");
                            flag = System.Console.ReadLine();
                            if (flag == "Y")
                            {
                                address = System.Console.ReadLine();
                                client.Address = address;
                            }

                            System.Console.WriteLine("Do you want to input passport? Y/N");
                            flag = System.Console.ReadLine();
                            if (flag == "Y")
                            {
                                passport = Convert.ToInt32(System.Console.ReadLine());
                                client.PassportNumber = passport;
                            }

                            System.Console.WriteLine("Client's data successfully update");
                            break;
                        }
                    }

                    break;
                case "4":
                    if (centralBank.Banks.Count == 0)
                    {
                        System.Console.WriteLine("There is no bank, you can't do it");
                    }
                    else
                    {
                        System.Console.WriteLine("Input bank's name");
                        string? nameBank = System.Console.ReadLine();
                        if (!centralBank.ExistBank(nameBank))
                        {
                            System.Console.WriteLine("There is no such bank, you can't do it");
                        }
                        else
                        {
                            System.Console.WriteLine("Input name and sername client");
                            name = System.Console.ReadLine();
                            sername = System.Console.ReadLine();
                            bool flagB = false;
                            foreach (Client client in clients)
                            {
                                if (client.Name == name && client.Surname == sername)
                                {
                                    Bank bank = centralBank.FindBank(nameBank);
                                    System.Console.WriteLine("What type of bank? Credit/Debit/Deposit");
                                    string type = System.Console.ReadLine() ?? throw new InvalidOperationException();
                                    System.Console.WriteLine("What sum of account?");
                                    int sum = Convert.ToInt32(System.Console.ReadLine());
                                    bank.AddClientWithNewAccount(client, sum, type);
                                    System.Console.WriteLine("Client's account successfully create");
                                    if (client.Accounts != null) System.Console.WriteLine(client.Accounts[0].Id);
                                    flagB = true;
                                    break;
                                }
                            }

                            if (!flagB)
                            {
                                System.Console.WriteLine("Do you want to input address? Y/N");
                                flag = System.Console.ReadLine();
                                if (flag == "Y")
                                {
                                    address = System.Console.ReadLine();
                                }

                                System.Console.WriteLine("Do you want to input passport? Y/N");
                                flag = System.Console.ReadLine();
                                if (flag == "Y")
                                {
                                    passport = Convert.ToInt32(System.Console.ReadLine());
                                }

                                address = null;
                                passport = 0;
                                if (name != null && sername != null)
                                {
                                    var client = Client.Builder
                                        .WithName(name)
                                        .WithSurname(sername)
                                        .WithAddress(address)
                                        .WithPassportNumber(passport)
                                        .Build();
                                    clients.Add(client);
                                    Bank bank = centralBank.FindBank(nameBank);
                                    System.Console.WriteLine("What type of bank? Credit/Debit/Deposit");
                                    string type = System.Console.ReadLine() ?? throw new InvalidOperationException();
                                    System.Console.WriteLine("What sum of account?");
                                    int sum = Convert.ToInt32(System.Console.ReadLine());
                                    bank.AddClientWithNewAccount(client, sum, type);
                                    System.Console.WriteLine("Client's account successfully create");
                                    if (client.Accounts != null) System.Console.WriteLine(client.Accounts[0].Id);
                                }
                            }
                        }
                    }

                    break;
                case "5":
                    if (centralBank.Banks.Count == 0)
                    {
                        System.Console.WriteLine("There is no bank, you can't do it");
                    }
                    else
                    {
                        System.Console.WriteLine("Input bank's name");
                        string? nameBank = System.Console.ReadLine();
                        if (!centralBank.ExistBank(nameBank))
                        {
                            System.Console.WriteLine("There is no such bank, you can't do it");
                        }

                        System.Console.WriteLine("Input name and sername client");
                        name = System.Console.ReadLine();
                        sername = System.Console.ReadLine();
                        foreach (Client client in clients)
                        {
                            if (client.Name == name && client.Surname == sername)
                            {
                                Bank bank = centralBank.FindBank(nameBank);
                                System.Console.WriteLine($"List of Client's accounts, chose the number:\n{client.Accounts}");
                                int number = Convert.ToInt32(System.Console.ReadLine());
                                System.Console.WriteLine("What sum does client want to put?");
                                int sum = Convert.ToInt32(System.Console.ReadLine());
                                if (client.Accounts != null) bank.PutMoney(client, client.Accounts[number - 1], sum);
                                if (client.Accounts != null)
                                    System.Console.WriteLine($"{client.Name}'s budget is {client.Accounts[number - 1].Sum}");
                                break;
                            }
                        }
                    }

                    break;
                case "6":
                    if (centralBank.Banks.Count == 0)
                    {
                        System.Console.WriteLine("There is no bank, you can't do it");
                    }
                    else
                    {
                        System.Console.WriteLine("Input bank's name");
                        string? nameBank = System.Console.ReadLine();
                        if (!centralBank.ExistBank(nameBank))
                        {
                            System.Console.WriteLine("There is no such bank, you can't do it");
                        }

                        System.Console.WriteLine("Input name and sername client");
                        name = System.Console.ReadLine();
                        sername = System.Console.ReadLine();
                        foreach (Client client in clients)
                        {
                            if (client.Name == name && client.Surname == sername)
                            {
                                Bank bank = centralBank.FindBank(nameBank);
                                System.Console.WriteLine($"List of Client's accounts, chose the number:\n{client.Accounts}");
                                int number = Convert.ToInt32(System.Console.ReadLine());
                                System.Console.WriteLine("What sum does client want to withdraw?");
                                int sum = Convert.ToInt32(System.Console.ReadLine());
                                if (client.Accounts != null) bank.WithdrawMoney(client, client.Accounts[number - 1], sum);
                                if (client.Accounts != null)
                                    System.Console.WriteLine($"{client.Name}'s budget is {client.Accounts[number - 1].Sum}");
                                break;
                            }
                        }
                    }

                    break;
                case "7":
                    if (centralBank.Banks.Count == 0)
                    {
                        System.Console.WriteLine("There is no bank, you can't do it");
                    }
                    else
                    {
                        System.Console.WriteLine("Input bank's name");
                        string? nameBank = System.Console.ReadLine();
                        if (!centralBank.ExistBank(nameBank))
                        {
                            System.Console.WriteLine("There is no such bank, you can't do it");
                        }

                        System.Console.WriteLine("Input name and sername client");
                        name = System.Console.ReadLine();
                        sername = System.Console.ReadLine();
                        foreach (Client client in clients)
                        {
                            if (client.Name == name && client.Surname == sername)
                            {
                                Bank bank = centralBank.FindBank(nameBank);
                                System.Console.WriteLine($"List of Client's accounts, chose the number:\n{client.Accounts}");
                                int number = Convert.ToInt32(System.Console.ReadLine());
                                System.Console.WriteLine("Input other bank's name");
                                string? nameOtherBank = System.Console.ReadLine();
                                Bank otherBank = centralBank.FindBank(nameOtherBank);
                                System.Console.WriteLine("Input name and sername client to transfer");
                                name = System.Console.ReadLine();
                                sername = System.Console.ReadLine();
                                foreach (Client otherClient in clients)
                                {
                                    if (otherClient.Name == name && otherClient.Surname == sername)
                                    {
                                        System.Console.WriteLine($"List of Client's accounts, chose the number:\n{client.Accounts}");
                                        int otherNumber = Convert.ToInt32(System.Console.ReadLine());
                                        System.Console.WriteLine("What sum does client want to transfer?");
                                        int sum = Convert.ToInt32(System.Console.ReadLine());
                                        if (client.Accounts != null)
                                        {
                                            if (otherClient.Accounts != null)
                                            {
                                                centralBank.TransferToAnotherBank(client.Accounts[number - 1], bank, otherBank, otherClient.Accounts[otherNumber - 1], sum);
                                                if (client.Accounts != null)
                                                {
                                                    System.Console.WriteLine(
                                                        $"{client.Name}'s budget is {client.Accounts[number - 1].Sum}");
                                                    System.Console.WriteLine($"{otherClient.Name}'s budget is {otherClient.Accounts[otherNumber - 1].Sum}");
                                                }
                                            }
                                        }

                                        break;
                                    }
                                }

                                break;
                            }
                        }
                    }

                    break;
            }
        }
    }
}*/