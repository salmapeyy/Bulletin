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

		public bool Login(string username, string password)
		{
			return _userAccess.ValidateCredentials(username, password);
		}
        //public bool CorrectUser(string username)
        //{
        //    return _userAccess.ValidateCredentials(username);
        //}
    }
}
