using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataLayer
{
	public class PostAccess
	{
		private List<PostContent> _posts;
		private int _nextPostId;

		public PostAccess()
		{
			_posts = new List<PostContent>();
			_nextPostId = 1;
		}
		//Creating Post
		public void CreatePost(PostContent post)
		{
			post.PostId = _nextPostId++;
			_posts.Add(post);
		}
		//Listing Post
		public List<PostContent> GetPosts()
		{
			return _posts;
		}
		public PostContent GetPostByNumber(int postNumber)
		{
			return _posts.FirstOrDefault(post => post.PostId == postNumber);
		}
		//Editing Post
		public void UpdatePost(PostContent post)
		{
			PostContent existingPost = _posts.FirstOrDefault(p => p.PostId == post.PostId);
			if (existingPost != null)
			{
				existingPost.Content = post.Content;
				existingPost.LastModified = post.LastModified;
			}
		}
		//Delete Post
		public void DeletePost(PostContent post) 
		{
			post.PostId = _nextPostId--;
			_posts.Remove(post);
		}
	}
}