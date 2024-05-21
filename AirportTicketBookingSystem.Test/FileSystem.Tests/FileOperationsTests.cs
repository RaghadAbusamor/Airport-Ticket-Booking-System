using AirportTicketBookingSystem.FileSystem;
using FluentAssertions;

namespace AirportTicketBookingSystem.Test.FileSystem.Tests
{
    public class FileOperationsTests
    {

        [Fact]
        public async Task ReadFromCsvAsync_EmptyFile_ShouldReturnEmptyList()
        {
            // Arrange
            var tempFilePath = Path.GetTempFileName();

            // Act
            var result = await FileOperations.ReadFromCSVAsync<TestData>(tempFilePath);

            // Assert
            result.Should().BeEmpty();
            File.Delete(tempFilePath);
        }

        [Fact]
        public async Task ReadFromCsvAsync_NonExistentFile_ShouldThrowException()
        {
            // Arrange
            var nonExistentFilePath = Path.Combine(Path.GetTempPath(), "nonexistent_file.csv");

            // Act & Assert
            await Assert.ThrowsAsync<FileNotFoundException>(async () =>
                await FileOperations.ReadFromCSVAsync<TestData>(nonExistentFilePath));
        }

        [Fact]
        public async Task ReadFromCsvAsync_ValidData_ShouldReturnCorrectData()
        {
            // Arrange
            var tempFilePath = Path.GetTempFileName();
            await File.WriteAllLinesAsync(tempFilePath, new[] { "Id,Name", "1,Test 1", "2,Test 2" });

            // Act
            var result = await FileOperations.ReadFromCSVAsync<TestData>(tempFilePath);

            // Assert
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
            // Arrange
            var nonExistentDirectoryPath = Path.Combine(Path.GetTempPath(), "nonexistent_directory");
            var nonExistentFilePath = Path.Combine(nonExistentDirectoryPath, "nonexistent_file.csv");
            var data = new List<TestData>();

            // Act & Assert
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
