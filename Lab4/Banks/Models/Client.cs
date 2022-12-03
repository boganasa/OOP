using Banks.Exceptions;
using Banks.Interfaces;

namespace Banks.Models;

public interface INameBuilder
{
    ISurnameBuilder WithName(string name);
}

public interface ISurnameBuilder
{
    IClientBuilder WithSurname(string surname);
}

public interface IClientBuilder
{
    IClientBuilder WithAddress(string? address);
    IClientBuilder WithPassportNumber(int passport);
    Client Build();
}

public class Client : IObserver
{
    private int _passportNumber;
    public static INameBuilder Builder => new ClientBuilder();

    public string? Name { get; private set; }
    public string? Surname { get; private set; }
    public string? Address { get; set; }
    public IReadOnlyList<IBankAccount>? Accounts { get; private set; }
    public IReadOnlyList<string>? Messages { get; private set; }

    public int PassportNumber
    {
        get
        {
            return _passportNumber;
        }

        set
        {
            if (_passportNumber != 0 && (int)Math.Log10(value) + 1 != 10)
            {
                throw new PassportException(_passportNumber);
            }

            _passportNumber = value;
        }
    }

    public static bool operator ==(Client client1, Client client2)
    {
        if (client1.Name == client2.Name && client1.Surname == client2.Surname)
        {
            return true;
        }

        return false;
    }

    public static bool operator !=(Client client1, Client client2)
    {
        if (client1.Name != client2.Name || client1.Surname != client2.Surname)
        {
            return true;
        }

        return false;
    }

    public void Update(object obj)
    {
        var newList = new List<string?>();
        if (Messages != null)
        {
            newList = new List<string?>(Messages);
        }

        newList.Add(obj.ToString());
        Messages = newList!;
    }

    public void AddAccount(IBankAccount account)
    {
        var newList = new List<IBankAccount>();
        if (Accounts != null)
        {
            newList = new List<IBankAccount>(Accounts);
        }

        newList.Add(account);
        Accounts = newList;
    }

    public bool SuspicionOfAttacker()
    {
        if (Address is null || PassportNumber == 0)
        {
            return true;
        }

        return false;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Client)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Surname, Address, Accounts);
    }

    protected bool Equals(Client other)
    {
        return Name == other.Name && Surname == other.Surname && Address == other.Address && Equals(Accounts, other.Accounts);
    }

    private class ClientBuilder : INameBuilder, ISurnameBuilder, IClientBuilder
    {
        private Client _client = new Client();

        public ISurnameBuilder WithName(string name)
        {
            _client.Name = name;
            return this;
        }

        public IClientBuilder WithSurname(string surname)
        {
            _client.Surname = surname;
            return this;
        }

        public IClientBuilder WithAddress(string? address)
        {
            _client.Address = address;
            return this;
        }

        public IClientBuilder WithPassportNumber(int passport)
        {
            if (passport != 0 && (int)Math.Log10(passport) + 1 != 10)
            {
                throw new PassportException(passport);
            }

            _client.PassportNumber = passport;
            return this;
        }

        public Client Build()
        {
            _client.Accounts = new List<IBankAccount>();
            _client.Messages = new List<string>();
            return _client;
        }
    }
}