using Google.Cloud.Firestore;
using Google.Type;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Google.Cloud.Firestore.V1.StructuredAggregationQuery.Types.Aggregation.Types;
using System.Xml.Linq;
using AdventureWorks4.FirebaseAuth;

namespace AdventureWorks4.Controllers
{
	public class ExpensesController : Controller
	{
		public IActionResult Index()
		{
			return View();
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

				return View("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}
