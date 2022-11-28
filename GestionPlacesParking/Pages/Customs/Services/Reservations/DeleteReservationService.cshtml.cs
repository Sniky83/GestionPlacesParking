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

        public override BadRequestObjectResult BadRequest(object error)
        {
            return base.BadRequest(new { message = error });
        }

        public IActionResult OnPost([FromBody] DeleteOneReservationDto DeleteOneReservationDto)
        {
            IActionResult result = Page();

            string errorMessage = "Problème lors de la suppression de la réservation. Veuillez réessayer ultérieurement.";

            string? userId = HttpContext.Session.GetString("UserId");
            int? isAdmin = HttpContext.Session.GetInt32("IsAdmin");

            DeleteOneReservationDto.UserId = userId;
            DeleteOneReservationDto.IsAdmin = (isAdmin == 1) ? true : false;

            try
            {
                _repository.DeleteOne(DeleteOneReservationDto);
            }
            catch (Exception)
            {
                result = BadRequest(errorMessage);
            }

            return result;
        }
    }
}
