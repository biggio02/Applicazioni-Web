using System.Text.Json.Serialization;

namespace MobiShare.Models
{
    public class Ricarica
    {
        [JsonPropertyName("utente")]
        public int IdUtente { get; set; }
        [JsonPropertyName("data")]
        public string Data { get; set; }
        [JsonPropertyName("importo")]
        public float Importo { get; set; }
        [JsonPropertyName("cartaDiCredito")]
        public string CartaDiCredito{ get; set; }
        [JsonPropertyName("cartaPrepagata")]
        public string CartaPrepagata { get; set; }

        public Ricarica(int id_utente,string data,float importo,string carta_di_credito,string codice)
        {
            this.CartaPrepagata = CartaPrepagata;
            this.IdUtente = id_utente;
            this.Data = data;
            this.Importo = importo;
            this.CartaDiCredito = carta_di_credito;
        }

        public Ricarica() { }


    }
}
