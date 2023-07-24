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

		private static void DisplayPostsForUser(string loggedInUsername)
		{
			var posts = _postManager.GetPostsForUser(loggedInUsername);
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
		}

		private static void Main(string[] args)
		{
			_userManager = new UserManager();
			_postManager = new PostManager();

			Console.WriteLine("WELCOME TO PUPHUB 2023!");

			bool loggedIn = false;
			string loggedInUsername = string.Empty;

			while (!loggedIn)
			{
				Console.Write("Username: ");
				string username = Console.ReadLine();
				Console.Write("Student Number: ");
				string studentNumber = Console.ReadLine();

				bool loginSuccess = _userManager.Login(username, studentNumber);

				if (loginSuccess)
				{
					loggedIn = true;
					loggedInUsername = username;
					Console.WriteLine("Login successful!");
				}
				else
				{
					Console.WriteLine("Invalid username or student number. Please try again.");
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
						DisplayPostsForUser(loggedInUsername);
						break;

					case "3":
						Console.WriteLine("Edit a post:");
						Console.Write("Enter the post number: ");
						int postNumber = int.Parse(Console.ReadLine());
						Console.Write("Enter the new content: ");
						string newContent = Console.ReadLine();

						_postManager.EditPost(postNumber, newContent);

		
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