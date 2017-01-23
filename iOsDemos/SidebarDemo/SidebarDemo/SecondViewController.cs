using Foundation;
using System;
using UIKit;
using SidebarNavigation;

namespace SidebarDemo
{
    public partial class SecondViewController : UIViewController
    {
		public SidebarController Sidebar { get; set; }

        public SecondViewController (IntPtr handle) : base (handle)
        {
        }

		partial void ToggleMenu(NSObject sender)
		{
			Sidebar?.ToggleMenu();
		}
    }
}