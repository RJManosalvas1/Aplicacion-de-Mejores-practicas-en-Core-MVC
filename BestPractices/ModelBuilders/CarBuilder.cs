using Best_Practices.Models;
using System;

namespace Best_Practices.ModelBuilders
{
    // Builder concreto para vehiculos tipo Car.
    // OJO: ya NO hardcodea valores de un modelo especifico (antes traia
    // "Ford"/"Mustang"/"Red" como default, mezclando responsabilidades
    // de un Creator dentro del Builder generico). Los defaults de marca
    // y modelo ahora viven en cada Creator (FordMustangCreator,
    // FordExplorerCreator, FordEscapeCreator), que es quien realmente
    // conoce esa informacion especifica.
    public class CarBuilder : VehicleBuilder
    {
        public new CarBuilder SetColor(string color) { base.SetColor(color); return this; }
        public new CarBuilder SetBrand(string brand) { base.SetBrand(brand); return this; }
        public new CarBuilder SetModel(string model) { base.SetModel(model); return this; }
        public new CarBuilder SetYear(int year) { base.SetYear(year); return this; }

        public override Vehicle Build()
        {
            var car = new Car(Color, Brand, Model);
            return ApplyCommonDefaults(car);
        }
    }
}
