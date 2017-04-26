using MotorPremiumCalculator.Shared.Repository.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MotorPremiumCalculator.Shared.Repository
{
    public class TwoWheelerActService
    {
        static TwoWheelerDb db = null;
        static TwoWheelerActService()
        {
            db = new TwoWheelerDb(DbPath);
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

        public static int GetTwoWheelerActPremium(string zoneName, string cc, int vechicleAge, bool isPaToDriver, bool isPaToPillionRider, long paToPillionRiderAmount)
        {
            TwoWheeler twoWheelerData = db.GetTwoWheelerData(zoneName,cc,vechicleAge, isPaToDriver, isPaToPillionRider);
            int actPremium = twoWheelerData.ActPremium;
            if (isPaToDriver)
            {
                actPremium += twoWheelerData.PAtoDriver;
            }
            if (isPaToPillionRider)
            {
                double paToPillionRider = paToPillionRiderAmount;

                paToPillionRider *= twoWheelerData.PAtoPillionRider;
                actPremium += (int)paToPillionRider;
            }
            return actPremium;
        }

    }
}
