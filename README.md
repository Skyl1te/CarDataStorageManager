## Car Data Storage Manager

This project, **Car Data Storage Manager**, provides functionalities to manage operations related to cars using XML as the primary data storage format. It includes classes and interfaces for reading, saving, filtering, and manipulating car data.

### Features

- **ReadXmlFile**: Reads car data from an XML file located at a specified path.
- **ShowAllCars**: Displays details of all cars currently stored in memory.
- **GetCarByRegNumber**: Retrieves details of a car by its registration number.
- **GetCarsByCompany**: Retrieves details of cars belonging to a specific company.
- **RemoveCarsWithHighMileage**: Removes cars from the XML file that have mileage above a specified threshold.
- **SaveToJson**: Saves the current list of cars to a JSON file.
- **SaveToXml**: Saves the current list of cars to an XML file.
- **GenerateRandomCars**: Generates and saves a specified number of random cars to an XML file.
- **IDataStorage**: Interface for reading and saving car data using XML files.
- **ICarManager**: Interface defining operations for managing cars.
- **CarExtensions**: Extension methods for filtering cars based on color and company.
- **XmlDataStorage**: Implements IDataStorage for reading and saving XML files.
- **Car**: Represents a car object with properties like registration number, model, company, color, mileage, and repaint status.

### Usage

To use this project, follow these steps:

1. **Read Car Data**: Use `ReadXmlFile()` to load car data from an XML file.
2. **Display Cars**: Use `ShowAllCars()` to display all loaded cars.
3. **Retrieve Specific Car**: Use `GetCarByRegNumber(regNumber)` to get details of a car by its registration number.
4. **Filter Cars**: Use extension methods like `FilterByColor(color)` and `FilterByCompany(company)` to filter cars based on color or company.
5. **Modify Data**: Use methods like `RemoveCarsWithHighMileage(thresholdMileage)` to remove cars based on specified conditions.
6. **Save Data**: Use `SaveToXml(xmlFilePath)` to save the current list of cars to an XML file.
7. **Generate Random Cars**: Use `GenerateRandomCars(N)` to generate and save a specified number of random cars to an XML file.

### Example

```csharp
static void Main(string[] args)
{
    CarManager carManager = new CarManager();

    // Read car data from XML
    carManager.ReadXmlFile();

    // Display all cars
    carManager.ShowAllCars();

    // Get car by registration number
    string regNumber = "WD4ULU2";
    string carDetails = carManager.GetCarByRegNumber(regNumber);
    Console.WriteLine($"Car details for registration number {regNumber}:\n{carDetails}");

    // Get cars by company
    string company = "Audi";
    var audiCars = carManager.GetCarsByCompany(company);
    Console.WriteLine($"Cars by company {company}:");
    foreach (var car in audiCars)
    {
        Console.WriteLine(car);
        Console.WriteLine("--------------");
    }

    // Remove cars with high mileage
    int thresholdMileage = 95000;
    carManager.RemoveCarsWithHighMileage(thresholdMileage);

    // Generate and save N random cars to XML file
    int N = 5;
    carManager.GenerateRandomCars(N);

    Console.WriteLine("\nPress any key to exit...");
    Console.ReadKey();
}
```

---
