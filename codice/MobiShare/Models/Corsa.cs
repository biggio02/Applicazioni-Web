using System.Text.Json.Serialization;

namespace MobiShare.Models
{
    public class Corsa
    {
        [JsonPropertyName("idCorsa")]
        public int IdCorsa { get; set; }
        [JsonPropertyName("dataInizio")]
        public DateTime? DataInizio { get; set; }
        [JsonPropertyName("dataFine")]
        public DateTime? DataFine { get; set; }
        [JsonPropertyName("utente")]
        public int Utente { get; set; }
        [JsonPropertyName("mezzo")]
        public int Mezzo { get; set; }
        [JsonPropertyName("costoCorsa")]
        public double CostoCorsa { get; set; }
        [JsonPropertyName("feedback")]
        public string Feedback { get; set; }

        public Corsa(DateTime dataInizio, DateTime dataFine,int utente,int mezzo,double costoCorsa)
        {
            this.DataInizio = dataInizio;
            this.DataFine = dataFine;
            this.Utente = utente;
            this.Mezzo = mezzo;
            this.CostoCorsa = costoCorsa;
        }

        public Corsa(DateTime dataInizio, DateTime dataFine, int utente, int mezzo,string feedback ,double costoCorsa)
        {
            this.DataInizio = dataInizio;
            this.DataFine = dataFine;
            this.Utente = utente;
            this.Mezzo = mezzo;
            this.CostoCorsa = costoCorsa;
            this.Feedback = feedback;
        }


        public Corsa(int idCorsa, DateTime dataInizio, DateTime dataFine, int utente, int mezzo, double costoCorsa)
        {
            this.IdCorsa = idCorsa;
            this.DataInizio = dataInizio;
            this.DataFine = dataFine;
            this.Utente = utente;
            this.Mezzo = mezzo;
            this.CostoCorsa = costoCorsa;
        }
        public Corsa() { }
    }
}
