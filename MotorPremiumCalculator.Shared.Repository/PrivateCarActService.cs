using MotorPremiumCalculator.Shared.Repository.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MotorPremiumCalculator.Shared.Repository
{
    public class PrivateCarActService
    {
        static PrivateCarDb db = null;
        static PrivateCarActService()
        {
            db = new PrivateCarDb(DbPath);
        }

        static string DbPath //generate platform specific path for db file
        {
            get
            {
                var dbPath = string.Empty;
#if __ANDROID__
                dbPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
#elif __IOS__
                dbPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); 
                Path.Combine(dbPath,"..","Library");
#elif WINDOWS_PHONE
#endif
                //return Path.Combine(dbPath, "MotorPremiumCalculator.db3");
                return Path.Combine(dbPath, DbDetails.DbName);
            }
        }

        public static int GetPrivateCarActPremium(string selectedZone, string selectedCc)
        {
            PrivateCar privateCarData = db.GetPrivateCarActData(selectedZone, Convert.ToInt32(selectedCc));
            return privateCarData.ActPremium;
        }
    }
}
