using Microsoft.EntityFrameworkCore.Storage;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace EF_Core_Dz_3
{
    internal class Program
    {
        public enum Menu { Register = 1, Login, Exit }
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var userService = new UserRepository(db);
                int count = 1;

                foreach (var item in Enum.GetValues(typeof(Menu)))
                {
                    Console.WriteLine($"{count++}. {item}");
                }
                int choice = 0;

                while (true)
                {
                    choice = Helper.GetInt("menu item");

                    switch ((Menu)choice)
                    {
                        case Menu.Register:
                            {
                                if (Register(userService))
                                {
                                    Helper.WhriteSuccessfulMessage("OK");
                                }
                                else
                                {
                                    goto case Menu.Register;
                                }
                                break;

                            }
                        case Menu.Login:
                            {
                                string authUsername = Helper.GetString("username");
                                string authPassword = Helper.GetString("password");
                                User user = new User()
                                {
                                    UserName = authUsername,
                                    PasswordHash = authPassword
                                };
                                if (Helper.Check(user))
                                {

                                    if (userService.AuthenticateUser(user))
                                    {
                                        Helper.WhriteSuccessfulMessage($"ok");
                                    }
                                    else
                                    {
                                        Helper.WhriteErrorMessage($"NOT OK");
                                    }

                                }
                                else
                                {
                                    goto case Menu.Login;
                                }

                                break;

                            }
                        case Menu.Exit:
                            {
                                return;
                            }
                        default:
                            Helper.WhriteErrorMessage("Try again");
                            break;

                    }
                }
            }
        }
        private static bool Register(UserRepository userService)
        {
            string regUsername = Helper.GetString("username");
            string regPassword = Helper.GetString("password");
            User user = new User()
            {
                UserName = regUsername,
                PasswordHash = regPassword
            };
            if (Helper.Check(user))
            {
                userService.RegisterUser(user);
                return true;
            }
            return false;
        }
    }
}
