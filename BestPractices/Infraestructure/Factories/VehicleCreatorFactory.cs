using System;
using System.Collections.Generic;

namespace Best_Practices.Infraestructure.Factories
{
    // Implementacion del resolver de Factory Method.
    // Agregar un modelo nuevo en el futuro = agregar una clase Creator
    // + una linea aqui. El HomeController y la vista no necesitan
    // cambios para que el nuevo modelo este disponible vía el
    // endpoint generico AddVehicle(modelKey).
    public class VehicleCreatorFactory : IVehicleCreatorFactory
    {
        private readonly Dictionary<string, Func<Creator>> _creators =
            new Dictionary<string, Func<Creator>>(StringComparer.OrdinalIgnoreCase)
            {
                { "Mustang", () => new FordMustangCreator() },
                { "Explorer", () => new FordExplorerCreator() },
                { "Escape", () => new FordEscapeCreator() },
            };

        public Creator Resolve(string modelKey)
        {
            if (modelKey != null && _creators.TryGetValue(modelKey, out var factoryMethod))
            {
                return factoryMethod();
            }

            throw new ArgumentException($"No existe un Creator registrado para el modelo '{modelKey}'.");
        }
    }
}
