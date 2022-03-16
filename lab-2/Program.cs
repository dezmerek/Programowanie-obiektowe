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

    interface IFlyAndSwim:IFly, ISwim
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

    class Program
    {
        static void Main(string[] args)
        {
            string messageType = "email";

            switch(messageType)
            {
                case "email":
                    Console.WriteLine("Wyslanie emaila");
                    break;
                case "sms":
                    Console.WriteLine("Wyslanie sms");
                    break;
                case "messanger":
                    Console.WriteLine("Wysylanie powiadomienia");
                    break;
            }
            AbstractMessage[] messages = new AbstractMessage[4];
            messages[0] = new EmailMessage() { Content = "hello", To = "adam@op.pl", From = "", Subject = "" };
            messages[1] = new SmsMessage() { Content = "hello from sms", PhoneNumber = "555444666" };
            messages[2] = new EmailMessage() { Content = "Cześć", To = "ola@op.pl", From = "", Subject = "" };
            messages[3] = new MessangerMessge() { Content = "Wiadomość" };

            int mailCounter = 0;

            //kod odpowiedzialny za wysyłke
            foreach (var message in messages)
            {
                message.Send();

                //1
                //if (message is EmailMessage)
                //{
                //    mailCounter++;
                //    EmailMessae email = (EmailMessage) message;
                //}

                //2 jeśli message nie jest typu EmailMessage to email jest rowny null
                EmailMessage email = message as EmailMessage;
                //Console.WriteLine(email.Subject);
                mailCounter += email == null ? 0 : 1;
            }
            Console.WriteLine($"Liczba wysłanych emaili: {mailCounter}");

            IFly[] flyingObjects = new IFly[3];
            flyingObjects[0] = new Duck();
            flyingObjects[1] = new Hydroplane();
            ISwim swimming = flyingObjects[0] as ISwim;

            string[] names = { "Adam", "Ewa", "Karol" };
            IAggregate aggregate = new StringAggregate(names);
            aggregate = new SimpleAggregate() { FirstName="Karol",LastName="Okrasa"};
            //
            IItreator itreator = aggregate.createItreator();
            while(itreator.HasNext())
            {
                Console.WriteLine(itreator.GetNext());
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

        class SimpleAggregate:IAggregate
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
            private int count = 0;
            private SimpleAggregate simpleAggregate;

            public SimpleItreator(SimpleAggregate simpleAggregate)
            {
                this.simpleAggregate = simpleAggregate;
            }

            public string GetNext()
            {
                switch(++count)
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
}
