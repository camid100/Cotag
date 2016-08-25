using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public static class Common
    {
        public static string getUsername()
        {
            string loggedUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string[] words = loggedUser.Split('\\');
            string username;
            username = words[words.Length - 1];
            return username;
        }
    }
}
