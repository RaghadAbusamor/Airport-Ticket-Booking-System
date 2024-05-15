using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.FileSystem
{
    public interface IFileOperations
    {
        Task<List<T>> ReadFromCSVAsync<T>(string filePath);
        Task WriteToCSVAsync<T>(string filePath, List<T> data);
    }
}
