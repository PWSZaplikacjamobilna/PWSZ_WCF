using System;
using System.IO;

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
                throw new ArgumentNullException("composite");
            if (composite.BoolValue)
                composite.StringValue += "Suffix";
            return composite;
        }

        public string Logowanie(string login, string haslo)
        {
            var db = new aplikacjaPWSZEntities2();
            try
            {
                var user = db.Rejestracja.Find(login).Haslo.Equals(haslo);

                if (user)
                {
                    var toUpdate = db.Rejestracja.Find(login);
                    var random = GetRandomString();

                    db.Rejestracja.Attach(toUpdate);
                    var entry = db.Entry(toUpdate);
                    entry.Property(e => e.Coockie).CurrentValue = random;
                    entry.Property(e => e.DateCoockie).CurrentValue = DateTime.Now;
                    db.SaveChanges();
                    return random;
                }
                return "false";
            }
            catch (NullReferenceException ex)
            {
                return "false";
            }
        }

        public string Rejestracja(string indeks, string haslo, string kierunek, string rok, string grupaW, string grupaL, string promotor)
        {
            var db = new aplikacjaPWSZEntities2();
            try
            {
                
                var user = db.Rejestracja.Find(indeks);
                return "jest";
            }
            catch (NullReferenceException ex)
            {
                var newUser = new Rejestracja();
                newUser.NumerIndeksu = indeks;
                newUser.GrupaWykladowa = grupaW;
                newUser.GrupaLaboratoryjna = grupaL;
                newUser.Haslo = haslo;
                newUser.Kierunek = kierunek;
                newUser.Promotor = promotor;
                
                var random = GetRandomString();
                newUser.Coockie = random;
                newUser.DateCoockie = DateTime.Now;

                db.Rejestracja.Add(newUser);
                try
                {
                    db.SaveChanges();
                    return random;
                }
                catch (Exception e)
                {
                    return "err "+e;
                }
            }
        }

        public static string GetRandomString()
        {
            var path = Path.GetRandomFileName();
            path = path.Replace(".", "");
            return path;
        }
    }
}