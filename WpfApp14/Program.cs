using ContactInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WorkingHours;

#region ---------- Working Hours Namespace ----------
namespace WorkingHours
{
    [DataContract]
    public class WorkingHour
    {
        [DataMember]
        public string StoresAndRestaurants = "10:00 - 22:00";
        [DataMember]
        public string Supermarket = "09:00 - 23:00";
        [DataMember]
        public string Cinema = "Son seansa qədər";

        public WorkingHour() { }

        public WorkingHour(in string cinema,
            in string supermarket,
            in string storesAndRestaurants)
        {

            StoresAndRestaurants = storesAndRestaurants;
            Supermarket = supermarket;
            Cinema = cinema;
        }
        public string WorkingInfo()
        {
            return ("Mağazalar və restoranlar: " + StoresAndRestaurants) +
                ("Supermarket: " + Supermarket) +
                ("Kinoteatr: " + Cinema);
        }
    }

}

namespace ContactInfo
{
    [DataContract]
    public class Contact
    {
        [DataMember]
        public string Phone = "(012) 404 82 42";

        [DataMember]
        public string Email = "info.ganjlikmall@pashamalls.az";

        [DataMember]
        public string Address = "Fətəli xan Xoyski 14, Nərimanov, Bakı";

        public Contact() { }

        public Contact(in string phone, in string email, in string address)
        {
            Phone = phone;
            Email = email;
            Address = address;
        }

        public string ContactInfo()
        {
            return ("Telefon: " + Phone) +
                ("Email: " + Email) +
                ("Ünvan: " + Address);
        }
    }
}

#endregion

public static partial class Checks
{
    public static bool IsValidGmail(in string _gmail)
    {
        return false;
    }
}



//=============================================
//=============================================




#region ---------- News ----------

[DataContract]
public class News
{

    [DataMember]
    public string? Headline = null;


    [DataMember]
    public string? Content = null;


    [DataMember]
    public string? News_Image_Path = null;

    public News() { }
    public News(in string headline, in string content, in string image_Path)
    {
        Headline = headline;
        Content = content;
        News_Image_Path = image_Path;
    }
    public News(in News news)
    {
        Headline = news.Headline;
        Content = news.Content;
        News_Image_Path = news.News_Image_Path;
    }

    public string Display()
    {
        return ("Headline: " + Headline) +
            ("Content: " + Content) +
            ("Image Path: " + News_Image_Path);
    }
}

#endregion




//=============================================
//=============================================




#region ---------- Application form ----------

#region ----------  Inquiry Form ----------
[DataContract]
public class RentalApplication
{
    [DataMember]
    public PersonalInfo? PersonalInfo { get; set; }

    [DataMember]
    public string? ProductOrServiceType { get; set; }

    //Required area

    [DataMember]
    public TradeArea? TradeArea { get; set; }

    [DataMember]
    public string? _UtilitiesRequirement { get; set; }

    //Operation type and address
    [DataMember]
    public RetailOutlet? RetailOutlet { get; set; }

    [DataMember]
    public string? AdditionalQuestionsAndComments { get; set; }

    [DataMember]
    public string? RentalApplicationID { get; set; }

    public RentalApplication() { }

    public RentalApplication(in RentalApplication other)
    {
        PersonalInfo = other.PersonalInfo != null ? new PersonalInfo(other.PersonalInfo) : null;

        ProductOrServiceType = other.ProductOrServiceType;

        TradeArea = other.TradeArea != null ? new TradeArea(other.TradeArea) : null;

        _UtilitiesRequirement = other._UtilitiesRequirement;

        RetailOutlet = other.RetailOutlet != null ? new RetailOutlet(other.RetailOutlet) : null;

        AdditionalQuestionsAndComments = other.AdditionalQuestionsAndComments;

        RentalApplicationID = other.RentalApplicationID;
    }

    public RentalApplication(
        in PersonalInfo personalInfo,
        in string productOrServiceType,
        in TradeArea tradeArea,
        in string utilitiesRequirement,
         in RetailOutlet retailOutlet,
        in string additionalQuestionsAndComments)
    {
        PersonalInfo = personalInfo;
        ProductOrServiceType = productOrServiceType;
        TradeArea = tradeArea;
        _UtilitiesRequirement = utilitiesRequirement;
        AdditionalQuestionsAndComments = additionalQuestionsAndComments;
        RetailOutlet = retailOutlet;
        RentalApplicationID = new string(
            Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 6).
            Select(s => s[new Random().Next(s.Length)]).ToArray()
            );
    }
}
#endregion

