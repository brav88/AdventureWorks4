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
}
