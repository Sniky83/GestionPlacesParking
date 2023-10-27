using GestionPlacesParking.Core.Global.Consts;
using GestionPlacesParking.Core.Global.EnvironmentVariables.Envs;
using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Pages.Customs.Services
{
    public class DeleteReservationServiceModel : PageModel
    {
        private readonly IReservationRepository _repository;
        private readonly ILogger<DeleteReservationServiceModel> _logger;

        public DeleteReservationServiceModel(IReservationRepository repository, ILogger<DeleteReservationServiceModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public override BadRequestObjectResult BadRequest(object error)
        {
            return base.BadRequest(new { message = error });
        }

        public IActionResult OnPost([FromBody] DeleteOneReservationDto DeleteOneReservationDto)
        {
            IActionResult result = Page();

            string errorMessage = "Problème lors de la suppression de la réservation. Veuillez réessayer ultérieurement.";

            int? isAdmin = HttpContext.Session.GetInt32(SessionConst.IsAdmin);

            if (IsSsoEnv.IsSso)
            {
                string? proprietaireId = HttpContext.Session.GetString(SessionConst.UserId);
                DeleteOneReservationDto.ProprietaireId = proprietaireId;
            }
            else
            {
                int? userId = HttpContext.Session.GetInt32(SessionConst.UserId);
                DeleteOneReservationDto.UserId = userId;
            }

            DeleteOneReservationDto.IsAdmin = (isAdmin == 1);

            try
            {
                _repository.DeleteOne(DeleteOneReservationDto);
            }
            catch (Exception ex)
            {
                result = BadRequest(errorMessage);
                _logger.LogError("Erreur lors de la supression d'une réservation.\nUserId : {userId}\n{ex}", HttpContext.Session.GetString(SessionConst.UserId), ex);
            }

            return result;
        }
    }
}
