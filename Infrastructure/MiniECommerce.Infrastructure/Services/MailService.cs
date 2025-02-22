﻿using Microsoft.Extensions.Configuration;
using MiniECommerce.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(_configuration["Mail:Username"], "MINI E-COMMERCE", System.Text.Encoding.UTF8);

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port = 587; //_configuration["Mail:Port"];
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"]; //"srvc179.turhost.com";

            await smtp.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();
            mail.AppendLine("Merhaba<br>Eger yeni sifre talebinde bulunduysaniz asagidaki linkten sifrenizi yenileyebilirsiniz.<br><strong><a target=\"_blank\" href=\"");
            mail.AppendLine(_configuration["AngularClientUrl"]);
            mail.AppendLine("/update-password/");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.AppendLine(resetToken);
            mail.AppendLine("\">Yeni sifre talebi icin tiklayiniz...</a><strong><br><br><span style=\"font-size:12px;\">NOT:  Bu sifre yenileme talebi tarafinizca yapilmadiysa bu maili dikkate almayiniz.</span><br>Saygilarimizla <br><br><br>MINI E-COMMERCE");

            await SendMailAsync(to, "Sifre Yenileme Talebi", mail.ToString());
        }

        public async Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string userName)
        {
            string mail = $"Sayin {userName} Merhaba<br>" +
                $"{orderDate} tarihinde vermis oldugunuz {orderCode} kodlu siparisiniz tamamlanmis ve kargo firmasina verilmistir.<br>Saygilarimizla";
            
            await SendMailAsync(to, $"{orderCode} siparis numarali siparisiniz tamamlandi", mail);
        }
    }
}
