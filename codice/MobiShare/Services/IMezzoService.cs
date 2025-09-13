using MobiShare.Models;

namespace MobiShare.Services
{
    public interface IMezzoService
    {
        public Task<Mezzo> postMezzo(string token,string tipo_mezzo, int parcheggio,int prelevabile,int batteria);
        public Task<List<Mezzo>> getMezzi(string token,string parametri);
        public Task<Mezzo> deleteMezzo(string token,string paramentri);
        public Task<Mezzo> patchMezzo(string token, string paramentri);
    }
}
