using AirportTicketBookingSystem.FileSystem;
using FluentAssertions;

namespace AirportTicketBookingSystem.Test
{
    public class FileOperationsTests
    {

        [Fact]
        public async Task ReadFromCsvAsync_EmptyFile_ShouldReturnEmptyList()
        {
            var tempFilePath = Path.GetTempFileName();
            var result = await FileOperations.ReadFromCSVAsync<TestData>(tempFilePath);
            result.Should().BeEmpty();
            File.Delete(tempFilePath);
        }

        [Fact]
        public async Task ReadFromCsvAsync_NonExistentFile_ShouldThrowException()
        {
            var tempFilePath = Path.GetTempFileName();
            var nonExistentFilePath = Path.Combine(Path.GetTempPath(), "nonexistent_file.csv");
            await Assert.ThrowsAsync<FileNotFoundException>(async () =>
            await FileOperations.ReadFromCSVAsync<TestData>(nonExistentFilePath));
        }

        [Fact]
        public async Task ReadFromCsvAsync_ValidData_ShouldReturnCorrectData()
        {
            var tempFilePath = Path.GetTempFileName();
            await File.WriteAllLinesAsync(tempFilePath,
                new[] { "Id,Name", "1,Test 1", "2,Test 2" });
            var result = await FileOperations.ReadFromCSVAsync<TestData>(tempFilePath);

            var expected = new List<TestData>
            {
                new TestData { Id = 1, Name = "Test 1" },
                new TestData { Id = 2, Name = "Test 2" }
            };
            result.Should().BeEquivalentTo(expected);
            File.Delete(tempFilePath);
        }


        [Fact]
        public async Task WriteToCsvAsync_NonExistentDirectory_ShouldThrowException()
        {
            var nonExistentDirectoryPath = Path.Combine(Path.GetTempPath(), "nonexistent_directory");
            var nonExistentFilePath = Path.Combine(nonExistentDirectoryPath, "nonexistent_file.csv");
            var data = new List<TestData>();

            await Assert.ThrowsAsync<DirectoryNotFoundException>(async () =>
                await FileOperations.WriteToCSVAsync(nonExistentFilePath, data));
        }
    }

    public class TestData
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
