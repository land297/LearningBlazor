﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;

namespace Learning.Server.Service {
    public class MailKitEmailSenderOptions {
        public MailKitEmailSenderOptions() {
            Host_SecureSocketOptions = SecureSocketOptions.Auto;
        }
        public string Host_Address { get; set; }
        public int Host_Port { get; set; }
        public string Host_Username { get; set; }
        public string Host_Password { get; set; }
        public SecureSocketOptions Host_SecureSocketOptions { get; set; }
        public string Sender_EMail { get; set; }
        public string Sender_Name { get; set; }
    }

    public interface IMailKitEmailSender {
        MailKitEmailSenderOptions Options { get; set; }

        Task Execute(string to, string subject, string message);
        Task SendEmailAsync(string email, string subject, string message);
    }

    public class MailKitEmailSender : IMailKitEmailSender {
        public MailKitEmailSender(IOptions<MailKitEmailSenderOptions> options) {
            this.Options = options.Value;
        }

        public MailKitEmailSenderOptions Options { get; set; }

        public Task SendEmailAsync(string email, string subject, string message) {
            return Execute(email, subject, message);
        }

        public Task Execute(string to, string subject, string message) {
            // create message
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(Options.Sender_Name, Options.Sender_EMail);
                //email.Sender.Name = Options.Sender_Name;
            email.From.Add(email.Sender);
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = message };

            // send email
            using (var smtp = new SmtpClient()) {
                smtp.Connect(Options.Host_Address, Options.Host_Port, Options.Host_SecureSocketOptions);
                smtp.Authenticate(Options.Host_Username, Options.Host_Password);
                smtp.Send(email);
                smtp.Disconnect(true);
            }

            return Task.FromResult(true);
        }
    }
}
