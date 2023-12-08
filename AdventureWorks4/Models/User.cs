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
		public List<string>? Pages { get; set; }
	}

	public class PermissionHandler
	{
		public async Task<Permissions> GetPermissionsCollection(string role)
		{
			Permissions permissions = new Permissions();
			permissions.Pages = new List<string>();
			dynamic pages = await GetDynamicPermissions(role);

			foreach (var page in pages)
			{
				permissions.Pages.Add(page);
			}

			return permissions;
		}

		public async Task<dynamic> GetDynamicPermissions(string role)
		{
			Query query = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId).Collection("Permissions");
			QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

			Dictionary<string, object> data = querySnapshot[0].ToDictionary();

			dynamic pages = data[role];

			return pages;
		}

		public async Task<bool> ValidatePageByRole(string role, string requestedPage)
		{
			Permissions permissions = await GetPermissionsCollection(role);

			foreach (var page in permissions.Pages)
			{
				if (page == requestedPage)
				{
					return true;
				}
			}

			return false;
		}
	}
}
