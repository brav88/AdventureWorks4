using AdventureWorks4.Models;
using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace AdventureWorks4.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("userSession")))
				return RedirectToAction("Index", "Error");

			//Aqui le pasamos la lista de Resorts a la vista
			ViewBag.ResortList = GetResorts();

			return View();
		}

		public List<Resort> GetResorts()
		{
			//Aqui nos traemos los Resorts de SQL Server
			List<Resort> resortList = new List<Resort>();
			DataTable ds = Database.DatabaseHelper.ExecuteQuery("spGetResorts", null);

			//Recorremos el objeto para crear la lista de Resorts
			foreach (DataRow dr in ds.Rows)
			{
				resortList.Add(new Resort
				{
					Id = Convert.ToInt16(dr["id"]),
					Name = dr["name"].ToString(),
					Description = dr["description"].ToString(),
					Photo = dr["photo"].ToString(),
					Price = Convert.ToDecimal(dr["price"])
				});
			}

			return resortList;
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Products()
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