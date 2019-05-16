using HelixToolkit.Wpf;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Diploma
{
    static class WireframeGenerator
    {
        public static LinesVisual3D GenerateWireframe(Point3D[,] points)
        {
            var wireframe = new List<Point3D>();
            wireframe.AddRange(Task.Run(() => Wireframe1(points)).Result);
            wireframe.AddRange(Task.Run(() => Wireframe2(points)).Result);
            return new LinesVisual3D() { Points = new Point3DCollection(wireframe) };
        }

        private static IEnumerable<Point3D> Wireframe1(Point3D[,] points)
        {
            var width = points.GetLength(0);
            var heigth = points.GetLength(1);
            var currentPoint = new Point(0, 0);
            var finishPoint = new Point(width - 1, heigth - 1);
            var currentDirection = new Vector(0, 0);
            var wireframe = new List<Point3D>();
            while (true)
            {
                if ((currentPoint.Y == 0 && currentPoint.X != width - 1) || currentPoint.Y == heigth - 1)
                {
                    currentPoint.Offset(1, 0);

                    currentDirection.X = currentPoint.Y == 0 ? -1 : 1;
                    currentDirection.Y = -currentDirection.X;
                }
                if (currentPoint.X == 0 || currentPoint.X == width - 1)
                {
                    currentPoint.Offset(0, 1);
                    if (currentPoint == finishPoint)
                        break;
                    currentDirection.X = currentPoint.X == 0 ? 1 : -1;
                    currentDirection.Y = -currentDirection.X;
                }
                wireframe.Add(points[(int)currentPoint.X, (int)currentPoint.Y]);
                currentPoint.Offset(currentDirection.X, currentDirection.Y);
                wireframe.Add(points[(int)currentPoint.X, (int)currentPoint.Y]);
            }
            return wireframe;
        }

        private static IEnumerable<Point3D> Wireframe2(Point3D[,] points)
        {
            var width = points.GetLength(0);
            var heigth = points.GetLength(1);
            var currentPoint = new Point(0, heigth - 1);
            var finishPoint = new Point(width - 1, 0);
            var currentDirection = new Vector(1, 0);
            var wireframe = new List<Point3D>();
            while (true)
            {
                wireframe.Add(points[(int)currentPoint.X, (int)currentPoint.Y]);
                currentPoint.Offset(currentDirection.X, currentDirection.Y);
                wireframe.Add(points[(int)currentPoint.X, (int)currentPoint.Y]);
                if (currentPoint.X == 0 || currentPoint.X == width - 1)
                {
                    currentPoint.Offset(0, -1);
                    currentDirection.X = currentPoint.X == 0 ? 1 : -1;
                    if (currentPoint == finishPoint)
                        break;
                }
            }
            return wireframe;
        }
    }
}
