using Android.OS;
using Android.Views;
using Android.Support.V4.App;

namespace SupportLibraryDemo
{
    public class YouTubeFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View fragmentView = inflater.Inflate(Resource.Layout.fragment_youtube, container, false);
            return fragmentView;
        }
    }
}