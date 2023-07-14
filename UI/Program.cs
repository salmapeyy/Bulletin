using BusinessRules;
using DataLayer;
using Models;
using System;

namespace UI
{
	public class Program
	{
		private static UserManager _userManager;
		private static PostManager _postManager;

		private static void Main(string[] args)
		{
			_userManager = new UserManager();
			_postManager = new PostManager();

			Console.WriteLine("WELCOME TO PUPHUB 2023!");

			bool loggedIn = false;
			string loggedInUsername = string.Empty;

			while (!loggedIn)
			{
				Console.WriteLine("\nDon't have an Account yet? Try to create a new one!");
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
				Console.WriteLine("4. Delete a Post");
				Console.WriteLine("5. Logout");
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
						var posts = _postManager.GetPosts();
						Console.WriteLine($"\nBulletin Board of {loggedInUsername}:\n");
						foreach (var post in posts)
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
						Console.WriteLine("Edit a post:");
						Console.Write("Enter the post number: ");
						int postNumber = int.Parse(Console.ReadLine());
						Console.Write("Enter the new content: ");
						string newContent = Console.ReadLine();

						_postManager.EditPost(postNumber, newContent);

						Console.WriteLine("Post edited successfully!");
						break;

					case "4":
						Console.WriteLine("Delete a post:");
						Console.Write("Enter the post number: ");
						int postNumberToDelete = int.Parse(Console.ReadLine());

						_postManager.DeletePost(postNumberToDelete);

						Console.WriteLine("Post deleted successfully!");
						break;

					case "5":
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