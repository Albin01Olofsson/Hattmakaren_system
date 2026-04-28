namespace Models
{
    public class SpecialBeställning : Produkt

    {
        public string BildURL { get; set; }


        //Behövde lägga till detta fält eftersom wpf inte accepterar relativa sökvägar i kombination med en listbox
        public string BildFullPath
        {
            get
            {
                return System.IO.Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName, "DAL", BildURL);

            }
        }

        public string Beskrivning { get; set; }
    }
}
