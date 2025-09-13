using System.Text.Json;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace MobiShare.Utils


{
    public class JwtUtil
    {

        private const string PublicKey = @"-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEApgy4NVRufgiuX9TJ+0rE
3QUVBNyQObq/i0fEWpLMj3m88gIJfqYcImVaIMS1tvtWPctu9Fg7tTcFSTnWutzV
ESEZiJxYdZ9gpEIwHObX2lUZem8llMYsT/pOoG3w3iF9Xw/fgji7XuYhtwm+RqfO
Vfg6//PpuyNtGBL7IcTFR3pCCh1jzESlkk6cw0BTOooEO7a1E/gFcWro4W55wRz+
bfXnp5MhR/jV8DdYiUG4Fr1N3WBfo3Af3nUxLMOk+QhQ9vu/1zKx4cc13/MgD4uE
+eAZVCAqplUGPhbDuxNNcaWZvpxSKsJBeHYW9sD7cueI1OlgPIh+lsfbHbYyLT86
swIDAQAB
-----END PUBLIC KEY-----";


        public static async Task<string?> ValidateAndExtractClaimAsync(string token, string claimName)
        {
            var tokenHandler = new JsonWebTokenHandler();
            var rsa = RSA.Create();
            rsa.ImportFromPem(PublicKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new RsaSecurityKey(rsa),
                ValidateIssuer = false,   // Non controllare da dove viene il token
                ValidateAudience = false,  // Non controllare per chi è il token
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                TokenValidationResult result = await tokenHandler.ValidateTokenAsync(token, validationParameters);

                if (result.IsValid)
                {
                    Console.WriteLine("Token verificato e valido!");
                    var claimValue = result.ClaimsIdentity.FindFirst(claimName)?.Value;

                    if (string.IsNullOrEmpty(claimValue))
                    {
                        Console.WriteLine($"Claim '{claimName}' non trovato nel token.");
                        return null;
                    }
                    return claimValue;
                }
                else
                {
                    Console.WriteLine($"Validazione fallita: {result.Exception.Message}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore critico durante la validazione: {ex.Message}");
                return null;
            }
        }
    }
}
