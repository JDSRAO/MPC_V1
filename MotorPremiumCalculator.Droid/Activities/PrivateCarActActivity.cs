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
using MotorPremiumCalculator.Shared.Repository;

namespace MotorPremiumCalculator.Droid.Activities
{
    [Activity]
    public class PrivateCarActActivity : Activity
    {
        Spinner ddl_zones;
        Spinner ddl_cc;
        Button calculate;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.layout_privateCarAct);
            ddl_cc = FindViewById<Spinner>(Resource.Id.privateCar_ddl_CC);
            ddl_zones = FindViewById<Spinner>(Resource.Id.privateCar_ddl_zones);
            calculate = FindViewById<Button>(Resource.Id.privateCar_Act_btn_Calculate);
            calculate.Click += Calculate_Click;
        }
        
        private void Calculate_Click(object sender, EventArgs e)
        {
            string selectedZone = ddl_zones.SelectedItem.ToString();
            string selectedCc = ddl_cc.SelectedItem.ToString();
            int actPremium = PrivateCarActService.GetPrivateCarActPremium(selectedZone,selectedCc);
            EditText totalAmt = FindViewById<EditText>(Resource.Id.et_privateCar_TotalAmount);
            totalAmt.Text = actPremium.ToString();
        }
    }
}