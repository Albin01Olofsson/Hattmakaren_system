using BL.Interfaces;
using DAL.Intefaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class MailService : IMailService
    {
        private readonly IMailRepository _mailRepo;
        public MailService(IMailRepository mailRepo)
        {
            _mailRepo = mailRepo;
        }

        public async Task<List<Mail>> GetMailListAsync() => await _mailRepo.GetMailsAsync();
    }
}
