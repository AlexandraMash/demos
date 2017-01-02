using Android.OS;
using Android.Views;
using Android.Support.V4.App;
using Android.Support.Design.Widget;
using Android.Graphics;
using Android.Transitions;
using Android.Widget;

namespace SupportLibraryDemo
{
    public class EmptyFragment : Fragment
    {
        private View _explodingView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View fragmentView = inflater.Inflate(Resource.Layout.fragment_empty, container, false);

            RelativeLayout rootView = fragmentView.FindViewById<RelativeLayout>(Resource.Id.root_view);
            _explodingView = fragmentView.FindViewById<View>(Resource.Id.view_exploding);

            //Set up Floating Action Button
            FloatingActionButton fab = fragmentView.FindViewById<FloatingActionButton>(Resource.Id.btn_fab);
            fab.Click += (sender, e) =>
            {
                if (_explodingView.Visibility == ViewStates.Visible)
                {
                    _explodingView.SetBackgroundColor(Color.Red);
                    Snackbar.Make(fab, GetString(Resource.String.fab_clicked), Snackbar.LengthLong)
                        .SetActionTextColor(Color.Red)
                        .SetAction(GetString(Resource.String.cancel), (e1) => { })
                        .SetCallback(new SnackbarCallback(rootView, _explodingView))
                        .Show();                    
                }
                else
                {              
                    _explodingView.SetBackgroundColor(Color.Black);
                    _explodingView.Visibility = ViewStates.Visible;
                    int dim = (int)Activity.Resources.GetDimension(Resource.Dimension.dim_small_square);
                    RelativeLayout.LayoutParams layoutParams = new RelativeLayout.LayoutParams(dim, dim);
                    layoutParams.AddRule(LayoutRules.CenterInParent);
                    _explodingView.LayoutParameters = layoutParams;
                }
            };

            return fragmentView;
        }
    }

    public class SnackbarCallback : Snackbar.Callback
    {
        public RelativeLayout _rootView;
        public View _explodingView;

        public const int HUGE_SIZE = 2000;

        public SnackbarCallback(RelativeLayout rootViewGroup, View explodingView)
        {
            _rootView = rootViewGroup;
            _explodingView = explodingView;
        }

        public override void OnDismissed(Snackbar snackbar, int e)
        {
            if (e == DismissEventTimeout)
            {
                ChangeBounds changeBoundsAnimation = new ChangeBounds();
                changeBoundsAnimation.SetDuration(100);
                Explode explodeAnimation = new Explode();                
                explodeAnimation.SetDuration(500);                
                TransitionSet set = new TransitionSet();                           
                set.AddTransition(changeBoundsAnimation);
                set.AddTransition(explodeAnimation);
                set.SetOrdering(TransitionOrdering.Sequential);

                TransitionManager.BeginDelayedTransition(_rootView, set);

                RelativeLayout.LayoutParams layoutParams =  new RelativeLayout.LayoutParams(HUGE_SIZE, HUGE_SIZE);
                layoutParams.AddRule(LayoutRules.CenterInParent);
                _explodingView.LayoutParameters = layoutParams;
                _explodingView.Visibility = ViewStates.Invisible;
            }
            else if (e == DismissEventAction)
            {
                _explodingView.SetBackgroundColor(Color.Black);
            }
        }
    }

}