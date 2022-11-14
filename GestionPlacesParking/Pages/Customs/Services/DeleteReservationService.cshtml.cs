using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Pages.Customs.Services
{
    public class DeleteReservationServiceModel : PageModel
    {
        private readonly IReservationRepository _repository;

        public DeleteReservationServiceModel(IReservationRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnPost([FromBody] DeleteOneReservationDto DeleteOneReservationDto)
        {
            IActionResult result = Page();

            string errorMessage = "Problème lors de la suppression de la réservation. Veuillez réessayer ultérieurement.";

            try
            {
                int? userId = HttpContext.Session.GetInt32("UserId");
                int? isAdmin = HttpContext.Session.GetInt32("IsAdmin");

                DeleteOneReservationDto.UserId = userId;
                DeleteOneReservationDto.IsAdmin = (isAdmin == 1) ? true : false;

                int rowAffected = _repository.DeleteReservation(DeleteOneReservationDto);

                if (rowAffected < 1)
                {
                    result = BadRequest(new { message = errorMessage });
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
