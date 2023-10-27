using GestionPlacesParking.Core.Interfaces.Repositories;
using GestionPlacesParking.Core.Models;
using GestionPlacesParking.Core.Models.Locals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionPlacesParking.Web.UI.Pages.Customs
{
    public class IndexModel : PageModel
    {
        private readonly IParkingSlotRepository _parkingSlotRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IDayLocalRepository _dayRepository;
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public List<ParkingSlot> ParkingSlotList { get; set; }
        public List<Reservation> ReservationList { get; set; }
        public DayLocal Day { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IParkingSlotRepository parkingSlotRepository, IReservationRepository reservationRepository, IDayLocalRepository dayRepository)
        {
            _logger = logger;
            _parkingSlotRepository = parkingSlotRepository;
            _reservationRepository = reservationRepository;
            _dayRepository = dayRepository;
        }

        public IActionResult OnGet()
        {
            IActionResult result = Page();

            if (ModelState.IsValid)
            {
                try
                {
                    ParkingSlotList = _parkingSlotRepository.GetAll();
                    ReservationList = _reservationRepository.GetAll();
                    Day = _dayRepository.GetDaysWithDate();
                }
                catch (Exception ex)
                {
                    //Erreur interne (Day repository)
                    result = StatusCode(StatusCodes.Status500InternalServerError);
                    _logger.LogCritical($"Erreur interne avec le Day repository.\n{ex}");
                }
            }

            return result;
        }
    }
}