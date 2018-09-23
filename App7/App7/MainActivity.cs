using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using AT.Nineyards.Anylinexamarin.Support.Modules.Ocr;
using AT.Nineyards.Anylinexamarin.Support.Modules.Barcode;
using Android.Util;
using AT.Nineyards.Anyline.Modules.Barcode;

namespace App7
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IBarcodeResultListener
    {
        public const string LicenseKey = "eyJzY29wZSI6WyJBTEwiXSwicGxhdGZvcm0iOlsiaU9TIiwiQW5kcm9pZCIsIldpbmRvd3MiXSwidmFsaWQiOiIyMDE4LTEwLTIzIiwibWFqb3JWZXJzaW9uIjoiMyIsImlzQ29tbWVyY2lhbCI6ZmFsc2UsInRvbGVyYW5jZURheXMiOjMwLCJzaG93UG9wVXBBZnRlckV4cGlyeSI6dHJ1ZSwiaW9zSWRlbnRpZmllciI6WyJBcHA3LkFwcDciXSwiYW5kcm9pZElkZW50aWZpZXIiOlsiQXBwNy5BcHA3Il0sIndpbmRvd3NJZGVudGlmaWVyIjpbIkFwcDcuQXBwNyJdfQpSTW9rTUJZekdPVndQb2cwZDRDNmQvTSthallsYUhnVWJlZnMxM2FaYk0rZ1Rzc0xaVVlvS00xeDY5cmlzcW1kUnpEbEVqMzhwL1Fvd01qUi90OHBwNVVDVzAzUkE2RldpaFJTWHoxN01ab1B6R05qUEhnZ3doMXZqZXpmMFdMZDNzanY1SmJTVHQySksyWUFBZzZRbEcxRnpDdUZRRmNBYzEyVkhSZHFPdzRnRFVkNWllWEJEcVFhVFUyM2J4NGFqaG1KQzdWQXRKQnJGVFYzaVgrOVVRd0FMZEo2c2VjdXpZZnRkVytHcTBNbENjQ21VR3pYZ0NTTHp6TVpsQUhqa0lBMjgxZ3N5VUtDTnVrdWpmcTgrZE9CMVFNSWE0c0NodFpITHMyK3RUTXNBQTkwdmNUV3N1Z2c4TWVVbzQ4TXQvV2RPQlpSRXk4M21JeDhYQnVzckE9PQ==";
        private BarcodeScanView scanView = null;

        public void OnResult(BarcodeResult scanResult)
        {
            // Show a short Toast message if a result is found
            Toast.MakeText(this, scanResult.Result.ToString(), ToastLength.Short).Show();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            scanView = FindViewById<BarcodeScanView>(Resource.Id.AnylineScanView);

            // Load config from the .json file
            // We don't need this, if we define our configuration in the XML code
            scanView.SetConfigFromAsset("scanConfig.json");

            // Initialize with our license key and our result listener
            // Important: use the license key for your app package
            scanView.InitAnyline(LicenseKey, this);

            // Don't stop scanning when a result is found
            scanView.SetCancelOnResult(false);

            // Register event that shows if the camera is initialized
            scanView.CameraOpened += (s, e) => {
                Log.Debug("Camera", "Camera opened successfully. Frame resolution " + e.Width + " x " + e.Height);
            };

            // Register event that shows if the camera returns an error
            scanView.CameraError += (s, e) => {
                Log.Error("Camera", "OnCameraError: " + e.Event.Message);
            };
        }
    }
}