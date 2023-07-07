using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataLayer
{
	public class UserAccess
	{
		private List<UserInfo> _users;

		public UserAccess()
		{
			_users = new List<UserInfo>();
		}

		public bool CreateUser(UserInfo user)
		{

			if (_users.Exists(u => u.Username == user.Username))
			{
				return false;
			}

			_users.Add(user);
			return true;
		}

		public bool ValidateCredentials(string username, string password)
		{
			UserInfo user = _users.Find(u => u.Username == username && u.Password == password);
			return user != null;
		}
	}
	public class AdminAccess
	{
		private List<AdminInfo> _admins;

		public AdminAccess()
		{
			_admins = new List<AdminInfo>();
		}

		public bool ValidateCredentials(string FacultyNumber, string passAdmin)
		{
			AdminInfo admin = _admin.Find(a => a.FacultyNumber == facultyNumber && a.PassAdmin == passAdmin);
			return admin != null;
		}
         }
}
