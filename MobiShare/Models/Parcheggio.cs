using System.Text.Json.Serialization;

namespace MobiShare.Models
{
    public class Parcheggio
    {
        [JsonPropertyName("idParcheggio")]
        public int IdParcheggio {  get; set; }
        [JsonPropertyName("indirizzo")]
        public string Indirizzo { get; set; }
        [JsonPropertyName("numeroMezzi")]
        public int NumeroMezzi {  get; set; }

        public Parcheggio(string indirizzo)
        {
            this.Indirizzo = indirizzo;
        }
        public Parcheggio(int idParcheggio, string indirizzo)
        {
            this.IdParcheggio = idParcheggio;
            this.Indirizzo = indirizzo;
        }

        public Parcheggio(int idParcheggio, string indirizzo,int NumeroMezzi)
        {
            this.IdParcheggio = idParcheggio;
            this.Indirizzo = indirizzo;
            this.NumeroMezzi = NumeroMezzi;
        }

        public Parcheggio() { }
    }
}
