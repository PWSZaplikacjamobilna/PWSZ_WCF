using System;
using System.IO;
using System.Data.Entity;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Text;
using System.Net;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.IO.Compression;
using System.Text.RegularExpressions;
using PWSZ_WCF.ServiceReference1;
using System.Net.Mail;
using System.Security.Cryptography;

namespace PWSZ_WCF
{
    public class Service1 : IService1
    {
        string PlanUpdate;
        ServiceReference1.BazusApiClient ser = new ServiceReference1.BazusApiClient();

        public string GetNumer(string cookie)
        {


            var db = new aplikacjaPWSZEntities2();
            DateTime now = DateTime.Now;
            var queryUser = (from c in db.Rejestracja
                             where c.Coockie == cookie
                             select c).First();

            string indeks = queryUser.NumerIndeksu;
            return indeks;
        }

        public LessonPlan test(string cookie, string date)
        {
            var db = new aplikacjaPWSZEntities2();
            DateTime now = DateTime.Now;
            var queryUser = (from c in db.Rejestracja
                             where c.Coockie == cookie
                             select c).First();

            string indeks = queryUser.NumerIndeksu;

            DateTime enteredDate = DateTime.Parse(date);

            System.Net.ServicePointManager.ServerCertificateValidationCallback =
    ((sender, certificate, chain, sslPolicyErrors) => true);

            LessonPlan ser2 = ser.GetDataLessonPlan(indeks, enteredDate);


            LessonPlan nowy = new LessonPlan();

            if (ser2.Lessons.Length != 0)
            {
                if (DateTime.Compare(ser2.Lessons.Last().EndTime, enteredDate) < 0)
                {
                    ser2 = ser.GetDataLessonPlan(indeks, enteredDate.AddDays(1));
                }
            }

            for (int p = 1; ser2.Lessons.Length == 0; p++)
            {

                ser2 = ser.GetDataLessonPlan(indeks, enteredDate.AddDays(p));
            }

            bool found = false;
            foreach (Lesson l in ser2.Lessons)
            {

                if (l.Instructor.Equals(""))
                {
                    l.Instructor = l.GroupName;
                    l.Instructor = l.Instructor.Replace("Seminarium -", "");
                    int dl = l.Instructor.IndexOf(" 2");
                    l.Instructor = l.Instructor.Substring(0, dl);
                }

                TimeSpan eta2 = new TimeSpan();
                eta2 = l.StartTime - enteredDate;
                string eta3 = "err";
                string spec = "E";
                if (eta2.Days >= 1)
                {
                    eta3 = spec + "ETA: " + eta2.Days.ToString() + " Dni";
                }
                else if (eta2.Hours > 0)
                {
                    eta3 = spec + "ETA: " + eta2.Hours.ToString() + "Godzina " + eta2.Minutes.ToString() + "Minut";
                }
                else if (eta2.Hours == 0)
                {
                    if (eta2.Minutes < 40)
                    {
                        spec = "Z";
                    }
                    if (eta2.Minutes < 15)
                    {
                        spec = "R";
                    }
                    eta3 = spec + "ETA: " + eta2.Minutes.ToString() + " Minut";

                }
                if (eta2.TotalMinutes < 0)
                {
                    spec = "E";

                    eta3 = spec + "Pozostało : " + l.EndTime.Subtract(enteredDate).TotalMinutes + " minut";
                }
                l.GroupName = eta3;
                l.LessonId = Int32.Parse(indeks);

                if (DateTime.Compare(l.StartTime, enteredDate) <= 0 & DateTime.Compare(l.EndTime, enteredDate) >= 0)
                {

                    l.SpecialtyId = "NOW";

                }
                if (DateTime.Compare(l.StartTime, enteredDate) > 0 & found == false)
                {
                    found = true;
                    l.SpecialtyId = "NEXT";


                }
            }


            return ser2;
        }

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

        public string ChangeConf(string numertoken, string password)
        {
            string hash = Gethaslo(password);
            string token = numertoken.Split(';')[1];
            string numer = numertoken.Split(';')[0];

            var db = new aplikacjaPWSZEntities2();
            var queryUser = (from c in db.Rejestracja
                             where c.NumerIndeksu == numer
                             select c).First();



            if (queryUser.Token.Equals(token))
            {

                try
                {

                    var random = GetRandomString();


                    var entry = db.Entry(queryUser);
                    entry.Property(e => e.Haslo).CurrentValue = hash;
                    entry.Property(e => e.Coockie).CurrentValue = random;
                    entry.Property(e => e.DateCoockie).CurrentValue = DateTime.Now;
                    db.SaveChanges();
                    return random;
                }
                catch (NullReferenceException ex)
                {
                    return "ERR " + ex;
                }
            }
            else
            {
                return "false";
            }

        }

