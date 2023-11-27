using TravelDatabase.Data.Handlers;
using TravelPlanner.TravelPlannerApp.Data.Handlers;

namespace TravelPlanner.UnitTests.TravelPlannerApp.Data {
	internal class FileHandlerTests
    {
        private static readonly string folderLocation = Directory.GetCurrentDirectory();
        private static readonly string fileName = "FileHandlerTest.txt";
        private static readonly string fullPath = Path.Combine(folderLocation, fileName);

        [SetUp]
        public void Setup_CreateNewFile()
        {
            FileHandler.WriteToFile("", fullPath);
        }

        [TearDown]
        public void TearDown_DeleteFile()
        {
            FileHandler.DeleteFile(fullPath);
        }

        [Test]
        public void WriteToFile_CreateNewFile_FileIsCreated()
        {
            Assert.That(File.Exists(fileName), Is.True);
        }

        [Test]
        public void WriteToFile_AddText_TextIsAdded()
        {
            FileHandler.WriteToFile("X", fullPath);
            string? textFromFile = FileHandler.ReadFromFile(fullPath);

            Assert.That(textFromFile, Does.Contain('X'));
        }
    }
}
