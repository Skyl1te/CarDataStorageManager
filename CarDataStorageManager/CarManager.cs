// Manages operations related to cars, implements ICarManager interface
using CarDataStorageManager.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CarDataStorageManager
{
    public class CarManager : ICarManager
    {
        // Path to the XML file containing car data
        private static string xmlPath = @"C:\Users\chuch\source\repos\CarDataStorageManager\CarDataStorageManager\bin\Debug\net8.0\cars.xml";
        private XDocument xmlDoc;
        // List to store car details as strings
        public List<string> cars = new List<string>();

        // Method to read car data from XML
        public XmlTextReader reader = new XmlTextReader(xmlPath);

        public void ReadXmlFile()
        {
            // Check if XML file exists at the specified path
            if (!File.Exists(xmlPath))
            {
                Console.WriteLine("Error: XML file not found at specified path.");
                return;
            }

            try
            {
                // Create an XmlTextReader to read the XML file
                    while (reader.Read())
                    {
                        // Check if the current node is a Car element
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Car")
                        {
                            string regNumber = "";
                            string model = "";
                            string company = "";
                            string color = "";
                            string mileage = "";
                            string isRepainted = "";

                            // Read through child nodes of Car element
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Element)
                                {
                                    // Read element values based on their names
                                    switch (reader.Name)
                                    {
                                        case "RegNumber":
                                            regNumber = reader.ReadElementContentAsString();
                                            break;
                                        case "Model":
                                            model = reader.ReadElementContentAsString();
                                            break;
                                        case "Company":
                                            company = reader.ReadElementContentAsString();
                                            break;
                                        case "Color":
                                            color = reader.ReadElementContentAsString();
                                            break;
                                        case "Mileage":
                                            mileage = reader.ReadElementContentAsString();
                                            break;
                                        case "IsRepainted":
                                            isRepainted = reader.ReadElementContentAsString();
                                            break;
                                    }
                                }
                                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Car")
                                {
                                    // Construct car details string and add to list
                                    string carDetails = $"RegNumber: {regNumber},\n" +
                                                        $"Model: {model},\n" +
                                                        $"Company: {company},\n" +
                                                        $"Color: {color},\n" +
                                                        $"Mileage: {mileage},\n" +
                                                        $"IsRepainted: {isRepainted}";

                                    cars.Add(carDetails);
                                    break;
                                }
                            }
                        }
                    }
                    reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading XML file: {ex.Message}");
            }
        }

        // Method to display all cars in the list
        public void ShowAllCars()
        {
            foreach (var car in cars)
            {
                Console.WriteLine(car);
                Console.WriteLine("--------------");
            }
        }

        // Method to retrieve car details by registration number
        public string GetCarByRegNumber(string regNumber)
        {
            foreach (var car in cars)
            {
                if (car.Contains(regNumber))
                {
                    return car;
                }
            }
            return $"Car with registration number {regNumber} not found";
        }

        // Method to retrieve cars by company
        public IEnumerable<string> GetCarsByCompany(string company)
        {
            foreach (var car in cars)
            {
                if (car.Contains(company))
                {
                    yield return car;
                    Console.WriteLine("--------------");
                }
            }
            if (cars.Count == 0)
            {
                throw new Exception($"Car with registration number {company} not found");
            }
        }

        // Method to remove cars with mileage above a specified threshold
        public void RemoveCarsWithHighMileage(int thresholdMileage)
        {

            xmlDoc = XDocument.Load(xmlPath);
            // Get all Car elements
            var cars = xmlDoc.Descendants("Car").ToList();

            // List to hold cars to remove
            List<XElement> carsToRemove = new List<XElement>();

            // Iterate through cars and find those to remove
            foreach (var car in cars)
            {
                // Parse mileage
                int mileage = int.Parse(car.Element("Mileage").Value);

                // Check if mileage is above threshold
                if (mileage >= thresholdMileage)
                {
                    carsToRemove.Add(car);
                }
            }

            // Remove cars from XML document
            foreach (var carToRemove in carsToRemove)
            {
                carToRemove.Remove();
            }

            // Save changes back to XML file
            xmlDoc.Save(xmlPath);

            // Optionally, display all cars after removal
            ShowAllCars();
        }

        // Method to save cars list to JSON file
        public void SaveToJson(string jsonFilePath)
        {
            try
            {
                List<Car> _cars = new List<Car>();
                foreach (var carDetails in cars)
                {
                    string[] details = carDetails.Split(",\n", ',');
                    Car car = new Car
                    {
                        RegNumber = details[0].Split(':')[1].Trim(),
                        Model = details[1].Split(':')[1].Trim(),
                        Company = details[2].Split(':')[1].Trim(),
                        Color = details[3].Split(':')[1].Trim(),
                        Mileage = details[4].Split(':')[1].Trim(),
                        IsRepainted = details[5].Split(':')[1].Trim(),
                    };
                    _cars.Add(car);
                }

                // Serialize cars list to JSON
                JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string jsonString = JsonSerializer.Serialize(_cars, jsonSerializerOptions);

                File.WriteAllText(jsonFilePath, jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving cars to JSON: {ex.Message}");
            }

        }

        // Method to save cars list to XML file
        public void SaveToXml(string xmlFilePath)
        {
            try
            {
                List<Car> _cars = new List<Car>();
                foreach (var carDetails in cars)
                {
                    string[] details = carDetails.Split(",\n", ',');
                    Car car = new Car
                    {
                        RegNumber = details[0].Split(':')[1].Trim(),
                        Model = details[1].Split(':')[1].Trim(),
                        Company = details[2].Split(':')[1].Trim(),
                        Color = details[3].Split(':')[1].Trim(),
                        Mileage = details[4].Split(':')[1].Trim(),
                        IsRepainted = details[5].Split(':')[1].Trim(),
                    };
                    _cars.Add(car);
                }

                // Serialize cars list to XML
                XmlSerializer serializer = new XmlSerializer(typeof(List<Car>));
                TextWriter writer = new StreamWriter(xmlFilePath);
                serializer.Serialize(writer, _cars);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving cars to XML: {ex.Message}");
            }

        }

        // Method to generate and save N random cars to XML file
        public void GenerateRandomCars(int N)
        {
            Random random = new Random();
            List<Car> randomCars = new List<Car>();

            string[] models = { "Model S", "Civic", "Corolla", "Mustang", "A4", "3 Series", "Camry", "Accord",
                        "Charger", "Outback", "Cayenne", "Range Rover", "Model X", "Macan", "Model 3", "Impreza", "S-Class", "RAV4", "Explorer" };
            string[] companies = { "Tesla", "Honda", "Toyota", "Ford", "Audi", "BMW", "Subaru", "Porsche", "Land Rover", "Mercedes-Benz", "Dodge" };
            string[] colors = { "Red", "Blue", "White", "Black", "Silver", "Grey", "Green" };
            bool[] isRepaintedOptions = { true, false };

            try
            {
                // Generate random cars
                TextWriter writer = new StreamWriter(xmlPath);
                for (int i = 0; i < N; i++)
                {
                    string regNumber = GenerateRandomRegNumber(random);
                    string model = models[random.Next(models.Length)];
                    string company = companies[random.Next(companies.Length)];
                    string color = colors[random.Next(colors.Length)];
                    int mileage = random.Next(10000, 100000); // Random mileage between 10,000 and 100,000
                    bool isRepainted = isRepaintedOptions[random.Next(isRepaintedOptions.Length)];

                    Car car = new Car
                    {
                        RegNumber = regNumber,
                        Model = model,
                        Company = company,
                        Color = color,
                        Mileage = mileage.ToString(),
                        IsRepainted = isRepainted.ToString()
                    };

                    randomCars.Add(car);
                }

                // Serialize random cars list to XML
                XmlSerializer serializer = new XmlSerializer(typeof(List<Car>));
                serializer.Serialize(writer, randomCars);

                writer.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating random cars: {ex.Message}");
            }
        }

        // Method to generate a random registration number
        private string GenerateRandomRegNumber(Random random)
        {
            // Generate a random registration number with format AA12345
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 7)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
