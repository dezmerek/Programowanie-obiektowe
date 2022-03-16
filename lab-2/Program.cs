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

            //kod odpowiedzialny za wysyłke
            foreach (var message in messages)
            {
                message.Send();
            }
        }
    }
}
