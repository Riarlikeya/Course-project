using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminProgram.Models
{
    public static class UserManager
    {
        private static int currentUserId;

        public static int CurrentUserId
        {
            get => currentUserId;
            private set
            {
                currentUserId = value;
            }
        }

        public static void SetCurrentUser(int userId)
        {
            CurrentUserId = userId;
        }

        public static int GetCurrentUser()
        {
            return CurrentUserId;
        }

       
    }
}
