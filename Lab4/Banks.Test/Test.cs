using Banks.Entities;
using Banks.Models;
using Banks.Services;
using Xunit;

namespace Banks.Test;

public class Test
{
    [Fact]
    public void CanCreateAndAddBank()
    {
        var centralBank = new CentralBank();
        centralBank.CreateBank("SberBank", 1, 10, 10000, 5, "0-10000=1%,10000-100000=3%,5%", 50000, 5);

        Assert.Equal(1, centralBank.Banks.Count);
    }

    [Fact]
    public void CanCreateClientAndAddHimCreditAccount()
    {
        var centralBank = new CentralBank();
        Bank sberBank = centralBank.CreateBank("SberBank", 1, 10, 10000, 5, "0-10000=1%,10000-100000=3%,5%", 50000, 5);
        var client = Client.Builder
            .WithName("Alesha")
            .WithSurname("Ivanov")
            .WithAddress("Lenina 20A")
            .Build();

        Assert.True(client.SuspicionOfAttacker());
        client.PassportNumber = 1234567890;

        Assert.False(client.SuspicionOfAttacker());

        sberBank.AddClientWithNewAccount(client, 10000, "Credit");

        if (client.Accounts != null) Assert.Equal(10, client.Accounts[0].Commission);
    }

    [Fact]
    public void CanCreateClientAndAddHimDebitAccount()
    {
        var centralBank = new CentralBank();
        Bank sberBank = centralBank.CreateBank("SberBank", 1, 10, 10000, 5, "0-10000=1%,10000-100000=3%,5%", 50000, 5);
        var client = Client.Builder
            .WithName("Alesha")
            .WithSurname("Ivanov")
            .WithAddress("Lenina 20A")
            .Build();

        Assert.True(client.SuspicionOfAttacker());
        client.PassportNumber = 1234567890;

        Assert.False(client.SuspicionOfAttacker());

        sberBank.AddClientWithNewAccount(client, 10000, "Debit");

        if (client.Accounts != null) Assert.Equal(5, client.Accounts[0].InterestOnTheBalance);
    }

    [Fact]
    public void CanCreateClientAndAddHimDepositAccount()
    {
        var centralBank = new CentralBank();
        Bank sberBank = centralBank.CreateBank("SberBank", 1, 10, 10000, 5, "0-10000=1%,10000-100000=3%,5%", 50000, 5);
        var client = Client.Builder
            .WithName("Alesha")
            .WithSurname("Ivanov")
            .WithAddress("Lenina 20A")
            .Build();

        Assert.True(client.SuspicionOfAttacker());
        client.PassportNumber = 1234567890;

        Assert.False(client.SuspicionOfAttacker());

        sberBank.AddClientWithNewAccount(client, 30000, "Deposit");

        if (client.Accounts != null) Assert.Equal(3, client.Accounts[0].InterestOnTheBalance);
    }
}