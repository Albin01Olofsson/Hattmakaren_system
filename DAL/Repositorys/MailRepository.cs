using DAL.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using MailKit;
using MailKit.Net.Imap;

namespace DAL.Repositorys
{
    public class MailRepository : IMailRepository
    {

        private readonly string _server;
        private readonly int _port;
        private readonly string _email;
        private readonly string _lösenord;
        private readonly bool _användSsl;

        public MailRepository(string server, int port, string email, string lösenord, bool användSsl)
        {
            _server = server;
            _port = port;
            _email = email;
            _lösenord = lösenord;
            _användSsl = användSsl;
        }

        public async Task<List<Mail>> GetMailsAsync()
        {
            var emailList = new List<Mail>();

            using var klient = new ImapClient();

            await klient.ConnectAsync(_server, _port, _användSsl);
            await klient.AuthenticateAsync(_email, _lösenord);

            var inkorg = klient.Inbox;
            await inkorg.OpenAsync(FolderAccess.ReadOnly);

            int läsInAntal = 100;
            int startIndex = Math.Max(0, inkorg.Count - läsInAntal);

            for(int i = inkorg.Count - 1; i >= startIndex; i--)
            {
                var meddelande = await inkorg.GetMessageAsync(i);
                var email = new Mail
                {
                    mailId = meddelande.MessageId ?? "",
                    Avsändare = meddelande.From.ToString(),
                    Ämne = meddelande.Subject ?? "",
                    Innehåll = meddelande.TextBody ?? "",
                    Datum = meddelande.Date.DateTime
                };
                emailList.Add(email);
            }
            await klient.DisconnectAsync(true);
            return emailList;
        }
    }
}
