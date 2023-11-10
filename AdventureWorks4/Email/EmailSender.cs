using AdventureWorks4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using QRCoder;

namespace AdventureWorks4.Email
{
	public static class EmailSender
	{
		public static void SendEmail(SmtpParams smtpParams)
		{
			using (MailMessage mm = new MailMessage("claseprogra5castrocarazo@gmail.com", smtpParams.ReceiverEmail))
			{
				mm.Subject = "Bienvenido a AdventureWorks4";
				mm.IsBodyHtml = true;

				using (var sr = new StreamReader("wwwroot/welcome.txt"))
				{
					// Read the stream as a string, and write the string to the console.
					string body = sr.ReadToEnd().Replace("@CLIENTNAME", smtpParams.ClientName);
					body = body.Replace("@IMAGEQR", Misc.QRGenerator.GenerateQR());
					mm.Body = body;
				}

				SmtpClient smtp = new SmtpClient();
				smtp.Host = "smtp.gmail.com";
				smtp.EnableSsl = true;
				NetworkCredential NetworkCred = new NetworkCredential("claseprogra5castrocarazo@gmail.com", "mebgvqujickehjsh");
				smtp.UseDefaultCredentials = false;
				smtp.Credentials = NetworkCred;
				smtp.Port = 587;
				smtp.Send(mm);
			}
		}
	}
}
