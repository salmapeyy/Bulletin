using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules
{
    public class AdminManager
    {
        private AdminAccess _adminAccess;

        public AdminManager()
        {
            _adminAccess = new AdminAccess();
   
        }

        public bool Login(string FacultyNumber, string PassAdmin)
        {
            return _adminAccess.ValidateCredentials(FacultyNumber, PassAdmin);
        }
    }
}
