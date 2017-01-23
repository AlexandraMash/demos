using Foundation;
using System;
using UIKit;

namespace SidebarDemo
{
    public partial class MenuCell : UITableViewCell
    {
        public MenuCell (IntPtr handle) : base (handle)
        {
        }

		public override void SetHighlighted(bool highlighted, bool animated)
		{

			base.SetHighlighted(highlighted, animated);
			ContentView.BackgroundColor = highlighted ? UIColor.Orange : UIColor.White;
		}

		public override void SetSelected(bool selected, bool animated)
		{
			base.SetSelected(selected, animated);
			ContentView.BackgroundColor = selected ? UIColor.Red : UIColor.White;
		}
    }
}