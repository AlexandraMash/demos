using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Java.Lang;
using Android.Runtime;

namespace SupportLibraryDemo
{
    public class TabsFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //Inflate layout
            View fragmentView = inflater.Inflate(Resource.Layout.fragment_tabs, container, false);

            //Find views
            TabLayout tabLayout = fragmentView.FindViewById<TabLayout>(Resource.Id.lt_tabs);
            ViewPager viewPager = fragmentView.FindViewById<ViewPager>(Resource.Id.cmp_viewpager);

            //Set adapter for ViewPager
            viewPager.Adapter = new TabsAdapter(this.ChildFragmentManager);            
            tabLayout.SetupWithViewPager(viewPager);            

            return fragmentView;
        }
    }    

    public class TabsAdapter : FragmentPagerAdapter
    {
        private const int COUNT = 3;
        private string[] _pageTitles = 
        {
            Android.App.Application.Context.Resources.GetString(Resource.String.recycler_header),
            Android.App.Application.Context.Resources.GetString(Resource.String.empty_header),
            Android.App.Application.Context.Resources.GetString(Resource.String.empty_header)
        };

        public TabsAdapter(FragmentManager fm) : base(fm) { }

        public override int Count
        {
            get
            {
                return COUNT;
            }
        }

        public override Fragment GetItem(int position)
        {
            switch (position)
            {
                case 0: return new RecyclerFragment();
                case 1: return new EmptyFragment();
                case 2: return new EmptyFragment();
            }
            return null;
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            // Generate title based on item position
            return CharSequence.ArrayFromStringArray(_pageTitles)[position];
        }
    }
}