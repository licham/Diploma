using HelixToolkit.Wpf;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace Diploma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private MeshGeometryVisual3D _mesh;
        
        public MeshGeometryVisual3D Model
        {
            get => _mesh;
            set
            {
                _mesh = value;
                Viewport.Children.Remove(Viewport.Children.OfType<MeshGeometryVisual3D>().FirstOrDefault());
                Viewport.Children.Add(Model);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void OpenModel(object sender, RoutedEventArgs e) =>
            Model = await ModelManager.OpenNewModel();

        private void SaveModel(object sender, RoutedEventArgs e)
        {
        }

        private async void NextModel(object sender, RoutedEventArgs e) =>
            Model = await ModelManager.OpenNextModel();

        private async void PreviousModel(object sender, RoutedEventArgs e) =>
            Model = await ModelManager.OpenPreviousModel();

        private void WireframeChecked(object sender, RoutedEventArgs e)
        {
            var viewport = (Content as Grid).Children[0] as HelixViewport3D;
            viewport.Children.Add(WireframeGenerator.GenerateWireframe(PointProcessor.Points));
        }

        private void WireframeUnchecked(object sender, RoutedEventArgs e)
        {
            var viewport = (Content as Grid).Children[0] as HelixViewport3D;
            viewport.Children.Remove(viewport.Children.OfType<LinesVisual3D>().First());
        }

        private void PerspectiveCameraSelected(object sender, RoutedEventArgs e)
        {
            Viewport.Orthographic = false;
            (Viewport.Camera as PerspectiveCamera).FieldOfView = 60;
            if (OrthographicCamera != null)
                OrthographicCamera.IsChecked = false;
            if (PerspectiveCamera != null)
                PerspectiveCamera.IsChecked = true;
        }

        private void OrthographicCameraSelected(object sender, RoutedEventArgs e)
        {
            Viewport.Orthographic = true;
            (Viewport.Camera as OrthographicCamera).Width = new Point3D(0, 0, 0).DistanceTo(Viewport.Camera.Position);
            if (OrthographicCamera != null)
                OrthographicCamera.IsChecked = true;
            if (PerspectiveCamera != null)
                PerspectiveCamera.IsChecked = false;
        }
    }
}
