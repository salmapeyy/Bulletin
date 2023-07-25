﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Models;

namespace BusinessRules
{
	public class PostManager
	{
		private PostAccess _postAccess;
		private string loggedInUsername;

		public PostManager()
		{
			_postAccess = new PostAccess();
		}

		public void CreatePost(string content, string username, string postType)
		{
			PostContent post = new PostContent
			{
				Content = content,
				Username = username,
				DateCreated = DateTime.Now,
				LastModified = DateTime.Now,
				PostType = postType
			};

			_postAccess.CreatePost(post);
			Console.WriteLine("Post Created Successfully!");
		}

		public void EditPost(int postNumber, string newContent)
		{
			PostContent post = _postAccess.GetPostByNumber(postNumber);
			if (post != null)
			{
				post.Content = newContent;
				post.LastModified = DateTime.Now;
				_postAccess.UpdatePost(post);
				Console.WriteLine("Post Edited Successfully!");
			}
			else
			{
				Console.WriteLine("No Post Found!");
			}
		}

		public void DeletePost(int postNumber)
		{
			PostContent post = _postAccess.GetPostByNumber(postNumber);
			if (post != null)
			{
				_postAccess.DeletePost(post);
				Console.WriteLine("Post Deleted Successfully!");
			}
			else
			{
				Console.WriteLine("No Post Found!");
			}
		}

		public List<PostContent> GetPostsForUser(string username)
		{
			var allPosts = _postAccess.GetPosts();
			return allPosts.FindAll(post => post.Username == username);
		}
		public List<PostContent> GetPosts()
		{
			return _postAccess.GetPosts();
		}
	}
}