using Best_Practices.ModelBuilders;
using Best_Practices.Models;

namespace Best_Practices.Infraestructure.Factories
{
    // FACTORY METHOD: cada Creator concreto sabe construir UN modelo
    // especifico, usando el Builder para los detalles de construccion.
    public class FordMustangCreator : Creator
    {
        public override Vehicle Create()
        {
            return new CarBuilder()
                .SetBrand("Ford")
                .SetModel("Mustang")
                .SetColor("Red")
                .Build();
        }
    }
}
