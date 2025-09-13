
using System.Text.Json.Serialization;

namespace MobiShare.Models
{
    public class Utente
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("bloccato")]
        public bool Bloccato { get; set; }
        [JsonPropertyName("credito")]
        public double Credito { get; set; }
        [JsonPropertyName("punti")]
        public int Punti { get; set; }
        [JsonPropertyName("google")]
        public bool Google { get; set; }

        // Costruttore completo
        public Utente(string username, string email, string password, bool bloccato, double credito, int punti, bool google)
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.Bloccato = bloccato;
            this.Credito = credito;
            this.Punti = punti;
            this.Google = google;
        }

        // Costruttore senza password
        public Utente(string username, string email, bool bloccato, double credito, int punti, bool google)
        {
            this.Username = username;
            this.Email = email;
            this.Bloccato = bloccato;
            this.Credito = credito;
            this.Punti = punti;
            this.Google = google;
            this.Password = ""; // valore di default
        }

        public Utente(string username, string email, string password):
            this(username, email, password, false, 0.0, 0, false)
        {
        }

        public Utente(string username,string password) :
               this(username, "", password, false, 0.0, 0, false)
        {
        }

        // Costruttore vuoto (necessario per deserializzazione)
        public Utente() { }
    }
}
