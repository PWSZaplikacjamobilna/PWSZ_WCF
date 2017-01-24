using System.Collections;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Collections.Generic;
using System;
using PWSZ_WCF.ServiceReference1;

namespace PWSZ_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string Logowanie(string login, string haslo);
        [OperationContract]
        string sendTokenChange(string numer);
        [OperationContract]
        string GetNumer(string cookie);
        [OperationContract]
        string ChangeConf(string numertoken, string password);

        [OperationContract]
        LessonPlan test(string cookie, string date);
        [OperationContract]
        string RejstracjaPotwierdzenie(string numer, string token);
        [OperationContract]
        string LogowanieCookie(string cookie);
        //[OperationContract]
        //string RejstracjaDane();
        //[OperationContract]
        //DaneRejestracji RejstracjaDane2();

        [OperationContract]
        List<News> NewsData();
        [OperationContract]
        List<Terminarz> AllEvents(string indeks);

        //[OperationContract]
        //AktualneZajecia Aktualne(string numer);
        [OperationContract]
        LessonPlan[] AktualneNa7(string cookie, string date);
        [OperationContract]
        string AddEvent(string indeks, string data, string nazwaopis);
        [OperationContract]
        List<Kontakt> AllContacts();

        [OperationContract]
        string Rejestracja(string indeks, string haslo);

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);
        [OperationContract]
        string GetUpdatedPlan(string kierunek, string rok);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        [DataMember]
        public bool BoolValue { get; set; }

        [DataMember]
        public string StringValue { get; set; }
    }
    //[DataContract]
    //public class DaneRejestracji
    //{
    //    [DataMember]
    //    public List<Kierunki> kierunki = new List<Kierunki>();
    //    [DataMember]
    //    public List<Specjalnosc> specjalnosc = new List<Specjalnosc>();
    //    [DataMember]
    //    public List<Rok> rok = new List<Rok>();
    //    [DataMember]
    //    public List<Promotor> promotor = new List<Promotor>();
    //    [DataMember]
    //    public List<GrupaWykladowa> grupaW = new List<GrupaWykladowa>();
    //    [DataMember]
    //    public List<GrupaLaboratoryjna> grupaL = new List<GrupaLaboratoryjna>();

    //}
    [DataContract]
    public class Newsy
    {
        [DataMember]
        public List<News> newsy = new List<News>();

    }
    [DataContract]
    public class AktualneZajecia
    {
        [DataMember]
        public List<AktualneZajecie> zajecia = new List<AktualneZajecie>();

    }
    public class AktualneZajecie
    {
        public TimeSpan GodzinaRoz;
        public TimeSpan GodzinaZak;
        public string przedmiot;
        public string wykladowca;
        public string typ;
        public string sala;
        public string budynek;
        public DateTime dzień;
        public string eta;

    }
    public class News
    {
        public string komunikat;
    }



}