#region ---------- Contact Info ----------
[DataContract]
public class Person
{

    [DataMember]
    public string _Name { get; set; }
    [DataMember]
    public string _Surname { get; set; }

    public Person() { }


    public Person(in string name, in string surname)
    {
        _Name = name;
        _Surname = surname;
    }


    public Person(in Person person)
    {
        _Name = person._Name;
        _Surname = person._Surname; ;
    }


    public string GetInfo()
    {
        return $"{_Name} {_Surname}\n";
    }
}

[DataContract]
public class IdentifyingInformation
{
    [DataMember]
    public string _Gmail = "Empty";
    [DataMember]
    public string _PhoneNumber = "Empty";
    [DataMember]
    private const byte _PhoneNumberSize = 20;

    public IdentifyingInformation() { }

    public IdentifyingInformation(in IdentifyingInformation identifInfo)
    {
        _Gmail = identifInfo._Gmail;
        _PhoneNumber = identifInfo._PhoneNumber;
    }

    public IdentifyingInformation(in string phoneNumber, in string gmail)
    {
        _PhoneNumber = phoneNumber;
        _Gmail = gmail;
    }

    public string GetInfo()
    {
        return _PhoneNumber + "\n" + _Gmail + "\n";
    }
}

[DataContract]
public class PersonalInfo
{
    [DataMember]
    public string? TradeMark { get; set; }

    [DataMember]
    public Person? Person { get; set; }

    [DataMember]
    public IdentifyingInformation? Identifying { get; set; }

    public PersonalInfo() { }

    public PersonalInfo(in PersonalInfo other)
    {
        TradeMark = other.TradeMark;
        Person = new Person(other.Person);
        Identifying = new IdentifyingInformation(other.Identifying);
    }

    public PersonalInfo(in string tradeMark, in Person person, in IdentifyingInformation identifying)
    {
        TradeMark = tradeMark;
        Person = new Person(person);
        Identifying = new IdentifyingInformation(identifying);
    }
}
#endregion

#region ---------- Required area ----------
[DataContract]
public class TradeArea
{
    //Торговая зона
    [DataMember]
    public double? TradingArea { get; set; }
    // Область хранения
    public double? StorageArea { get; set; }

    [DataMember]
    //Площадь склада 
    public double? WarehouseArea { get; set; }

    public TradeArea() { }

    public TradeArea(TradeArea other)
    {
        TradingArea = other.TradingArea;
        StorageArea = other.StorageArea;
        WarehouseArea = other.WarehouseArea;
    }

    public TradeArea(in double tradingArea, in double storageArea, in double warehouseArea)
    {
        TradingArea = tradingArea;
        StorageArea = storageArea;
        WarehouseArea = warehouseArea;
    }
}

//[Serializable]
//public enum UtilitiesRequirement
//{
//    Water = 1,
//    Sewage = 2,
//    Electricity = 4
//}


#endregion

#region ----------  Operation type and address ----------
[DataContract]
public class RetailOutlet
{
    [DataMember]
    public byte? Number { get; set; }
    [DataMember]
    public string? Address { get; set; }
    [DataMember]
    public string? OperationType { get; set; }


    public RetailOutlet() { }

    public RetailOutlet(in byte number, in string address, in string operationType)
    {
        Number = number;
        Address = address;
        OperationType = operationType;
    }
    public RetailOutlet(in RetailOutlet other)
    {
        Number = other.Number;
        Address = other.Address;
        OperationType = other.OperationType;
    }
}
#endregion

#endregion




//=============================================
//=============================================




#region ----------- Brands  -----------

[DataContract]
public class PlaceInfo
{
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public sbyte Floor { get; set; }
    [DataMember]
    public string News_Image_Path { get; set; }
    [DataMember]
    public string Telephone { get; set; }
    [DataMember]
    public string Description { get; set; }


    public PlaceInfo() { }

