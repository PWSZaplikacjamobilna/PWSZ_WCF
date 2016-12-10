using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

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
            System.Diagnostics.Debug.WriteLine("-------------------start");
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

        public string RejstracjaDane()
        {

            string[,] arr = new string[4, 30];
            var db = new aplikacjaPWSZEntities2();

            var kierunki = (from c in db.Kierunki
                            select c).ToList();
            var specjalnosc = (from c in db.Specjalnosc
                               select c).ToList();
            var rok = (from c in db.Rok
                       select c).ToList();
            var promotor = (from c in db.Promotor
                            select c).ToList();
            var grupaW = (from c in db.GrupaWykladowa
                          select c).ToList();
            var grupaL = (from c in db.GrupaLaboratoryjna
                          select c).ToList();


            foreach (Kierunki k in kierunki)
            {
                int y = 0;
                arr[0, y] = k.Kierunek;
                y++;
            }

            foreach (Specjalnosc s in specjalnosc)
            {
                int y = 0;
                arr[1, y] = s.Specjalność;
                y++;
            }
            foreach (Rok r in rok)
            {
                int y = 0;
                arr[2, y] = r.Rok1;
                y++;
            }
            foreach (Promotor s in promotor)
            {
                int y = 0;
                arr[3, y] = s.Promotor1;
                y++;
            }


            return "";
        }
        public DaneRejestracji RejstracjaDane2()
        {

            System.Diagnostics.Debug.WriteLine("-------------------start");

            Console.WriteLine("start");
            ArrayList arr = new ArrayList();
            var db = new aplikacjaPWSZEntities2();
            db.Configuration.ProxyCreationEnabled = false;

            List<Kierunki> kier = (from c in db.Kierunki
                                   select c).ToList();
            var specjalnosc = (from c in db.Specjalnosc
                               select c).ToList();
            var rok = (from c in db.Rok
                       select c).ToList();
            var promotor = (from c in db.Promotor
                            select c).ToList();
            var grupaW = (from c in db.GrupaWykladowa
                          select c).ToList();
            var grupaL = (from c in db.GrupaLaboratoryjna
                          select c).ToList();


            DaneRejestracji dane = new DaneRejestracji();
            dane.kierunki = kier;
            dane.grupaL = grupaL;
            dane.grupaW = grupaW;
            dane.rok = rok;
            dane.specjalnosc = specjalnosc;
            dane.promotor = promotor;

            return dane;

        }

        public string LogowanieCookie(string cookie)
        {
            var db = new aplikacjaPWSZEntities2();


            try
            {
                var random = GetRandomString();
                var user = db.Rejestracja.Where(c => c.Coockie == cookie).First();
                db.Rejestracja.Attach(user);
                var entry = db.Entry(user);
                entry.Property(e => e.Coockie).CurrentValue = random;
                entry.Property(e => e.DateCoockie).CurrentValue = DateTime.Now;
                db.SaveChanges();

                return random;

            }
            catch (NullReferenceException ex)
            {
                return "false";
            }
        }


        public string Rejestracja(string indeks, string haslo, int kierunek, int rok, int grupaW, int grupaL, int promotor, int specjalnosc)
        {

            var db = new aplikacjaPWSZEntities2();
            db.Configuration.ProxyCreationEnabled = false;
            var user = db.Rejestracja.Find(indeks);


            if (user == null)
            {
                var newUser = new Rejestracja();

                newUser.NumerIndeksu = indeks;
                newUser.GrupaWykladowa = grupaW;
                newUser.GrupaLaboratoryjna = grupaL;
                newUser.Haslo = haslo;
                newUser.Kierunek = kierunek;
                newUser.Promotor = promotor;
                newUser.Specjalnosc = specjalnosc;
                newUser.Rok = rok;
                System.Diagnostics.Debug.WriteLine("------bulwa-start" + newUser.NumerIndeksu);
                System.Diagnostics.Debug.WriteLine("------bulwa-start" + newUser.GrupaWykladowa);
                System.Diagnostics.Debug.WriteLine("------bulwa-start" + newUser.GrupaLaboratoryjna);
                System.Diagnostics.Debug.WriteLine("------bulwa-start" + newUser.Kierunek);
                System.Diagnostics.Debug.WriteLine("------bulwa-start" + newUser.Promotor);
                System.Diagnostics.Debug.WriteLine("------bulwa-start" + newUser.Specjalnosc);
                var random = GetRandomString();
                newUser.Coockie = random;
                newUser.DateCoockie = DateTime.Now;

                db.Rejestracja.Add(newUser);
                try
                {
                    db.SaveChanges();
                    return random;
                }
                catch (DbEntityValidationException ex)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var failure in ex.EntityValidationErrors)
                    {
                        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                        foreach (var error in failure.ValidationErrors)
                        {
                            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                            sb.AppendLine();
                        }
                    }

                    throw new DbEntityValidationException(
                        "Entity Validation Failed - errors follow:\n" +
                        sb.ToString(), ex
                    ); // Add the original exception as the innerException
                }
            }
            else
            {
                return "jest";
            }


        }

        public static string GetRandomString()
        {
            var path = Path.GetRandomFileName();
            path = path.Replace(".", "");
            return path;
        }
        public string data;
        public List<News> NewsData()
        {
            var myRequest = (HttpWebRequest)WebRequest.Create("https://www.pwsz.wloclawek.pl/komunikaty-dla-studentow");
            myRequest.Method = "GET";
            var myResponse = myRequest.GetResponse();
            var sr = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            var result = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();


            var re1 = @"""og:description"" content=""([^>]+)"" \/>";

            Console.WriteLine();
            var r = new Regex(re1);
            var m = r.Matches(result);
            var mydocpath =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


            List<News> newsy = new List<News>();
            foreach (Match match in m)
            {
                News n = new News();
                n.komunikat = match.Groups[1].Value.Replace("&nbsp;", "").Replace("&quot;", "\"");
                newsy.Add(n);

            }

            return newsy;
        }
        public AktualneZajecia Aktualne(string cookie)
        {
            var db = new aplikacjaPWSZEntities2();
            DateTime now = DateTime.Now;
            var queryUser = (from c in db.Rejestracja
                             where c.Coockie == cookie
                             select c).First();

            var queryZajecia = (from z in db.Zajecia
                                join d in db.DniZajec on z.ZajeciaID equals d.ZajeciaID
                                where z.Specjalnosc == queryUser.Specjalnosc
                                where z.Rok == queryUser.Rok
                                where z.GrupaLaboratoryjna == queryUser.GrupaLaboratoryjna
                                where z.GrupaWykladowa == queryUser.GrupaWykladowa
                                where z.Kierunek == queryUser.Kierunek
                                where d.Dzien >= now
                                orderby d.DniZajecID
                                select new { z.Sala, z.TypZajec, z.Wykladowca, z.GodzinaRozpoczecia, z.GodzinaZakonczenia, z.Przedmiot, d.Dzien }).Take(5);



            AktualneZajecia akt = new AktualneZajecia();
            foreach (var data in queryZajecia)
            {
                akt.zajecia.Add(new AktualneZajecie
                {
                    budynek = "brak",
                    przedmiot = data.Przedmiot,
                    GodzinaRoz = (TimeSpan)data.GodzinaRozpoczecia,
                    GodzinaZak = (TimeSpan)data.GodzinaZakonczenia,
                    typ = data.TypZajec,
                    sala = data.Sala,
                    dzień = (DateTime)data.Dzien


                });
            }

            return akt;
        }
    }
}