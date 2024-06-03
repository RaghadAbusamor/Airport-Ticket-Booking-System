namespace AirportTicketBookingSystem.FileSystem
{
    public interface IFileOperations
    {
        Task<List<T>> ReadFromCSVAsync<T>(string filePath);
        Task WriteToCSVAsync<T>(string filePath, T data);
    }
}
