// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace iOSDownloadManager
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIBarButtonItem CancelButton { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem DownloadButton { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView DownloadingActivity { get; set; }

		[Outlet]
		UIKit.UILabel DownloadingLabel { get; set; }

		[Outlet]
		UIKit.UIProgressView DownloadingProgress { get; set; }

		[Action ("StartDownloading:")]
		partial void StartDownloading (Foundation.NSObject sender);

		[Action ("StopDownloading:")]
		partial void StopDownloading (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (DownloadingLabel != null) {
				DownloadingLabel.Dispose ();
				DownloadingLabel = null;
			}

			if (DownloadingActivity != null) {
				DownloadingActivity.Dispose ();
				DownloadingActivity = null;
			}

			if (DownloadingProgress != null) {
				DownloadingProgress.Dispose ();
				DownloadingProgress = null;
			}

			if (DownloadButton != null) {
				DownloadButton.Dispose ();
				DownloadButton = null;
			}

			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}
		}
	}
}
