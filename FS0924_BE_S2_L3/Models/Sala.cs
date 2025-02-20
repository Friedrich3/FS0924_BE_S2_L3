namespace FS0924_BE_S2_L3.Models
{
    public class Sala
    {
        public Guid IdSala { get; set; } 
        public string NomeSala { get; set; }
        public List<Utente> ListaUtenti { get; set; } = new List<Utente>();

        //TODO cambiare dopo in massimale diverso
        public int Posti { get; set; } = 5;
        public bool isOpen { get; set; } = true;
    }
}
