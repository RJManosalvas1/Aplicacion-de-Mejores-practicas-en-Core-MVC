using Best_Practices.ModelBuilders;
using Best_Practices.Models;

namespace Best_Practices.Infraestructure.Factories
{
    public class FordExplorerCreator : Creator
    {
        public override Vehicle Create()
        {
            return new CarBuilder()
                .SetBrand("Ford")
                .SetModel("Explorer")
                .SetColor("Black")
                .Build();
        }
    }
}
