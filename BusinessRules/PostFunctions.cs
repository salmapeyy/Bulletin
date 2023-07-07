using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Models;
using System.Collections.Generic;

namespace BusinessRules
{
	public class PostFunctions
	{
		private GetPostNumber _getPostNumber;

		public PostFunctions()
		{
			_getPostNumber = new GetPostNumber();
		}

		public void CreatePost(string content)
		{
			GetPostContent post = new GetPostContent
			{
				Content = content
			};

			_getPostNumber.AddPost(post);
		}

		public List<GetPostContent> GetPosts()
		{
			return _getPostNumber.GetPosts();
		}
	}
}