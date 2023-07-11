using Models;
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
		static string connectionString
	   = "Server=tcp:20.205.106.109,1433;Database=PostManagement;User Id=sa;Password=POST123!;";
		private static SqlConnection sqlConnection;

		static public void Connect()
		{
			sqlConnection.Open();	
		}

		public DatabaseManager()
		{
			sqlConnection = new SqlConnection(connectionString);
		}

		public List<PostContent> GetPosts()
		{
			var selectStatement = "SELECT PostNumber, StudentNumber, PostContent, DateTimePosted FROM Post";
			SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
			sqlConnection.Open();
			SqlDataReader reader = selectCommand.ExecuteReader();

			var posts = new List<PostContent>();

			while (reader.Read())
			{
				posts.Add(new PostContent
				{
					PostId = Convert.ToInt16(reader["Post Number"].ToString()),
					Username = reader["Username"].ToString(),
					Content = reader["PostContent"].ToString(),
					DateCreated = DateTime.Now,
					LastModified = DateTime.Now
				});
			}

			sqlConnection.Close();
			return posts;
		}

		public int CreatePost(PostContent post)
		{
			int success;
			var insertStatement = "INSERT INTO Post VALUES (@Post Number, @Username, @Content,  @Date Created, @LastModified)";

			SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

			insertCommand.Parameters.AddWithValue("@Post Number", post.PostId);
			insertCommand.Parameters.AddWithValue("@Username", post.Username);
			insertCommand.Parameters.AddWithValue("@Content", post.Content);
			insertCommand.Parameters.AddWithValue("@Date Createad", post.DateCreated);
			insertCommand.Parameters.AddWithValue("@Last Modified", post.LastModified);
			sqlConnection.Open();

			success = insertCommand.ExecuteNonQuery();

			sqlConnection.Close();

			return success;
		}

	}
}