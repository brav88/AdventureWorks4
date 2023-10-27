using AdventureWorks4.FirebaseAuth;
using Firebase.Auth;
using Firebase.Storage;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks4.Models
{
	public class Expense
	{
		public string? Id { get; set; }
		public string? Name { get; set; }
		public string? Amount { get; set; }
		public string? Date { get; set; }
	}

	public class ExpensesHandler
	{
		public async Task<List<Expense>> GetExpensesCollection()
		{
			List<Expense> expensesList = new List<Expense>();
			Query query = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId).Collection("Expenses");
			QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

			foreach (var item in querySnapshot)
			{
				Dictionary<string, object> data = item.ToDictionary();

				expensesList.Add(new Expense
				{
					Id = item.Id,
					Name = data["name"].ToString(),
					Amount = data["amount"].ToString(),
					Date = data["date"].ToString()
				});
			}

			return expensesList;
		}

		public async Task<bool> Create(string name, int amount, string date)
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

				return true;
			}
			catch (FirebaseStorageException ex)
			{
				throw ex;
			}
		}

		public async Task<bool> Edit(string id, string name, int amount, string date)
		{
			try
			{
				FirestoreDb db = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId);
				DocumentReference docRef = db.Collection("Expenses").Document(id);
				Dictionary<string, object> dataToUpdate = new Dictionary<string, object>
				{
					{ "name", name },
					{ "date", date },
					{ "amount", amount}
				};

				WriteResult result = await docRef.UpdateAsync(dataToUpdate);

				return true;
			}
			catch (FirebaseStorageException ex)
			{
				throw ex;
			}
		}
	}
}
