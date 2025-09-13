using MobiShare.Models;

namespace MobiShare.Services
{
    public interface IAccountService
    {
        public Task<Utente> postAccount(string email,string username, string password);
        public Task<Utente> getAccount(string username, string token);
        //public Task<Ricarica> postRicarica(string username, int idUtente, float importo, string data, string cartaDiCredito, string codice);
        public Task<List<CronologiaCorse>> getCorse(string username,string par ,string token);
        public Task<List<Utente>> getAllAccount(string token,string parametri);
        public Task<Messaggio> patchUtente(string username, string token);
        public Task<Messaggio> putUtente(string token, string password);
        public Task<Ricarica> postRicaricaPrepagata(string token ,string codice);
        public Task<Ricarica> postRicaricaCarta(float importoCarta, string numeroCarta, string scadenza, string cvv,string token);
        Task<List<Ricarica>> getRicariche(string? username,string par,string token);
    }
}
