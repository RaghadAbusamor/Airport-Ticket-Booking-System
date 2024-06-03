using AirportTicketBookingSystem.Flights.FlightServices;

namespace AirportTicketBookingSystem.Flights
{
    public class FlightManagement
    {
        private readonly FlightManagementService _managementService;
        private readonly FlightSearchService _searchService;
        private readonly FlightEditService _editService;
        private readonly FlightCancelService _cancelService;
        private readonly FlightFilterService _filterService;



        public FlightManagement()
        {
            _managementService = new FlightManagementService();
            _searchService = new FlightSearchService();
            _editService = new FlightEditService();
            _cancelService = new FlightCancelService();
            _filterService = new FlightFilterService();


        }

        public async Task BatchFlightUploadAsync()
        {
            await _managementService.BatchFlightUploadAsync();
        }

        public async Task BookFlightAsync(int passengerId)
        {
            await _managementService.BookFlightAsync(passengerId);
        }

        public async Task CancelBookingAsync()
        {
            await _cancelService.CancelBookingAsync();
        }

        public async void EditBooking()
        {
            await _editService.EditBookingAsync();
        }

        public void Exit()
        {
            _managementService.Exit();
        }

        public async void FilterBookings()
        {
            await _filterService.FilterBookingsAsync();
        }

        public void ListAllFlights(int PassengerId)
        {
            _managementService.ListAllFlights(PassengerId);
        }

        public void ViewPersonalBookings(int PassengerId)
        {
            _managementService.ViewPersonalBookings(PassengerId);
        }

        public void Search()
        {
            _searchService.SearchBookingAsync();
        }
    }
}
