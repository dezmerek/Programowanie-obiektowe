using System;

namespace lab_2
{
    abstract class AbstractMessage
    {
        public string Content { get; init; }
        abstract public void Send();
    }

    class EmailMessage : AbstractMessage
    {
        public string To { get; init; }
        public string From { get; init; }
        public string Subject { get; init; }
        public override void Send()
        {
            Console.WriteLine($"Sending email from {From} with content {Content}");
        }
    }

    class SmsMessage : AbstractMessage
    {
        public string PhoneNumber { get; init; }
        public override void Send()
        {
            Console.WriteLine($"Sending sms to {PhoneNumber} with content {Content}");
        }
    }
    class MessangerMessge : AbstractMessage
    {
        public override void Send()
        {
            Console.WriteLine($"Sending messager message with content {Content}");
        }
    }
    interface IFly
    {
        void Fly();
    }

    interface ISwim
    {
        void Swim();
    }

    interface IFlyAndSwim : IFly, ISwim
    {

    }

    class Duck : IFlyAndSwim
    {
        public void Fly()
        {
            throw new NotImplementedException();
        }

        public void Swim()
        {
            throw new NotImplementedException();
        }
    }

    class Wasp : IFly
    {
        public void Fly()
        {
            throw new NotImplementedException();
        }
    }

    class Hydroplane : IFlyAndSwim
    {
        public void Fly()
        {
            throw new NotImplementedException();
        }

        public void Swim()
        {
            throw new NotImplementedException();
        }
    }

    // Do zrobienia Ćwiczenie 1,2,3
    public abstract class Vehicle
    {
        public double Weight { get; init; }
        public int MaxSpeed { get; init; }
        protected int _mileage;
        public int Mealeage
        {
            get { return _mileage; }
        }
        public abstract decimal Drive(int distance);
        public override string ToString()
        {
            return $"Vehicle{{ Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage} }}";
        }
    }

    public class Car : Vehicle
    {
        public bool isFuel { get; set; }
        public bool isEngineWorking { get; set; }
        public override decimal Drive(int distance)
        {
            if (isFuel && isEngineWorking)
            {
                _mileage += distance;
                return (decimal)(distance / (double)MaxSpeed);
            }
            return -1;
        }
        public override string ToString()
        {
            return $"Car{{Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage}}}";
        }
    }

    public class Bicycle : Vehicle
    {
        public bool isDriver { get; set; }
        public override decimal Drive(int distance)
        {
            if (isDriver)
            {
                _mileage += distance;
                return (decimal)(distance / (double)MaxSpeed);
            }
            return -1;
        }
        public override string ToString()
        {
            return $"Bicycle{{Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage}}}"; ;
        }
    }

    public abstract class Scooter : Vehicle { }

    public class ElectricScooter : Scooter
    {
        private int _battery = 0;
        public int BatteriesLevel { get { return _battery; } }
        public int MaxRange = 100;

