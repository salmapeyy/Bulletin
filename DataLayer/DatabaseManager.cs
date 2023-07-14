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
		static string connectionString = "Data Source = localhost; Initial Catalog = PUPHubPosts; Integrated Security = True;";
		private static SqlConnection sqlConnection;

		public DatabaseManager()
		{
			sqlConnection = new SqlConnection(connectionString);
		}

		public static void Connect()
		{
			sqlConnection.Open();
		}

		public List<PostContent> GetPosts()
		{
			var selectStatement = "SELECT PostNumber, Username, PostContent, DateCreated, LastModified FROM Post";
			SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
			sqlConnection.Open();
			SqlDataReader reader = selectCommand.ExecuteReader();

			var posts = new List<PostContent>();

			while (reader.Read())
			{
				posts.Add(new PostContent
				{
					PostId = Convert.ToInt16(reader["PostNumber"].ToString()),
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
			var insertStatement = "INSERT INTO Post VALUES (@PostNumber, @Username, @PostContent,  @DateCreated, @LastModified)";

			SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

			insertCommand.Parameters.AddWithValue("@PostNumber", post.PostId);
			insertCommand.Parameters.AddWithValue("@Username", post.Username);
			insertCommand.Parameters.AddWithValue("@PostContent", post.Content);
			insertCommand.Parameters.AddWithValue("@DateCreated", post.DateCreated);
			insertCommand.Parameters.AddWithValue("@LastModified", post.LastModified);
			sqlConnection.Open();

			success = insertCommand.ExecuteNonQuery();

			sqlConnection.Close();

			return success;
		}

		public PostContent GetPostByNumber(int postNumber)
		{
			var selectStatement = "SELECT PostNumber, Username, PostContent, DateCreated, LastModified FROM Post WHERE PostNumber = @PostNumber";
			SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
			selectCommand.Parameters.AddWithValue("@PostNumber", postNumber);
			sqlConnection.Open();
			SqlDataReader reader = selectCommand.ExecuteReader();

			PostContent post = null;

			if (reader.Read())
			{
				post = new PostContent
				{
					PostId = Convert.ToInt16(reader["PostNumber"].ToString()),
					Username = reader["Username"].ToString(),
					Content = reader["PostContent"].ToString(),
					DateCreated = DateTime.Now,
					LastModified = DateTime.Now
				};
			}

			sqlConnection.Close();
			return post;
		}

		public void UpdatePost(PostContent post)
		{
			var updateStatement = "UPDATE Post SET Post = @PostContent, LastModified = @LastModified WHERE PostNumber = @PostNumber";
			SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);
			updateCommand.Parameters.AddWithValue("@PostNumber", post.PostId);
			updateCommand.Parameters.AddWithValue("@PostContent", post.Content);
			updateCommand.Parameters.AddWithValue("@LastModified", post.LastModified);
			sqlConnection.Open();
			updateCommand.ExecuteNonQuery();
			sqlConnection.Close();
		}

		public void DeletePost(PostContent post)
		{
			var deleteStatement = "DELETE FROM Post WHERE PostNumber = @PostNumber";
			SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);
			deleteCommand.Parameters.AddWithValue("@PostNumber", post.PostId);
			sqlConnection.Open();
			deleteCommand.ExecuteNonQuery();
			sqlConnection.Close();
		}
	}
}
