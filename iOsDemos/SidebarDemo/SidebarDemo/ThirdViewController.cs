using Foundation;
using System;
using UIKit;
using SidebarNavigation;

namespace SidebarDemo
{
    public partial class ThirdViewController : UIViewController
    {
		public SidebarController Sidebar { get; set; }

        public ThirdViewController (IntPtr handle) : base (handle)
        {
        }

		partial void ToggleMenu(NSObject sender)
		{
			Sidebar?.ToggleMenu();
		}
    }
}