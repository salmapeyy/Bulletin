using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataLayer
{
	public class GetPostNumber
	{
		private List<GetPostContent> _posts;

		public GetPostNumber()
		{
			_posts = new List<GetPostContent>();
		}

		// Automatic Increment for PostNumber
		public void AddPost(GetPostContent post)
		{
			post.Number = _posts.Count + 1;
			_posts.Add(post);
		}

		public List<GetPostContent> GetPosts()
		{
			return _posts;
		}
	}
}