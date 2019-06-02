using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Diploma
{
    static class BSplineSurfaceBuilder
    {
        public static Point3D[,] SurfacePoints;

        public static Point3D[,] BuildSurface(Point3D[,] points, int degree, Size size)
        {
            SurfacePoints = new Point3D[(int)size.Width, (int)size.Height];
            var knotsX = BuildKnots(points.GetLength(0), degree);
            var knotsY = BuildKnots(points.GetLength(1), degree);
            var knotsXList = knotsX.ToList();
            var knotsYList = knotsY.ToList();
            var incrementX = (points.GetLength(0) - degree + 2) / (size.Width - 1);
            var incrementY = (points.GetLength(1) - degree + 2) / (size.Height - 1);
            
            Parallel.For(0, SurfacePoints.GetLength(0), i => 
            {
                var intervalX = i * incrementX;
                Parallel.For(0, SurfacePoints.GetLength(1), j => 
                {
                    var intervalY = j * incrementY;
                    var kFlag = false;
                    for (var k = 0; k < points.GetLength(0); k++)
                    {
                        var bi = SplineBlend(k, degree, knotsX, intervalX);
                        if (bi == 0)
                        {
                            if (kFlag)
                                break;
                            continue;
                        }
                        kFlag = true;
                        var lFlag = false;
                        for (var l = 0; l < points.GetLength(1); l++)
                        {
                            var bj = SplineBlend(l, degree, knotsY, intervalY);
                            if (bj == 0)
                            {
                                if (lFlag)
                                    break;
                                continue;
                            }
                            lFlag = true;
                            SurfacePoints[i, j] = SurfacePoints[i, j].Add(points[k, l].Multiply(bi).Multiply(bj));
                        }
                    }
                    intervalY += incrementY;
                });
            });

            var intervalX2 = 0d;
            for (var i = 0; i < SurfacePoints.GetLength(0) - 1; i++)
            {
                for (var j = 0; j < points.GetLength(0); j++)
                {
                    SurfacePoints[i, SurfacePoints.GetLength(1) - 1] = 
                        SurfacePoints[i, SurfacePoints.GetLength(1) - 1]
                        .Add(points[j, points.GetLength(1) - 1]
                        .Multiply(SplineBlend(j, degree, knotsX, intervalX2)));
                }
                intervalX2 += incrementX;
            }
            var intervalY2 = 0d;
            for (var i = 0; i < SurfacePoints.GetLength(1) - 1; i++)
            {
                for (var j = 0; j < points.GetLength(1); j++)
                {
                    SurfacePoints[SurfacePoints.GetLength(0) - 1, i] = 
                        SurfacePoints[SurfacePoints.GetLength(0) - 1, i]
                        .Add(points[points.GetLength(0) - 1, j]
                        .Multiply(SplineBlend(j, degree, knotsY, intervalY2)));
                }
                intervalY2 += incrementY;
            }
            SurfacePoints[SurfacePoints.GetLength(0) - 1, SurfacePoints.GetLength(1) - 1] = points[points.GetLength(0) - 1, points.GetLength(1) - 1];
            return SurfacePoints;
        }

        private static double SplineBlend(int k, int t, double[] u, double v) => 
            t == 1
                ? (u[k] <= v) && (v < u[k + 1]) ? 1 : 0
                : (u[k + t - 1] == u[k]) && (u[k + t] == u[k + 1])
                    ? 0
                    : u[k + t - 1] == u[k]
                    ? (u[k + t] - v) / (u[k + t] - u[k + 1]) * SplineBlend(k + 1, t - 1, u, v)
                    : u[k + t] == u[k + 1]
                    ? (v - u[k]) / (u[k + t - 1] - u[k]) * SplineBlend(k, t - 1, u, v)
                    : (v - u[k]) / (u[k + t - 1] - u[k]) * SplineBlend(k, t - 1, u, v) +
                            (u[k + t] - v) / (u[k + t] - u[k + 1]) * SplineBlend(k + 1, t - 1, u, v);

        private static double[] BuildKnots(int pointsCount, int degree)
        {
            var knots = new double[pointsCount + degree + 1];
            for (var i = 0; i < knots.Length; i++)
                knots[i] = i < degree ? 0 : i > pointsCount ? pointsCount - degree + 2 : i - degree + 1;
            return knots;
        }
    }
}
