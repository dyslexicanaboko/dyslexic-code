using IisManagementTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace AppPoolCredSecurityHoleConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (!IsAdministrator())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You need to run this program as Administrator in order for it to work.\nPlease restart as Administrator.");
                Console.Read();

                return;
            }

            if (args.Length == 0)
                ShowHints();
            else switch (args[0])
            {
                case "/s":
                case "-s":
                    PrintOutApplicationPools();
                    break;
                case "/c":
                case "-c":
                    UpdateApplicationPoolPasswords(args[1], args[2]);
                    break;
                case "help":
                case "?":
                case "/?":
                    ShowHints();
                    break;
                default:
                    Console.WriteLine("Unrecognized input");
                    ShowHints();
                    break;
            }

            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Press any key to continue...");
            Console.ResetColor();
            Console.Read();
        }

        public static void ShowHints()
        {
            Console.WriteLine("/s or -s                   //Shows your application pool usernames and passwords.");
            Console.WriteLine("/c or -c username password //Update all of your application pool usernames and passwords in one shot.");
        }

        public static void UpdateApplicationPoolPasswords()
        {
            using (var svc = new IisManagementService())
            {
                List<ApplicationPoolCredential> lst = svc.GetApplicationPoolCredentials();

                foreach (var ap in lst)
                    svc.ChangeCredentials("Your username here", "Your password here", ap.ApplicationPoolName);
            }
        }

        public static void PrintOutApplicationPools()
        {
            try
            {
                using (var svc = new IisManagementService())
                {
                    List<ApplicationPoolCredential> lst = svc.GetApplicationPoolCredentials();

                    string s0 = "App Pool";
                    string s1 = "Username";
                    string s2 = "Password";

                    int c0 = TakeLargerNumber(lst.Max(x => x.ApplicationPoolName.Length), s0);
                    int c1 = TakeLargerNumber(lst.Max(x => x.Username.Length), s1);
                    int c2 = TakeLargerNumber(lst.Max(x => x.Password.Length), s2);

                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    PrintRow(c0, c1, c2, s0, s1, s2);

                    Console.ForegroundColor = ConsoleColor.Black;

                    ApplicationPoolCredential c = null;

                    for (int i = 0; i < lst.Count; i++)
                    {
                        c = lst[i];

                        if (i % 2 == 0)
                            Console.BackgroundColor = ConsoleColor.White;
                        else
                            Console.BackgroundColor = ConsoleColor.Gray;

                        PrintRow(c0, c1, c2, c);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static int TakeLargerNumber(int largestValue, string header)
        {
            int len = header.Length;

            return (largestValue > len) ? largestValue : len;
        }

        public static void PrintRow(int c0, int c1, int c2, ApplicationPoolCredential c)
        {
            PrintRow(c0, c1, c2, c.ApplicationPoolName, c.Username, c.Password);
        }

        public static void PrintRow(int c0, int c1, int c2, string s0, string s1, string s2)
        {
            Console.WriteLine("{0} {1} {2}", s0.PadRight(c0), s1.PadRight(c1), s2.PadRight(c2));
        }

        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

    }
}
