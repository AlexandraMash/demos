using Android.Content.PM;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using System;
using Android;

namespace SupportLibraryDemo.Utils
{
    public class PermissionManager
    {
        public static void CheckAndRequestPermission(Fragment fragment, string permission, string permissionExplanation,
            int requestCode, Action permissionAvailableAction)
        {
            if (ContextCompat.CheckSelfPermission(fragment.Activity, Manifest.Permission_group.Contacts) == (int)Permission.Granted)
            {
                permissionAvailableAction();
                return;
            }

            if (fragment.ShouldShowRequestPermissionRationale(permission))
            {
                //Explain to the user why we need to read the contacts
                Snackbar.Make(fragment.Activity.CurrentFocus, permissionExplanation, Snackbar.LengthIndefinite)
                        .SetAction(Android.Resource.String.Ok,
                                v => fragment.RequestPermissions(new string[] { permission }, requestCode))
                        .Show();

                return;
            }

            fragment.RequestPermissions(new string[] { permission }, requestCode);
        }
    }
}