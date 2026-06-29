using Best_Practices.ModelBuilders;
using Best_Practices.Models;

namespace Best_Practices.Infraestructure.Factories
{
    // Nuevo modelo solicitado: Ford Escape, color Red.
    // Notese que para agregar este modelo NO se modifico Creator,
    // ni HomeController, ni Vehicle: solo se agrego esta clase y
    // una linea de registro en VehicleCreatorFactory (ver ese archivo).
    // Esto es justamente lo que pidio el Arquitecto de Software al
    // sugerir Factory Method: que la introduccion de nuevos modelos
    // sea una extension, no una modificacion (Open/Closed Principle).
    public class FordEscapeCreator : Creator
    {
        public override Vehicle Create()
        {
            return new CarBuilder()
                .SetBrand("Ford")
                .SetModel("Escape")
                .SetColor("Red")
                .Build();
        }
    }
}
