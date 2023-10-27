using Google.Cloud.Firestore;
using Google.Type;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Google.Cloud.Firestore.V1.StructuredAggregationQuery.Types.Aggregation.Types;
using System.Xml.Linq;
using AdventureWorks4.FirebaseAuth;
using AdventureWorks4.Models;
using Firebase.Storage;
using System.Collections.Generic;

namespace AdventureWorks4.Controllers
{
	public class ExpensesController : Controller
	{
		public IActionResult Index()
		{
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("userSession")))
				return RedirectToAction("Index", "Error");

			return GetExpenses();			
		}

		private IActionResult GetExpenses()
		{
			ExpensesHandler expensesHandler = new ExpensesHandler();

			ViewBag.Expenses = expensesHandler.GetExpensesCollection().Result;

			return View("Index");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(string name, int amount, string date)
		{
			try
			{
				ExpensesHandler expensesHandler = new ExpensesHandler();

				bool result = expensesHandler.Create(name, amount, date).Result;

				return GetExpenses();	
			}
			catch (FirebaseStorageException ex)
			{
				ViewBag.Error = new ErrorHandler()
				{
					Title = ex.Message,
					ErrorMessage = ex.InnerException?.Message,
					ActionMessage = "Go to Expenses",
					Path = "/Expenses"
				};

				return View("ErrorHandler");
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditExpense(string id, string name, int amount, string date)
		{
			try
			{
				ExpensesHandler expensesHandler = new ExpensesHandler();

				bool result = expensesHandler.Edit(id, name, amount, date).Result;

				return GetExpenses();
			}
			catch (FirebaseStorageException ex)
			{
				ViewBag.Error = new ErrorHandler()
				{
					Title = ex.Message,
					ErrorMessage = ex.InnerException?.Message,
					ActionMessage = "Go to Expenses",
					Path = "/Expenses"
				};

				return View("ErrorHandler");
			}
		}

		public IActionResult Edit(string id, string name, string amount, string date)
		{
			Expense edited = new Expense
			{
				Id = id,
				Name = name,
				Amount = amount,
				Date = date,
			};

			ViewBag.Edited = edited;

			return View();
		}
	}
}
