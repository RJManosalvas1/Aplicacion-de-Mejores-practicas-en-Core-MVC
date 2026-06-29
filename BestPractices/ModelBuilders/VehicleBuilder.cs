using Best_Practices.Models;
using System;
using System.Collections.Generic;

namespace Best_Practices.ModelBuilders
{
    // BUILDER PATTERN (clase base abstracta)
    // -----------------------------------------------------------------
    // Centraliza los valores por defecto que aplican a CUALQUIER vehiculo
    // (Color, Brand, Model, Year, y el diccionario ExtraProperties para
    // las ~20 propiedades que negocio pedira el siguiente sprint).
    //
    // Por que este patron resuelve el requerimiento de negocio:
    // - Hoy: agregar "Year" significa una sola linea de default aqui.
    // - Sprint siguiente: agregar cada una de las 20 propiedades nuevas
    //   significa agregar una entrada en ExtraProperties (o un nuevo
    //   With/Set fluido) UNICAMENTE en este archivo. No se modifica
    //   Vehicle, Car, Motocycle, ni los Creator existentes
    //   (cumple el principio Open/Closed: abierto a extension,
    //   cerrado a modificacion).
    public abstract class VehicleBuilder
    {
        protected string Color = "White";
        protected string Brand = "Generic";
        protected string Model = "Generic";

        // Default solicitado por negocio: año actual.
        protected int Year = DateTime.Now.Year;

        // Espacio reservado para las 20 propiedades del siguiente sprint.
        // Ejemplo de uso futuro: ExtraProperties["Transmission"] = "Automatica";
        protected Dictionary<string, object> ExtraProperties = new Dictionary<string, object>();

        public VehicleBuilder SetColor(string color)
        {
            Color = color;
            return this;
        }

        public VehicleBuilder SetBrand(string brand)
        {
            Brand = brand;
            return this;
        }

        public VehicleBuilder SetModel(string model)
        {
            Model = model;
            return this;
        }

        public VehicleBuilder SetYear(int year)
        {
            Year = year;
            return this;
        }

        // Permite, desde ya, agregar propiedades adicionales sin esperar
        // a que existan como propiedades fuertemente tipadas.
        public VehicleBuilder SetExtraProperty(string key, object value)
        {
            ExtraProperties[key] = value;
            return this;
        }

        // Cada subclase concreta (CarBuilder, MotocycleBuilder, ...)
        // sabe que tipo de Vehicle concreto instanciar.
        public abstract Vehicle Build();

        // Aplica los defaults comunes (Year, ExtraProperties) a la
        // instancia ya creada por la subclase. Evita repetir este
        // codigo en cada Builder concreto.
        protected Vehicle ApplyCommonDefaults(Vehicle vehicle)
        {
            vehicle.Year = Year;
            vehicle.ExtraProperties = new Dictionary<string, object>(ExtraProperties);
            return vehicle;
        }
    }
}
