using TravelPlanner.TravelPlannerApp.DataType;

namespace TravelPlanner.UnitTests.TravelPlannerApp.DataType
{
    internal class CoordinateTests
    {
        [Test]
        public void Constructor_NewCoordinate_IsCreated()
        {
            Coordinate cord = new(1.0d, 2.0d);
            Assert.Multiple(() =>
            {
                Assert.That(cord.Longitude, Is.EqualTo(1.0d));
                Assert.That(cord.Latitude, Is.EqualTo(2.0d));
            });
        }

        [Test]
        public void SubtractionOperation_SubtractCoordinates_DistanceBetween()
        {
            Coordinate cord1 = new(1.0d, 0.0d);
            Coordinate cord2 = new(-1.0d, 0.0d);

            double distance = cord1 - cord2;

            Assert.That(distance, Is.EqualTo(2));
            Assert.That(distance, Is.Not.EqualTo(1));
        }
    }
}
