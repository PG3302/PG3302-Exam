﻿using TravelDatabase.Data.Handlers;
using TravelDatabase.Data.Log;

namespace TravelPlanner.UnitTests.TravelPlannerApp.Data
{
    internal class LoggerTests
    {
        private static readonly string folderLocation = Directory.GetCurrentDirectory();
        private static readonly string fileName = "LoggerTests.txt";
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
        public void LogError_CauseException_ErrorMessageShowsInFile()
        {
            int[] oneItemArray = new int[1];
            string? textFromFile;

            try
            {
                _ = oneItemArray[100];
            }
            catch (Exception error)
            {
                Logger.LogError("Test123", error, fullPath);
            }
            finally
            {
                textFromFile = FileHandler.ReadFromFile(fullPath);

                Assert.Multiple(() =>
                {
                    Assert.That(textFromFile, Does.Contain("[Error]"));
                    Assert.That(textFromFile, Does.Contain("outside the bounds"));
                    Assert.That(textFromFile, Does.Contain("Test123"));
                    Assert.That(textFromFile, Does.Not.Contain("Fish"));
                });
            }
        }

        [Test]
        public void LogInfo_AddInfoToFile_InfoMessageShowsInFile()
        {
            string? textFromFile;

            Logger.LogInfo("Info987", fullPath);

            textFromFile = FileHandler.ReadFromFile(fullPath);

            Assert.Multiple(() =>
            {
                Assert.That(textFromFile, Does.Contain("[Info]"));
                Assert.That(textFromFile, Does.Contain("Info987"));
                Assert.That(textFromFile, Does.Not.Contain("Fish"));
            });
        }
    }
}
