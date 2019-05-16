using System;
using System.IO;
using System.Windows.Media.Media3D;

namespace Diploma
{
    static class PointReader
    {
        public static Point3D[,] Points;

        public static Point3D[,] ReadPoints(string fileName)
        {
            using (var fstream = File.OpenRead(fileName))
            {
                var array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                var textFromFile = System.Text.Encoding.Default.GetString(array);
                var rows = textFromFile.Split("\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var firstRow = rows[0].Split('\t');
                Points = new Point3D[firstRow.Length, rows.Length];
                for (var y = 0; y < rows.Length; y++)
                {
                    var values = rows[y].Split('\t');
                    for (var x = 0; x < values.Length; x++)
                    {
                        Points[x, y] = new Point3D(x, y, int.Parse(values[x]));
                    }
                }
                return Points;
            }
        }
    }
}
