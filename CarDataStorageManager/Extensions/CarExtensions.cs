// Extension methods for filtering cars
using System.Collections.Generic;
using System.Linq;

namespace CarDataStorageManager.Extensions
{
    public static class CarExtensions
    {
        // Extension method to filter cars by color
        public static IEnumerable<string> FilterByColor(this IEnumerable<string> cars, string color)
        {
            return cars.Where(c => c.Contains($"Color: {color}"));
        }

        // Extension method to filter cars by company
        public static IEnumerable<string> FilterByCompany(this IEnumerable<string> cars, string company)
        {
            return cars.Where(c => c.Contains($"Company: {company}"));
        }
    }
}
