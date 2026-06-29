using Best_Practices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Best_Practices.Repositories
{
    public interface IVehicleRepository
    {
        ICollection<Vehicle> GetVehicles();

        void AddVehicle(Vehicle vehicle);

        Vehicle Find(string id);

    }
}
