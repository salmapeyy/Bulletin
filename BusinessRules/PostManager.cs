using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using Models;

namespace BusinessRules
{
	public class PostManager
	{
		private PostAccess _postAccess;
		private UserAccess _userAccess;
		private const string AdminUsername = "admin";

		public PostManager()
		{
			_postAccess = new PostAccess();
			_userAccess = new UserAccess();
		}
		// CREATE
		public void CreatePost(string content, string username, string postType)
		{
			var post = new PostContent
			{
				Content = content,
				Username = username,
				DateCreated = DateTime.Now,
				LastModified = DateTime.Now,
				PostType = postType
			};

			_postAccess.CreatePost(post);
			//Console.WriteLine("\n------------------- Post Created Successfully! -----------------");
		}
		//UPDATE
		public bool EditPost(int postNumber, string newContent)
		{
			var post = _postAccess.GetPostByNumber(postNumber);
			if (post != null)
			{
				post.Content = newContent;
				post.LastModified = DateTime.Now;
				_postAccess.UpdatePost(post);
				//Console.WriteLine("\n------------------- Post Edited Successfully! -----------------");
				return true;
			}
			else
			{
				//Console.WriteLine("\n------------------- No Post Found! -----------------");
				return false;

			}

		}

		public void DeletePost(int postNumber)
		{
			var post = _postAccess.GetPostByNumber(postNumber);
			if (post != null)
			{
				_postAccess.DeletePost(post);
			}
			else
			{
				Console.WriteLine("\n------------------- No Post Found! -------------------");
			}
		}
		// DELETING OWN POST METHOD
		public void DeleteOwnPost(string loggedInUsername)
		{
			Console.WriteLine($"Logged-in user: {loggedInUsername}");

			var userPosts = GetPostsForUser(loggedInUsername);

			if (userPosts.Any())
			{
				Console.WriteLine($"Your Posts:");
				//DisplayPosts(userPosts);

				Console.Write("Enter the post number to delete: ");
				if (int.TryParse(Console.ReadLine(), out int postNumberToDelete))
				{
					var postToDelete = userPosts.FirstOrDefault(p => p.PostId == postNumberToDelete);
					if (postToDelete != null)
					{
						DeletePost(postNumberToDelete);
						//Console.WriteLine("\n------------------- Post Deleted Successfully! ------------------");
					}
					else
					{
						//Console.WriteLine("\n------------------- Invalid post number or post not found -------------------");
					}
				}
				else
				{
					Console.WriteLine("\n------------------- Invalid post number. Please enter a valid number -------------------");
				}
			}
			else
			{
				Console.WriteLine($"No posts found for user '{loggedInUsername}'.");
			}
		}
		//DELETING POST BY ADMIN
		public void DeleteOwnPostOrForUser(string loggedInUsername)
		{
			Console.WriteLine($"\nUSERNAME: {loggedInUsername}");
			bool isAdmin = loggedInUsername.Equals("ADMIN", StringComparison.OrdinalIgnoreCase);

			if (isAdmin)
			{
				Console.WriteLine("\nDo you want to delete your own post or the post of another user?");
				Console.WriteLine("1. Delete your own post");
				Console.WriteLine("2. Delete a post of another user");
				Console.Write("Enter your choice (1 or 2): ");
				if (int.TryParse(Console.ReadLine(), out int choice))
				{
					switch (choice)
					{
						case 1:
							DeleteOwnPost(loggedInUsername);
							break;
						case 2:
							DeletePostForUser(loggedInUsername);
							break;
						default:
							Console.WriteLine("\n------------------- Invalid choice. Please enter 1 or 2 -------------------");
							break;
					}
				}
				else
				{
					Console.WriteLine("\n------------------- Invalid input. Please enter a valid choice -------------------");
				}
			}
			else
			{
				DeleteOwnPost(loggedInUsername);
			}
		}

		public void DeletePostForUser(string loggedInUsername)
		{
			bool isAdmin = loggedInUsername.Equals("ADMIN", StringComparison.OrdinalIgnoreCase);

			if (isAdmin)
			{
				Console.Write("\nEnter the username of the user whose post you want to delete: ");
				string userToDelete = Console.ReadLine().Trim();

				if (!_userAccess.UserExists(userToDelete))
				{
					Console.WriteLine("\n------------------- No user found -------------------");
					return;
				}

				var userPosts = GetPostsForUser(userToDelete);

				Console.WriteLine($"Posts of user '{userToDelete}':");
				//DisplayPosts(userPosts);

				Console.Write("\nEnter the post number to delete: ");
				if (int.TryParse(Console.ReadLine(), out int postNumberToDelete))
				{
					var postToDelete = userPosts.FirstOrDefault(p => p.PostId == postNumberToDelete);
					if (postToDelete != null)
					{
						if (userToDelete.Equals(postToDelete.Username, StringComparison.OrdinalIgnoreCase))
						{
							DeletePost(postNumberToDelete);
							Console.WriteLine("\n------------------- Post Deleted Successfully! -------------------");
						}
						else
						{
							Console.WriteLine("You don't have permission to delete this post.");
						}
					}
					else
					{
						Console.WriteLine("Invalid post number or post not found for the specified user.");
					}
				}
				else
				{
					Console.WriteLine("Invalid post number. Please enter a valid number.");
				}
			}
			else
			{
				Console.WriteLine("You don't have permission to delete posts.");
			}
		}
		// DISPLAYING POSTS
		public List<PostContent> GetPostsForUser(string username)
		{
			var allPosts = _postAccess.GetPosts();
			return allPosts.FindAll(post => post.Username == username);
		}

		public List<PostContent> GetPosts()
		{
			return _postAccess.GetPosts();
		}


		//METHODS FOR LIKE AND DISLIKE
		public void LikeOrDislikePost(int postNumber, bool isLiked)
		{
			var post = _postAccess.GetPostByNumber(postNumber);
			if (post != null)
			{
				if (isLiked)
				{
					post.Likes++;
					//Console.WriteLine("Post Liked!");
				}
				else
				{
					post.Dislikes++;
					//Console.WriteLine("Post Disliked!");
				}

				_postAccess.UpdatePost(post);
			}
		}
	}
}
