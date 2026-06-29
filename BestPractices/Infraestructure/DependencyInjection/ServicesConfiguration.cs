using Best_Practices.Infraestructure.Factories;
using Best_Practices.Infraestructure.Storage;
using Best_Practices.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Best_Practices.Infraestructure.DependencyInjection
{
    public class ServicesConfiguration
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // IVehicleStore como Singleton del contenedor de DI:
            // reemplaza al Singleton manual (VehicleCollection.Instance).
            // Una sola instancia compartida durante la vida de la app,
            // pero ahora inyectable/reemplazable en pruebas.
            services.AddSingleton<IVehicleStore, InMemoryVehicleStore>();

            // Repositorio en memoria mientras el equipo de BD entrega el
            // esquema. El dia que este listo, esta es la UNICA linea que
            // cambia para pasar a DBVehicleRepository:
            // services.AddTransient<IVehicleRepository, DBVehicleRepository>();
            services.AddTransient<IVehicleRepository, MyVehiclesRepository>();

            // Factory Method: resolver de Creators por nombre de modelo.
            services.AddSingleton<IVehicleCreatorFactory, VehicleCreatorFactory>();
        }
    }
}
