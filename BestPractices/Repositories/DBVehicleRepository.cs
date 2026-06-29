using Best_Practices.Models;
using System;
using System.Collections.Generic;

namespace Best_Practices.Repositories
{
    // Implementacion pendiente: el equipo de BD aun no entrega el
    // esquema. Mientras tanto, MyVehiclesRepository (in-memory) permite
    // probar toda la funcionalidad de Home/Add/Find/StartEngine/etc.
    // Cuando el esquema este listo, esta clase se completa y el unico
    // cambio necesario en el resto de la app es UNA linea en
    // ServicesConfiguration.cs (cambiar el tipo registrado para
    // IVehicleRepository). Ni el controlador ni las vistas se tocan,
    // porque ambas dependen de la abstraccion IVehicleRepository
    // (Dependency Inversion Principle), no de la implementacion concreta.
    public class DBVehicleRepository : IVehicleRepository
    {
        public void AddVehicle(Vehicle vehicle)
        {
            throw new NotImplementedException("Pendiente: esquema de base de datos aun no entregado por el equipo de BD.");
        }

        public Vehicle Find(string id)
        {
            throw new NotImplementedException("Pendiente: esquema de base de datos aun no entregado por el equipo de BD.");
        }

        public ICollection<Vehicle> GetVehicles()
        {
            throw new NotImplementedException("Pendiente: esquema de base de datos aun no entregado por el equipo de BD.");
        }
    }
}
