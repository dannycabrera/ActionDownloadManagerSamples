using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading;
using ActionComponents;

namespace DownloadTesterAndroid
{

	[Activity (Label = "DownloadManager", MainLauncher = true)]
	public class Activity1 : Activity
	{
		#region Private Variables
		private ACDownloadManager downloadManager;
		#endregion 	

		#region Overrides
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Create a new download manager
			downloadManager=new ACDownloadManager(); 

			// Get all of our interface items
			ProgressBar bar = FindViewById<ProgressBar> (Resource.Id.progressBar);
			TextView title = FindViewById<TextView> (Resource.Id.progressText);
			Button startDowload = FindViewById<Button> (Resource.Id.startDownload);
			Button cancel = FindViewById<Button> (Resource.Id.cancel);

			// Create path to hold downloaded files
			string directory = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			Console.WriteLine("Directory: {0}",directory);

			// Wireup progress bar
			downloadManager.FileDownloadProgressPercent += (percentage) => {

				// Adjust percentage and display
				RunOnUiThread (() => {
					percentage *= 100f;
					bar.Progress = (int)percentage;
				});
			};

			// Wireup completion handler
			downloadManager.AllDownloadsCompleted += () => {

				// Run on UI Thread
				RunOnUiThread (() => {
					// Inform caller
					ACAlert.ShowAlertOK(this, "Download Manager", "All files have been downloaded");

					// Update GUI
					startDowload.Enabled = true;
					title.Visibility = ViewStates.Invisible;
					bar.Visibility = ViewStates.Invisible;
					cancel.Visibility = ViewStates.Invisible;
				});

			};

			// Wireup error handler
			downloadManager.DownloadError += (message) => {

				// Run on UI Thread
				RunOnUiThread (() => {
					// Update GUI
					startDowload.Enabled = true;
					title.Visibility = ViewStates.Invisible;
					bar.Visibility = ViewStates.Invisible;
					cancel.Visibility = ViewStates.Invisible;

					// Inform caller
					ACAlert.ShowAlertOK(this, "Download Error", message);
				});
			};
			
			// Wireup start button
			startDowload.Click += (sender, e) => {

				// Update GUI
				startDowload.Enabled = false;
				title.Visibility = ViewStates.Visible;
				bar.Visibility = ViewStates.Visible;
				cancel.Visibility = ViewStates.Visible;

				// Queue up files to download
				downloadManager.QueueFile ("http://appracatappra.com/wp-content/uploads/et_temp/ssh-140751_232x117.jpg",directory);
				downloadManager.QueueFile ("http://appracatappra.com/wp-content/uploads/et_temp/4-TD-web-5-662620_960x332.png",directory);

				// Start the download process
				ThreadPool.QueueUserWorkItem((callback) =>{
					downloadManager.StartDownloading();
				});

			};

			// Wireup cancel button
			cancel.Click += (sender, e) => {

				// Stop any running downloads
				downloadManager.AbortDownload ();

				// Update GUI
				startDowload.Enabled = true;
				title.Visibility = ViewStates.Invisible;
				bar.Visibility = ViewStates.Invisible;
				cancel.Visibility = ViewStates.Invisible;

				// Inform user
				ACAlert.ShowAlertOK(this, "Download Manager", "User has aborted all downloads");
			};

		}
		#endregion 
	}
}


