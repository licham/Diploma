using HelixToolkit.Wpf;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Diploma
{
    static class GeometryCreator
    {
        public static Geometry3D Geometry;

        public static async Task<Geometry3D> CreateGeometryAsync(string fileName) => 
            await Task.Run(() =>
            {
                var meshBuilder = new MeshBuilder(false, true);
                var points = PointReader.ReadPoints(fileName);
                points = PointProcessor.ProcessPoints(points);
                points = BSplineSurfaceBuilder.BuildSurface(points, 5, new Size(100, 100));
                var width = points.GetLength(0);
                var heigth = points.GetLength(1);
                for (var y = 0; y < heigth; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        meshBuilder.Positions.Add(points[x, y]);
                        meshBuilder.TextureCoordinates.Add(new Point(x, y));
                        if (x != width - 1 && y != heigth - 1)
                        {
                            meshBuilder.TriangleIndices.Add(x + y * width);
                            meshBuilder.TriangleIndices.Add(x + (y + 1) * width);
                            meshBuilder.TriangleIndices.Add(x + 1 + y * width);
                            meshBuilder.TriangleIndices.Add(x + 1 + y * width);
                            meshBuilder.TriangleIndices.Add(x + (y + 1) * width);
                            meshBuilder.TriangleIndices.Add(x + 1 + (y + 1) * width);
                        }
                    }
                }
                Geometry = meshBuilder.ToMesh();
                Geometry.Freeze();
                return Geometry;
            });
    }
}
