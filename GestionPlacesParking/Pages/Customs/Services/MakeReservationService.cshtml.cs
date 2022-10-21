using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Pages.Customs.Services
{
    public class MakeReservationServiceModel : PageModel
    {
        private readonly IReservationRepository _repository;

        public MakeReservationServiceModel(IReservationRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnPost([FromBody] Reservation Reservation)
        {
            IActionResult result = Page();

            if(ModelState.IsValid)
            {
                int rowsAffected = _repository.MakeReservation(Reservation);

                if (rowsAffected < 1)
                {
                    result = BadRequest(new { message = "Problème lors de l'ajout de la réservation. Veuillez remplir le champ Nom." });
                }
            }
            else
            {
                result = BadRequest(new { message = ModelState.Values.First().Errors.First().ErrorMessage });
            }

            return result;
        }
    }
}
