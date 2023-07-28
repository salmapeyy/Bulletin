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

		private static void DisplayPosts(List<PostContent> posts)
		{
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

		private static void DisplayPostsForUser(string loggedInUsername)
		{
			var posts = _postManager.GetPostsForUser(loggedInUsername);
			Console.WriteLine($"\nBulletin Board of {loggedInUsername}:\n");
			DisplayPosts(posts);
		}

		private static void DisplayPostsForAdmin()
		{
			var posts = _postManager.GetPostsForUser("ADMIN");
			Console.WriteLine($"\nBulletin Board of ADMIN:\n");
			DisplayPosts(posts);
		}

		private static bool AdminLogin()
		{
			Console.Write("\nAdmin Number: ");
			string adminNumber = Console.ReadLine();
			Console.Write("Password: ");
			string adminPassword = Console.ReadLine();

			// Admin Credentials (Only admins have access)
			string adminNumberExpected = "ADMIN123";
			string adminPasswordExpected = "adminpassword";

			return adminNumber == adminNumberExpected && adminPassword == adminPasswordExpected;
		}

		private static void DisplayPostsForAllUsers()
		{
			var allPosts = _postManager.GetPosts();
			Console.WriteLine("\nAll Users' Bulletin Board:\n");
			DisplayPosts(allPosts);
		}

		private static void Main(string[] args)
		{
			_userManager = new UserManager();
			_postManager = new PostManager();

			Console.WriteLine(" --------------------------------------------------------");
			Console.WriteLine("|               PUPHUB BULLETIN BOARD                    |");
			Console.WriteLine(" --------------------------------------------------------");

			bool loggedIn = false;
			string loggedInUsername = string.Empty;
			bool isAdmin = false;

			// The system will ask if the user is an Admin or a Student
			while (!loggedIn)
			{
				Console.Write("\nAre you a Student or an Admin? (Student/Admin): ");
				string userType = Console.ReadLine().ToLower();

				if (userType == "admin")
				{
					if (AdminLogin())
					{
						loggedIn = true;
						isAdmin = true;
						loggedInUsername = "ADMIN";
						Console.WriteLine("\n------------------- Admin login successful! -----------------");
					}
					else
					{
						Console.WriteLine("\nInvalid admin credentials. Please try again.");
					}
				}
				else if (userType == "student")
				{
					Console.Write("\nUsername: ");
					string username = Console.ReadLine();
					Console.Write("Student Number: ");
					string studentNumber = Console.ReadLine();

					bool loginSuccess = _userManager.Login(username, studentNumber);

					if (loginSuccess)
					{
						loggedIn = true;
						loggedInUsername = username;
						Console.WriteLine("\n--------------------Login successful!--------------------\n");

						Console.WriteLine("---------------------------------------------------------");
						Console.WriteLine($"|                WELCOME {username}                    |");
						Console.WriteLine("---------------------------------------------------------");
					}
					else
					{
						Console.WriteLine("\nInvalid username or student number. Please try again.");
					}
				}
				else
				{
					Console.WriteLine("\nInvalid choice. Please enter 'Student' or 'Admin' only.");
				}
			}

			// After logging in, Post Functions (CREATE, VIEW, DELETE, EDIT)
			while (loggedIn)
			{
				Console.WriteLine("\n1. Create Post");
				Console.WriteLine("2. View Posts");
				Console.WriteLine("3. Edit a Post");
				Console.WriteLine("4. Delete a Post");
				Console.WriteLine("5. Like / Dislike a Post");
				Console.WriteLine("6. Logout");
				Console.Write("\nEnter your choice: ");
				string choice = Console.ReadLine();

				switch (choice)
				{
					case "1":
						Console.WriteLine("\nSelect the post type:");
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
							Console.WriteLine("\nInvalid post type selection. Please try again.");
							break;
						}

						Console.Write("\nEnter your post content/link: ");
						string content = Console.ReadLine();

						_postManager.CreatePost(content, loggedInUsername, postType);
						break;

					case "2":
						Console.WriteLine("\nSelect which posts to view:");
						Console.WriteLine("1. MY BULLETIN BOARD");
						Console.WriteLine("2. ADMIN'S BULLETIN BOARD");
						Console.WriteLine("3. ALL USERS' BULLETIN BOARD");

						Console.Write("\nEnter your choice: ");
						string viewChoice = Console.ReadLine();

						if (viewChoice == "1")
						{
							DisplayPostsForUser(loggedInUsername);
						}
						else if (viewChoice == "2")
						{
							DisplayPostsForAdmin();
						}
						else if (viewChoice == "3")
						{
							DisplayPostsForAllUsers();
						}
						else
						{
							Console.WriteLine("\nInvalid choice.");
						}
						break;

					case "3":
						Console.WriteLine("\nEdit a post:");
						Console.Write("Enter the post number: ");
						int postNumber = int.Parse(Console.ReadLine());
						Console.Write("Enter the new content: ");
						string newContent = Console.ReadLine();

						_postManager.EditPost(postNumber, newContent);
						break;

					case "4":
						_postManager.DeleteOwnPostOrForUser(loggedInUsername);
						break;

					case "5":
						Console.Write("\nEnter the username of the user whose post you want to like or dislike: ");
						string userToInteract = Console.ReadLine().Trim();
						DisplayPostsForUser(userToInteract);

						Console.Write("Enter the post number to like or dislike: ");
						if (int.TryParse(Console.ReadLine(), out int postNumberToInteract))
						{
							Console.Write("Do you want to like or dislike the post? (like/dislike): ");
							string likeOrDislikeChoice = Console.ReadLine().ToLower();

							if (likeOrDislikeChoice == "like")
							{
								_postManager.LikeOrDislikePost(postNumberToInteract, isLiked: true);
							}
							else if (likeOrDislikeChoice == "dislike")
							{
								_postManager.LikeOrDislikePost(postNumberToInteract, isLiked: false);
							}
							else
							{
								Console.WriteLine("Invalid choice. Please enter 'like' or 'dislike'.");
							}
						}
						else
						{
							Console.WriteLine("Invalid post number. Please enter a valid number.");
						}
						break;

					case "6":
						loggedIn = false;
						loggedInUsername = string.Empty;

						Console.WriteLine("");
						Console.WriteLine("-----------------------------------------------------");
						Console.WriteLine($"|                  THANK YOU!                      |");
						Console.WriteLine("-----------------------------------------------------");
						break;

					default:
						Console.WriteLine("\nInvalid choice. Please try again.");
						break;


				}
			}
		}
	}
}
