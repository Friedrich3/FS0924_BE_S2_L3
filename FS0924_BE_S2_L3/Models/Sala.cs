namespace FS0924_BE_S2_L3.Models
{
    public class Sala
    {
        public Guid IdSala { get; set; } 
        public string NomeSala { get; set; }
        public List<Utente> ListaUtenti { get; set; }
    }
}
