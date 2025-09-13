using System.Text.Json.Serialization;

namespace MobiShare.Models
{
    public class CartaPrepagata
    {
        [JsonPropertyName("codice")]
        public string Codice {  get; set; }
        [JsonPropertyName("valore")]
        public int Valore {  get; set; }
        [JsonPropertyName("scadenza")]
        public string Scadenza {  get; set; }
        [JsonPropertyName("valida")]
        public bool Valida {  get; set; }

        public CartaPrepagata() { }
        public CartaPrepagata(string codice, int valore, string scadenza, bool valida)
        {
            this.Codice = codice;
            this.Valore = valore;
            this.Scadenza = scadenza;
            this.Valida = valida;
        }
        public CartaPrepagata(int valore)
        {
            this.Valore = valore;
        }

        public CartaPrepagata(string codice)
        {
            this.Codice = codice;
        }
    }
}
