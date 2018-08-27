using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;

namespace ERP.Presentation.Api.Identity.Services
{
    /// <summary>
    /// Implementa o envio de mensagens de email
    /// Essa mesma classé é usada com Serviço de email pelo Identity
    /// </summary>
    public class EmailService : IIdentityMessageService
    {
        private string _destination, _subject, _body;
        public string Destionation { get => this._destination; set => this._destination = value; }
        public string Subject { get { return _subject; } set { _subject = value; } }
        public string Body { get { return _body; } set { _body = value; } }

        public EmailService()
        {
                
        }

        public EmailService(string destination, string subject, string body)
        {
            _destination = destination;
            _subject = subject;
            _body = body;
        }

        /// <summary>
        /// Envia o email com as propriedades ja preenchidas
        /// </summary>
        /// <returns></returns>
        public Task SendAsync()
        {
            return Task.FromResult(SendMailAsync());
        }

        /// <summary>
        /// Envia um email informando as informações de : Destinatario, Assunto e Corpo
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public Task SendAsync(string destination, string subject, string body)
        {
            _destination = destination;
            _subject = subject;
            _body = body;
            return Task.FromResult(SendMailAsync());
        }
        /// <summary>
        /// Envia a mensagem gerada pelo Identity
        /// </summary>
        /// <param name="message">Representa uma mensagem gerada pelo ASP.NET Identity</param>
        /// <returns>Retorna 0</returns>
        public Task SendAsync(IdentityMessage message)
        {
            // Habilitar o envio de e-mail
            if (true)
            {
                _destination = message.Destination;
                _subject = message.Subject;
                _body = message.Body;
                return Task.FromResult(SendMailAsync());
            }
        }

        private Task SendMailAsync()
        {
            try
            {
                //Cria a mensagem informando usuário remetente e o nome de exibição
                var msg = new MailMessage { From = new MailAddress(ConfigurationManager.AppSettings["SMTP_USUARIO"], ConfigurationManager.AppSettings["SMTP_DISPLAYNAME"]) };

                msg.To.Add(new MailAddress(this._destination));
                msg.Subject = _subject;

                //var htmlBody = HttpUtility.HtmlEncode(this._body);

                //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Plain));
                //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html));

                msg.IsBodyHtml = true;
                msg.Body = this._body;

                //Prepara o Client com suas credencias
                var smtpClient = new SmtpClient
                {
                    Host = ConfigurationManager.AppSettings["SMTP_SERVIDOR"],
                    Port = 587,
                    Credentials = new NetworkCredential()
                    {
                        UserName = ConfigurationManager.AppSettings["SMTP_USUARIO"],
                        Password = ConfigurationManager.AppSettings["SMTP_SENHA"]
                    },
                    EnableSsl = false
                };
                smtpClient.Send(msg);

                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                return Task.FromResult(false);
            }
        }

        
    }
}