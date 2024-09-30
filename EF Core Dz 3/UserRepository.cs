using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Dz_3
{
    public class UserRepository
    {
        private readonly ApplicationContext context;
        public UserRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public bool RegisterUser(User user)
        {
            if (context.Users.Any(u => u.UserName == user.UserName))
            {

                return false;
            }

            string passwordHash = ComputeHash(user.PasswordHash);
            user.PasswordHash = passwordHash;
            context.Users.Add(user);
            context.SaveChanges();
            return true;

        }
        public bool AuthenticateUser(User user)
        {
            string passwordHash = ComputeHash(user.PasswordHash);
            return context.Users.Any(u => u.UserName == user.UserName && u.PasswordHash == passwordHash);
        }
        private string ComputeHash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
