using System;
using UIKit;
using ActionComponents;
using Foundation;

namespace iOSDownloadManager
{
	public partial class ViewController : UIViewController
	{
		#region Private Variables
		private ACDownloadManager downloadManager;
		#endregion

		#region Constructors
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
		#endregion

		#region Override Methods
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			//Call base
			base.ViewDidLoad();

			// Build a download manager
			downloadManager = new ACDownloadManager();

			//Wireup progress bar
			downloadManager.FileDownloadProgressPercent += delegate (float percentage) {

				//Update GUI
				using (var pool = new NSAutoreleasePool())
				{
					pool.BeginInvokeOnMainThread(delegate {
						//Set current percentage
						DownloadingProgress.Progress = percentage;
					});
				}

			};

			//Wireup completion handler
			downloadManager.AllDownloadsCompleted += delegate () {

				//Update GUI
				using (var pool = new NSAutoreleasePool())
				{
					pool.BeginInvokeOnMainThread(delegate {
						//Display Alert Dialog Box
						ACAlert.ShowAlertOK("Download Manager", "All files have been downloaded.");

						//Update GUI
						DownloadingProgress.Hidden = true;
						DownloadingActivity.StopAnimating();
						DownloadButton.Enabled = true;
						CancelButton.Enabled = false;
						DownloadingLabel.Hidden = true;
					});
				}
			};

			//Wireup download error event
			downloadManager.DownloadError += delegate (string message) {

				//Update GUI
				using (var pool = new NSAutoreleasePool())
				{
					pool.BeginInvokeOnMainThread(delegate {
						//Display Alert Dialog Box
						ACAlert.ShowAlertOK("Download Error", message);

						//Update GUI
						DownloadingProgress.Hidden = true;
						DownloadingActivity.StopAnimating();
						DownloadButton.Enabled = true;
						CancelButton.Enabled = false;
						DownloadingLabel.Hidden = true;
					});
				}
			};

			// Perform any additional setup after loading the view, typically from a nib.
			DownloadingProgress.Hidden = true;
			DownloadingActivity.StopAnimating();
			DownloadButton.Enabled = true;
			CancelButton.Enabled = false;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
		#endregion

		#region Custom Actions
		partial void StartDownloading(Foundation.NSObject sender) {

			//Get download directory
			string directory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

			//Update GUI
			DownloadingProgress.Hidden = false;
			DownloadingActivity.StartAnimating();
			DownloadButton.Enabled = false;
			CancelButton.Enabled = true;
			DownloadingLabel.Hidden = false;

			//Add a file to the queue
			downloadManager.QueueFile("http://appracatappra.com/wp-content/uploads/et_temp/ssh-140751_232x117.jpg", directory);

			//Add a second file to the queue
			downloadManager.QueueFile("http://appracatappra.com/wp-content/plugins/download-monitor/download.php?id=4", directory, "NDA.pdf");

			//Start the download process
			downloadManager.StartDownloading();
		}

		partial void StopDownloading(Foundation.NSObject sender) {
			//Terminate active download
			downloadManager.AbortDownload();

			//Update GUI
			DownloadingProgress.Hidden = true;
			DownloadingActivity.StopAnimating();
			DownloadButton.Enabled = true;
			CancelButton.Enabled = false;
			DownloadingLabel.Hidden = true;
		}
		#endregion
	}
}
