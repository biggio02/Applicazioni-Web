using Microsoft.AspNetCore.Mvc.Cors;

namespace MobiShare.Models
{
    public class CronologiaCorse
    {
        public Corsa Corsa { get; set; }
        public Utente Utente { get; set; }
        public Mezzo Mezzo { get; set; }

        public CronologiaCorse(Corsa corsa, Utente utente, Mezzo mezzo)
        {
            this.Corsa = corsa;
            this.Utente = utente;
            this.Mezzo = mezzo;
        }

        public CronologiaCorse() { }

    }

}
