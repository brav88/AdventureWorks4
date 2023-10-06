using System.Data.SqlClient;
using System.Data;

namespace AdventureWorks4.Database
{
	public class DatabaseHelper
	{
		const string server = "localhost";
		const string database = "Booking";
		private static string connectionString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True", server, database);

		//select
		public static DataTable ExecuteQuery(string procedureName, List<SqlParameter>? param)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					SqlCommand cmd = new SqlCommand();
					cmd.Connection = connection;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = procedureName;

					if (param != null)
					{
						foreach (SqlParameter p in param)
						{
							cmd.Parameters.Add(p);
						}
					}

					cmd.ExecuteNonQuery();
					SqlDataAdapter adapter = new SqlDataAdapter(cmd);
					DataTable dt = new DataTable();
					adapter.Fill(dt);
					return dt;
				}
			}
			catch
			{
				throw;
			}
		}


		//update - delete - insert
		public static void ExecuteNonQuery(string procedureName, List<SqlParameter> param)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					SqlCommand cmd = new SqlCommand();
					cmd.Connection = connection;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = procedureName;

					if (param != null)
					{
						foreach (SqlParameter p in param)
						{
							cmd.Parameters.Add(p);
						}
					}

					cmd.ExecuteNonQuery();
				}
			}
			catch
			{
				throw;
			}
		}
	}
}
