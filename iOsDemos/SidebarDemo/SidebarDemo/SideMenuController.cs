using Foundation;
using System;
using UIKit;
using SidebarNavigation;

namespace SidebarDemo
{
    public partial class SideMenuController : UITableViewController
    {
        public SideMenuController (IntPtr handle) : base (handle)
        {
        }

		public SidebarController Sidebar { get; set; }

		public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			switch (indexPath.Row)
			{
				case 0:
					var contentController = (ContentViewController)Storyboard.InstantiateViewController("ContentViewController");
					contentController.Sidebar = Sidebar;
					Sidebar.ChangeContentView(contentController);
					break;
				case 1:
					var second = (SecondViewController)Storyboard.InstantiateViewController("SecondViewController");
					second.Sidebar = Sidebar;
					Sidebar.ChangeContentView(second);
					break;
				case 2:
					var third = (ThirdViewController)Storyboard.InstantiateViewController("ThirdViewController");
					third.Sidebar = Sidebar;
					Sidebar.ChangeContentView(third);
					break;
				default:
					break;
			}
		}
    }
}