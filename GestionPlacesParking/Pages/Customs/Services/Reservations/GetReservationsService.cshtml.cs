using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Pages.Customs.Services
{
    public class GetReservationsService : PageModel
    {
        private readonly IReservationRepository _reservationRepository;

        public GetReservationsService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public JsonResult OnGet()
        {
            List<Reservation> nbReservations = _reservationRepository.GetAll(true);
            return new JsonResult(new { nbReservations = nbReservations.Count });
        }
    }
}
