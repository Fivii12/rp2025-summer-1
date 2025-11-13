namespace GeometryLib.Tests
{
    public class Sphere3DTests
    {
        [Fact]
        public void Cannot_create_sphere_with_negative_radius()
        {
            Point3D center = new Point3D(0, 0, 0);

            Assert.Throws<ArgumentException>(() => new Sphere3D(center, -1));
            Assert.Throws<ArgumentException>(() => new Sphere3D(center, -0.1));
        }

        [Fact]
        public void Can_create_sphere_with_positive_radius()
        {
            Point3D center = new Point3D(1, 2, 3);
            double radius = 5;

            Sphere3D sphere = new Sphere3D(center, radius);

            Assert.Equal(center, sphere.Center);
            Assert.Equal(radius, sphere.Radius);
        }

        [Theory]
        [MemberData(nameof(SpherePropertiesData))]
        public void Can_check_sphere_properties(Sphere3D sphere, double expectedDiameter, double expectedArea, double expectedVolume)
        {
            Assert.Equal(expectedDiameter, sphere.Diameter, 10);
            Assert.Equal(expectedArea, sphere.Area, 10);
            Assert.Equal(expectedVolume, sphere.Volume, 10);
        }

        public static TheoryData<Sphere3D, double, double, double> SpherePropertiesData()
        {
            return new TheoryData<Sphere3D, double, double, double>
            {
                { new Sphere3D(new Point3D(0, 0, 0), 1), 2, 4 * Math.PI, 4.0 / 3.0 * Math.PI },
                { new Sphere3D(new Point3D(0, 0, 0), 5), 10, 100 * Math.PI, 4.0 / 3.0 * Math.PI * 125 },
                { new Sphere3D(new Point3D(0, 0, 0), double.Epsilon), 2 * double.Epsilon, 4 * Math.PI * double.Epsilon * double.Epsilon, 4.0 / 3.0 * Math.PI * Math.Pow(double.Epsilon, 3) },
                { new Sphere3D(new Point3D(0, 0, 0), 1e10), 2e10, 4 * Math.PI * 1e20, 4.0 / 3.0 * Math.PI * 1e30 },
            };
        }

        [Theory]
        [MemberData(nameof(DistanceToPointData))]
        public void Can_calculate_distance_to_point(Sphere3D sphere, Point3D point, double expected)
        {
            double actual = sphere.DistanceTo(point);
            Assert.Equal(expected, actual, 10);
        }

        public static TheoryData<Sphere3D, Point3D, double> DistanceToPointData()
        {
            Sphere3D s1 = new Sphere3D(new Point3D(0, 0, 0), 5);

            return new TheoryData<Sphere3D, Point3D, double>
            {
                { s1, new Point3D(0, 0, 0), 0 },
                { s1, new Point3D(3, 4, 0), 0 },
                { s1, new Point3D(5, 0, 0), 0 },
                { s1, new Point3D(10, 0, 0), 5 },
            };
        }

        [Theory]
        [MemberData(nameof(DistanceToSphereData))]
        public void Can_calculate_distance_to_other_sphere(Sphere3D a, Sphere3D b, double expected)
        {
            double actual = a.DistanceTo(b);
            Assert.Equal(expected, actual, 10);
        }

        public static TheoryData<Sphere3D, Sphere3D, double> DistanceToSphereData()
        {
            Sphere3D s1 = new Sphere3D(new Point3D(0, 0, 0), 5);
            Sphere3D s2 = new Sphere3D(new Point3D(10, 0, 0), 3);
            Sphere3D s3 = new Sphere3D(new Point3D(3, 0, 0), 1);
            Sphere3D s4 = new Sphere3D(new Point3D(6, 0, 0), 2);

            return new TheoryData<Sphere3D, Sphere3D, double>
            {
                { s1, s2, 2 },
                { s1, s3, 0 },
                { s1, s4, 0 },
            };
        }

        [Theory]
        [MemberData(nameof(ContainsPointData))]
        public void Can_check_point_containment(Sphere3D sphere, Point3D point, bool expected)
        {
            bool actual = sphere.Contains(point);
            Assert.Equal(expected, actual);
        }

        public static TheoryData<Sphere3D, Point3D, bool> ContainsPointData()
        {
            Sphere3D s1 = new Sphere3D(new Point3D(0, 0, 0), 5);

            return new TheoryData<Sphere3D, Point3D, bool>
            {
                { s1, new Point3D(0, 0, 0), true },
                { s1, new Point3D(3, 4, 0), true },
                { s1, new Point3D(5, 0, 0), true },
                { s1, new Point3D(6, 0, 0), false },
            };
        }

        [Theory]
        [MemberData(nameof(IntersectsSphereData))]
        public void Can_check_sphere_intersection(Sphere3D a, Sphere3D b, bool expected)
        {
            bool actual = a.IntersectsWith(b);
            Assert.Equal(expected, actual);
        }

        public static TheoryData<Sphere3D, Sphere3D, bool> IntersectsSphereData()
        {
            Sphere3D s1 = new Sphere3D(new Point3D(0, 0, 0), 5);
            Sphere3D s2 = new Sphere3D(new Point3D(6, 0, 0), 2);
            Sphere3D s3 = new Sphere3D(new Point3D(3, 0, 0), 2);
            Sphere3D s4 = new Sphere3D(new Point3D(6, 0, 0), 1);
            Sphere3D s5 = new Sphere3D(new Point3D(10, 0, 0), 3);

            return new TheoryData<Sphere3D, Sphere3D, bool>
            {
                { s1, s2, true },
                { s1, s3, true },
                { s1, s4, true },
                { s1, s5, false },
            };
        }

        [Theory]
        [MemberData(nameof(ContainsSphereData))]
        public void Can_check_sphere_containment(Sphere3D outer, Sphere3D inner, bool expected)
        {
            bool actual = outer.Contains(inner);
            Assert.Equal(expected, actual);
        }

        public static TheoryData<Sphere3D, Sphere3D, bool> ContainsSphereData()
        {
            Sphere3D s1 = new Sphere3D(new Point3D(0, 0, 0), 5);
            Sphere3D s2 = new Sphere3D(new Point3D(1, 1, 1), 3);
            Sphere3D s3 = new Sphere3D(new Point3D(3, 1, 1), 3);
            Sphere3D s4 = new Sphere3D(new Point3D(10, 0, 0), 1);

            return new TheoryData<Sphere3D, Sphere3D, bool>
            {
                { s1, s2, true },
                { s1, s3, false },
                { s1, s4, false },
            };
        }
    }
}
