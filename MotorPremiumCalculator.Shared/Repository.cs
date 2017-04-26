using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MotorPremiumCalculator.Shared
{
    public class Repository
    {
        static MotorPremiumCalculatorDB db = null;
        static Repository()
        {
            db = new MotorPremiumCalculatorDB(DbPath);
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
                return Path.Combine(dbPath, "MotorPremiumCalculator.db3");
            }
        }
    }
}
