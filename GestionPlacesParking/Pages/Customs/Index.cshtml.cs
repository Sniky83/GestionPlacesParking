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
        private readonly IDayRepository _dayRepository;
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public new List<ParkingSlot> ParkingSlotList { get; set; }
        public new List<Reservation> ReservationList { get; set; }
        public new Day Day { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IParkingSlotRepository parkingRepository, IReservationRepository reservationRepository, IDayRepository dayRepository)
        {
            _logger = logger;
            _parkingRepository = parkingRepository;
            _reservationRepository = reservationRepository;
            _dayRepository = dayRepository;
        }

        public IActionResult OnGet()
        {
            IActionResult result = Page();

            if (ModelState.IsValid)
            {
                ParkingSlotList = _parkingRepository.GetAll();
                Day = _dayRepository.ExtractDaysWithDate();
                ReservationList = _reservationRepository.GetAllReserved();
            }

            return result;
        }
    }
}