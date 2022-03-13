using Microsoft.UI.Xaml;
using System;
using Windows.Foundation.Collections;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.System;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SampleWinUI3_AccessWebcam_4710
{
    public class CameraCaptureUI
    {
        private LauncherOptions _launcherOptions;

        public CameraCaptureUI(Window window)
        {
            var hndl = WindowNative.GetWindowHandle(window);

            _launcherOptions = new LauncherOptions();
            InitializeWithWindow.Initialize(_launcherOptions, hndl);

            _launcherOptions.TreatAsUntrusted = false;
            _launcherOptions.DisplayApplicationPicker = false;
            _launcherOptions.TargetApplicationPackageFamilyName = "Microsoft.WindowsCamera_8wekyb3d8bbwe";
        }

        public async Task<StorageFile> CaptureFileAsync(CameraCaptureUIMode mode)
        {
            if (mode != CameraCaptureUIMode.Photo)
            {
                throw new NotImplementedException();
            }

            var currentAppData = ApplicationData.Current;
            var tempLocation = currentAppData.TemporaryFolder;
            var tempFileName = "CCapture.jpg";
            var tempFile = await tempLocation.CreateFileAsync(tempFileName, CreationCollisionOption.GenerateUniqueName);
            var token = Windows.ApplicationModel.DataTransfer.SharedStorageAccessManager.AddFile(tempFile);

            var set = new ValueSet
        {
            { "MediaType", "photo" },
            { "PhotoFileToken", token }
        };

            var uri = new Uri("microsoft.windows.camera.picker:");
            var result = await Launcher.LaunchUriForResultsAsync(uri, _launcherOptions, set);
            if (result.Status == LaunchUriStatus.Success)
            {
                return tempFile;
            }

            return null;
        }
    }
}
