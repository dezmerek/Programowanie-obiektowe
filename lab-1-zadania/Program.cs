using System;

namespace Lab_1
{
    public class PersonProperties : IEquatable<PersonProperties>
    {

        private string _firstName;
        public int ECTS { get; set; }
        private PersonProperties(string firstName)
        {
            _firstName = firstName;
        }

        public static PersonProperties OfName(string name)
        {
            if (name != null && name.Length >= 2)
            {
                return new PersonProperties(name);
            }
            throw new ArgumentOutOfRangeException("Zbyt krótkie imię");
        }


        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (value != null && value.Length >= 2)
                {
                    _firstName = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Imie zbyt krótkie");
                }
            }
        }

        public override string ToString()
        {
            return $"Name: {_firstName}";
        }


        public override bool Equals(object obj)
        {
            return obj is PersonProperties properties &&
                   ECTS == properties.ECTS &&
                   FirstName == properties.FirstName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ECTS, FirstName);
        }
        public bool Equals(PersonProperties other)
        {
            if (!other._firstName.Equals(_firstName) && other.ECTS != ECTS)
            {
                throw new ArgumentOutOfRangeException("Różne imienia");
            }
            return true;
        }







    }
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
            if (value > 0)
            {
                return new Money(value, currency);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Liczba nie może być ujemna");
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

        private static void IsSameCurrencies(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                throw new ArgumentOutOfRangeException("Waluty nie są takie same");
            }
        }
        public static bool operator >(Money a, Money b)
        {
            IsSameCurrencies(a, b);
            return a.Value > b.Value;
        }
        public static bool operator <(Money a, Money b)
        {
            IsSameCurrencies(a, b);
            return a.Value < b.Value;
        }

        public static implicit operator decimal(Money money)
        {
            return money.Value;
        }

        public static explicit operator double(Money money)
        {
            return (double)money.Value;
        }

        public static explicit operator String(Money money)
        {
            return $"{money.Value} {money.Currency}";
        }

        public override string ToString()
        {
            return $"Value: {_value}, Currency: {_currency}";
        }

        public int CompareTo(Money other)
        {
            int currencyCon = Currency.CompareTo(other.Currency);
            if (currencyCon == 0)
            {
                return Value.CompareTo(other.Value);
            }
            else
            {
                return currencyCon;
            }
        }

        public decimal Value
        {
            get { return _value; }
        }

        public Currency Currency
        {
            get { return _currency; }
        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Person:");
            PersonProperties person = PersonProperties.OfName("Adrian");
            PersonProperties person2 = PersonProperties.OfName("Adrian");

            Console.WriteLine(person.Equals(person2));
            Console.WriteLine();
            Console.WriteLine("Money:");
            Money money = Money.OfWithException(10, Currency.PLN);
            Money money2 = Money.OfWithException(5, Currency.PLN);
            Console.WriteLine(money.Value);
            Console.WriteLine(money.Currency);
            Console.WriteLine();
            Console.WriteLine("Money Operator *(first)");
            Money result = money * 0.25m;
            Console.WriteLine(result.Value);

            Console.WriteLine("Money Operator *(second)");
            Money result2 = 0.25m * money;
            Console.WriteLine(result2.Value);
            Console.WriteLine("Money Operator >");
            Console.WriteLine(money > money2);
            Console.WriteLine("Money Operator <");
            Console.WriteLine(money < money2);

            Console.WriteLine("Konwersja");
            int c = 5;
            long b = 5l;
            b = c; // nie jawne
            c = (int)b; //jawne
            decimal cost = money;
            double price = (double)money;
            String str = (string)money;
            Console.WriteLine((string)money);

            Money[] prices =
            {
                Money.OfWithException(51,Currency.EUR),
                Money.OfWithException(88,Currency.PLN),
                Money.OfWithException(12,Currency.USD),
                Money.OfWithException(2,Currency.EUR),
                Money.OfWithException(102,Currency.PLN)
            };

            Array.Sort(prices);
            foreach (var item in prices)
            {
                Console.WriteLine((string)item);
            }
        }


    }
}