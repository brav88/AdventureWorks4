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
		public async Task<IActionResult> Index()
		{
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("userSession")))
				return RedirectToAction("Index", "Error");

			return await GetExpenses();			
		}

		private async Task<IActionResult> GetExpenses()
		{
			List<Expense> expensesList = new List<Expense>();
			Query query = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId).Collection("Expenses");
			QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

			foreach (var item in querySnapshot)
			{
				Dictionary<string, object> data = item.ToDictionary();

				expensesList.Add(new Expense
				{					
					Name = data["name"].ToString(),
					Amount = data["amount"].ToString(),
					Date = data["date"].ToString()
				});
			}

			ViewBag.Expenses = expensesList;

			return View("Index");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(string name, int amount, string date)
		{
			try
			{
				DocumentReference addedDocRef = 
					await FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId)
						.Collection("Expenses").AddAsync(new Dictionary<string, object>
							{
								{ "amount",amount },
								{ "date", date },
								{ "name", name },
							});

				return await GetExpenses();
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
	}
}
