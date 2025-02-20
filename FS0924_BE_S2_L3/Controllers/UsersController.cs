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
            var user = new UtenteAddViewModel();

            //ViewBag.Utente = new UtenteAddViewModel();
            ViewBag.Cinema = new CinemaListViewModel()
            {
                Theatre = ListaSale
            };
            return View(user);
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
                Nome = utenteAdd.Nome,
                Cognome = utenteAdd.Cognome,
                Sala = utenteAdd.Sala,
                IsReduced = utenteAdd.IsReduced

            };

            var lista = ListaSale.FirstOrDefault(sala => sala.IdSala.ToString() == utenteAdd.Sala);
            lista.Posti -= 1;
            lista.ListaUtenti.Add(newUtente);


            return RedirectToAction("Index");
        }

        [HttpGet("User/Delete/{sala:guid}/{Id:guid}")]
        public IActionResult Delete(Guid id, Guid sala)
        {                  
            //FACCIO UNA RICERCA PER LISTA E IN QUELLA LISTA RICERCO L'UTENTE
            var UserList = ListaSale.FirstOrDefault(lista => lista.IdSala == sala) as Sala;
            var user = UserList!.ListaUtenti.FirstOrDefault(u => u.Id == id);

            //ESEGUIRE SEMPRE UN CONTROLLO SE E' NULL QUANDO SI CERCA QUALCOSA
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var userRemoved = UserList.ListaUtenti.Remove(user);
            if (userRemoved)
            {
                UserList.Posti += 1;
            }
        
            return RedirectToAction("Index");
        }

        [HttpGet("User/Edit/{sala:guid}/{Id:guid}")]
        public IActionResult Edit(Guid sala, Guid id)
        {
            var UserList = ListaSale.FirstOrDefault(lista => lista.IdSala == sala) as Sala;
            var user = UserList!.ListaUtenti.FirstOrDefault(u => u.Id == id);

            //ESEGUIRE SEMPRE UN CONTROLLO DOPO AVER CERCATO QUALCOSAPER EVITARE CHE SIA NULL
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var editedUser = new UtenteEditViewModel()
            {
                Id = user.Id,
                Nome = user.Nome,
                Cognome = user.Cognome,
                Sala = UserList.NomeSala,
                IsReduced = user.IsReduced,
            };
            //Inserisco la ViewBag per le Categorie della Select
            ViewBag.Cinema = new CinemaListViewModel()
            {
                Theatre = ListaSale
            };
            ViewBag.Sala = UserList.IdSala;
            return View(editedUser);
        }

        [HttpPost("User/Edit/Save/{sala:guid}/{Id:guid}")]
        public IActionResult EditSave(Guid sala, Guid id, UtenteEditViewModel editModel) 
        {
            var UserList = ListaSale.FirstOrDefault(lista => lista.IdSala == sala) as Sala;
            var user = UserList!.ListaUtenti.FirstOrDefault(u => u.Id == id);
            //ESEGUIRE SEMPRE UN CONTROLLO DOPO AVER CERCATO QUALCOSAPER EVITARE CHE SIA NULL
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            user.Nome = editModel.Nome;
            user.Cognome = editModel.Cognome;
            user.Sala = editModel.Sala;
            user.IsReduced = editModel.IsReduced;

            return RedirectToAction("Index");
        }
    }
}
