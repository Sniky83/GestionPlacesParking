using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.Locals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Pages.Customs
{
    public class IndexModel : PageModel
    {
        private readonly IParkingSlotRepository _parkingRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public new List<ParkingSlot> ParkingSlotList { get; set; }
        public new List<Reservation> ReservationList { get; set; }
        public new Day Day { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IParkingSlotRepository parkingRepository, IReservationRepository reservationRepository)
        {
            _logger = logger;
            _parkingRepository = parkingRepository;
            _reservationRepository = reservationRepository;
        }

        public IActionResult OnGet()
        {
            IActionResult result = Page();

            if (ModelState.IsValid)
            {
                ParkingSlotList = _parkingRepository.GetAll();
                ReservationList = _reservationRepository.GetAllReserved();
                //TODO: Faire un _dayRepository
                Day = new Day();
            }

            return result;
        }
    }
}