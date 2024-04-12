using SignInAndSignUp.Controls.Concret;
using SignInAndSignUp.DataBase;
using SignInAndSignUp.Entity;
using System.Text;

namespace SigInAndSignUp
{
    internal class Program
    {
        private readonly static UserManager userManager = new UserManager();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool isC = true;
            while (isC)
            {
                Console.WriteLine("1-Daxil ol \n2-Qeydiyyat \n3-Çıxış");
                Console.Write("Seçim et(1-3): ");
                var isInteger = int.TryParse(Console.ReadLine(), out int value);

                if (isInteger)
                {
                    switch (value)
                    {
                        case 1:
                            User user = new User();
                            Console.WriteLine("UserName: ");
                            user.UserName = Console.ReadLine();
                            Console.WriteLine("Parol: ");
                            user.Password = Console.ReadLine();
                            (string ms, bool ch) = SignIn(user, out User outUser);

                            Console.WriteLine("\nDoğrulama aparılır...");
                            Thread.Sleep(2000);
                            Console.Clear();
                            if (ch)
                            {
                                Account account = new Account(outUser);
                                if(account.Profile.UserName == "admin")
                                {
                                    bool istrue = true;
                                    while (istrue)
                                    {
                                        Console.WriteLine("1-Add User; 2-Edit User by id; 3-Show Users; 4-Delete user by Id; 5-leave Program");
                                        bool isIntegers = int.TryParse(Console.ReadLine(), out int command);
                                        if (isIntegers)
                                        {
                                            switch (command)
                                            {
                                                case 1:
                                                    AddUser();
                                                    break;
                                                case 2:
                                                    EditUserById();
                                                    break;
                                                case 3:
                                                    ShowAllUsers();
                                                    break;
                                                case 4:
                                                    DeleteUserById();
                                                    break;
                                                case 5:
                                                    istrue = false;
                                                    break;

                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(ms);
                                    Console.WriteLine("----------------------------------------");
                                startSignIn:
                                    Console.WriteLine("\n1-Profili göstər" +
                                        "\n2-Porfili redaktə et" +
                                        "\n3-Profili sil" +
                                        "\n4-Profildən çıx");
                                    Console.WriteLine("\nSeçim et(1-4): ");
                                    bool isIntegerLogIn = int.TryParse(Console.ReadLine(), out int loginValue);
                                    if (isIntegerLogIn)
                                    {
                                        switch (loginValue)
                                        {
                                            case 1:
                                                List<User> a = new List<User>();
                                                a.Add(account.Profile);
                                                ConsoleShowUsers(a);
                                                goto startSignIn;
                                            case 2:
                                                Console.WriteLine("Enter user username");
                                                string userName = Console.ReadLine();

                                                Console.WriteLine("Enter user email");
                                                string email = Console.ReadLine();

                                                Console.WriteLine("Enter user password");
                                                string password = Console.ReadLine();

                                                User newUser = new User()
                                                {
                                                    ID = account.Profile.ID,
                                                    UserName = userName,
                                                    Email = email,
                                                    Password = password
                                                };
                                                UserManager.Instance.Update(newUser);
                                                break;

                                            case 3:
                                                userManager.Delete(account.Profile.ID);
                                                break;

                                            case 4:
                                                Console.Clear();
                                                Console.WriteLine("Çıxış edildi!!");
                                                break;

                                            default:
                                                Console.Clear();
                                                Console.WriteLine("Düzgün seçim edilməyib!!");
                                                Console.WriteLine("----------------------------------------");
                                                goto startSignIn;
                                        }
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Düzgün simvol daxil edilməyib!!");
                                        Console.WriteLine("----------------------------------------");
                                        goto startSignIn;
                                    }
                                }
                            }
                            Console.WriteLine(ms);
                            break;
                        case 2:
                            User signUpUser = new User();
                            Console.WriteLine("Profil adı: ");
                            signUpUser.UserName = Console.ReadLine();
                            Console.WriteLine("Email: ");
                            signUpUser.Email = Console.ReadLine();
                            Console.WriteLine("Parol: ");
                            signUpUser.Password = Console.ReadLine();
                            Console.WriteLine("Tekrar Parol: ");
                            string repeatPassword = Console.ReadLine();
                            if (SignUp(signUpUser, repeatPassword))
                            {
                                Console.Clear();
                                Console.WriteLine("Əlavə edildi!!");
                                Console.WriteLine("----------------------------------------\n");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Əlavə edilmədi!!");
                                Console.WriteLine("----------------------------------------\n");
                            }
                            break;
                        case 3:
                            isC = false;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Düzgün seçim edilməyib!!");
                            Console.WriteLine("----------------------------------------");
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Düzgün simvol daxil edilməyib!!");
                    Console.WriteLine("----------------------------------------");
                }
            }
        }

        private static bool SignUp(User signUpUser, string repeatPassword)
        {
            if (signUpUser.Password == repeatPassword)
                return userManager.Add(signUpUser);

            return false;
        }

        private static void ConsoleShowUsers(List<User> users)
        {
            Console.Clear();
            foreach (var user in users)
            {
                if(user.UserName != "admin")
                {
                    Console.WriteLine("\n***********************************");
                    Console.WriteLine($"Profil Id: {user.ID}");
                    Console.WriteLine("Profile adı: " + user.UserName);
                    Console.WriteLine("Email: " + user.Email);
                    Console.WriteLine("Parol: " + user.Password);
                    Console.WriteLine("***********************************");
                }
            }
        }

        private static (string m, bool ch) SignIn(User user, out User outUser)
        {
            var check = userManager.HasUser(user, out outUser);
            if (!check)
                return ("Profil adı və ya Parol səhvdir", false);

            return ("Daxil oldunuz)", true);
        }

        static void AddUser()
        {
            Console.WriteLine("Enter user username");
            string userName = Console.ReadLine();

            Console.WriteLine("Enter user email");
            string email = Console.ReadLine();

            Console.WriteLine("Enter user password");
            string password = Console.ReadLine();

            User newUser = new User()
            {
                UserName = userName,
                Email = email,
                Password = password
            };
            DB.DataBase.users.Add(newUser);
        }

        static void EditUserById()
        {
            ShowAllUsers();

            Console.WriteLine("Enter id for Edit");
            int.TryParse(Console.ReadLine(), out int id);

            var user = UserManager.Instance.GetById(id);

            Console.WriteLine("Enter user username");
            string userName = Console.ReadLine();

            Console.WriteLine("Enter user email");
            string email = Console.ReadLine();

            Console.WriteLine("Enter user password");
            string password = Console.ReadLine();

            User newUser = new User()
            {   ID = id,
                UserName = userName,
                Email = email,
                Password = password
            };
            UserManager.Instance.Update(newUser);

        }

        static void ShowAllUsers()
        {
            Console.Clear();
            var users = UserManager.Instance.GetAll();
            ConsoleShowUsers(users);
        }

        static void DeleteUserById()
        {
            Console.WriteLine("Enter id for Delete");
            int.TryParse(Console.ReadLine(), out int id);
            UserManager.Instance.Delete(id);
        }
    }
}