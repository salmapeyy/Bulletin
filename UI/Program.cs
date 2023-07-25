using BusinessRules;
using DataLayer;
using Models;
using System;
using System.ComponentModel.Design;
using System.Xml.Serialization;

namespace UI
{
	public class Program
	{
		private static AdminManager _adminManager;
		private static UserManager _userManager;
		private static PostManager _postManager;


        private static void DisplayPostsForAdmin(string loggedInfacultyNumber)
        {
            var posts = _postManager.GetPostsForAdmin(loggedInfacultyNumber);
            Console.WriteLine($"\nBulletin Board of {loggedInfacultyNumber}:\n");
            foreach (var post in posts)
            {
                Console.WriteLine($"Post #: {post.PostId}");
                Console.WriteLine($"Content: {post.Content}");
                Console.WriteLine($"Posted By: {post.FacultyNumber}");
                Console.WriteLine($"Date Created: {post.DateCreated}");
                Console.WriteLine($"Last Modified: {post.LastModified}");
                Console.WriteLine();
            }
        }


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
            _adminManager = new AdminManager();
			_userManager = new UserManager();
			_postManager = new PostManager();

			Console.WriteLine("WELCOME TO PUPHUB 2023!");
			Console.WriteLine("ENTER 1 IF ADMIN, AND IF NOT ENTER ANYKEY: ");
			string admin = Console.ReadLine();


               bool loggedIn = false;
			   string loggedInFacultyNumber = string.Empty;
			   string loggedInUsername = string.Empty;

			if (admin == "1")// ADMIN TASK - LEGASPINO & TORRES
			{
				Console.Write("Faculty Number: ");
				string FacultyNumber = Console.ReadLine();
				Console.Write("Password: ");
				string PassAdmin = Console.ReadLine();

				bool loginSuccess = _adminManager.Login(FacultyNumber, PassAdmin);

				while (loggedIn)

					Console.WriteLine("\n ------------------- WELCOME ADMIN -------------------");
				Console.WriteLine("\n1. Create Post");
				Console.WriteLine("2. View  Admin Post ");
				Console.WriteLine("3. View all post per Student");
				Console.WriteLine("4. Edit Admin Post ");
				Console.WriteLine("5. Delete post per Student");
				Console.WriteLine("6. Delete Admin Post");
				Console.WriteLine("7. Logout");

				Console.Write("Enter your choice: ");
				string choice = Console.ReadLine();

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

						_postManager.CreatePostAdmin(content, loggedInFacultyNumber, postType);

						break;

					case "2":
						DisplayPostsForAdmin(loggedInFacultyNumber);
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

						_postManager.EditPostAdmin(postNumber, newContent);


						break;


					case "5":
						Console.WriteLine("Delete post per Student");
						// wala papo 
						break;

					case "6":
						Console.WriteLine("Delete a post:");
						Console.Write("Enter the post number: ");
						int postNumberToDelete = int.Parse(Console.ReadLine());

						_postManager.DeletePostAdmin(postNumberToDelete);
						break;
					case "7":
						loggedIn = false;
						loggedInFacultyNumber = string.Empty;
						Console.WriteLine("Logout successful!");
						break;

					default:
						Console.WriteLine("Invalid choice. Please try again.");
						break;

				}
			}
			else if (!loggedIn)  
			   {
				Console.Write("Username: ");
				string username = Console.ReadLine();
				Console.Write("Student Number: ");
				string studentNumber = Console.ReadLine();

				bool loginSuccess = _userManager.Login(username, studentNumber);

				if(loginSuccess)
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