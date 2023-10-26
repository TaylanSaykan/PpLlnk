using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class MailService : IMailService
    {
        public async Task SendEmailAsync(string personelEmail, string password, string userId)
        {
            MailMessage mail = new MailMessage();
            mail.Subject = "Şirket Mail Bilgilendirmesi";
            mail.Body = "<h2>Aramıza hoşgeldiniz. Yeni işinizde başarılar dileriz.</h2> <br/>";
            mail.Body += "<h4>Şirket mail adresiniz ve geçici şifreniz tanımlanmıştır..</h4> <br/>";
            mail.Body += $"<strong>Mail Adresi : </strong> {personelEmail} <br/> <br/>";
            mail.Body += $"<strong>Geçici Şifre : </strong> {password}<br/> <br/>";
            mail.Body += $"<a asp-route-userId=\"{userId}\" href=\"https://peoplelinkweb1.azurewebsites.net/Identity/Account/PasswordChange?userId={userId}\"> Şifrenizi değiştirmek için buraya tıklayınız</a>\n";
            mail.IsBodyHtml = true;
            mail.From = new MailAddress("gurbuzysn@gmail.com", "People Link İnsan Kaynakları");
            mail.To.Add(personelEmail);

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("gurbuzysn@gmail.com", "mvjahfnxrswdoljy");

            await smtp.SendMailAsync(mail);
        }
    }
}
