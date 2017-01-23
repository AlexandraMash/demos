using Foundation;
using System;
using UIKit;

namespace iOsDemo
{
	public partial class FirstTabViewController : UIViewController
	{
		public FirstTabViewController(IntPtr handle) : base(handle)
		{
		}

		partial void SayHello(UIButton sender)
		{
			TextLabel.Text = "Hello";
		}

		partial void SayGoodbye(UIButton sender)
		{
			TextLabel.Text = "Bye-bye";
		}
	}
}