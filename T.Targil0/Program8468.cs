using System;

namespace T.Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome8468();
            Welcome8728();
            Console.ReadKey();
        }
        static partial void Welcome8728();
        private static void Welcome8468()
        {
            Console.WriteLine("Enter your name: ");
            String name_user = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name_user);
        }
    }
}
