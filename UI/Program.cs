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
				_userManager = new UserManager();
				_postManager = new PostManager();

				Console.WriteLine("WELOME TO PUPHUB 2023!");

				bool loggedIn = false;
				string loggedInUsername = string.Empty;

				while (!loggedIn)
				{
					Console.WriteLine("\nDont have an Account yet? Try to create a new one!");
					Console.WriteLine("\n1. Create New Account");
					Console.WriteLine("2. Login Existing Account");
					Console.Write("Enter your choice: ");
					string choice = Console.ReadLine();

					switch (choice)
					{
						case "1":
							Console.Write("Username: ");
							string newUsername = Console.ReadLine();
							Console.Write("Password: ");
							string newPassword = Console.ReadLine();

							bool accountCreated = _userManager.CreateUser(newUsername, newPassword);

							if (accountCreated)
							{
								Console.WriteLine("Account created successfully!\n");
							}
							else
							{
								Console.WriteLine("Username already exists. Please choose a different username.");
							}

							break;

						case "2":
							Console.Write("Username: ");
							string username = Console.ReadLine();
							Console.Write("Password: ");
							string password = Console.ReadLine();

							bool loginSuccess = _userManager.Login(username, password);

							if (loginSuccess)
							{
								loggedIn = true;
								loggedInUsername = username;
								Console.WriteLine("Login successful!");
							}
							else
							{
								Console.WriteLine("Invalid username or password. Please try again.");
							}

							break;

						default:
							Console.WriteLine("Invalid choice. Please try again.");
							break;
					}
				}

				while (loggedIn)
				{
					Console.WriteLine("\n1. Create Post");
					Console.WriteLine("2. View Posts");
					Console.WriteLine("3. Edit a Post");
					Console.WriteLine("4. Logout");
					Console.Write("Enter your choice:");
					string choice = Console.ReadLine();

					switch (choice)
					{
						case "1":
							Console.Write("Enter your post content: ");
							string content = Console.ReadLine();
                            Console.WriteLine("Do you want to insert some:\n1.IMAGE\n2.VIDEO\n3.FILE\n4.NO");
							Console.WriteLine("Enter your choice:   ");
	;						int option = Convert.ToInt32(Console.ReadLine());
                            if (option == 1)
                            {
                                Console.WriteLine("Image Link");
                            }
                            else if (option == 2)
                            {
                                Console.WriteLine("Video Link");
                            }
                            else if (option == 3)
                            {
                                Console.WriteLine("File Link");
                            }
                            else if (option == 4)
                            {
                                Console.WriteLine("OKAY!");
                            }
						
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
