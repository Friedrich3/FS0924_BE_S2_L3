using FS0924_BE_S2_L3.Models;
using FS0924_BE_S2_L3.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FS0924_BE_S2_L3.Controllers
{
    public class UsersController : Controller
    {
    private static List<Sala> ListaSale = new List<Sala>()
    {
          new Sala() { IdSala= Guid.Parse("915e76d9-b785-456b-aac9-b8c8e2c5b9e5"), NomeSala = "NORD", ListaUtenti = new List<Utente>()},
          new Sala() { IdSala= Guid.Parse("9b5de0d6-caea-45c2-a11f-4df964ee968c"), NomeSala = "EST", ListaUtenti = new List<Utente>() },
          new Sala() { IdSala= Guid.Parse("82d9fca3-10aa-4ee9-9db4-c9b18084442b"), NomeSala = "SUD", ListaUtenti = new List<Utente>()},
    };  


        public IActionResult Index()
        {
            var cinema = new CinemaListViewModel()
            {
                Theatre = ListaSale
            };

            return View(cinema);
        }

        public IActionResult Add()
        {
            var cinema = new CinemaListViewModel()
            {
                Theatre = ListaSale
            };
            return View(cinema);
        }

        [HttpPost]
        public IActionResult Add(UtenteAddViewModel utenteAdd)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var newUtente = new Utente()
            {
                Id = Guid.NewGuid(),
                Cognome = utenteAdd.Cognome,
                Sala = utenteAdd.Sala,
                IsReduced = utenteAdd.IsReduced

            };
            
            var lista = ListaSale.FirstOrDefault(sala => sala.NomeSala == utenteAdd.Sala);
            lista.ListaUtenti.Add(newUtente);


            return RedirectToAction("Index");
        }
    }
}
