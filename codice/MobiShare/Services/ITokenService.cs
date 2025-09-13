using MobiShare.Models;

namespace MobiShare.Services
{
    public interface ITokenService
    {
        public Task<Token> getToken(string username, string password);
        public Task<Token> getTokenGoogle(string code);
        //public Task<Token> getTokenGoogle(string id_token);
    }
}
