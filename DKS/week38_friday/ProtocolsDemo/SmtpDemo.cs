using System.Net;
using System.Net.Mail;

class SmtpDemo
{
  public static void Run()
  {
    Console.WriteLine("SMTP Demo:");
    try
    {
      using SmtpClient client = new("smtp.gmail.com")
      {
        Port = 587,
        EnableSsl = true,
        Credentials = new NetworkCredential("sebbed89@gmail.com", "rxpv usuq mczb mowu")
      };

      var mail = new MailMessage("john.doe@foo.io", "felix@cau.se", "Test Email", "Hello Felix, follow the white rabbit.");

      client.Send(mail);
      Console.WriteLine("Email sent successfully via Gmail SMTP");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"SMTP Error {ex.Message}");
    }
  }
}