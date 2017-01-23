using Foundation;
using System;
using UIKit;

namespace iOsDemo
{
    public partial class SecondTabViewController : UIViewController
	{
        public SecondTabViewController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			ProgressBarView.SetProgress(0, false);
			DemoImageView.Image = UIImage.FromBundle("smiley.png");
			VisibilitySlider.SetValue(1, false);
			TypeEditText.ShouldReturn += (textField) =>
			{
				TypeEditText.ResignFirstResponder();
				return true;
			};
		}

		partial void StepperValueChanged(UIStepper sender)
		{
			ProgressBarView.SetProgress((float)sender.Value / 100, true);
		}

		partial void SwicthChecked(UISwitch sender)
		{
			TypeEditText.Enabled = sender.On;
		}

		partial void SliderValueChanged(UISlider sender)
		{
			DemoImageView.Alpha = sender.Value;
		}

		partial void NameChanged(UITextField sender)
		{
			var text = sender.Text;
			if (string.IsNullOrEmpty(text))
			{
				text = "Picture";
			}
			PictureName.Text = text;
		}
	}
}