using Best_Practices.Models;

namespace Best_Practices.ModelBuilders
{
    // Builder concreto para Motocycle. Se agrega para demostrar que el
    // patron Builder es reutilizable para cualquier subtipo de Vehicle,
    // no solo para Car. No se usa todavia desde ningun Creator, queda
    // disponible para cuando negocio pida agregar motos.
    public class MotocycleBuilder : VehicleBuilder
    {
        public override Vehicle Build()
        {
            var moto = new Motocycle(Color, Brand, Model);
            return ApplyCommonDefaults(moto);
        }
    }
}
