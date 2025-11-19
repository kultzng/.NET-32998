using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment
{    class Program
    {        
        static void Main(string[] args)
        {
            Admin admin = new Admin("password", "admin", "admin", "admin@dotnet.com", "012345678", "default");
            Doctor doctor = new Doctor("password", "doctor", "doctor", "doctor@dotnet.com", "012345678", "default");
            Patient patient = new Patient("password", "patient", "patient", "patient@dotnet.com", "012345678", "default");
            Login(admin, doctor, patient);
        }

        // id and password cross check in file user.txt
        static bool ValidateUser(string id, string password)
        {
            string[] lines = File.ReadAllLines("user.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(",");
                if (parts[0] == id && parts[1] == password)
                {
                    return true;
                }
            }
            return false;
        }

        // display "*" for password input instead of text
        static string HidePassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter);
            return password;
        }

        // method to log in
        static void Login(Admin admin, Doctor doctor, Patient patient)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|DOTNET Hospital Management System|");
                Console.WriteLine("|---------------------------------|");
                Console.WriteLine("|              Login              |");
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine();
                Console.Write("ID: ");
                string userId = Console.ReadLine();
                Console.Write("Password: ");
                string password = HidePassword();

                if (ValidateUser(userId, password))
                {
                    Console.WriteLine("\nValid Credentials");
                    string role = Utils.GetRole(userId);
                    if (role == "admin")
                    {
                        admin.SetUserData(userId);
                        admin.LoadDoctors();
                        admin.LoadPatients();
                        admin.ShowAdminMenu();
                    }
                    else if (role == "doctor")
                    {
                        doctor.SetUserData(userId);
                        doctor.LoadAppointments();
                        doctor.LoadPatients();
                        doctor.ShowDoctorMenu();
                    }
                    else if (role == "patient")
                    {
                        patient.SetUserData(userId);
                        patient.LoadAppointments();
                        patient.ShowPatientMenu();
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid Credentials");                    
                }
            }
        }
    }
}

                 
        
        
         

        

       
    