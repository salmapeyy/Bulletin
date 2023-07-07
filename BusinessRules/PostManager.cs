using System;
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
		private PostAccess _postRepository;

		public PostManager()
		{
			_postRepository = new PostAccess();
		}

		public void CreatePost(string content, string username)
		{
			PostContent post = new PostContent
			{
				Content = content,
				Username = username,
				DateCreated = DateTime.Now,
				LastModified = DateTime.Now
			};

			_postRepository.CreatePost(post);
		}

		public void EditPost(int postNumber, string newContent)
		{
			PostContent post = _postRepository.GetPostByNumber(postNumber);
			if (post != null)
			{
				post.Content = newContent;
				post.LastModified = DateTime.Now;
				_postRepository.UpdatePost(post);
			}
		}

		public List<PostContent> GetPosts()
		{
			return _postRepository.GetPosts();
		}
	}
}