// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace iOsDemo
{
    [Register ("SecondTabViewController")]
    partial class SecondTabViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView DemoImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel PictureName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIProgressView ProgressBarView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TypeEditText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider VisibilitySlider { get; set; }

        [Action ("NameChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void NameChanged (UIKit.UITextField sender);

        [Action ("SliderValueChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SliderValueChanged (UIKit.UISlider sender);

        [Action ("StepperValueChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void StepperValueChanged (UIKit.UIStepper sender);

        [Action ("SwicthChecked:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SwicthChecked (UIKit.UISwitch sender);

        void ReleaseDesignerOutlets ()
        {
            if (DemoImageView != null) {
                DemoImageView.Dispose ();
                DemoImageView = null;
            }

            if (PictureName != null) {
                PictureName.Dispose ();
                PictureName = null;
            }

            if (ProgressBarView != null) {
                ProgressBarView.Dispose ();
                ProgressBarView = null;
            }

            if (TypeEditText != null) {
                TypeEditText.Dispose ();
                TypeEditText = null;
            }

            if (VisibilitySlider != null) {
                VisibilitySlider.Dispose ();
                VisibilitySlider = null;
            }
        }
    }
}