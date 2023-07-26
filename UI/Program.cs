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

		// This method will Display All Posts of USERS/STUDENTS
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
		// This method will Display All Posts of Admin
		private static void DisplayPostsForAdmin()
		{
			var posts = _postManager.GetPostsForUser("ADMIN");
			Console.WriteLine($"\nBulletin Board of ADMIN:\n");
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
		// This method is for Admin Login
		private static bool AdminLogin()
		{
			Console.Write("Admin Number: ");
			string adminNumber = Console.ReadLine();
			Console.Write("Password: ");
			string adminPassword = Console.ReadLine();

		// Admin Credentials (Ito lang may access sa Admin)
			string adminNumberExpected = "ADMIN123";
			string adminPasswordExpected = "adminpassword";

			return adminNumber == adminNumberExpected && adminPassword == adminPasswordExpected;
		}

		// This method will Display All Posts of Users and Admin 
		private static void DisplayPostsForAllUsers()
		{
			var allPosts = _postManager.GetPosts();
			Console.WriteLine("\nAll Users' Bulletin Board:\n");
			foreach (var post in allPosts)
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

			Console.WriteLine("WELCOME TO PUPHUB 2023");

			bool loggedIn = false;
			string loggedInUsername = string.Empty;
			bool isAdmin = false;

			//The system will ask if the user is Admin or Student
			while (!loggedIn)
			{
				Console.Write("Are you a Student or an Admin? (Student/Admin): ");
				string userType = Console.ReadLine().ToLower();

				if (userType == "admin")
				{
					if (AdminLogin())
					{
						loggedIn = true;
						isAdmin = true;
						loggedInUsername = "ADMIN";
						Console.WriteLine("Admin login successful!");
					}
					else
					{
						Console.WriteLine("Invalid admin credentials. Please try again.");
					}
				}
				else if (userType == "student")
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
				else
				{
					Console.WriteLine("Invalid choice. Please enter 'Student' or 'Admin' only.");
				}
			}
			// After logging in, Post Functions (CREATE, VIEW, DELETE, EDIT)
			while (loggedIn)
			{
				Console.WriteLine("\n1. Create Post");
				Console.WriteLine("2. View Posts");
				Console.WriteLine("3. Edit a Post");
				Console.WriteLine("4. Delete a Post");
				Console.WriteLine("5. Logout");
				Console.Write("Enter your choice: ");
				string choice = Console.ReadLine();

				// Post Type (Image, Text, Videos)
				switch (choice)
				{
					case "1":
						Console.WriteLine("Select the post type:");
						Console.WriteLine("A. Image");
						Console.WriteLine("B. Text");
						Console.WriteLine("C. Video");
						Console.Write("Enter your Choice: ");
						string postTypeInput = Console.ReadLine();

						string postType;
						if (postTypeInput.Equals("A", StringComparison.OrdinalIgnoreCase))
						{
							postType = "Image";
						}
						else if (postTypeInput.Equals("B", StringComparison.OrdinalIgnoreCase))
						{
							postType = "Text";
						}
						else if (postTypeInput.Equals("C", StringComparison.OrdinalIgnoreCase))
						{
							postType = "Video";
						}
						else
						{
							Console.WriteLine("Invalid post type selection. Please try again.");
							break;
						}

						Console.Write("Enter your post content/link: ");
						string content = Console.ReadLine();

						_postManager.CreatePost(content, loggedInUsername, postType);
					
						break;

					//Viewing Posts
			
					case "2":
						Console.WriteLine("Select which posts to view:");
						Console.WriteLine("1. MY BULLETIN BOARD");
						Console.WriteLine("2. ADMIN'S BULLETIN BOARD");
						Console.WriteLine("3. ALL USERS' BULLETIN BOARD"); // Add option to view all users' posts

						Console.Write("Enter your choice: ");
						string viewChoice = Console.ReadLine();

						if (viewChoice == "1")
						{
							DisplayPostsForUser(loggedInUsername);
						}
						else if (viewChoice == "2")
						{
							DisplayPostsForAdmin();

						}
						else if (viewChoice == "3") // Only admins can view all users' posts
						{
							DisplayPostsForAllUsers();
						}
						else
						{
							Console.WriteLine("Invalid choice.");
						}
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
						Console.Write("Enter the Username to delete a post: ");
						string username = Console.ReadLine();

						//bool correctuser = _userManager.CorrectUser(username);

						//if (correctuser)
						//{
						//	Console.WriteLine("Enter Post Number to Delete: ");
						//	int postNumberToDelete = int.Parse(Console.ReadLine());
						//	_postManager.DeletePost(postNumberToDelete);
						//}
						//else
						//{
						//	Console.WriteLine("Invalid username. Please try again.");
						//}


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