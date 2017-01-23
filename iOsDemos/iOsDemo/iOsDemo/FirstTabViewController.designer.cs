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
    [Register ("FirstTabViewController")]
    partial class FirstTabViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ABButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ByeButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton HelloButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TextLabel { get; set; }

        [Action ("SayGoodbye:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SayGoodbye (UIKit.UIButton sender);

        [Action ("SayHello:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SayHello (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (ABButton != null) {
                ABButton.Dispose ();
                ABButton = null;
            }

            if (ByeButton != null) {
                ByeButton.Dispose ();
                ByeButton = null;
            }

            if (HelloButton != null) {
                HelloButton.Dispose ();
                HelloButton = null;
            }

            if (TextLabel != null) {
                TextLabel.Dispose ();
                TextLabel = null;
            }
        }
    }
}