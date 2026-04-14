using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace WpfApp1
{
    public static class Session// Vi gör denna klass statisk så att vi kan komma åt den från vilken del av applikationen som helst utan att behöva skapa en instans av den
    {
        public static Användare CurrentUser { get; set; }// Denna egenskap kommer att hålla den inloggade användaren under sessionen
    }
}
