namespace GeometryLib;

/// <summary>
/// Класс шара в трехмерном пространстве.
/// </summary>
public class Sphere3D
{
    public Sphere3D(Point3D center, double radius)
    {
        if (radius <= 0)
        {
            throw new ArgumentException("Радиус должен быть положительным.");
        }

        Center = center;
        Radius = radius;
    }

    /// <summary>
    ///  Центр шара.
    /// </summary>
    public Point3D Center { get; }

    /// <summary>
    ///  Радиус шара.
    /// </summary>
    public double Radius { get; }

    /// <summary>
    /// Диаметр шара.
    /// </summary>
    public double Diameter => 2 * Radius;

    /// <summary>
    /// Площадь поверхности шара.
    /// </summary>
    public double Area => 4 * Math.PI * Radius * Radius;

    /// <summary>
    /// Объём шара.
    /// </summary>
    public double Volume => (4.0 / 3.0) * Math.PI * Math.Pow(Radius, 3);

    /// <summary>
    /// Возвращает расстояние от точки до ближайшей точки поверхности шара.
    /// </summary>
    public double DistanceTo(Point3D p)
    {
        double distToCenter = Center.DistanceTo(p);
        return Math.Max(0, distToCenter - Radius);
    }

    /// <summary>
    /// Возвращает расстояние между ближайшими точками поверхностей двух шаров.
    /// </summary>
    public double DistanceTo(Sphere3D other)
    {
        double distCenters = Center.DistanceTo(other.Center);
        double distSurfaces = distCenters - (Radius + other.Radius);
        return Math.Max(0, distSurfaces);
    }

    /// <summary>
    /// Проверяет, лежит ли точка внутри шара (включая границу).
    /// </summary>
    public bool Contains(Point3D p)
    {
        return Center.DistanceTo(p) <= Radius + Vector3.Tolerance;
    }

    /// <summary>
    /// Проверяет, пересекаются ли два шара.
    /// </summary>
    public bool IntersectsWith(Sphere3D other)
    {
        double distCenters = Center.DistanceTo(other.Center);
        return distCenters <= (Radius + other.Radius + Vector3.Tolerance);
    }

    /// <summary>
    /// Проверяет, лежит ли другой шар полностью внутри этого шара.
    /// </summary>
    public bool Contains(Sphere3D other)
    {
        double distCenters = Center.DistanceTo(other.Center);
        return distCenters + other.Radius <= Radius + Vector3.Tolerance;
    }

    public override string ToString()
    {
        return $"Sphere(Center={Center}, Radius={Radius})";
    }
}
