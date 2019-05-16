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
                Material = new DiffuseMaterial(new ImageBrush(new CroppedBitmap(image, CROP_RECT)));
                Material.Freeze();
                return Material;
                //ModelMaterial = MaterialHelper.CreateEmissiveImageMaterial(fileName, BrushHelper.CreateGrayBrush(1), UriKind.Absolute);
                //return MaterialHelper.CreateEmissiveImageMaterial(fileName, BrushHelper.CreateGrayBrush(1), UriKind.Absolute);
            });
    }
}
