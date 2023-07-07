using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
	public class DatabaseManager
	{
		//	private string _connectionString;

		//	public DatabaseManager(string connectionString)
		//	{
		//		_connectionString = connectionString;
		//	}

		//	public void ConnectAndExecute()
		//	{
		//		using (SqlConnection connection = new SqlConnection(_connectionString))
		//		{
		//			try
		//			{
		//				connection.Open();
		//				Console.WriteLine("Connected to the database.");


		//			}
		//			catch (Exception ex)
		//			{
		//				Console.WriteLine($"Error: {ex.Message}");
		//			}
		//			finally
		//			{
		//				connection.Close();
		//				Console.WriteLine("Disconnected from the database.");
		//			}
		//		}
		//	}

		//	public List<string> GetNamesFromTable(string tableName)
		//	{
		//		List<string> names = new List<string>();

		//		using (SqlConnection connection = new SqlConnection(_connectionString))
		//		{
		//			try
		//			{
		//				connection.Open();

		//				string query = $"SELECT Name FROM {tableName}";
		//				SqlCommand command = new SqlCommand(query, connection);

		//				SqlDataReader reader = command.ExecuteReader();
		//				while (reader.Read())
		//				{
		//					string name = reader.GetString(0);
		//					names.Add(name);
		//				}
		//				reader.Close();
		//			}
		//			catch (Exception ex)
		//			{
		//				Console.WriteLine($"Error: {ex.Message}");
		//			}
		//			finally
		//			{
		//				connection.Close();
		//			}
		//		}

		//		return names;
		//	}

		//	public bool InsertNameIntoTable(string tableName, string name)
		//	{
		//		using (SqlConnection connection = new SqlConnection(_connectionString))
		//		{
		//			try
		//			{
		//				connection.Open();

		//				string query = $"INSERT INTO {tableName} (Name) VALUES (@name)";
		//				SqlCommand command = new SqlCommand(query, connection);
		//				command.Parameters.AddWithValue("@name", name);

		//				int rowsAffected = command.ExecuteNonQuery();
		//				return rowsAffected > 0;
		//			}
		//			catch (Exception ex)
		//			{
		//				Console.WriteLine($"Error: {ex.Message}");
		//				return false;
		//			}
		//			finally
		//			{
		//				connection.Close();
		//			}
		//		}
		//	}
	}
}
