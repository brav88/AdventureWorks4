using AdventureWorks4.Database;
using AdventureWorks4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AdventureWorks4.Controllers
{
    public class ReportController : Controller
    {
		public IActionResult Index()
		{
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
