using GestionPlacesParking.Core.Global.Consts;
using GestionPlacesParking.Core.Global.EnvironmentVariables.Envs;
using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Pages.Customs.Services
{
    public class MakeReservationServiceModel : PageModel
    {
        private readonly IReservationRepository _reservationRepository;

        public MakeReservationServiceModel(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
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
                    //Si c'est une résa depuis Keycloak on prend comme référence la Session
                    //Dans le cas échéant c'est la data de l'user via la modale
                    if (IsSsoEnv.IsSso)
                    {
                        Reservation.ReservingName = HttpContext.Session.GetString(SessionConst.FullName);
                    }

                    _reservationRepository.AddOne(Reservation);
                }
                catch (Exception)
                {
                    result = BadRequest(errorMessage);
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
