namespace GeometryLib.Tests
{
    public class Point3DTests
    {
        [Theory]
        [MemberData(nameof(DistanceData))]
        public void Can_calculate_distance(Point3D a, Point3D b, double expected)
        {
            double actual = a.DistanceTo(b);
            Assert.Equal(expected, actual, 10);
        }

        public static TheoryData<Point3D, Point3D, double> DistanceData()
        {
            return new TheoryData<Point3D, Point3D, double>
            {
                { new Point3D(0, 0, 0), new Point3D(0, 0, 0), 0 },
                { new Point3D(0, 0, 0), new Point3D(3, 4, 0), 5 },
                { new Point3D(1, 2, 3), new Point3D(4, 6, 3), 5 },
            };
        }
    }
}
