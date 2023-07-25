using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AdminAccess
    {
        private Dictionary<string, string> _adminCredentials;

        public AdminAccess()
        {
            _adminCredentials = new Dictionary<string, string>
            {
                { "AD-000-BN-0", "ADMIN"},
            };

        }

        public bool ValidateCredentials(string FacultyNumber, string PassAdmin)

        {
            if (_adminCredentials.TryGetValue(FacultyNumber, out string storedPassAdmin))
            {
                return PassAdmin == storedPassAdmin;
            }

            return false;
        }
    }
}
