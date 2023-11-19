using System;
using System.Collections.Generic;
using System.IO;

// Тип абонента
enum SubscriberType
{
    Regular,
    Premium,
    VIP
}

// Структура для інформації про абонента
struct CableSubscriber
{
    public int AccountNumber;
    public string FullName;
    public string Address;
    public string PhoneNumber;
    public int ContractNumber;
    public DateTime ContractDate;
    public bool HasDiscount;
    public SubscriberType Type;
    public string TariffPlan;

    // Конструктор для ініціалізації полів структури
    public CableSubscriber(int accountNumber, string fullName, string address, string phoneNumber,
        int contractNumber, DateTime contractDate, bool hasDiscount, SubscriberType type, string tariffPlan)
    {
        AccountNumber = accountNumber;
        FullName = fullName;
        Address = address;
        PhoneNumber = phoneNumber;
        ContractNumber = contractNumber;
        ContractDate = contractDate;
        HasDiscount = hasDiscount;
        Type = type;
        TariffPlan = tariffPlan;
    }

    // Перевизначення методу ToString() для зручного виведення на консоль або запису в файл
    public override string ToString()
    {
        return $"Account Number: {AccountNumber}, FullName: {FullName}, Address: {Address}, " +
               $"Phone Number: {PhoneNumber}, Contract Number: {ContractNumber}, " +
               $"Contract Date: {ContractDate.ToShortDateString()}, Has Discount: {HasDiscount}, " +
               $"Type: {Type}, Tariff Plan: {TariffPlan}";
    }
}

class Program
{
    private const string FileName = "cable_subscribers.txt";

    static void Main()
    {
        List<CableSubscriber> subscribers = new List<CableSubscriber>();

        // Зчитуємо дані з файлу при запуску програми
        ReadDataFromFile(subscribers);

        // Виводимо абонентів на консоль
        DisplaySubscribers(subscribers);

        // Додаємо нового абонента
        AddSubscriber(subscribers);

        // Виводимо оновлену колекцію абонентів
        DisplaySubscribers(subscribers);

        // Записуємо оновлені дані у файл
        WriteDataToFile(subscribers);
    }

    static void ReadDataFromFile(List<CableSubscriber> subscribers)
    {
        if (File.Exists(FileName))
        {
            try
            {
                using (StreamReader reader = new StreamReader(FileName))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        CableSubscriber subscriber = ParseSubscriber(line);
                        subscribers.Add(subscriber);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        }
    }

    static void WriteDataToFile(List<CableSubscriber> subscribers)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(FileName, true))
            {
                foreach (var subscriber in subscribers)
                {
                    writer.WriteLine(subscriber.ToString());
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error writing to file: {ex.Message}");
        }
    }

    static void DisplaySubscribers(List<CableSubscriber> subscribers)
    {
        Console.WriteLine("Cable Subscribers:");
        foreach (var subscriber in subscribers)
        {
            Console.WriteLine(subscriber);
        }
        Console.WriteLine();
    }

    static void AddSubscriber(List<CableSubscriber> subscribers)
    {
        Console.WriteLine("Enter new subscriber details:");

        // Приклад - введення даних з клавіатури. Вам можливо знадобиться реалізувати більше деталей введення.
        Console.Write("Account Number: ");
        int accountNumber = int.Parse(Console.ReadLine());

        Console.Write("Full Name: ");
        string fullName = Console.ReadLine();

        Console.Write("Address: ");
        string address = Console.ReadLine();

        Console.Write("Phone Number: ");
        string phoneNumber = Console.ReadLine();

        Console.Write("Contract Number: ");
        int contractNumber = int.Parse(Console.ReadLine());

        Console.Write("Contract Date (yyyy-MM-dd): ");
        DateTime contractDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Has Discount (true/false): ");
        bool hasDiscount = bool.Parse(Console.ReadLine());

        Console.Write("Subscriber Type (Regular/Premium/VIP): ");
        SubscriberType type = (SubscriberType)Enum.Parse(typeof(SubscriberType), Console.ReadLine());

        Console.Write("Tariff Plan: ");
        string tariffPlan = Console.ReadLine();

        // Додаємо нового абонента до колекції
        CableSubscriber newSubscriber = new CableSubscriber(accountNumber, fullName, address, phoneNumber,
            contractNumber, contractDate, hasDiscount, type, tariffPlan);
        subscribers.Add(newSubscriber);
    }

    static CableSubscriber ParseSubscriber(string line)
    {
        string[] parts = line.Split(',');
        int accountNumber = int.Parse(parts[0].Split(':')[1].Trim());
        string fullName = parts[1].Split(':')[1].Trim();
        string address = parts[2].Split(':')[1].Trim();
        string phoneNumber = parts[3].Split(':')[1].Trim();
        int contractNumber = int.Parse(parts[4].Split(':')[1].Trim());
        DateTime contractDate = DateTime.Parse(parts[5].Split(':')[1].Trim());
        bool hasDiscount = bool.Parse(parts[6].Split(':')[1].Trim());
        SubscriberType type = (SubscriberType)Enum.Parse(typeof(SubscriberType), parts[7].Split(':')[1].Trim());
        string tariffPlan = parts[8].Split(':')[1].Trim();

        return new CableSubscriber(accountNumber, fullName, address, phoneNumber,
            contractNumber, contractDate, hasDiscount, type, tariffPlan);
    }
}
