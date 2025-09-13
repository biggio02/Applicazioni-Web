using MobiShare.Models;

namespace MobiShare.Services
{
    public interface ICartaPrepagataService
    {
        public Task<CartaPrepagata> postCartaPrepagata(int valore, string token);
    }
}
