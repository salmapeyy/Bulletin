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
			_databaseManager.CreatePost(post);
		}

		public List<PostContent> GetPosts()
		{
			return _databaseManager.GetPosts();
		}

		public PostContent GetPostByNumber(int postNumber)
		{
			return _databaseManager.GetPostByNumber(postNumber);
		}

		public void UpdatePost(PostContent post)
		{
			_databaseManager.UpdatePost(post);
		}

		public void DeletePost(PostContent post)
		{
			_databaseManager.DeletePost(post);
		}
	}
}