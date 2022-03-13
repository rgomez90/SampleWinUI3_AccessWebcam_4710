using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SampleWinUI3_AccessWebcam_4710
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private Image image;

        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var captureUI = new CameraCaptureUI(this);
            var file = await captureUI.CaptureFileAsync(Windows.Media.Capture.CameraCaptureUIMode.Photo);
            if (file != null)
            {
                var bitmapImage = new BitmapImage()
                {
                    UriSource = new System.Uri(file.Path),
                };
                this.image.Source = bitmapImage;
            }
        }
    }
}
