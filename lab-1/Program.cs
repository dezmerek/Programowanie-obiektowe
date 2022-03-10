using System;

namespace lab_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person = Person.OfName("Mateusz");
            person.Equals(1);
            Console.WriteLine(person.Name == null ? "Null" : person.Name);
            Money money = Money.OfWithException(13, Currency.USD);
            Console.WriteLine(money.Value);
            Console.WriteLine(4 * money);
            Money result = 4 * money;
            Console.WriteLine(result.Value);
            Money result2 = money * 4;
            Console.WriteLine(result.Value);
            Console.WriteLine(money < Money.OfWithException(5, Currency.USD));
            //jest to porónanie referencji a nie wartości więc zawsze będzie błędne
            if (money == Money.OfWithException(13, Currency.USD))
            {
                Console.WriteLine("rowne");
            }
            else
            {
                Console.WriteLine("rozne");
            }
            int c = 5;
            long b = 5;
            b = c;
            // c = b; niejawne
            c = (int)b; //jawne
            decimal d = money;
            double price = (double)money;
            string str = (string)money;
            Console.WriteLine((string)money);
            Money[] prices =
            {
                Money.OfWithException(10, Currency.USD),
                Money.OfWithException(13, Currency.PLN),
                Money.OfWithException(15, Currency.USD),
                Money.OfWithException(12, Currency.EUR),
                Money.OfWithException(16, Currency.USD),
                Money.OfWithException(19, Currency.PLN),
                Money.OfWithException(11, Currency.EUR),
            };
            Array.Sort(prices);
            foreach (var i in prices)
            {
                Console.WriteLine((string)i);
            }
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

        public decimal Value
        {
            get
            {
                return _value;
            }

        }
        public Currency Currency
        {
            get
            {
                return _currency;
            }
        }
        public static Money OfWithException(decimal value, Currency currency)
        {
            if (value > 0)
            {
                return new Money(value, currency);
            }
            else
            {
                throw new ArgumentException("Niepoprawny Obiekt");
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
        private static object IsSameCurrencies(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                throw new ArgumentException("Currecncy Needs To Be Same");
            }
            else
            {

                return a.Value > b.Value;
            }
        }
        //money 8 4 --> *(money, 4)
        public static Money operator *(decimal factor, Money money)
        {
            return Money.OfWithException(money.Value * factor, money.Currency);
        }
        public static Money operator *(Money money, decimal factor)
        {
            return Money.OfWithException(money.Value * factor, money.Currency);
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
            return $"{_value}, Currency: {_currency}";
        }

        public int CompareTo(Money other)
        {
            int x = Currency.CompareTo(other.Currency);
            if (x == 0)
            {
                return Value.CompareTo(other.Value);
            }
            else
            {
                return x;
            }
        }
    }
    public class Person : IEquatable<Person>
    {
        private Person(string name)
        {
            _name = name;
        }
        private string _name;
        public int ects { get; set; }
        public static Person OfName(String name)
        {
            if (name != null && name.Length >= 2)
            {
                return new Person(name);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Imię Zbyt Krótkie");
            }
        }
        public override string ToString()
        {
            return $"Name: {_name}";
        }
        public bool Equals(Person other)
        {
            return other._name.Equals(_name) && other.ects == ects;
        }
        public override bool Equals(object obj)
        {
            return obj is Person person &&
                   ects == person.ects &&
                   Name == person.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ects, Name);
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value != null && value.Length >= 2)
                {
                    _name = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Imię jest zbyt krótkie");
                }
            }
        }
    }
}