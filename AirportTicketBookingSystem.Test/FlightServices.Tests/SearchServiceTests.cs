using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Flights.DataModel;
using AirportTicketBookingSystem.Flights.FlightServices;
using FlightClassPrice = AirportTicketBookingSystem.Flights.DataModel.FlightClassPrice;

namespace AirportTicketBookingSystem.Test.FlightServices.Test
{
    public class FlightSearchServiceTests
    {
        [Fact]
        public void SearchFlights_ReturnsMatchingFlights()
        {
            // Arrange
            var flightData1 = new FlightData { FlightNumber = "ABC123", DepartureCountry = "USA", DestinationCountry = "UK", DepartureDate = DateTime.Parse("2024-05-20"), DepartureAirport = "JFK", ArrivalAirport = "LHR", Prices = new List<FlightClassPrice> { new FlightClassPrice { Class = FlightClass.Business, Price = 1000 } } };
            var flightData2 = new FlightData { FlightNumber = "XYZ456", DepartureCountry = "UK", DestinationCountry = "USA", DepartureDate = DateTime.Parse("2024-05-21"), DepartureAirport = "LHR", ArrivalAirport = "JFK", Prices = new List<FlightClassPrice> { new FlightClassPrice { Class = FlightClass.Economy, Price = 800 } } };
            var flightData3 = new FlightData { FlightNumber = "PQR789", DepartureCountry = "Canada", DestinationCountry = "USA", DepartureDate = DateTime.Parse("2024-05-20"), DepartureAirport = "YYZ", ArrivalAirport = "JFK", Prices = new List<FlightClassPrice> { new FlightClassPrice { Class = FlightClass.FirstClass, Price = 1200 } } };

            List<FlightData> flights = new List<FlightData> { flightData1, flightData2, flightData3 };
            var flightSearchService = new FlightSearchService();
            flightSearchService._flights = flights;

            // Act
            var searchResults = flightSearchService.SearchFlights("ABC123", "USA", "UK", DateTime.Parse("2024-05-20"), "JFK", "LHR", "Business", 1000);

            // Assert
            Assert.Single(searchResults); // Expecting only one flight to match the search criteria
            Assert.Equal("ABC123", searchResults[0].FlightNumber);
        }

        [Fact]
        public void SearchFlights_EmptySearch_ReturnsAllFlights()
        {
            // Arrange
            var flightData1 = new FlightData { FlightNumber = "ABC123", DepartureCountry = "USA", DestinationCountry = "UK", DepartureDate = DateTime.Parse("2024-05-20"), DepartureAirport = "JFK", ArrivalAirport = "LHR", Prices = new List<FlightClassPrice> { new FlightClassPrice { Class = FlightClass.Business, Price = 1000 } } };
            var flightData2 = new FlightData { FlightNumber = "XYZ456", DepartureCountry = "UK", DestinationCountry = "USA", DepartureDate = DateTime.Parse("2024-05-21"), DepartureAirport = "LHR", ArrivalAirport = "JFK", Prices = new List<FlightClassPrice> { new FlightClassPrice { Class = FlightClass.Economy, Price = 800 } } };
            var flightData3 = new FlightData { FlightNumber = "PQR789", DepartureCountry = "Canada", DestinationCountry = "USA", DepartureDate = DateTime.Parse("2024-05-20"), DepartureAirport = "YYZ", ArrivalAirport = "JFK", Prices = new List<FlightClassPrice> { new FlightClassPrice { Class = FlightClass.FirstClass, Price = 1200 } } };

            List<FlightData> flights = new List<FlightData> { flightData1, flightData2, flightData3 };
            var flightSearchService = new FlightSearchService();
            flightSearchService._flights = flights;
            // Act
            var searchResults = flightSearchService.SearchFlights(null, null, null, null, null, null, null, null);

            // Assert
            Assert.Equal(3, searchResults.Count);
        }
    }
}