        public string sendTokenChange(string numer)
        {
            var db = new aplikacjaPWSZEntities2();
            db.Configuration.ProxyCreationEnabled = false;
            var queryUser = db.Rejestracja.Find(numer);


            try
            {


                var random = GetRandomString();
                string token = random.Substring(0, 3);
                var entry = db.Entry(queryUser);
                entry.Property(e => e.Token).CurrentValue = token;


                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                var mail = new MailMessage();
                mail.From = new MailAddress("aplikacjapwsz@gmail.com");
                mail.To.Add(numer + "@pwsz.wloclawek.pl");
                mail.Subject = "Mobilna Aplikacja PWSZ - Zmiana hasła";
                mail.IsBodyHtml = true;
                string htmlBody;
                htmlBody = "Token do zmiany hasła:" + token;
                mail.Body = htmlBody;
                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("aplikacjapwsz@gmail.com", "tylkomirko");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);



                db.SaveChanges();
                return "true";
            }
            catch (NullReferenceException ex)
            {
                return "ERR " + ex;
            }



        }
        string Gethaslo(string pswd)
        {
            var sha1 = new SHA1CryptoServiceProvider();

            var data = Encoding.ASCII.GetBytes(pswd);
            var sha1data = sha1.ComputeHash(data);
            string hashedPassword = System.Text.Encoding.Default.GetString(sha1data);
            return hashedPassword;
        }
        public string Logowanie(string login, string haslo)
        {
            string hash = Gethaslo(haslo);
            System.Diagnostics.Debug.WriteLine("-------------------start");
            var db = new aplikacjaPWSZEntities2();
            try
            {
                var user = db.Rejestracja.Find(login).Haslo.Equals(hash);

                if (user)
                {
                    var toUpdate = db.Rejestracja.Find(login);
                    var random = GetRandomString();

                    db.Rejestracja.Attach(toUpdate);
                    var entry = db.Entry(toUpdate);
                    entry.Property(e => e.Coockie).CurrentValue = random;
                    entry.Property(e => e.DateCoockie).CurrentValue = DateTime.Now;
                    db.SaveChanges();
                    return login + random;
                }
                return "false";
            }
            catch (NullReferenceException ex)
            {
                return "false";
            }
        }

        //public string RejstracjaDane()
        //{

        //    string[,] arr = new string[4, 30];
        //    var db = new aplikacjaPWSZEntities2();

        //    var kierunki = (from c in db.Kierunki
        //                    select c).ToList();
        //    var specjalnosc = (from c in db.Specjalnosc
        //                       select c).ToList();
        //    var rok = (from c in db.Rok
        //               select c).ToList();
        //    var promotor = (from c in db.Promotor
        //                    select c).ToList();
        //    var grupaW = (from c in db.GrupaWykladowa
        //                  select c).ToList();
        //    var grupaL = (from c in db.GrupaLaboratoryjna
        //                  select c).ToList();


        //    foreach (Kierunki k in kierunki)
        //    {
        //        int y = 0;
        //        arr[0, y] = k.Kierunek;
        //        y++;
        //    }

        //    foreach (Specjalnosc s in specjalnosc)
        //    {
        //        int y = 0;
        //        arr[1, y] = s.Specjalność;
        //        y++;
        //    }
        //    foreach (Rok r in rok)
        //    {
        //        int y = 0;
        //        arr[2, y] = r.Rok1;
        //        y++;
        //    }
        //    foreach (Promotor s in promotor)
        //    {
        //        int y = 0;
        //        arr[3, y] = s.Promotor1;
        //        y++;
        //    }


        //    return "";
        //}
        //public DaneRejestracji RejstracjaDane2()
        //{

        //    System.Diagnostics.Debug.WriteLine("-------------------start");

        //    Console.WriteLine("start");
        //    ArrayList arr = new ArrayList();
        //    var db = new aplikacjaPWSZEntities2();
        //    db.Configuration.ProxyCreationEnabled = false;

        //    List<Kierunki> kier = (from c in db.Kierunki
        //                           select c).ToList();
        //    var specjalnosc = (from c in db.Specjalnosc
        //                       select c).ToList();
        //    var rok = (from c in db.Rok
        //               select c).ToList();
        //    var promotor = (from c in db.Promotor
        //                    select c).ToList();
        //    var grupaW = (from c in db.GrupaWykladowa
        //                  select c).ToList();
        //    var grupaL = (from c in db.GrupaLaboratoryjna
        //                  select c).ToList();


