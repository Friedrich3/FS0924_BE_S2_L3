﻿namespace FS0924_BE_S2_L3.Models.ViewModel
{
    public class UtenteEditViewModel
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
        public string? Sala { get; set; }
        public bool IsReduced { get; set; }
    }
}
