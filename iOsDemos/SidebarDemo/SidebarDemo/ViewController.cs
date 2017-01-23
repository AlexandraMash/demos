using System;
using Foundation;
using SidebarNavigation;
using UIKit;

namespace SidebarDemo
{
	public partial class ViewController : UIViewController
	{
		public SidebarController SidebarController { get; private set; }

		private UIStoryboard _storyboard;
		public override UIStoryboard Storyboard
		{
			get
			{
				if (_storyboard == null)
					_storyboard = UIStoryboard.FromName("Main", null);
				return _storyboard;
			}
		}

		protected ViewController(IntPtr handle) : base(handle)
		{
			
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var contentController = (ContentViewController)Storyboard.InstantiateViewController("ContentViewController");
			var menuController = (SideMenuController)Storyboard.InstantiateViewController("SideMenuController");

			SidebarController = new SidebarController(this, contentController, menuController);

			SidebarController.ReopenOnRotate = false;
			SidebarController.MenuLocation = SidebarController.MenuLocations.Left;

			contentController.Sidebar = SidebarController;
			menuController.Sidebar = SidebarController;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
