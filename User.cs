using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class User
    {
        //user have id, password, role, name, email, phone number, address

        public string Id { get; set; } 
        public string Password { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public User() { }
       
        // constructor
        public User(string password, string role, string name, string email, string phoneNumber, string address)
        {
            // generate a random id for the user
            Random random = new Random();
            Id = random.Next(10000, 99999).ToString();
            Password = password;
            Role = role;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }
        // constructor for name, email, phoneNumber, address
        public User(string name, string email, string phoneNumber, string address)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }            

        // crosscheck Id login with Id in file user.txt and set data
        public void SetUserData(string id)
        {
            string[] lines = FileManager.ReadFromFile("user.txt").Split("\n");
            foreach (string line in lines)
            {
                string[] parts = line.Split(",");
                if (parts[0] == id)
                {
                    Id = parts[0];
                    Password = parts[1];
                    Role = parts[2];
                    Name = parts[3];
                    Email = parts[4];
                    PhoneNumber = parts[5];
                    Address = parts[6];
                    break;
                }
            }
        }        

        // method to display user details
        public void DisplayUserDetails()
        {
            SetUserData(Id);
            Console.Clear();
            Console.WriteLine("+---------------------------------+");
            Console.WriteLine("|DOTNET Hospital Management System|");
            Console.WriteLine("|---------------------------------|");
            Console.WriteLine("|           My Details            |");
            Console.WriteLine("+---------------------------------+");
            Console.WriteLine();
            Console.WriteLine($"{Name}'s Details");
            Console.WriteLine();
            Console.WriteLine($"{Role} ID: {Id}");
            Console.WriteLine($"Full Name: {Name}");
            Console.WriteLine($"Address: {Address}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Phone Number: {PhoneNumber}");
            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }


        // method to list details of user
        
        public void UserDetails(int userIndex=0)
        {
            SetUserData(Id);
            Utils.WriteAt($"{Name}", 0, 11 + userIndex);
            Utils.WriteAt($"| {Email}", 20, 11 + userIndex);
            Utils.WriteAt($"| {PhoneNumber}", 45, 11 + userIndex);
            Utils.WriteAt($"| {Address}", 65, 11 + userIndex);        
        }
        public string UserDetailsCount()
        {
            SetUserData(Id);            
            return $"{Name}  |{Email}   |{PhoneNumber}  |{Address}";
        }

        public virtual void ToString()
        {            
            Console.WriteLine("");
        }        

    }
}
