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

        public IActionResult OnPost([FromBody] int ReservationId)
        {
            IActionResult result = Page();

            if (ModelState.IsValid)
            {
                int? userId = HttpContext.Session.GetInt32("UserId");
                int? isAdmin = HttpContext.Session.GetInt32("IsAdmin");

                DeleteOneReservationDto deleteOneReservationDto = new DeleteOneReservationDto();

                deleteOneReservationDto.ReservationId = ReservationId;
                deleteOneReservationDto.UserId = userId;
                deleteOneReservationDto.IsAdmin = (isAdmin == 1) ? true : false;

                int rowAffected = _repository.DeleteReservation(deleteOneReservationDto);

                if (rowAffected < 1)
                {
                    result = BadRequest(new { message = "Problème lors de la suppression de la réservation. Veuillez réessayer ultérieurement." });
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
