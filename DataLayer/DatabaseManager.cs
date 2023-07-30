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
		//DATABASE CONNECTION
		static string connectionString = "Data Source = localhost; Initial Catalog = PUPBulletinBoard; Integrated Security = True;";
		private static SqlConnection sqlConnection;

		public DatabaseManager()
		{
			sqlConnection = new SqlConnection(connectionString);
		}

		// LISTING DATAS
		public List<PostContent> GetPosts()
		{
			var selectStatement = "SELECT PostNumber, Username, PostContent, DateCreated, LastModified, Likes, Dislikes FROM PUPHubBulletinPost";
			SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
			sqlConnection.Open();

			List<PostContent> posts = new List<PostContent>();

			try
			{
				using (SqlDataReader reader = selectCommand.ExecuteReader())
				{
					while (reader.Read())
					{
						PostContent post = new PostContent
						{
							PostId = Convert.ToInt32(reader["PostNumber"]),
							Username = reader["Username"].ToString(),
							Content = reader["PostContent"].ToString(),
							DateCreated = Convert.ToDateTime(reader["DateCreated"]),
							LastModified = Convert.ToDateTime(reader["LastModified"]),
							Likes = Convert.ToInt32(reader["Likes"]),
							Dislikes = Convert.ToInt32(reader["Dislikes"])
						};
						posts.Add(post);
					}
				}
			}
			catch (Exception ex)
			{
				// Handle the exception (e.g., log the error or display a user-friendly message)
				Console.WriteLine("An error occurred while fetching posts: " + ex.Message);
			}
			finally
			{
				sqlConnection.Close();
			}

			return posts;
		}
		// INSERTING DATAS IN DATABASE
		public int CreatePost(PostContent post)
		{
			int success;
			var insertStatement = "INSERT INTO PUPHubBulletinPost (PostNumber, Username, PostContent, DateCreated, LastModified, Likes, Dislikes) " +
								  "VALUES (@PostNumber, @Username, @PostContent, @DateCreated, @LastModified, @Likes, @Dislikes)";


			SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

			insertCommand.Parameters.AddWithValue("@PostNumber", post.PostId);
			insertCommand.Parameters.AddWithValue("@Username", post.Username);
			insertCommand.Parameters.AddWithValue("@PostContent", post.Content);
			insertCommand.Parameters.AddWithValue("@DateCreated", post.DateCreated);
			insertCommand.Parameters.AddWithValue("@LastModified", post.LastModified);
			insertCommand.Parameters.AddWithValue("@Likes", post.Likes); 
			insertCommand.Parameters.AddWithValue("@Dislikes", post.Dislikes); 
			sqlConnection.Open();

			success = insertCommand.ExecuteNonQuery();

			sqlConnection.Close();

			return success;
		}
		// POSTNUMBER IN DATABASE
		public PostContent GetPostNumber(int postNumber)
		{
			var selectStatement = "SELECT PostNumber, Username, PostContent, DateCreated, LastModified, Likes, Dislikes FROM PUPHubBulletinPost WHERE PostNumber = @PostNumber";
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
					LastModified = DateTime.Now,
					Likes = Convert.ToInt32(reader["Likes"]),
					Dislikes = Convert.ToInt32(reader["Dislikes"])
				};
			}

			sqlConnection.Close();
			return post;
		}

		//UPDATE DATABASE 
		public void UpdatePost(PostContent post)
		{
			
			var updateStatement = "UPDATE PUPHubBulletinPost SET PostContent = @PostContent, " +
								"LastModified = @LastModified, " +
								"Likes = @Likes, " +      // Update Likes count
								"Dislikes = @Dislikes " + // Update Dislikes count
								"WHERE PostNumber = @PostNumber AND Username = @Username";

			SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);
			updateCommand.Parameters.AddWithValue("@PostNumber", post.PostId);
			updateCommand.Parameters.AddWithValue("@Username", post.Username);
			updateCommand.Parameters.AddWithValue("@PostContent", post.Content);
			updateCommand.Parameters.AddWithValue("@LastModified", post.LastModified);
			updateCommand.Parameters.AddWithValue("@Likes", post.Likes);       
			updateCommand.Parameters.AddWithValue("@Dislikes", post.Dislikes); 

			sqlConnection.Open();
			updateCommand.ExecuteNonQuery();
			sqlConnection.Close();
		}
		//DELETE DATA IN DATABASE WHEN DELETING A POST
			public void DeletePost(PostContent post)
		{
			var deleteStatement = "DELETE FROM PUPHubBulletinPost WHERE PostNumber = @PostNumber AND Username = @Username";
			SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);
			deleteCommand.Parameters.AddWithValue("@PostNumber", post.PostId);
			deleteCommand.Parameters.AddWithValue("@Username", post.Username);
			sqlConnection.Open();
			deleteCommand.ExecuteNonQuery();
			sqlConnection.Close();
		}
	}
}
