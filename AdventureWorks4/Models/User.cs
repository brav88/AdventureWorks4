using AdventureWorks4.FirebaseAuth;
using Google.Cloud.Firestore;
using System.Data.SqlClient;

namespace AdventureWorks4.Models
{
	public class User
	{
		public string? DocumentId { get; set; }
		public string? Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? PhotoPath { get; set; }
		public int? Role { get; set; }
	}

	public static class UserHandler
	{
		public static void SaveFirebaseUser(User user)
		{
			List<SqlParameter> parameters = new List<SqlParameter>() {
				new SqlParameter("@documentId", user.DocumentId),
				new SqlParameter("@email", user.Email),
				new SqlParameter("@id", user.Id),
				new SqlParameter("@name", user.Name),
				new SqlParameter("@photoPath", user.PhotoPath)
			};

			Database.DatabaseHelper.ExecuteNonQuery("[SaveFirebaseUser]", parameters);
		}
	}

	public class Permissions
	{
		public List<Object>? Pages { get; set; }
	}

	public class PermissionHandler
	{
		public async Task<Permissions> GetPermissionsCollection()
		{			
			Query query = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId).Collection("Permissions");
			QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
			Permissions permissions = new Permissions();
			permissions.Pages = new List<object>();

			foreach (var item in querySnapshot)
			{
				Dictionary<string, object> data = item.ToDictionary();

				dynamic data2 = data["Pages"];
				
				foreach (var item2 in data2)
				{
					permissions.Pages.Add(item2);
				}
			}

			return permissions;
		}
	}
}
