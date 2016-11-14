using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PWSZ_WCF
{

    public class Service1 : IService1
    {

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string Logowanie(string login, string haslo)
        {
            aplikacjaPWSZEntities db = new aplikacjaPWSZEntities();
            try
            {
              var user = db.Users.Find(Int16.Parse(login)).Haslo.Equals(haslo);

                if (user == true)
                {
                    var toUpdate = db.Users.Find(Int16.Parse(login));
                    string random = GetRandomString();
                    
                    db.Users.Attach(toUpdate);
                    var entry = db.Entry(toUpdate);
                    entry.Property(e => e.Cookie).CurrentValue = random;
                    db.SaveChanges();
                    return random;
                }
                else
                {
                    return "false";
                }
            }
            catch (NullReferenceException ex)
            {
                return "false";
            }


        }

        public static string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path;
        }
    }
}
