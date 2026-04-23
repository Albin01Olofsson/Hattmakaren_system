using DAL.Intefaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;

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

                //Uppkoppling
                await klient.ConnectAsync("imap.gmail.com", 993, true);

                //Inloggning
                await klient.AuthenticateAsync("hattmakaren005@gmail.com", "wuambzrflbnvvfkk");//gmail och APP-lösen inte vanligt lösen

                //Hämta mailen
                var inkorg = klient.Inbox;
                await inkorg.OpenAsync(FolderAccess.ReadOnly);

                //variabler för att begränsa antalet inlästa emails
                int läsInAntal = 100;
                int startIndex = Math.Max(0, inkorg.Count - läsInAntal);

                //Sätt en plats att skapa en mapp som bilderna från mailen kommer att sparas i,
                //Görs automatiskt vid inläsning så att användaren kan välja bilden från mappen i specialbeställning och lägga till den där
                string baseDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName;
                string bildMapp = Path.Combine(baseDirectory, "DAL", "FörfråganBilder");
                Directory.CreateDirectory(bildMapp);


                //Loop för inläsning av mail
                for (int i = inkorg.Count - 1; i >= startIndex; i--)
                {
                    //Hämta ett meddelande
                    var meddelande = await inkorg.GetMessageAsync(i);

                    string bildSökväg = "";

                    //extrahera bilden ur inbakad fil i mail objektet
                    foreach (var bilaga in meddelande.Attachments)
                    {
                        //Objektet läses som en text fil, och bilden finns i länk format, om en länk innehåller "image/", skapa en Mime av den bilden. 
                        if (bilaga is MimePart mimePart && mimePart.ContentType.MimeType.StartsWith("image/"))
                        {
                            //Hämta Mimens extension
                            string extension = Path.GetExtension(mimePart.FileName);

                            //finns ingen extension så får den .png
                            if (string.IsNullOrWhiteSpace(extension))
                            {
                                extension = ".png";
                            }

                            //Tilldela det filla unika namnet inkluderat extenson
                            string filnamn = $"{Guid.NewGuid()}{extension}";

                            //Skapa den fulla sökvägen som sträng
                            string fullPath = Path.Combine(bildMapp, filnamn);

                            //Skapa hela sökvägen till bilden och öppna en ström dit
                            using (var stream = File.Create(fullPath))
                            {
                                //Här görs själva extraheringen av länken, länken hittas på webben i form av Binär fil, den översätts sedan och en kopia skapas vilket är vad vi använders sedan.
                                await mimePart.Content.DecodeToAsync(stream);
                            }
                            bildSökväg = fullPath;
                            break;
                        }
                    }
                    //För BILD}

                    //{För hela mailet
                    var mail = new Mail
                    {
                        mailId = meddelande.MessageId ?? "",
                        Avsändare = meddelande.From.ToString(),
                        Ämne = meddelande.Subject ?? "",
                        Innehåll = meddelande.TextBody ?? meddelande.HtmlBody ?? "",
                        BildSökVäg = bildSökväg
                    };

                    emailList.Add(mail);
                }

                await klient.DisconnectAsync(true);
                return emailList;
                //var emailList = new List<Mail>();

                //using var klient = new ImapClient();

                //await klient.ConnectAsync("imap.gmail.com", 993, true);
                //await klient.AuthenticateAsync("hattmakaren005@gmail.com", "wuambzrflbnvvfkk");//gmail och APP-lösen inte vanligt lösen
                //var inkorg = klient.Inbox;
                //await inkorg.OpenAsync(FolderAccess.ReadOnly);

                //int läsInAntal = 100;
                //int startIndex = Math.Max(0, inkorg.Count - läsInAntal);

                //for (int i = inkorg.Count - 1; i >= startIndex; i--)
                //{
                //    var meddelande = await inkorg.GetMessageAsync(i);
                //    var email = new Mail
                //    {
                //        mailId = meddelande.MessageId ?? "",
                //        Avsändare = meddelande.From.ToString(),
                //        Ämne = meddelande.Subject ?? "",
                //        Innehåll = meddelande.TextBody ?? "",
                //        Datum = meddelande.Date.DateTime
                //    };
                //    emailList.Add(email);
                //}
                //await klient.DisconnectAsync(true);
                //return emailList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}
