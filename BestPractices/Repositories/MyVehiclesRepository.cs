using Best_Practices.Infraestructure.Storage;
using Best_Practices.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Best_Practices.Repositories
{
    // REPOSITORY PATTERN: implementacion en memoria. Ya no depende de
    // un Singleton estatico manual; recibe el almacenamiento (IVehicleStore)
    // inyectado por el contenedor de DI. Esto la hace testeable: en un
    // test unitario se puede inyectar un InMemoryVehicleStore "limpio"
    // sin estado compartido con otros tests.
    public class MyVehiclesRepository : IVehicleRepository
    {
        private readonly IVehicleStore _store;

        public MyVehiclesRepository(IVehicleStore store)
        {
            _store = store;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            _store.Vehicles.Add(vehicle);
        }

        public Vehicle Find(string id)
        {
            return _store.Vehicles.FirstOrDefault(v => v.ID.Equals(new Guid(id)));
        }

        public ICollection<Vehicle> GetVehicles()
        {
            return _store.Vehicles;
        }
    }
}
