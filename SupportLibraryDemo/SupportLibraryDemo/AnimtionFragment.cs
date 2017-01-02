using Android.OS;
using Android.Views;
using Android.Support.V4.App;
using Android.Transitions;
using Android.Widget;
using Android.Views.Animations;

namespace SupportLibraryDemo
{
    public class AnimationFragment : Fragment
    {
        private TextView _headerTextView;
        private Button _animationButton;
        private ViewGroup _viewGroup;

        public const long ANIMATION_DURATION = 500;
        public const long ANIMATION_DELAY = 200;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View fragmentView = inflater.Inflate(Resource.Layout.fragment_animation, container, false);

            _viewGroup = fragmentView.FindViewById<ViewGroup>(Resource.Id.lyt_base_anim);
            _headerTextView = fragmentView.FindViewById<TextView>(Resource.Id.tv_header_label);

            _animationButton = fragmentView.FindViewById<Button>(Resource.Id.btn_animation);
            _animationButton.Click += OnAnimationInit;      

            return fragmentView;
        }

        private void OnAnimationInit(object sender, System.EventArgs args)
        {
            bool isVisible = _headerTextView.Visibility == ViewStates.Visible;

            Slide slideAnimation = new Slide(GravityFlags.Top);
            Fade fadeAnimation = new Fade(isVisible ? FadingMode.Out : FadingMode.In);

            fadeAnimation.AddTarget(_headerTextView);

            TransitionSet animationSet = new TransitionSet();
            animationSet.AddTransition(slideAnimation);
            animationSet.AddTransition(fadeAnimation);

            animationSet.SetInterpolator(new AccelerateInterpolator());
            animationSet.SetDuration(ANIMATION_DURATION);
            animationSet.SetOrdering(TransitionOrdering.Together);

            TransitionManager.BeginDelayedTransition(_viewGroup, animationSet);
            
             _headerTextView.Visibility = isVisible ? ViewStates.Invisible : ViewStates.Visible;
        }
    }
}