    //Telephone - Ne obazatelno
    public PlaceInfo(
        in string name,
        in sbyte floor,
        in string description,
        in string news_Image_Path,
        in string telephone)
    {
        Name = name;
        Floor = floor;
        Description = description;
        News_Image_Path = news_Image_Path;
        Telephone = telephone;
    }

    public PlaceInfo(in PlaceInfo other)
    {
        Name = other.Name;
        Floor = other.Floor;
        Telephone = other.Telephone;
        Description = other.Description;
    }
}

#endregion




//=============================================
//=============================================

#region ---------- Save data ----------
[DataContract]
public class FileMenager
{
    private static string _Server { get; set; } = Path.Combine("C:", "Server");
    public static string Brands { get; set; } = Path.Combine("C:", "Server", "Brands");
    public static string RentalApplicants { get; set; } = Path.Combine(_Server, "RentalApplicants");

    public FileMenager()
    {
        if (Directory.Exists(_Server) && Directory.Exists(RentalApplicants))
        {
            return;
        }
        Directory.CreateDirectory(_Server);
        Directory.CreateDirectory(RentalApplicants);
    }

    [DataMember]
    public static InfoManager Data = new InfoManager();

    [DataMember]
    public static List<RentalApplication> RentalApplicationList = new List<RentalApplication>();


    public static void RentalApplicationListRemoveAt(int index)
    {
        if (index >= RentalApplicationList.Count)
        {
            return;
        }
        File.Delete(Path.Combine(RentalApplicants, RentalApplicationList[index].RentalApplicationID + ".xml"));
        RentalApplicationList.RemoveAt(index);
    }

    public static void BrandsWriteToFile()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(InfoManager));

        using (FileStream writeToFile = new FileStream(Brands + ".xml", FileMode.Create))
        {
            xmlSerializer.Serialize(writeToFile, Data);
        }
    }

    public static void BrandsReadToFile()
    {
        if (!File.Exists(Brands + ".xml")) { return; }
        try
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(InfoManager));

            using (FileStream readFromFile = new FileStream(Brands + ".xml", FileMode.Open))
            {
                Data = (InfoManager)xmlSerializer.Deserialize(readFromFile);
            }
        }
        catch (Exception ex)
        {
            return;
        }
    }

    public static void RentalApplicationWriteToFile(RentalApplication item)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(RentalApplication));

        using (FileStream writeToFile = new FileStream(
            Path.Combine(RentalApplicants, item.RentalApplicationID + ".xml"), FileMode.Create))
        {
            xmlSerializer.Serialize(writeToFile, item);
        }

        RentalApplicationList.Add(item);
    }

    public static void RentalApplicationListWriteToFile()
    {
        foreach (var item in RentalApplicationList)
        {
            RentalApplicationWriteToFile(item);
        }
    }
    public static void RentalApplicationReadAllFiles()
    {
        try
        {
            foreach (var file in Directory.GetFiles(RentalApplicants))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(RentalApplication));

                using (FileStream readFromFile = new FileStream(file, FileMode.Open))
                {
                    RentalApplication rentalApp = (RentalApplication)xmlSerializer.Deserialize(readFromFile);
                    RentalApplicationList.Add(rentalApp);
                }

            }

        }
        catch (Exception)
        {

            return;
        }
    }
}


[DataContract]
public class InfoManager
{
    public InfoManager() { }

    [DataMember]
    public List<PlaceInfo> StorePlaces = new List<PlaceInfo>();

    [DataMember]
    public List<PlaceInfo> RestaurantPlaces = new List<PlaceInfo>();

    [DataMember]
    public List<PlaceInfo> EntertainmentPlaces = new List<PlaceInfo>();

    [DataMember]
    public WorkingHour WorkingHour = new WorkingHour();

    [DataMember]
    public Contact ContactInformation = new Contact();

    [DataMember]
    public List<News> News = new List<News>();

    [DataMember]
    public string MainImagePath { get; set; } = "https://www.chapmantaylor.com/images/uploads/News/GnajlikLRNews.JPG";

    public static List<PlaceInfo> FindPlacesByFloor(sbyte floor)
    {
        InfoManager Data = FileMenager.Data;
        return Data.StorePlaces.Concat(Data.RestaurantPlaces).Concat(Data.EntertainmentPlaces)
                                .Where(place => place.Floor == floor)
                                .ToList();
    }
}

#endregion
