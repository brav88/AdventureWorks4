using AdventureWorks4.Database;
using AdventureWorks4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace AdventureWorks4.Controllers
{
    public class ReportController : Controller
    {
		public IActionResult Index()
		{
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("userSession")))
				return RedirectToAction("Index", "Error");

			Models.User user = JsonConvert.DeserializeObject<Models.User>(HttpContext.Session.GetString("userSession"));

			PermissionHandler permissionHandler = new PermissionHandler();

			if (!permissionHandler.ValidatePageByRole(user.Role.ToString(), "Report").Result)
				return RedirectToAction("Index", "Error", new { id = 99 });

			ViewBag.User = user;

			return View();
		}

		public IActionResult Covid19()
        {
            return View();
        }

        public IActionResult GetSalesReport()
        {
            DataTable ds = DatabaseHelper.ExecuteQuery("[dbo].[spGetSales]", null);

            List<List<int>> seriesList = new List<List<int>>();
            List<int> labels = new List<int>();
            List<int> series1 = new List<int>();

            foreach (DataRow row in ds.Rows)
            {
                labels.Add(Convert.ToInt32(row["Year"]));
                series1.Add(Convert.ToInt32(row["SumOfSales"]));
            }

            seriesList.Add(series1);

            SalesData data = new SalesData()
            {
                labels = labels,
                series = seriesList
            };

            return Json(data);
        }        
    }
}
