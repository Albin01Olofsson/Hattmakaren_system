using BL.Interfaces;
using System.Text.Json;

namespace WpfApp1.Services
{
    // Modell som matchar JSON-API
    public class TullRegel
    {
        public string LandKod { get; set; }
        public string LandNamn { get; set; }
        public decimal TullProcent { get; set; }
        public decimal TullAvgiftFast { get; set; }
    }

    public class TullService : ITullService
    {
        // Motorn för att hämta data från internet
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<decimal> BeraknaTullViaAPI(decimal orderSumma, string valtLand)
        {

            // Frihandel inom EU. Vi behöver inte belasta nätverket.
            string[] euLänder = { "Sverige", "Danmark", "Finland", "Tyskland", "Frankrike" };
            if (euLänder.Contains(valtLand))
            {
                return 0;
            }

            // Vår Länk
            string url = "https://mocki.io/v1/047387f9-1452-45ce-9cb0-1b690121e0dc";

            try
            {
                //  Ladda ner listan 
                string jsonSvar = await _httpClient.GetStringAsync(url);

                // Så den inte är case sensitive

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                // Deserilarisering
                var allaRegler = JsonSerializer.Deserialize<List<TullRegel>>(jsonSvar, options);

                //  Sök i listan efter exakt det land kunden valde
                var landetsRegel = allaRegler.FirstOrDefault(r => r.LandNamn == valtLand);

                if (landetsRegel != null)
                {
                    // 4. Gör matematiken om landet fanns i API:et
                    return (orderSumma * landetsRegel.TullProcent) + landetsRegel.TullAvgiftFast;
                }
                else
                {
                    // 5.Standardvärde ifall landet inte finns med i listan 
                    return (orderSumma * 0.15m) + 100.00m;
                }
            }
            catch (Exception)
            {
                // Ifall ORUs internet sviker 
                return (orderSumma * 0.15m) + 100.00m;
            }
        }
    }
}
