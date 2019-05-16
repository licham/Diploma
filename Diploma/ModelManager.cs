using HelixToolkit.Wpf;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Diploma
{
    static class ModelManager
    {
        public static string CurrentFile;
        public static string CurrentDirectory;
        public static MeshGeometryVisual3D Model;

        public static async Task<MeshGeometryVisual3D> OpenNewModel()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Data files (*.dat) | *.dat",
                InitialDirectory = @"C:\Users\Silaiev\source\repos\Diploma\Diploma\bin\Debug\InputData"
            };
            var dialogResult = openFileDialog.ShowDialog();
            return dialogResult.HasValue && dialogResult.Value
                ? await OpenModel(openFileDialog.FileName)
                : new MeshGeometryVisual3D();
        }

        private static async Task<MeshGeometryVisual3D> OpenModel(string fileName) 
        {
            CurrentFile = fileName;
            CurrentDirectory = Path.GetDirectoryName(CurrentFile);
            var cutFileName = CurrentFile.Remove(CurrentFile.Length - 5);
            var geometry = await GeometryCreator.CreateGeometryAsync(cutFileName + "d.dat");
            var material = await MaterialCreator.CreateMaterialAsync(cutFileName + "c.bmp");
            Model = new MeshGeometryVisual3D()
            {
                MeshGeometry = (MeshGeometry3D)geometry,
                Material = material
            };
            return Model;
        }

        public static async Task<MeshGeometryVisual3D> OpenNextModel()
        {
            var files = Directory.GetFiles(CurrentDirectory, "*.dat");
            var index = Array.IndexOf(files, CurrentFile);
            index = index == files.Length - 1 ? 0 : index + 1;
            return await OpenModel(files[index]);
        }

        public static async Task<MeshGeometryVisual3D> OpenPreviousModel()
        {
            var files = Directory.GetFiles(CurrentDirectory, "*.dat");
            var index = Array.IndexOf(files, CurrentFile);
            index = index == 0 ? files.Length - 1 : index - 1;
            return await OpenModel(files[index]);
        }
    }
}
