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
		private DatabaseManager _databaseManager;

		public PostAccess()
		{
			_databaseManager = new DatabaseManager();
		}



		public void CreatePost(PostContent post)
		{
			int maxPostNumber = GetLatestPostNumber(post.Username);
			post.PostId = maxPostNumber + 1;

			_databaseManager.CreatePost(post);
		}

		public List<PostContent> GetPosts()
		{
			return _databaseManager.GetPosts();
		}

		public PostContent GetPostByNumber(int postNumber)
		{
			return _databaseManager.GetPostNumber(postNumber);
		}

		public void UpdatePost(PostContent post)
		{
			_databaseManager.UpdatePost(post);
		}

		public void DeletePost(PostContent post)
		{
			_databaseManager.DeletePost(post);
		}
		private int GetLatestPostNumber(string username)
		{
			var posts = _databaseManager.GetPosts().Where(post => post.Username == username);
			int maxPostNumber = posts.Any() ? posts.Max(post => post.PostId) : 0;
			return maxPostNumber;
		}

        public void CreatePostAdmin(PostContent post)
        {
            throw new NotImplementedException();
        }
    }
}