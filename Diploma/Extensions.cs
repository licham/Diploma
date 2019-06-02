using System.Windows.Media.Media3D;

namespace Diploma
{
    static class Extensions
    {
        public static Point3D Multiply(this Point3D point, double multiplier) =>
            new Point3D(point.X * multiplier, point.Y * multiplier, point.Z * multiplier);

        public static Point3D Add(this Point3D point, Point3D other) =>
            new Point3D(point.X + other.X, point.Y + other.Y, point.Z + other.Z);
    }
}
