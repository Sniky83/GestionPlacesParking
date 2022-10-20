using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Pages.Customs.Services
{
    public class MakeReservationServiceModel : PageModel
    {
        private readonly IReservationRepository _repository;

        //[BindProperty]
        //public new Reservation Reservation { get; set; }
        //public new string ErrorMessage { get; set; }

        public MakeReservationServiceModel(IReservationRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet(ReservationDto reservation)
        {
            IActionResult result = Page();

            int rowsAffected = 1;// _repository.MakeReservation(reservation);

            if (rowsAffected < 1)
            {
                result = BadRequest("Problème lors de l'ajout de la réservation. Veuillez remplir le champ Nom.");
            }

            return result;
        }

        public JsonResult OnPost([FromBody] string ReservingNames)
        {
            return new JsonResult(new List<Reservation>() { new() { ReservingName = "Test" } });
        }
    }
}
