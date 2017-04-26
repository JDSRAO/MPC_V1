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
using Android.Content.PM;
using MotorPremiumCalculator.Shared.Repository;
using MotorPremiumCalculator.Droid.Fragments;

namespace MotorPremiumCalculator.Droid.Activities
{
    [Activity(NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class TwoWheelerActActivity : Activity
    {
        Spinner ddl_zones;
        Spinner ddl_cc;
        EditText selectedRegdDate;
        string vechicleAgeInDays;
        Spinner ddl_paToPillioRiderValues;
        RadioButton rb_paToPillioRiderNo;
        RadioButton rb_PAToOwnerDriverYes;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.layout_twoWheelerAct);
            //ActionBar.SetHomeButtonEnabled(true);
            //ActionBar.SetDisplayHomeAsUpEnabled(true);
            ddl_zones = FindViewById<Spinner>(Resource.Id.ddl_zones);
            ddl_cc = FindViewById<Spinner>(Resource.Id.ddl_CC);
            selectedRegdDate = FindViewById<EditText>(Resource.Id.et_dateSelect);
            selectedRegdDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
            selectedRegdDate.Clickable = true;
            selectedRegdDate.Focusable = false;
            selectedRegdDate.Click += SelectedRegdDate_Click;
            ddl_paToPillioRiderValues = FindViewById<Spinner>(Resource.Id.ddl_value);
            ddl_paToPillioRiderValues.Enabled = false;
            var paToPillioRiderArrayAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, Resources.GetStringArray(Resource.Array.twoWheelerPaPillionRiderValues_array));
            ddl_paToPillioRiderValues.Adapter = paToPillioRiderArrayAdapter;
            rb_paToPillioRiderNo = FindViewById<RadioButton>(Resource.Id.rb_paToPillioRiderNo);
            rb_paToPillioRiderNo.CheckedChange += Rb_paToPillioRiderNo_CheckedChange;
            rb_PAToOwnerDriverYes = FindViewById<RadioButton>(Resource.Id.rb_PAToOwnerDriverYes);
            Button calculate = FindViewById<Button>(Resource.Id.btn_Calculate);
            calculate.Click += Calculate_Click;
        }

        private void Rb_paToPillioRiderNo_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if(!rb.Checked)
            {
                ddl_paToPillioRiderValues.Clickable = true;
                ddl_paToPillioRiderValues.Enabled = true;
            }
            else
            {
                ddl_paToPillioRiderValues.Clickable = false;
                ddl_paToPillioRiderValues.Enabled = false;
            }
        }

        private void SelectedRegdDate_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                selectedRegdDate.Text = time.ToShortDateString();
                vechicleAgeInDays = Convert.ToString((DateTime.Now.Date - time.Date).TotalDays);

            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            string selectedZone = ddl_zones.SelectedItem.ToString();
            string selectedCc = ddl_cc.SelectedItem.ToString();
            var vAge = (Convert.ToDouble(vechicleAgeInDays) / 365);
            int paToPillionRiderAmount = Convert.ToInt32(ddl_paToPillioRiderValues.SelectedItem.ToString());
            bool isPatoDriver = rb_PAToOwnerDriverYes.Checked;
            int actpremium = TwoWheelerActService.GetTwoWheelerActPremium(selectedZone, selectedCc, (int)vAge, isPatoDriver, ddl_paToPillioRiderValues.Clickable, paToPillionRiderAmount);
            EditText etAct = FindViewById<EditText>(Resource.Id.et_TotalAmount);

            etAct.Text = actpremium.ToString();
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