        public void ChargeBatteries()
        {
            _battery = 100;
        }
        public override decimal Drive(int distance)
        {
            if (distance > 0)
            {
                if (_battery / MaxRange * 100 > distance)
                {
                    _battery -= distance;
                    return distance;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }

    public class KickScooter : Scooter
    {
        public override decimal Drive(int distance)
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            ElectricScooter scooter = new ElectricScooter();
            scooter.ChargeBatteries();
            scooter.Drive(10);
            Console.WriteLine(scooter.BatteriesLevel);

            Vehicle[] vehicles =
            {
                new Bicycle(){Weight = 15, MaxSpeed = 30, isDriver = true},
                new Car(){Weight = 900, MaxSpeed = 120, isFuel = true, isEngineWorking = true},
                new Bicycle(){Weight = 21, MaxSpeed = 40, isDriver = true},
                new Bicycle(){Weight = 19, MaxSpeed = 35, isDriver = true},
                new Car(){Weight = 1200, MaxSpeed = 130, isFuel = true, isEngineWorking = true}
            };
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine("Time for distance: " + vehicle.Drive(45));
            }
            int bicycleCounter = 0;
            int carCounter = 0;
            foreach (var vehicle in vehicles)
            {
                if (vehicle is Bicycle)
                {
                    bicycleCounter++;
                    Bicycle bicycle = (Bicycle)vehicle;
                    Console.WriteLine("Czy rower ma kierowcę: " + bicycle.isDriver);
                }
                if (vehicle is Car)
                {
                    carCounter++;
                }
            }
            Console.WriteLine($"Liczba rowerów: {bicycleCounter}, liczba samochodów: {carCounter}");

            //
            AbstractMessage[] messages = new AbstractMessage[4];
            messages[0] = new EmailMessage() { Content = "hello", To = "adam@op.pl", From = "", Subject = "" };
            messages[1] = new SmsMessage() { Content = "hello from sms", PhoneNumber = "555444666" };
            messages[2] = new EmailMessage() { Content = "Cześć", To = "ola@op.pl", From = "", Subject = "" };
            messages[3] = new MessangerMessge() { Content = "Wiadomość" };

            int mailCounter = 0;

            foreach (var message in messages)
            {
                message.Send();

                ////1
                //if (message is EmailMessage)
                //{
                //    mailCounter++;
                //    //EmailMessae email = (EmailMessage)message;
                //}

                ////2 jeśli message nie jest typu EmailMessage to email jest rowny null
                EmailMessage email = message as EmailMessage;
                //Console.WriteLine(email.Subject);
                mailCounter += email == null ? 0 : 1;
            }

            Console.WriteLine($"Liczba wysłanych emaili: {mailCounter}");

            IFly[] flyingObjects = new IFly[3];
            flyingObjects[0] = new Duck();
            flyingObjects[1] = new Hydroplane();
            flyingObjects[2] = new Wasp();

            int flyCounter = 0;
            int swimCounter = 0;
            foreach (var message in flyingObjects)
            {
                if (message is IFly)
                {
                    flyCounter++;
                }
                if (message is ISwim)
                {
                    swimCounter++;
                }
            }
            Console.WriteLine($"Liczba Latajacych: {flyCounter}, liczba pływających: {swimCounter}");





            string[] names = { "Adam", "Ewa" };
            IAggregate aggregate = new StringAggregate(names);
            aggregate = new SimpleAggregate() { FirstName = "Mateusz", LastName = "S" };
            //
            IItreator iterator = aggregate.createItreator();
            while (iterator.HasNext())
            {
                Console.WriteLine(iterator.GetNext());
            }
        }
    }

    interface IAggregate
    {
        IItreator createItreator();
    }

    interface IItreator
    {
        bool HasNext();
        string GetNext();
    }

    class SimpleAggregate : IAggregate
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }

        public IItreator createItreator()
        {
            return new SimpleItreator(this);
        }
    }

    class SimpleItreator : IItreator
    {
        private SimpleAggregate aggregate;
        public int count = 0;

        public SimpleItreator(SimpleAggregate aggreagate)
        {
            this.aggregate = aggreagate;
        }

        public string GetNext()
        {
            switch (++count)
            {
                case 1:
                    return aggregate.FirstName;
                case 2:
                    return aggregate.LastName;
                default:
                    throw new Exception();
            }
        }

        public bool HasNext()
        {
            return count < 2;
        }
    }

    class StringAggregate : IAggregate
    {
        internal string[] names;

        public StringAggregate(string[] names)
        {
            this.names = names;
        }

        public IItreator createItreator()
        {
            return new StringItreator(this);
        }
    }

    class StringItreator : IItreator
    {
        private StringAggregate aggregate;
        private int index = 0;

        public StringItreator(StringAggregate aggregate)
        {
            this.aggregate = aggregate;
        }

        public string GetNext()
        {
            return aggregate.names[index++];
        }

        public bool HasNext()
        {
            return index < aggregate.names.Length;
        }
    }
}
