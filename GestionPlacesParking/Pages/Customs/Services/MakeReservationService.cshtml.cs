using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Pages.Customs.Services
{
    public class MakeReservationServiceModel : PageModel
    {
        private readonly IReservationRepository _reservationRepository;

        public MakeReservationServiceModel(IReservationRepository reservationRepository, IDayRepository dayRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public IActionResult OnPost([FromBody] Reservation Reservation)
        {
            IActionResult result = Page();
            string errorMessage = "Problème lors de l'ajout de la réservation. Veuillez réessayer ultérieurement.";

            try
            {
                if (ModelState.IsValid)
                {
                    int rowsAffected = _reservationRepository.MakeReservation(Reservation);

                    if (rowsAffected < 1)
                    {
                        result = BadRequest(new { message = errorMessage });
                    }
                }
                else
                {
                    result = BadRequest(new { message = ModelState.Values.First().Errors.First().ErrorMessage });
                }
            }
            catch (Exception)
            {
                result = BadRequest(new { message = errorMessage });
            }

            return result;
        }
    }
}
