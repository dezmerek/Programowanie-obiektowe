using System;

namespace lab_1
{
    public enum Currency
    {
        PLN = 1,
        USD = 2,
        EUR = 3
    }

    public class Money : IComparable<Money>
    {
        private readonly decimal _value;
        private readonly Currency _currency;
        private Money(decimal value, Currency currency)
        {
            _value = value;
            _currency = currency;
        }
        public static Money? OfWithException(decimal value, Currency currency)
        {

            if (value < 0)
            {
                throw new ArgumentException("Liczba mniejsza od zera");
            }
            else
            {
                return new Money(value, currency);

            }
        }
        public decimal Value
        {
            get { return _value; }
        }
        public Currency Currency
        {
            get
            {
                return _currency;
            }
        }
        public static Money operator *(Money money, decimal factor)
        {
            return Money.OfWithException(money.Value * factor, money.Currency);
        }
        public static Money operator *(decimal factor, Money money)
        {
            return Money.OfWithException(money.Value * factor, money.Currency);
        }

        public static bool operator >(Money a, Money b)
        {
            IsSameCurrencies(a, b);
            return a.Value > b.Value;
        }

        private static void IsSameCurrencies(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                throw new ArgumentException("Inne waluty!");
            }

        }
        public static void ToCurrency(Money a, Money b)
        {
            decimal result;
            if (a.Currency == Currency.PLN && b.Currency == Currency.USD)
            {
                result = a.Value * 2;
            }

        }
        public static implicit operator decimal(Money money)
        {
            return money.Value;
        }
        public static explicit operator double(Money money)
        {
            return (double)money.Value;
        }
        public static explicit operator string(Money money)
        {
            return $"{money.Value} {money.Currency}";
        }

        public override string ToString()
        {
            return $"Value: {_value}, Currency: {_currency}";
        }

        public int CompareTo(Money? other)
        {
            return Currency.CompareTo(other.Currency);
            var currencyCon = Currency.CompareTo(other.Currency);
            if (currencyCon == 0)
            {
                return Value.CompareTo(other.Value);
            }
            else
            {
                return currencyCon;
            }
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var currencyComparison = _currency.CompareTo(other._currency);
            if (currencyComparison != 0) return currencyComparison;
            return _value.CompareTo(other._value);
        }

        public static bool operator <(Money a, Money b)
        {
            IsSameCurrencies(a, b);
            return a.Value < b.Value;
        }

    }
    public class Tank
    {
        public readonly float Capacity;
        private float _level;
        public Tank(float capacity)
        {
            Capacity = (float)capacity;
        }
        public float Level
        {
            get
            {
                return _level;
            }
            private set
            {
                if (value < 0 || value > Capacity)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _level = value;
            }
        }
        public float refuel(float amount)
        {
            if (amount < 0 || _level == Capacity)
            {
                return 0;
            }
            if (_level + amount > Capacity)
            {
                float result = (_level + amount) - Capacity;
                _level = Capacity;
                if (result > 0)
                {
                    Tank tank2 = new Tank(100);
                    tank2.refuel(result);
                    Console.WriteLine("tank 2 = " + tank2.Level);
                }
                return result;
            }

            _level += amount;
            return amount;
        }

    }

    public class Person : IEquatable<Person>
    {
        private string _name;
        public int Ects { get; set; }
        //.//
        private Person(string name)
        {
            _name = name;
        }
        public static Person OfName(String name)
        {
            if (name != null && name.Length >= 2)
            {
                return new Person(name);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Imię zbyt krótkie");
            }
        }

        public override string ToString()
        {
            return $"Name: {_name}";
        }

        public override bool Equals(object obj)
        {
            return obj is Person person &&
                   Ects == person.Ects &&
                   Name == person.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Ects, Name);
        }


        public bool Equals(Person other)
        {
            return other.Name.Equals(_name) && other.Ects == Ects;
        }


        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != null && value.Length >= 2)
                {
                    _name = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Imię zbyt krótkie");
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Tank tank = new Tank(100);
            Console.WriteLine("tank 1 = " + tank.Level);
            tank.refuel(10);
            Console.WriteLine("tank 1 = " + tank.Level);
            tank.refuel(100);
            Console.WriteLine("tank 1 = " + tank.Level);


            Person person = Person.OfName("Ada");
            Console.WriteLine(person);
            Object obj = person;
            IEquatable<Person> ie = person;
            Console.WriteLine(person.Name == null ? "null" : "person");
            Money money = Money.OfWithException(13, Currency.EUR);
            Console.WriteLine(money.Value);
            Money result = 4 * money;
            Console.WriteLine(result.Value);
            Console.WriteLine(money > Money.OfWithException(5, Currency.EUR));
            decimal money2 = money;
            double price = (double)money;
            string str = (string)money;
            Console.WriteLine(str);
            Console.WriteLine("SORT");
            Money[] pricies =
            {
                Money.OfWithException(10,Currency.PLN),
                Money.OfWithException(8,Currency.USD),
                Money.OfWithException(12,Currency.EUR),
                Money.OfWithException(6,Currency.PLN),
                Money.OfWithException(14,Currency.PLN),
                Money.OfWithException(3,Currency.PLN),
            };
            Array.Sort(pricies);
            foreach (var p in pricies)
            {
                Console.WriteLine((string)p);
            }
        }
    }
}