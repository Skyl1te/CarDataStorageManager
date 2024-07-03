// Interface defining methods for car management operations
using System.Collections.Generic;

namespace CarDataStorageManager.Interfaces
{
    public interface ICarManager
    {
        void ShowAllCars();
        string GetCarByRegNumber(string regNumber);
        IEnumerable<string> GetCarsByCompany(string company);
        void RemoveCarsWithHighMileage(int thresholdMileage);
        void GenerateRandomCars(int N);
    }
}
