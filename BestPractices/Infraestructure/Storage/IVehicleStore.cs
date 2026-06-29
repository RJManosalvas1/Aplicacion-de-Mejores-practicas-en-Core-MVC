using Best_Practices.Models;
using System.Collections.Generic;

namespace Best_Practices.Infraestructure.Storage
{
    // Abstraccion del almacenamiento en memoria. Se inyecta via DI con
    // ciclo de vida Singleton (services.AddSingleton), reemplazando al
    // antiguo VehicleCollection (Singleton hecho a mano con campo
    // estatico). Ventaja: en tests unitarios se puede registrar una
    // instancia nueva por test (sin estado global compartido), y en
    // produccion el contenedor de DI sigue garantizando una sola
    // instancia compartida durante la vida de la app.
    public interface IVehicleStore
    {
        ICollection<Vehicle> Vehicles { get; }
    }
}
