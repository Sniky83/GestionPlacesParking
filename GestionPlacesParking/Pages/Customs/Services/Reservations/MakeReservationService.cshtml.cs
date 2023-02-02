using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Pages.Customs.Services
{
    public class MakeReservationServiceModel : PageModel
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ILogger<MakeReservationServiceModel> _logger;

        public MakeReservationServiceModel(IReservationRepository reservationRepository, ILogger<MakeReservationServiceModel> logger)
        {
            _reservationRepository = reservationRepository;
            _logger = logger;
        }

        public override BadRequestObjectResult BadRequest(object error)
        {
            return base.BadRequest(new { message = error });
        }

        public IActionResult OnPost([FromBody] Reservation Reservation)
        {
            IActionResult result = Page();
            string errorMessage = "Problème lors de l'ajout de la réservation. Veuillez réessayer ultérieurement.";

            if (ModelState.IsValid)
            {
                try
                {
                    _reservationRepository.AddOne(Reservation);
                }
                catch (Exception ex)
                {
                    result = BadRequest(errorMessage);
                    _logger.LogError("Erreur lors de l'ajout d'une réservation.\nUserId : {userId}\n{ex}", Reservation.ProprietaireId, ex);
                }
            }
            else
            {
                result = BadRequest(ModelState.Values.First().Errors.First().ErrorMessage);
            }

            return result;
        }
    }
}
