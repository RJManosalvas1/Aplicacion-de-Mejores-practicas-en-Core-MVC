using Best_Practices.Infraestructure.Factories;
using Best_Practices.Models;
using Best_Practices.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Best_Practices.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleCreatorFactory _creatorFactory;

        // Ambas dependencias son abstracciones inyectadas por el
        // contenedor de DI (Dependency Inversion Principle). El
        // controlador no instancia ni VehicleCollection ni
        // FordMustangCreator/FordExplorerCreator directamente.
        public HomeController(
            IVehicleRepository vehicleRepository,
            IVehicleCreatorFactory creatorFactory,
            ILogger<HomeController> logger)
        {
            _vehicleRepository = vehicleRepository;
            _creatorFactory = creatorFactory;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel();

            // BUGFIX: antes se leia VehicleCollection.Instance.Vehicles
            // directamente, pasando por encima de la abstraccion
            // IVehicleRepository inyectada. Por eso QA reportaba
            // comportamiento inconsistente: Add usaba el repositorio,
            // pero Index usaba el Singleton. Ahora todo pasa por el
            // mismo repositorio.
            model.Vehicles = _vehicleRepository.GetVehicles();

            string error = Request.Query.ContainsKey("error") ? Request.Query["error"].ToString() : null;
            ViewBag.ErrorMessage = error;

            return View(model);
        }

        // Endpoint generico: agregar nuevos modelos (Factory Method) ya
        // NO requiere tocar este controlador. Solo se necesita una
        // clase Creator nueva + su registro en VehicleCreatorFactory.
        [HttpGet]
        public IActionResult AddVehicle(string model)
        {
            var creator = _creatorFactory.Resolve(model);
            var vehicle = creator.Create();
            _vehicleRepository.AddVehicle(vehicle);
            return Redirect("/");
        }

        // Se mantienen estas rutas con nombre porque el requerimiento
        // original las pide explicitamente ("add Mustang y add
        // Explorer"); ambas son ahora simples atajos sobre el endpoint
        // generico AddVehicle.
        [HttpGet]
        public IActionResult AddMustang() => AddVehicle("Mustang");

        [HttpGet]
        public IActionResult AddExplorer() => AddVehicle("Explorer");

        [HttpGet]
        public IActionResult AddEscape() => AddVehicle("Escape");

        [HttpGet]
        public IActionResult StartEngine(string id)
        {
            try
            {
                var vehicle = _vehicleRepository.Find(id);
                vehicle.StartEngine();
                return Redirect("/");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return Redirect($"/?error={ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult AddGas(string id)
        {
            try
            {
                var vehicle = _vehicleRepository.Find(id);
                vehicle.AddGas();
                return Redirect("/");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return Redirect($"/?error={ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult StopEngine(string id)
        {
            try
            {
                var vehicle = _vehicleRepository.Find(id);
                vehicle.StopEngine();
                return Redirect("/");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return Redirect($"/?error={ex.Message}");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
