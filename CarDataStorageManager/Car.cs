using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Represents a Car object with various properties
namespace CarDataStorageManager
{
    public class Car
    {
        public string RegNumber { get; set; }
        public string Model { get; set; }
        public string Company { get; set; }
        public string Color { get; set; }
        public string Mileage { get; set; }
        public string IsRepainted { get; set; }
    }
}
