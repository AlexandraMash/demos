using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V4.App;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Graphics;

namespace SupportLibraryDemo
{
    [Android.App.Activity(Label = "@string/application_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        private DrawerLayout _drawerLayout;
        private NavigationView _navigationView;
        private Toolbar _toolbar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView (Resource.Layout.activity_main);

            //Find views
            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.lt_navigation_drawer);
            _navigationView = _drawerLayout.FindViewById<NavigationView>(Resource.Id.cmp_left_drawer);
            _toolbar = FindViewById<Toolbar>(Resource.Id.cmp_toolbar);

            //Set up navigation drawer           
            _navigationView.InflateHeaderView(Resource.Layout.view_navigation_header);
            _navigationView.NavigationItemSelected += NavigationItemSelectedListener;

            //Inflate initial fragment if there is no saved state
            if (bundle == null)
            {
                ReplaceFragment(new TabsFragment());
            }

            //Set toolbar as action bar
            SetSupportActionBar(_toolbar);

            //Set up navigation toggle in toolbar
            Android.Support.V7.App.ActionBarDrawerToggle drawerToggle =
                new Android.Support.V7.App.ActionBarDrawerToggle(this, _drawerLayout, _toolbar,
                Resource.String.application_name, Resource.String.application_name);

            _drawerLayout.AddDrawerListener(drawerToggle);
            drawerToggle.SyncState();
        }

        private void NavigationItemSelectedListener(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            //Close navigation drawer panel
            _drawerLayout.CloseDrawers();

            //Inflate selected fragment and update toolbar title
            switch(e.MenuItem.ItemId)
            {
                case Resource.Id.nav_item_animation:
                    ReplaceFragment(new AnimationFragment());
                    ToolbarTitle = GetString(Resource.String.nav_item_animation);
                    break;

                case Resource.Id.nav_item_tabs:
                default:
                    ReplaceFragment(new TabsFragment());
                    ToolbarTitle = GetString(Resource.String.application_name);
                    break;                  
            }        
        }

        public void ReplaceFragment(Fragment fragment)
        {
            FragmentTransaction fragmentTx = this.SupportFragmentManager.BeginTransaction();
            fragmentTx.Replace(Resource.Id.content, fragment).Commit();
        }

        public string ToolbarTitle
        {
            set 
            {
                _toolbar.Title = value;
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_options, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_share:
                    Snackbar.Make(_toolbar, GetString(Resource.String.share_hint), Snackbar.LengthLong).Show();
                    return true;

                case Resource.Id.action_info:
                    Snackbar.Make(_toolbar, GetString(Resource.String.info), Snackbar.LengthLong).Show();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }           
        }
    }
}

