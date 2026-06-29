using Best_Practices.Models;
using System.Collections.Generic;

namespace Best_Practices.Infraestructure.Storage
{
    // Implementacion en memoria, util mientras el equipo de BD termina
    // el esquema. Se registra como Singleton en el contenedor de DI
    // (ver ServicesConfiguration), no como un Singleton estatico manual.
    public class InMemoryVehicleStore : IVehicleStore
    {
        public ICollection<Vehicle> Vehicles { get; } = new List<Vehicle>();
    }
}
