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
        public MailRepository()
        {
        }

        public async Task<List<Mail>> GetMailsAsync()
        {
            

            try
            {
                var emailList = new List<Mail>();

                using var klient = new ImapClient();

                await klient.ConnectAsync("imap.gmail.com", 993, true);
                await klient.AuthenticateAsync("hattmakaren005@gmail.com", "wuambzrflbnvvfkk");//gmail och APP-lösen inte vanligt lösen
                var inkorg = klient.Inbox;
                await inkorg.OpenAsync(FolderAccess.ReadOnly);

                int läsInAntal = 100;
                int startIndex = Math.Max(0, inkorg.Count - läsInAntal); 

                for (int i = inkorg.Count - 1; i >= startIndex; i--)
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
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}
