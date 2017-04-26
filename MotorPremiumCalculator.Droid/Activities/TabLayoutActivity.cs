using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics.Drawables;
using Android.Content.PM;

namespace MotorPremiumCalculator.Droid.Activities
{
    [Activity(NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class TabLayoutActivity : TabActivity
    {
        private const string appNamespace = "MotorPremiumCalculator.Droid.Activities";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_tab);
            // Create your application here
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            string vechicleType = Intent.GetStringExtra("vechicleType");
            Type vechicle = Type.GetType(appNamespace + "." + vechicleType);

            CreateTab(vechicle, vechicle.Name, "Calculation", Resource.Drawable.ic_action_input);
            CreateTab(typeof(DetailsActivity), "DetailsActivity", "Details", Resource.Drawable.ic_action_details_icon);
        }

        private void CreateTab(Type activityType, string tag, string label, int drawableId)
        {
            var intent = new Intent(this, activityType);
            intent.AddFlags(ActivityFlags.NewTask);
            var spec = TabHost.NewTabSpec(tag);
            switch (drawableId)
            {
                case Resource.Drawable.ic_action_input:
                    var drawableIcon1 = Resources.GetDrawable(Resource.Drawable.ic_action_input);
                    spec.SetIndicator(label, drawableIcon1);
                    break;
                case Resource.Drawable.ic_action_details_icon:
                    var drawableIcon2 = Resources.GetDrawable(Resource.Drawable.ic_action_details_icon);
                    spec.SetIndicator(label, drawableIcon2);
                    break;
                default:
                    break;
            }
            //spec.SetIndicator(label);
            spec.SetContent(intent);

            TabHost.AddTab(spec);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;
                case Resource.Id.menu_refreshForm:
                    Toast.MakeText(this, "Action selected: " + item.TitleFormatted, ToastLength.Short).Show();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}