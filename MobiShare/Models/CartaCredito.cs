using System.Text.Json.Serialization;

namespace MobiShare.Models
{
    public class CartaCredito
    {
        [JsonPropertyName("numero")]
        public string Numero { get; set; }
        [JsonPropertyName("cvc")]
        public string Cvc { get; set; }
        [JsonPropertyName("scadenza")]
        public string Scadenza { get; set; }
        [JsonPropertyName("importo")]
        public float Importo { get; set; }
        public CartaCredito(string Numero, string Cvc, string Scadenza, float Importo)
        {
            this.Numero = Numero;
            this.Cvc = Cvc;
            this.Scadenza = Scadenza;
            this.Importo = Importo;
        }

        public CartaCredito()
        {
        }
    }
}
