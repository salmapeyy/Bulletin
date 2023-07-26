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
		private Dictionary<string, string> _userCredentials;

		public UserAccess()
		{
			_userCredentials = new Dictionary<string, string>
			{
				{ "NelsonAbn", "2021-00413-BN-0" },
				{ "itsme_kai", "2021-00217-BN-0" },
				{ "JB", "2021-00417-BN-0" },
				{ "Raf_7", "2021-00416-BN-0" },
				{ "Nìcs", "2021-00215-BN-0" },
				{ "kylaaki", "2021-00062-BN-0" },
				{ "Bentor", "2021-00223-BN-0" },
				{ "Jaskuno", "2021-00404-BN-0" },
				{ "andreabalaba", "2021-00490-BN-0" },
				{ "rosejoy_balonzo", "2021-0058-BN-0" },
				{"Yityet07", "2021-00200-BN-0"},
				{ "Laica Erica", "2021-00341-BN-0" },
				{ "andreilangtoguys", "021-00216-BN-0"},
				{"Nica_21", "2021-00155-BN-0" },
				{"JAC", "2021-00157-BN-0" },
				{"sharie.crvn", "2021-00397-BN-0" },
				{ "Charles_A", "2021-00158-BN-0"},
				{ "minatamis", "2021-00057-BN-0"},
				{ "rishingz", "2021-00059-BN-0"},
				{ "Jm Dinglasan", "2021-00065-BN-0" },
				{"Kyrue", "2021-00074-BN-0" },
				{"hyunysss", "2021-00064-BN-0" },
				{"encisohappy08", "2021-00428-BN-0" },
				{ "irwennn", "2021-00489-BN-0"},
				{ "Seanshine", "2021-00199-BN-0" },
				{"Eyaann27", "2021-00156-BN-0" },
				{"Nolongerhakori", "2021-00211-BN-0" },
				{"Earth", "2021-00152-BN-0" },
				{"Itschii", "2021-00212-BN-0" },
				{"reginaflopezx", "2021-00268-BN-0" },
				{"salmafae0809", "2021-00159-BN-0"},
				{"Watss", "2021-00418-BN-0"},
				{"Angel", "2021-00182-BN-0"},
				{"knnth0220", "2021-00347-BN-0"},
				{"chilikalbo", "2021-00188-BN-0"},
				{"gjmp", "2021-00412-BN-0"},
				{"Jemen0225", "2021-00515-BN-0"},
				{"stphnprz09", "2021-00066-BN-0"},
				{"@rzllmqtlg", "2021-00408-BN-0"},
				{"Johnjan69", "2021-00063-BN-0"},
				{"Jimwell", "2021-00154-BN-0"},
				{"paaats", "2021-00061-BN-0"},
				{"WD_mstrJHNT", "2021-00224-BN-0"},
				{"Vic", "2021-00266-BN-0"},

		};
		}

		public bool ValidateCredentials(string username, string studentNumber)
		{
			if (_userCredentials.TryGetValue(username, out string storedStudentNumber))
			{
				return studentNumber == storedStudentNumber;
			}

			return false;
		}
        //public bool ValidateCredentials2(string username)
        //{
        //    if (_userCredentials.TryGetValue(username))
        //    {
        //        return studentNumber == storedStudentNumber;
        //    }

        //    return false;
        //}
    }
}