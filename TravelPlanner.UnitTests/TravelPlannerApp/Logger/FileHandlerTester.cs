using System.Text;
using TravelPlanner.TravelPlannerApp.Logger;

namespace TravelPlanner.UnitTests.TravelPlannerApp.Logger
{
    internal class FileHandlerTester
    {
        private static readonly string folderLocation = Directory.GetCurrentDirectory();
        private static readonly string fileName = "FileHandlerTest.txt";
        private static readonly string fullPath = Path.Combine(folderLocation, fileName);

        [SetUp]
        public void Setup_CreateNewFile()
        {
            FileHandler.WriteToFile("", fullPath.ToString(), false);
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
            FileHandler.WriteToFile("X", fullPath.ToString(), false);
            string? textFromFile = FileHandler.ReadFromFile(fullPath);

            Assert.That(textFromFile, Does.Contain('X'));
        }
    }
}
