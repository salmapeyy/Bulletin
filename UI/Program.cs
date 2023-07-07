using BusinessRules;
using DataLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace UI
{
	internal class Program
	{
		private static UserManager _userManager;
	        private static AdminManager _adminManager;
		private static PostManager _postManager;

		private static void Main(string[] args)
		{
			//database
			//{
			//	string connectionString = "Server=iphost;Port=3306;Database=bulletinpost;Uid=bulletin;Pwd=123;";

			//	DatabaseManager databaseManager = new DatabaseManager(connectionString);

			//	string tableName = "Post Database";

			//	Console.WriteLine("Datas from Post Database");

			//	var names = databaseManager.GetNamesFromTable(tableName);
			//	foreach (var name in names)
			//	{
			//		Console.WriteLine(name);
			//	}

			//	Console.ReadLine();
			//}

			{       
				_adminManager = new AdminManager();
				_userManager = new UserManager();
				_postManager = new PostManager();

				Console.WriteLine("WELOME TO PUPHUB 2023!");
				Console.WriteLine("STUDENT OR ADMIN ? ");//  Admin Access in Post "Michi"
				Console.WriteLine("If ADMIN enter number 3 ");

				bool loggedIn = false;
				string loggedInUsername = string.Empty;

				while (!loggedIn)
				{
					if(choice == "3")
				  {
					    Console.WriteLine("\n1. Create Post")
					    Console.WriteLine("2. View Post");
					    Console.WriteLine("3. View all post per Student");
					    Console.WriteLine("4. Edit Post");
						Console.WriteLine("5. Delete post per Student")
						console.WriteLine("6. Delete your Post")
						Console.WriteLine("7. Logout");
						string choice ("Enter your choice: ");
					
					switch (choice)
					    {
						case "1": 
						    Console.WriteLine("Enter your post content: ");
							string content = Console.ReadLine();
							_postManager.CreatePost(content, loggedInFacultyNumber);
							Console.WriteLine("Post created successfully!");
							break;
							
						case "2":
						    List<PostContent> posts = _postManager.GetPosts();
							Console.WriteLine($"\nBulletin Board of {loggedInFacultyNumber}:\n");
							foreach (PostContent post in posts)
							{
								Console.WriteLine($"Post #: {post.PostId}");
								Console.WriteLine($"Content: {post.Content}");
								Console.WriteLine($"Posted By: {post.Username}");
								Console.WriteLine($"Date Created: {post.DateCreated}");
								Console.WriteLine($"Last Modified: {post.LastModified}");
								Console.WriteLine();
							}
							break;
							
						case "3":
						    Console.WriteLine("View all post per Student");
							// wala papo
							 break;
						
						case "4":
							Console.WriteLine("Edit a post:");
							Console.Write("Enter the post number: ");
							int postNumber = int.Parse(Console.ReadLine());
							Console.Write("Enter the new content: ");
							string newContent = Console.ReadLine();

							_postManager.EditPost(postNumber, newContent);

							Console.WriteLine("Post edited successfully!");
							break;
							
						case "5":
							Console.WriteLine("Delete post per Student");
							// wala papo 
							break;
						
						case "6":
						    Console.WriteLine("Delete your post");
							// wala papo 
							break;
						case "7":
							loggedIn = false;
							loggedInUsername = string.Empty;
							Console.WriteLine("Logout successful!");
							break;

						default:
							Console.WriteLine("Invalid choice. Please try again.");
							break;
					    }	
				        }
					else if (choice == "2")
				    {
					Console.WriteLine("\n1. Create Post");
					Console.WriteLine("2. View Posts");
					Console.WriteLine("3. Edit a Post");
					Console.WriteLine("4. Logout");
					Console.Write("Enter your choice: ");
					string choice = Console.ReadLine();

					switch (choice)
					{
						case "1":
							Console.Write("Enter your post content: ");
							string content = Console.ReadLine();
							_postManager.CreatePost(content, loggedInUsername);
							Console.WriteLine("Post created successfully!");
							break;

						case "2":
							List<PostContent> posts = _postManager.GetPosts();
							Console.WriteLine($"\nBulletin Board of {loggedInUsername}:\n");
							foreach (PostContent post in posts)
							{
								Console.WriteLine($"Post #: {post.PostId}");
								Console.WriteLine($"Content: {post.Content}");
								Console.WriteLine($"Posted By: {post.Username}");
								Console.WriteLine($"Date Created: {post.DateCreated}");
								Console.WriteLine($"Last Modified: {post.LastModified}");
								Console.WriteLine();
							}
							break;
						//Editing A Post - Lumaoang
						case "3":
							Console.WriteLine("Edit a post:");
							Console.Write("Enter the post number: ");
							int postNumber = int.Parse(Console.ReadLine());
							Console.Write("Enter the new content: ");
							string newContent = Console.ReadLine();

							_postManager.EditPost(postNumber, newContent);

							Console.WriteLine("Post edited successfully!");
							break;

						case "4":
							loggedIn = false;
							loggedInUsername = string.Empty;
							Console.WriteLine("Logout successful!");
							break;

						default:
							Console.WriteLine("Invalid choice. Please try again.");
							break;
					        }
					}
				}
			}
		}
	}
}
