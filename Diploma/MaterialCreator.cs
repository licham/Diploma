using HelixToolkit.Wpf;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace Diploma
{
    static class MaterialCreator
    {
        private static readonly Int32Rect CROP_RECT = new Int32Rect(100, 25, 1150, 880);

        public static Material Material;

        public static async Task<Material> CreateMaterialAsync(string fileName) => 
            await Task.Run(() =>
            {
                var image = new BitmapImage(new Uri(fileName));
                var textureMaterial = new DiffuseMaterial(new ImageBrush(new CroppedBitmap(image, CROP_RECT)));
                Material = MaterialHelper.CreateMaterial(new ImageBrush(new CroppedBitmap(image, CROP_RECT)), 1000, 255, true);
                return Material;
            });
    }
}
