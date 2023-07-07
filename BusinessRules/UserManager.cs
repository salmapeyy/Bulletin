using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Models;

namespace BusinessRules
{
	public class UserManager
	{
		private UserAccess _userAccess;

		public UserManager()
		{
			_userAccess = new UserAccess();
		}

		public bool CreateUser(string username, string password)
		{
			UserInfo user = new UserInfo
			{
				Username = username,
				Password = password
			};

			return _userAccess.CreateUser(user);
		}

		public bool Login(string username, string password)
		{
			return _userAccess.ValidateCredentials(username, password);
		}
	}
}
