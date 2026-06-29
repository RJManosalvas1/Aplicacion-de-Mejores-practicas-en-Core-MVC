# Tarea: Aplicacion de Mejores practicas en Core MVC

ASP.NET Core MVC (.NET 8) que gestiona un listado de vehículos en memoria: alta de
Mustang/Explorer/Escape, encendido/apagado de motor y carga de combustible. Este
README documenta los problemas encontrados en el código original y las mejoras
aplicadas en términos de principios SOLID y patrones de diseño.

## Problemas identificados

1. **`HomeController.Index()` no usaba el repositorio inyectado.** Leía
   directamente de un Singleton estático (`VehicleCollection.Instance`)
   mientras que `AddMustang`/`AddExplorer` sí usaban `IVehicleRepository`.
   Esta inconsistencia era el bug que reportaba QA.
2. **Imposible probar sin base de datos.** `DBVehicleRepository` lanzaba
   `NotImplementedException` en todo, y la única alternativa
   (`MyVehiclesRepository`) dependía de un Singleton manual con estado
   global estático, difícil de aislar en pruebas.
3. **Creación de vehículos acoplada a clases concretas.** El controlador
   hacía `new FordMustangCreator()` directamente, en vez de depender de la
   abstracción `Creator`. Agregar un modelo nuevo (Escape) obligaba a tocar
   el controlador y la vista.
4. **Defaults hardcodeados y constructor rígido.** `CarBuilder` traía
   valores de un modelo específico ("Ford"/"Mustang"/"Red") hardcodeados en
   una clase que debía ser genérica, y agregar propiedades nuevas a
   `Vehicle` implicaba modificar constructores en cascada.

## Principios SOLID aplicados

| Principio | Dónde se aplica | Cómo |
|---|---|---|
| **Dependency Inversion (DIP)** | `HomeController`, `MyVehiclesRepository` | El controlador solo depende de `IVehicleRepository` e `IVehicleCreatorFactory` (interfaces), nunca de `VehicleCollection`, `FordMustangCreator`, etc. directamente. |
| **Open/Closed (OCP)** | `Infraestructure/Factories`, `ModelBuilders` | Agregar el modelo Ford Escape solo requirió una clase nueva (`FordEscapeCreator`) + una entrada en un diccionario; no se modificó `Creator`, el controlador ni la vista. |
| **Single Responsibility (SRP)** | `ModelBuilders/VehicleBuilder.cs` vs. `Infraestructure/Factories/*Creator.cs` | El Builder solo sabe construir un `Vehicle` con sus defaults genéricos; cada `Creator` es quien decide marca/modelo/color específicos. Antes esa responsabilidad estaba mezclada en `CarBuilder`. |

## Patrones de diseño aplicados

### 1. Repository Pattern (corregido)
`IVehicleRepository` es la única fuente de verdad para leer/escribir
vehículos. `MyVehiclesRepository` (in-memory) y `DBVehicleRepository`
(pendiente, a la espera del esquema de BD) implementan esa interfaz.
El almacenamiento en memoria se extrajo a `IVehicleStore` /
`InMemoryVehicleStore`, registrado como **Singleton del contenedor de DI**
(`services.AddSingleton<IVehicleStore, InMemoryVehicleStore>()`) en lugar
de un Singleton estático manual. Esto permite testear sin tocar base de
datos y cambiar a `DBVehicleRepository` el día de mañana modificando una
sola línea en `ServicesConfiguration.cs`.

### 2. Factory Method
`Creator` (abstracta) define `Create(): Vehicle`. Cada modelo
(`FordMustangCreator`, `FordExplorerCreator`, `FordEscapeCreator`) es una
subclase concreta. `IVehicleCreatorFactory` / `VehicleCreatorFactory`
resuelve el `Creator` correcto a partir de un nombre de modelo, y es lo
único que el controlador conoce (vía DI). El endpoint genérico
`AddVehicle(string model)` queda preparado para cualquier modelo futuro.

### 3. Builder Pattern
`VehicleBuilder` (abstracta) centraliza los valores por defecto comunes a
cualquier vehículo (`Color`, `Brand`, `Model`, `Year = DateTime.Now.Year`)
y un diccionario `ExtraProperties` reservado para las ~20 propiedades que
negocio pedirá en un sprint futuro. `CarBuilder` y `MotocycleBuilder`
heredan esos defaults y solo definen cómo construir su tipo concreto de
`Vehicle`. Agregar una propiedad nueva el próximo sprint se reduce a
modificar un único archivo (`VehicleBuilder.cs`).

## Estructura del proyecto

```
BestPractices/
├── Controllers/HomeController.cs        # Depende solo de abstracciones (DIP)
├── Infraestructure/
│   ├── Factories/                        # Factory Method (Creator + resolver)
│   └── Storage/                          # IVehicleStore / InMemoryVehicleStore (reemplaza al Singleton manual)
├── ModelBuilders/                        # Builder Pattern (VehicleBuilder, CarBuilder, MotocycleBuilder)
├── Models/                                # Vehicle, Car, Motocycle, IVehicle
├── Repositories/                          # Repository Pattern (IVehicleRepository, MyVehiclesRepository, DBVehicleRepository)
└── Infraestructure/DependencyInjection/  # Registro de todas las abstracciones en el contenedor de DI
```

**URL del proyecto desplegado:** https://aplicacion-de-mejores-practicas-en-core.onrender.com
