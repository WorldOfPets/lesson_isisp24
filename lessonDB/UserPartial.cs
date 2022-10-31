using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lessonDB
{
    public partial class User
    {
        public int Age
        {
            get
            {
                if (birthdate != null)
                {
                    int age = DateTime.Now.Year - birthdate.Value.Year;
                    return age;
                }
                else
                {
                    return 0;
                }

            }
        }

        public string color
        {
            get
            {
                if (idRole == 1)
                {
                    return "lightgreen";
                }
                else
                {
                    return "lightgray";
                }

            }
        }

        public string pass {
            get {
                char first_char = password[0];
                char last_char = password[password.Length - 1];
                return first_char + "*******" + last_char;
            }
        }
    }
}
