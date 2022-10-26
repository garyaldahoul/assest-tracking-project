// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Reflection;
using System.Xml.Linq;

Console.WriteLine("Asset Traking Project");
Console.WriteLine("Enter q to quie.....");
Console.WriteLine("=================");

List<Device> devices = new List<Device>();

bool enterName = true;
bool enterBrand = true;
bool enterModel = true;
bool enterOffice = true;
bool enterPurchase = true;
bool enterPrice = true;

string deviceName = "";
string deviceBrand = "";
string deviceModel = "";
string deviceOffice = "";
string devicePurchase = "";
string devicePrice = "";

while (true)
{
    if (enterName)
    {
        Console.Write("Add Device Name : Computer/Mobil...  ");
        string name = Console.ReadLine();
        if (QuitFunction(name))
        {
            break;
        }
        enterName = IsEmpty(name);
        deviceName = name;

    }
    else if (enterBrand)
    {
        Console.Write("Add Device Brand :  ");
        string brand = Console.ReadLine();
        if (QuitFunction(brand))
        {
            break;
        }

        enterBrand = IsEmpty(brand);
        deviceBrand = brand;
    }
    else if (enterModel)
    {
        Console.Write("Add Device Model :  ");
        string model = Console.ReadLine();
        if (QuitFunction(model))
        {
            break;
        }
        enterModel = IsEmpty(model);
        deviceModel = model;
    }
    else if (enterOffice)
    {
        Console.Write("Add Device Office :Sweden/USA/Europ  ");
        string office = Console.ReadLine();
        if (QuitFunction(office))
        {
            break;
        }
        enterOffice = IsEmpty(office);
        deviceOffice = office;
    }
    else if (enterPurchase)
    {
        Console.Write("Add Device Purchase Date : mm/dd/yyyy  ");
        string purchase = Console.ReadLine();
        if (QuitFunction(purchase))
        {
            break;
        }
        else if (!IsValidDate(purchase))
        {
            Console.WriteLine("Date Is Not Valid..  ");
            enterPurchase = true;
        }
        else
        {
            enterPurchase = IsEmpty(purchase);
            enterPurchase = false;
            devicePurchase = purchase;
        }

    }
    else if (enterPrice)
    {
        Console.Write("Add Device Price : ");
        string price = Console.ReadLine();
        if (QuitFunction(price))
        {
            break;
        }
        else if (!price.All(char.IsDigit))
        {
            Console.WriteLine("InvalidEnter...Price Should Be A Number...");
        }

        enterPrice = IsEmpty(price);
        devicePrice = price;

    }
    else
    {
        Device newDevice = new Device(deviceName, deviceBrand, deviceModel, deviceOffice, devicePurchase, devicePrice);
        devices.Add(newDevice);
        //if (deviceName.ToLower().Trim() == "computer")
        //{
        //    Computer newDevice = new Computer(deviceName, deviceBrand, deviceModel,deviceOffice, devicePurchase, devicePrice);
        //    devices.Add(newDevice);

        //}
        //else if (deviceName.ToLower().Trim() == "mobil")
        //{
        //    Mobil newDevice = new Mobil(deviceName, deviceBrand, deviceModel,deviceOffice, devicePurchase, devicePrice);
        //    devices.Add(newDevice);

        //}
        enterName = true;
        enterBrand = true;
        enterModel = true;
        enterOffice = true;
        enterPurchase = true;
        enterPrice = true;
    }

}


List<Device> sortProductsList = devices.OrderBy(device => DateTime.Parse(device.Purchase)).OrderBy(device => device.Name.ToLower().Trim() == "computer").OrderBy(device => device.Name.ToLower().Trim() == "mobil").OrderBy(device => device.Office).ToList();


static bool checkingDate(string input)
{
    DateTime newDate = new DateTime();
    newDate = DateTime.Now;
    DateTime inputValue = Convert.ToDateTime(input);
    DateTime afterThreeYears = inputValue.AddYears(3);
    DateTime finishThreeMonths = afterThreeYears.AddMonths(-3);
    DateTime finishSixMonths = afterThreeYears.AddMonths(-6);

    if (DateTime.Compare(newDate, afterThreeYears) > 0)
    {

        // Console.WriteLine("More Than 3 Years");
        return false;
    }
    if (DateTime.Compare(newDate, finishThreeMonths) > 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }
    else if (DateTime.Compare(newDate, finishSixMonths) > 0)
    {

        Console.ForegroundColor = ConsoleColor.Yellow;
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.White;
    }
    return true;
}


static bool IsValidDate(string input)
{
    DateTime tempObject; ;
    return DateTime.TryParse(input, out tempObject);
}

Console.WriteLine("Type".PadRight(15) + "Brand".PadRight(15) + "Model".PadRight(15) + "Office".PadRight(15) + "Purchase Date".PadRight(15) + "Price in USD".PadRight(15) + "Currency".PadRight(15) +
"Local price today");


foreach (Device device in sortProductsList)
{
    if (checkingDate(device.Purchase))
    {
        Console.WriteLine(
            device.Name.PadRight(15) + device.Brand.PadRight(15) +
            device.Model.PadRight(15) + device.Office.PadRight(15) +
            device.Purchase.PadRight(15) + device.Price.PadRight(15) +
            device.useCurrency(device.Office).PadRight(15) +
            device.gettingLocalPrice(device.useCurrency(device.Office), device.Price));

    }
}

static bool IsEmpty(string input)
{
    if (String.IsNullOrWhiteSpace(input))
    {
        Console.WriteLine("Error... Empty Field...");
        return true;
    }
    return false;

}

static bool QuitFunction(string input)
{

    if (input.ToLower().Trim() == "q")
    {
        return true;
    }
    else
    {
        return false;
    }
}



Console.ReadLine();

class Device
{

    public Device() { }
    public Device(string name, string brand, string model, string office, string purchase, string price)
    {
        Name = name;
        Brand = brand;
        Model = model;
        Office = office;
        Purchase = purchase;
        Price = price;
    }

    public string useCurrency(string office)
    {
        string currency = "";
        string inputValue = office.ToLower().Trim();
        if (inputValue == "usa")
        {
            currency = "USD";
        }
        else if (inputValue == "sweden")
        {
            currency = "SEK";
        }
        else if (inputValue == "europ")
        {
            currency = "EUR";
        }
        else
        {
            currency = "OPS....Office NOt Found!";
        }
        return currency;
    }

    public string gettingLocalPrice(string currency, string price)
    {
        double rate = 1;
        currency = currency.ToLower().Trim();
        if (currency == "sek")
        {
            rate = 10.7;
        }
        else if (currency == "eru")
        {
            rate = 0.99;
        }
        else if (currency == "usd")
        {
            rate = 1;
        }
        double localPrice = Convert.ToDouble(price) * rate;
        return localPrice.ToString();
    }



    public string Name { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Office { get; set; }
    public string Purchase { get; set; }
    public string Price { get; set; }
}

//class Computer:Device
//{
//    public Computer(string name, string brand, string model,string office, string purchase, string price) : base(name,brand,model,office,purchase,price) { }



//}


//class Mobil : Device
//{
//    public Mobil(string name, string brand, string model,string office, string purchase, string price) : base(name, brand, model,office, purchase, price) { }
//}





