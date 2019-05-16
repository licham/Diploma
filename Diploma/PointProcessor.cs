using System.Windows.Media.Media3D;

namespace Diploma
{
    static class PointProcessor
    {
        public static Point3D[,] Points;

        public static Point3D[,] ProcessPoints(Point3D[,] points)
        {
            Points = RemoveNoizes(points);
            //for (int y = 0; y < points.GetLength(1); y++)
            //{
            //    for (int x = 0; x < points.GetLength(0); x++)
            //    {
            //        if (y > 1 && (points[x, y] - points[x, y - 1]).Z < double.Epsilon)
            //            points[x, y - 1].Z = (points[x, y].Z + points[x, y - 2].Z) / 2;
            //        if (x > 1 && (points[x, y] - points[x - 1, y]).Z < double.Epsilon)
            //            points[x - 1, y].Z = (points[x, y].Z + points[x - 2, y].Z) / 2;
            //    }
            //}
            return Points;
        }

        private static Point3D[,] RemoveNoizes(Point3D[,] points)
        {
            for (var y = 0; y < points.GetLength(1); y++)
            {
                for (var x = 0; x < points.GetLength(0); x++)
                {
                    var z = points[x, y].Z;
                    points[x, y] = new Point3D(x, y, (z > 1200 || z < 700) ? 1200 : z);
                }
            }
            return points;
        }
    }
}
