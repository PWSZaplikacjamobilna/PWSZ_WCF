using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;



namespace PWSZ_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string Logowanie(string login, string haslo);

        [OperationContract]
        string LogowanieCookie(string cookie);
        [OperationContract]
        string RejstracjaDane();
        [OperationContract]
        DaneRejestracji RejstracjaDane2();

        [OperationContract]
        List<News> NewsData();

        [OperationContract]
        AktualneZajecia Aktualne(string numer);

        [OperationContract]
        string Rejestracja(string indeks, string haslo, int kierunek, int rok, int grupaW, int grupaL, int promotor, int specjalnosc);

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

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
    [DataContract]
    public class DaneRejestracji
    {
        [DataMember]
        public List<Kierunki> kierunki = new List<Kierunki>();
        [DataMember]
        public List<Specjalnosc> specjalnosc = new List<Specjalnosc>();
        [DataMember]
        public List<Rok> rok = new List<Rok>();
        [DataMember]
        public List<Promotor> promotor = new List<Promotor>();
        [DataMember]
        public List<GrupaWykladowa> grupaW = new List<GrupaWykladowa>();
        [DataMember]
        public List<GrupaLaboratoryjna> grupaL = new List<GrupaLaboratoryjna>();

    }
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

    }
    public class News
    {
        public string komunikat;
    }

}