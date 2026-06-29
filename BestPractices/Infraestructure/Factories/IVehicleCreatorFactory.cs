namespace Best_Practices.Infraestructure.Factories
{
    // Abstraccion que el HomeController consume (Dependency Inversion
    // Principle): el controlador no conoce las clases concretas
    // FordMustangCreator/FordExplorerCreator/FordEscapeCreator, solo
    // pide "el Creator para tal modelo" a esta interfaz.
    public interface IVehicleCreatorFactory
    {
        Creator Resolve(string modelKey);
    }
}
