using CommunityToolkit.Mvvm.ComponentModel;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;
using System.Windows;
using System.Diagnostics;

namespace WpfApp1.ViewModels
{
    public partial class FörfrågningVM : ObservableObject
    {
        private readonly IMailService _MailService;

        [ObservableProperty]

        private ObservableCollection<Mail> mails = new();

        public FörfrågningVM(IMailService mailService)
        {
            _MailService = mailService;
            Mails = new ObservableCollection<Mail>();
            LoadMails();
        }

        public async Task LoadMails()
        {
            Mails.Clear();

            try
            {
                foreach (Mail mail in await _MailService.GetMailListAsync())
                {
                    Mails.Add(mail);
                }
            }catch(Exception e)
            {
                MessageBox.Show("Ett fel inträffade vid inläsningen av förfrågningar!", $"Vi ber om ursäkt {Session.CurrentUser.Namn}", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }
    }
}
