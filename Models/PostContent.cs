using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Models
{
	public class PostContent
	{
		public int PostId { get; set; }
		public string Content { get; set; }
		public string Username { get; set; }
		//Editing A Post Time and Date Changes
		public DateTime DateCreated { get; set; }
		public DateTime LastModified { get; set; }
	}
}