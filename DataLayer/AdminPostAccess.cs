using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AdminPostAccess
    {
        private DatabaseManager _databaseManager;

        public AdminPostAccess()
        {
            _databaseManager = new DatabaseManager();
        }



        public void CreatePostAdmin(PostContent post)
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

      
    }
}
