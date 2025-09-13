using System.Text.Json.Serialization;

namespace MobiShare.Models
{
    public class Costo
    {
        [JsonPropertyName("tipoMezzo")]
        public string TipoMezzo { get; set; }
        [JsonPropertyName("costoFisso")]
        public double CostoFisso { get; set; }
        [JsonPropertyName("costoVariabile")]
        public double CostoVariabile { get; set; }

        public Costo(double costoFisso, double costoVariabile)
        {
            this.CostoFisso = costoFisso;
            this.CostoVariabile = costoVariabile;
        }
        public Costo() { }
    }


}
