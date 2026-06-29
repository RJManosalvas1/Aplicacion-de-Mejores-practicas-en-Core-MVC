using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Best_Practices.Models
{
    public abstract class Vehicle : IVehicle
    {
        #region Private properties
        private bool _isEngineOn { get; set; }
        #endregion

        #region Properties
        public readonly Guid ID;
        public virtual int Tires { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public double Gas { get; set; }
        public double FuelLimit { get; set; }

        // Propiedad solicitada por negocio: año del vehiculo.
        // Se agrega como propiedad simple (no como parametro de constructor)
        // para no romper Car/Motocycle ni el resto de invocadores.
        public int Year { get; set; }

        // Contenedor para las ~20 propiedades adicionales que negocio
        // pedira en el siguiente sprint. En vez de agregar 20 propiedades
        // fuertemente tipadas (lo que obligaria a tocar Vehicle, Car,
        // Motocycle y todos los Builders cada vez), se centraliza en un
        // diccionario que el VehicleBuilder llena con sus defaults.
        // Cuando negocio defina las 20 propiedades reales, basta agregar
        // sus defaults en VehicleBuilder (un solo archivo) sin tocar esta clase.
        public Dictionary<string, object> ExtraProperties { get; set; } = new Dictionary<string, object>();

        #endregion

        #region Constructors

        public Vehicle(string color, string brand, string model, double fuelLimit = 10)
        {
            ID = Guid.NewGuid();
            Color = color;
            Brand = brand;
            Model = model;
            FuelLimit = fuelLimit;
        }

        #endregion

        #region Methods
        public void AddGas()
        {
            if(Gas <= FuelLimit)
            {
                Gas += 0.1;
            }
            else
            {
                throw new Exception("Gas Full");
            }
        }
        public void StartEngine()
        {
            if (_isEngineOn)
            {
                throw new Exception("Engine is already on");
            }
            if (NeedsGas())
            {
                throw new Exception("No enoguht gas. You need to go to Gas Station");
            }
            _isEngineOn = true;
        }

        public bool NeedsGas()
        {
            return !(Gas > 0);
        }

        public bool IsEngineOn()
        {
            return _isEngineOn;
        }

        public void StopEngine()
        {
            if (!_isEngineOn)
            {
                throw new Exception("Enigne already stopped");
            }

            _isEngineOn = false;
        }

        #endregion

    }
}
