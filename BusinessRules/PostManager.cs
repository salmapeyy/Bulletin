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
		public bool DeletePostByNumber(int postNumber)
		{
			var post = _postAccess.GetPosts().FirstOrDefault(p => p.PostId == postNumber);
			if (post != null)
			{
				_postAccess.DeletePost(post);
				return true;
			}
			return false;
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
