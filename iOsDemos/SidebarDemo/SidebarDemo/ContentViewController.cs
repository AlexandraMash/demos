using Foundation;
using System;
using UIKit;
using SidebarNavigation;

namespace SidebarDemo
{
    public partial class ContentViewController : UIViewController
    {
        public ContentViewController (IntPtr handle) : base (handle)
        {
		}

		public SidebarController Sidebar { get; set; }

		partial void AlerAction(UIButton sender)
		{
			var alert = new UIAlertView();
			alert.Message = "Hello!";
			alert.AddButton("OK");
			alert.Show();
		}

		partial void ToggleMenu(NSObject sender)
		{
			Sidebar?.ToggleMenu();
		}
	}
}