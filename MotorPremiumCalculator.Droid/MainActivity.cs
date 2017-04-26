using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Content;
using System.IO;
using Android.Content.Res;

namespace MotorPremiumCalculator.Droid
{
    [Activity(ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = true, Icon = "@mipmap/ic_launcher")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            MoveDbToPhone();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Button btnNext = FindViewById<Button>(Resource.Id.btn_Next);
            btnNext.Click += BtnNext_Click;
        }

        private void MoveDbToPhone()
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string dbName = "MotorPremiumCalculator.db3";
            string dbPath = Path.Combine(path, dbName);
            // Check if your DB has already been extracted.
            if (!File.Exists(dbPath))
            {
                try
                {
                    AssetManager assets = this.Assets;
                    using (BinaryReader br = new BinaryReader(assets.Open("Database/" + dbName)))
                    {
                        using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                        {
                            byte[] buffer = new byte[2048];
                            int len = 0;
                            while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                bw.Write(buffer, 0, len);
                            }
                        }
                    }
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }

        private void BtnNext_Click(object sender, System.EventArgs e)
        {
            Toast.MakeText(this, "btn clicked", ToastLength.Short);
            RadioGroup rbg_policyType = FindViewById<RadioGroup>(Resource.Id.rbg_policyType);
            //RadioButton rb_selecetdPolicyType = FindViewById<RadioButton>(rbg_policyType.CheckedRadioButtonId);
            RadioGroup rbg_vechicleType = FindViewById<RadioGroup>(Resource.Id.rbg_vechicleType);
            //RadioButton rb_selectedVechicleType = FindViewById<RadioButton>(rbg_vechicleType.CheckedRadioButtonId);
            switch (rbg_policyType.CheckedRadioButtonId)
            {
                case Resource.Id.rb_actPolicy:
                    GetSelectedVechicleTypeForActPolicy(rbg_vechicleType);
                    break;
                case Resource.Id.rb_comprehensvePolicy:
                    GetSelectedVechicleTypeForComprehensivePolicy(rbg_vechicleType);
                    break;
                default:
                    break;
            }
        }

        private void GetSelectedVechicleTypeForActPolicy(RadioGroup rbg_vechicleType)
        {
            Intent intent = new Intent(this, typeof(Activities.TabLayoutActivity));
            switch (rbg_vechicleType.CheckedRadioButtonId)
            {
                case Resource.Id.rb_twoWheeler:
                    intent.PutExtra("vechicleType", Resources.GetString(Resource.String.TwoWheelerActActivity));
                    StartActivity(intent);
                    break;
                case Resource.Id.rb_privateCar:
                    intent.PutExtra("vechicleType", Resources.GetString(Resource.String.PrivateCarActActivity));
                    StartActivity(intent);
                    break;
                case Resource.Id.rb_3wheeledPassengerLessThan6Passenger:
                    intent.PutExtra("vechicleType", Resources.GetString(Resource.String.string_twoWheeler));
                    StartActivity(intent);
                    break;
                case Resource.Id.rb_3wheeledPassengerMoreThan6Passenger:
                    break;
                case Resource.Id.rb_3wheelerGoodsAutoPrivate:
                    break;
                default:
                    break;
            }
        }

        private void GetSelectedVechicleTypeForComprehensivePolicy(RadioGroup rbg_vechicleType)
        {
            Intent intent = new Intent(this, typeof(Activities.TabLayoutActivity));
            switch (rbg_vechicleType.CheckedRadioButtonId)
            {
                case Resource.Id.rb_twoWheeler:
                    intent.PutExtra("vechicleType", Resources.GetString(Resource.String.TwoWheelerComprehensiveActivity));
                    StartActivity(intent);
                    break;
                case Resource.Id.rb_privateCar:
                    break;
                case Resource.Id.rb_3wheeledPassengerLessThan6Passenger:
                    break;
                case Resource.Id.rb_3wheeledPassengerMoreThan6Passenger:
                    break;
                case Resource.Id.rb_3wheelerGoodsAutoPrivate:
                    break;
                default:
                    break;
            }
        }
    }
}

