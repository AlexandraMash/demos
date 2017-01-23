using Android.OS;
using Android.Views;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Widget;
using Android.Provider;
using Android.Support.V4.Content;
using Android.Database;
using Square.Picasso;
using Android.Support.V4.Widget;
using static Android.Support.V4.Widget.SwipeRefreshLayout;
using Android;
using SupportLibraryDemo.Utils;
using Android.Content.PM;
using System;
using Android.Support.Design.Widget;

namespace SupportLibraryDemo
{
    public class RecyclerFragment : Fragment, LoaderManager.ILoaderCallbacks, IOnRefreshListener
    {
        private RecyclerView _recyclerView;
        private ContactsAdapter _contactsAdapter;
        private SwipeRefreshLayout _swipeLayout;
        private TextView _emptyLabel;

        private const int CONTACTS_LOADER_ID = 42;

        private readonly string PermissionRequired = Manifest.Permission.ReadContacts;
        public const int CONTACTS_PERMISSION_REQUEST_CODE = 328;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View fragmentView = inflater.Inflate(Resource.Layout.fragment_recycler, container, false);

            //Set up recycler view
            _recyclerView = fragmentView.FindViewById<RecyclerView>(Resource.Id.lst_recycler_view);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(this.Activity));
            _contactsAdapter = new ContactsAdapter(Activity.ApplicationContext);
            _recyclerView.SetAdapter(_contactsAdapter);

            //Set up swipe to refresh layout
            _swipeLayout = fragmentView.FindViewById<SwipeRefreshLayout>(Resource.Id.lt_refresh);
            _swipeLayout.SetOnRefreshListener(this);
            _swipeLayout.SetColorSchemeResources(Resource.Color.sky_blue);

            _emptyLabel = fragmentView.FindViewById<TextView>(Resource.Id.tv_empty);
            _emptyLabel.Visibility = ViewStates.Visible;

            PermissionManager.CheckAndRequestPermission(this, this.PermissionRequired, 
                GetString(Resource.String.contacts_permission_explanation), CONTACTS_PERMISSION_REQUEST_CODE, 
                LoadContacts);
            _swipeLayout.Refreshing = true;

            return fragmentView;
        }

        public void LoadContacts()
        {
            //Init loader
            LoaderManager.InitLoader(CONTACTS_LOADER_ID, null, this);            
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (grantResults.Length > 0)
            {
                switch (requestCode)
                {
                    case CONTACTS_PERMISSION_REQUEST_CODE:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            //Permission granted
                            LoadContacts();
                        }
                        else
                        {
                            //Permission Denied
                            _swipeLayout.Refreshing = true;
                            _emptyLabel.Visibility = ViewStates.Visible;
                        }
                    }
                        break;
                }
            }
        }

        public Loader OnCreateLoader(int id, Bundle args)
        {
            var uri = ContactsContract.Contacts.ContentUri;

            string[] projection =
            {
                ContactsContract.Contacts.InterfaceConsts.Id,
                ContactsContract.Contacts.InterfaceConsts.PhotoThumbnailUri,
                ContactsContract.Contacts.InterfaceConsts.DisplayName
            };

            string sortOrder = ContactsContract.Contacts.InterfaceConsts.Id + " ASC";

            return new CursorLoader(
                    Activity.ApplicationContext,
                    uri,
                    null,
                    null,
                    null,
                    sortOrder);
        }

        public void OnLoaderReset(Loader loader)
        {
            _contactsAdapter.ContactsCursor = null;
            _swipeLayout.Refreshing = false;
            _emptyLabel.Visibility = ViewStates.Visible;
        }

        public void OnLoadFinished(Loader loader, Java.Lang.Object data)
        {
            _contactsAdapter.ContactsCursor = data as ICursor;
            _swipeLayout.Refreshing = false;
            if (_contactsAdapter.ItemCount > 0)
            {
                _emptyLabel.Visibility = ViewStates.Gone;
            }
            else
            {
                _emptyLabel.Visibility = ViewStates.Visible;
            }
        }

        public void OnRefresh()
        {
            //Restart loader
            LoaderManager.RestartLoader(CONTACTS_LOADER_ID, null, this);
        }
    }

    public class ViewHolder : RecyclerView.ViewHolder
    {
        public TextView NameTextView;
        public ImageView ElementImage;
        public View BaseView;

        public ViewHolder(View view) : base(view)
        {
            NameTextView = view.FindViewById<TextView>(Resource.Id.tv_element_name);
            ElementImage = view.FindViewById<ImageView>(Resource.Id.img_element_picture);
            BaseView = view.FindViewById<CardView>(Resource.Id.cmp_card_view);
        }
    }

    public class ContactsAdapter : RecyclerView.Adapter {
        private ICursor _contactsCursor;
        private Android.Content.Context _context;      
        
        public ContactsAdapter(Android.Content.Context context)
        {
            _context = context;
        }

        public ICursor ContactsCursor
        {
            set {
                _contactsCursor = value;                
                NotifyDataSetChanged();
            }
        }
        
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context)
                                   .Inflate(Resource.Layout.list_item_card, parent, false);
               
            ViewHolder vh = new ViewHolder(view);
            return vh;
        }
        
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ViewHolder vh = holder as ViewHolder;

            if (_contactsCursor == null || _contactsCursor.IsClosed)
            {
                return;
            }

            _contactsCursor.MoveToPosition(position);
            var name = _contactsCursor.GetString(_contactsCursor.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.DisplayName));
            vh.NameTextView.Text = name;

            vh.BaseView.Click += (sender, args) =>
            {
                Snackbar.Make(vh.BaseView, name, Snackbar.LengthLong).Show();
            };

            var imageUri = _contactsCursor.GetString(_contactsCursor.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.PhotoThumbnailUri));

            Picasso.With(_context)
                    .Load(imageUri)
                    .Placeholder(Resource.Drawable.placeholder_user_photo)
                    .Error(Resource.Drawable.placeholder_user_photo)
                    .Into(vh.ElementImage);
        }

        public override int ItemCount
        {
            get { return _contactsCursor != null && !_contactsCursor.IsClosed ? _contactsCursor.Count : 0; }
        }
    }
}