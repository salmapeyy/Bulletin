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
		
		 private PostAccess _postAccess;
		 private AdminPostAccess _adminPostAccess;
		 private string loggedInUsername;
		 private string loggedInfacultyNumber;

		public PostManager()
		{
			_postAccess = new PostAccess();
			_adminPostAccess = new AdminPostAccess();	
		}
		  

        public void CreatePostAdmin(string content, string facultyNumber, string postType)
        {
            PostContent post = new PostContent
            {
                Content = content,
                FacultyNumber = facultyNumber,
                DateCreated = DateTime.Now,
                LastModified = DateTime.Now,
                PostType = postType
            };

            _adminPostAccess.CreatePostAdmin(post);
            Console.WriteLine("Post Created Successfully!");
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
        public void EditPostAdmin(int postNumber, string newContent)
        {
            PostContent post = _adminPostAccess.GetPostByNumber(postNumber);
            if (post != null)
            {
                post.Content = newContent;
                post.LastModified = DateTime.Now;
                _adminPostAccess.UpdatePost(post);
                Console.WriteLine("Post Edited Successfully!");
            }
            else
            {
                Console.WriteLine("No Post Found!");
            }
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
        public void DeletePostAdmin(int postNumber)
        {
            PostContent post = _adminPostAccess.GetPostByNumber(postNumber);
            if (post != null)
            {
                _adminPostAccess.DeletePost(post);
                Console.WriteLine("Post Deleted Successfully!");
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
        public List<PostContent> GetPostsForAdmin(string facultyNumber)
        {
            var allPosts = _adminPostAccess.GetPosts();
            return allPosts.FindAll(post => post.FacultyNumber == facultyNumber);
        }


        public List<PostContent> GetPostsForUser(string username)
		{
			var allPosts = _postAccess.GetPosts();
			return allPosts.FindAll(post => post.Username == username);
		}

        public void CreatePost(string? content, object loggedInFacultyNumber)
        {
            throw new NotImplementedException();
        }
    }
}