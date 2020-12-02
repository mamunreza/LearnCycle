using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace EmailSenderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Send email start");
            Execute().Wait();
            Console.WriteLine("Send email end");
            Console.ReadKey();
        }

        static async Task Execute()
        {
            var apiKey = @"###";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("mamunrezadev@gmail.com", "Ant");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("mamun.reza09@gmail.com", "Mamun Reza");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
