// Implements IDataStorage interface for reading and saving XML files
using CarDataStorageManager.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CarDataStorageManager
{
    public class XmlDataStorage : IDataStorage
    {
        private readonly string filePath;

        public XmlDataStorage(string filePath)
        {
            this.filePath = filePath;
        }

        // Method to read car data from XML file
        public void Read()
        {
            try
            {
                // Deserialize XML file into a list of Car objects
                XmlSerializer serializer = new XmlSerializer(typeof(List<Car>));

                using (StreamReader reader = new StreamReader(filePath))
                {
                    List<Car> cars = (List<Car>)serializer.Deserialize(reader);

                    // Display cars
                    foreach (var car in cars)
                    {
                        Console.WriteLine($"RegNumber: {car.RegNumber}, Model: {car.Model}, Company: {car.Company}, " +
                            $"Color: {car.Color}, Mileage: {car.Mileage}, IsRepainted: {car.IsRepainted}");
                        Console.WriteLine("--------------");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading XML file: {ex.Message}");
            }
        }


        // Method to save cars list to XML file
        public void Save(string xmlFilePath)
        {
            try
            {
                List<Car> _cars = new List<Car>();

                // Assuming CarManager.cars is where your car data is stored
                foreach (var carDetails in new CarManager().cars)
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

                using (TextWriter writer = new StreamWriter(xmlFilePath))
                {
                    serializer.Serialize(writer, _cars);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving cars to XML: {ex.Message}");
            }
        }


    }

}
