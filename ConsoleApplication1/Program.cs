using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var users = new List<Models.User>();

            users.Add(new Models.User { Name = "Dave", Password = "hello" });
            users.Add(new Models.User { Name = "Steve", Password = "steve" });
            users.Add(new Models.User { Name = "Lisa", Password = "hello" });

            //==============================================
            //Display to the console, all the passwords that are "hello".Hint: Where

            var helloPasswordList = users.Where(u => u.Password.Equals("hello")).Select(u => u.Password).ToList(); //passwords of users that have the password hello
            var helloPasswordList2 = users.Where(u => u.Password.Equals("hello")).Select(u => u.Name).ToList(); //In case the homework had typo and actually wanted 

            helloPasswordList.ForEach(Console.WriteLine);

            //==============================================
            //Delete any passwords that are the lower - cased version of their Name. Do not just look for "steve".It has to be data - driven.Hint: Remove or RemoveAll 

            users.RemoveAll(u=>u.Password.Equals(u.Name.ToLower()));

            //==============================================
            //Delete the first User that has the password "hello". Hint: First or FirstOrDefault

            users.Remove(users.First(u => u.Password.Equals("hello")));

            //==============================================
            //Display to the console the remaining users with their Name and Password. Here is the only place you can use a for/foreach loop :)

            foreach (Models.User user in users)
            {
                Console.WriteLine("Name: " + user.Name + " Password: " + user.Password);
            }


            Console.Read();
        }
    }
}
