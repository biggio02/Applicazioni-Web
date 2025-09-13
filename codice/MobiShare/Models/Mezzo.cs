using System.Text.Json.Serialization;

namespace MobiShare.Models
{
    public class Mezzo
    {
        [JsonPropertyName("idMezzo")]
        public int IdMezzo { get; set; }
        [JsonPropertyName("tipoMezzo")]
        public string TipoMezzo { get; set; }
        [JsonPropertyName("prelevabile")]
        public int Prelevabile { get; set; }
        [JsonPropertyName("parcheggio")]
        public int Parcheggio { get; set; }
        [JsonPropertyName("batteria")]
        public int Batteria { get; set; }
        [JsonPropertyName("indirizzo")]
        public string Indirizzo { get; set; }

        public Mezzo(string tipo_mezzo, int prelevabile, int parcheggio, int batteria)
        {
            this.TipoMezzo = tipo_mezzo;
            this.Prelevabile = prelevabile;
            this.Parcheggio = parcheggio;
            this.Batteria = batteria;
        }

        public Mezzo(int idMezzo,string tipo_mezzo, int prelevabile, int parcheggio, int batteria)
        {
            this.IdMezzo = idMezzo;
            this.TipoMezzo = tipo_mezzo;
            this.Prelevabile = prelevabile;
            this.Parcheggio = parcheggio;
            this.Batteria = batteria;
        }

        public Mezzo(int idMezzo, string tipo_mezzo, int prelevabile, int parcheggio, int batteria,string indirizzo)
        {
            this.IdMezzo = idMezzo;
            this.TipoMezzo = tipo_mezzo;
            this.Prelevabile = prelevabile;
            this.Parcheggio = parcheggio;
            this.Batteria = batteria;
            this.Indirizzo = indirizzo;
        }

        public Mezzo() { }

    }
    
}