        //    DaneRejestracji dane = new DaneRejestracji();
        //    dane.kierunki = kier;
        //    dane.grupaL = grupaL;
        //    dane.grupaW = grupaW;
        //    dane.rok = rok;
        //    dane.specjalnosc = specjalnosc;
        //    dane.promotor = promotor;

        //    return dane;

        //}

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

                return user.NumerIndeksu + random;

            }
            catch (NullReferenceException ex)
            {
                return "false";
            }
        }
        public string RejstracjaPotwierdzenie(string numer, string token)
        {
            var db = new aplikacjaPWSZEntities2();
            var queryUser = (from c in db.Rejestracja
                             where c.NumerIndeksu == numer
                             select c).First();



            if (queryUser.Token.Equals(token))
            {

                try
                {

                    var random = GetRandomString();


                    var entry = db.Entry(queryUser);
                    entry.Property(e => e.Coockie).CurrentValue = random;
                    entry.Property(e => e.DateCoockie).CurrentValue = DateTime.Now;
                    db.SaveChanges();
                    return random;
                }
                catch (NullReferenceException ex)
                {
                    return "ERR " + ex;
                }
            }
            else
            {
                return "false";
            }


        }

        public string Rejestracja(string indeks, string haslo)
        {
            string haslo2 = Gethaslo(haslo);

            var db = new aplikacjaPWSZEntities2();
            db.Configuration.ProxyCreationEnabled = false;
            var user = db.Rejestracja.Find(indeks);


            if (user == null)
            {
                var newUser = new Rejestracja();

                newUser.NumerIndeksu = indeks;
                //   newUser.GrupaWykladowa = grupaW;
                //   newUser.GrupaLaboratoryjna = grupaL;
                newUser.Haslo = haslo2;
                //    newUser.Kierunek = kierunek;
                //    newUser.Promotor = promotor;
                //   newUser.Specjalnosc = specjalnosc;
                //    newUser.Rok = rok;
                //       System.Diagnostics.Debug.WriteLine("------bulwa-start" + newUser.NumerIndeksu);
                //       System.Diagnostics.Debug.WriteLine("------bulwa-start" + newUser.GrupaWykladowa);
                //       System.Diagnostics.Debug.WriteLine("------bulwa-start" + newUser.GrupaLaboratoryjna);
                //       System.Diagnostics.Debug.WriteLine("------bulwa-start" + newUser.Kierunek);
                //       System.Diagnostics.Debug.WriteLine("------bulwa-start" + newUser.Promotor);
                //       System.Diagnostics.Debug.WriteLine("------bulwa-start" + newUser.Specjalnosc);
                var random = GetRandomString();
                //newUser.Coockie = random;
                //newUser.DateCoockie = DateTime.Now;
                string token = random.Substring(0, 3);
                newUser.Token = token;
                db.Rejestracja.Add(newUser);
                try
                {


                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    var mail = new MailMessage();
                    mail.From = new MailAddress("aplikacjapwsz@gmail.com");
                    mail.To.Add(indeks + "@pwsz.wloclawek.pl");
                    mail.Subject = "Mobilna Aplikacja PWSZ - Rejestracja";
                    mail.IsBodyHtml = true;
                    string htmlBody;
                    htmlBody = "Token do rejstacji:" + token;
                    mail.Body = htmlBody;
                    SmtpServer.Port = 587;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("aplikacjapwsz@gmail.com", "tylkomirko");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);

                    db.SaveChanges();
                    return indeks;
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
        //public AktualneZajecia Aktualne(string cookie)
        //{
        //    var db = new aplikacjaPWSZEntities2();
        //    DateTime now = DateTime.Now;
        //    var queryUser = (from c in db.Rejestracja where c.Coockie == cookie
        //                select c).First();


        //    var queryZajecia = (from z in db.Zajecia
        //                        join d in db.DniZajec on z.ZajeciaID equals d.ZajeciaID
        //                        join w in db.Wykladowca on z.Wykładowca equals w.WykladowcaID
        //                        where z.Specjalnosc == queryUser.Specjalnosc
        //                        where z.Rok == queryUser.Rok
        //                        where z.GrupaLaboratoryjna == queryUser.GrupaLaboratoryjna
        //                        where z.GrupaWykladowa == queryUser.GrupaWykladowa
        //                        where z.Kierunek == queryUser.Kierunek
        //                        where d.Dzien >= now
        //                        orderby d.Dzien,z.GodzinaRozpoczecia
        //                        select new {z.Sala,z.TypZajec,w.Wykładowca,z.GodzinaRozpoczecia,z.GodzinaZakonczenia,z.Przedmiot,d.Dzien}).Take(5);



        //    AktualneZajecia akt = new AktualneZajecia();
        //    foreach( var data in queryZajecia)
        //    {
        //        DateTime eta = new DateTime();
        //        eta = (DateTime)data.Dzien + (TimeSpan)data.GodzinaRozpoczecia;
        //        TimeSpan eta2 = new TimeSpan();
        //        eta2 = eta - now ;
        //        string eta3 = "err";
        //        string spec = "E";
        //        if(eta2.Days >= 1)
        //        {
        //            eta3 = spec + "ETA: "+eta2.Days.ToString()+" Dni";
        //        }else if(eta2.Hours >0)
        //        {
        //            eta3 = spec + "ETA: " + eta2.Hours.ToString() + "G " + eta2.Minutes.ToString() + "M";
        //        }
        //        else if (eta2.Hours == 0)
        //        {
        //            if(eta2.Minutes < 40)
        //            {
        //                spec = "Z";
        //            }
        //            if (eta2.Minutes < 15)
        //            {
        //                spec = "R";
        //            }
        //            eta3 = spec + "ETA: " + eta2.Minutes.ToString() + " Minut";

        //        }


        //            akt.zajecia.Add(new AktualneZajecie
        //        {


        //            wykladowca = data.Wykładowca,
        //            budynek = "brak",
        //            przedmiot = data.Przedmiot,
        //            GodzinaRoz = (TimeSpan)data.GodzinaRozpoczecia,
        //            GodzinaZak = (TimeSpan)data.GodzinaZakonczenia,
        //            typ = data.TypZajec,
        //            sala = data.Sala,
        //            dzień = (DateTime)data.Dzien,
        //            eta = eta3


        //        });
        //    }

        //    return akt;
        //}

        public LessonPlan[] AktualneNa7(string cookie, string date)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
((sender, certificate, chain, sslPolicyErrors) => true);

            var db = new aplikacjaPWSZEntities2();
            LessonPlan[] akt7 = new LessonPlan[12];
            DateTime enteredDate = DateTime.Parse(date);
            var queryUser = (from c in db.Rejestracja
                             where c.Coockie == cookie
                             select c).First();

            string indeks = queryUser.NumerIndeksu;
            for (int y = 0; y <= 11; y++)
            {

                LessonPlan ser2 = ser.GetDataLessonPlan(indeks, enteredDate);


                foreach (Lesson l in ser2.Lessons)
                {

                    l.LessonId = Int32.Parse(indeks);
                    if (l.Instructor.Equals(""))
                    {
                        l.Instructor = l.GroupName;
                        l.Instructor = l.Instructor.Replace("Seminarium -", "");
                        int dl = l.Instructor.IndexOf(" 2");
                        l.Instructor = l.Instructor.Substring(0, dl);
                    }
                }

                akt7[y] = ser2;
                enteredDate = enteredDate.AddDays(1);
            }

            return akt7;

        }


        public List<Terminarz> AllEvents(string indeks)
        {
            var db = new aplikacjaPWSZEntities2();

            var queryEvents = (from c in db.Terminarz
                               where c.indeks == indeks
                               select c).ToList();

            List<Terminarz> terlist = new List<Terminarz>();


            foreach (var ter in queryEvents)
            {
                terlist.Add(new Terminarz() { indeks = ter.indeks, Nazwa = ter.Nazwa, Data = ter.Data, Opis = ter.Opis });

            }


            return terlist;
        }
        public List<Kontakt> AllContacts()
        {
            var db = new aplikacjaPWSZEntities2();

            var queryEvents = (from c in db.Kontakt
                               select c).ToList();

            List<Kontakt> terlist = new List<Kontakt>();


            foreach (var ter in queryEvents)
            {
                terlist.Add(new Kontakt() { Budynek = ter.Budynek, Email = ter.Email, IDKontakt = ter.IDKontakt, Imię_i_Nazwisko = ter.Imię_i_Nazwisko, Nr_telefonu = ter.Nr_telefonu, Pokoj = ter.Pokoj, Stanowisko = ter.Stanowisko });

            }


            return terlist;
        }

        public string AddEvent(string indeks, string data, string nazwaopis)
        {

            string nazwa = nazwaopis.Split(';')[0];
            string opis = nazwaopis.Split(';')[1];
            try
            {
                var db = new aplikacjaPWSZEntities2();
                DateTime enteredDate = DateTime.Parse(data);

                Terminarz ter = new Terminarz();

                ter.indeks = indeks;
                ter.Data = enteredDate;
                ter.Nazwa = nazwa;
                ter.Opis = opis;

                db.Terminarz.Add(ter);


                db.SaveChanges();
                return "true";
            }
            catch (Exception e)
            {
                return "ERR" + e;
            }

        }
        public string GetUpdatedPlan(string kierunek, string rok)
        {
            try
            {
                string temp = kierunek + " - " + rok + " rok";

                var myRequest = (HttpWebRequest)WebRequest.Create("https://www.pwsz.wloclawek.pl/2013-05-10-07-29-18/plan-zajec");
                myRequest.Method = "GET";
                var myResponse = myRequest.GetResponse();
                var sr = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                var result2 = sr.ReadToEnd();
                sr.Close();
                myResponse.Close();


                byte[] byteArray = Encoding.UTF8.GetBytes(result2);
                //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
                MemoryStream stream2 = new MemoryStream(byteArray);
                MemoryStream stream3 = new MemoryStream(byteArray);
                List<string> found = new List<string>();
                string line;
                using (StreamReader file = new StreamReader(stream2))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.Contains(temp))
                        {
                            found.Add(line);
                        }
                    }
                    //  "/images/student/
                }
                string firstItem = found.ElementAt(0);
                int pFrom = firstItem.IndexOf("href=\"/images/") + "href=\"/images/".Length;
                int pTo = firstItem.LastIndexOf("rok.pdf");

                String result = firstItem.Substring(pFrom, pTo - pFrom);

                //string result = "0111111111111111111111111111111111111111111";
                if (result.Length > 19)
                {
                    List<string> found1 = new List<string>();
                    string line1;
                    using (StreamReader file = new StreamReader(stream3))
                    {
                        while ((line1 = file.ReadLine()) != null)
                        {
                            if (line1.Contains(temp))
                            {
                                found1.Add(line1);
                            }
                        }
                        //  "/images/student/
                    }
                    string firstItem1 = found1.ElementAt(0);
                    int pFrom1 = firstItem1.IndexOf("rok</a><a href=\"/images/") + "rok</a><a href=\"/images/".Length;
                    int pTo1 = firstItem1.LastIndexOf("rok.pdf");

                    String result1 = firstItem1.Substring(pFrom1, pTo1 - pFrom1);
                    result1 = result1.Substring(0, 5);
                    PlanUpdate = result1;







                    result = result1.Substring(0, 5);
                    DateTime today1 = DateTime.Today;
                    string dni1 = today1.ToString("dd");
                    string miesiąc1 = today1.ToString("MM");
                    string rokNow1 = today1.ToString("yyyy");
                    int rokNowint1 = Int32.Parse(rokNow1);

                    int miesNowint1 = Int32.Parse(miesiąc1);
                    int dniNowint1 = Int32.Parse(dni1);



                    //int DniIntNow= Int32.Parse(dni);
                    int DniIntPlan1 = Int32.Parse(result1.Substring(0, 2));

                    int mieIntPlan1 = Int32.Parse(result1.Substring(3, 2));
                    DateTime plan1 = new DateTime(rokNowint1, mieIntPlan1, DniIntPlan1);
                    DateTime test1 = new DateTime(rokNowint1, miesNowint1, dniNowint1);
                    double lul1 = (test1 - plan1).TotalDays;
                    if (lul1 < 0)
                    {
                        lul1 = lul1 * -1;


                    }
                    if (lul1 > 0 & lul1 < 5)
                    {

                        return result;
                    }
                    else
                    {
                        return "null";
                    }






                }

                // string line = File.ReadLines("temp_web.txt").Skip(14).Take(1).First();
                PlanUpdate = result;

                result = result.Substring(0, 5);
                DateTime today = DateTime.Today;
                string dni = today.ToString("dd");
                string miesiąc = today.ToString("MM");
                string rokNow = today.ToString("yyyy");
                int rokNowint = Int32.Parse(rokNow);
                int miesNowint = Int32.Parse(miesiąc);
                int dniNowint = Int32.Parse(dni);

                //int DniIntNow= Int32.Parse(dni);
                int DniIntPlan = Int32.Parse(result.Substring(0, 2));

                int mieIntPlan = Int32.Parse(result.Substring(3, 2));
                DateTime plan = new DateTime(rokNowint, mieIntPlan, DniIntPlan);

                DateTime test = new DateTime(rokNowint, miesNowint, dniNowint);
                double lul = (test - plan).TotalDays;
                if (lul < 0)
                {
                    lul = lul * -1;


                }
                if (lul > 0 & lul < 5)
                {

                    return result;
                }
                else
                {
                    return "null";
                }
            }
            catch (Exception e)
            {
                return "błąd" + e;

            }


        }
    }
}