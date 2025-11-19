using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace Assignment
{
    //Patient class inherits from User class
    class Patient : User
    {
        public Patient(string password, string role, string name, string email, string phoneNumber, string address) 
            : base(password, role, name, email, phoneNumber, address)
        {
            myAppointments = new List<Appointment>();
        }
        
        // A patient has a doctor and list of appointments with this doctor
        public Doctor myDoctor { get; set; }
        public List<Appointment> myAppointments { get; set; }

        // method to load appointments from file appointment.txt
        public void LoadAppointments()
        {
            myAppointments.Clear();
            string[] lines = FileManager.ReadFromFile("appointment.txt").Split("\n");
            List<Doctor> registeredDoctors = this.LoadDoctors();
            foreach (string line in lines)
            {
                string[] parts = line.Split(",");
                if (line != "" && parts[1] == Name)
                {
                    Appointment appointment = new Appointment(parts[0], parts[1], parts[2]);
                    foreach (Doctor doctor in registeredDoctors)
                    {
                        if (doctor.Name == parts[0])
                        {
                            myDoctor = doctor;
                            break;
                        }
                    }
                    myAppointments.Add(appointment);
                }
            }
        }

        //method to load doctor from file user.txt
        public List<Doctor> LoadDoctors(bool print=false)
        {
            string[] lines = FileManager.ReadFromFile("user.txt").Split("\n");
            List<Doctor> doctors = new List<Doctor>();
            int count = 0;
            foreach (string line in lines)
            {
                string[] parts = line.Split(",");
                if (parts[2] == "doctor")
                {
                    count++;
                    Doctor doctor = new Doctor(parts[1], parts[2], parts[3], parts[4], parts[5], parts[6]);
                    if(print==true) Console.WriteLine($"{count}. {doctor.UserDetailsCount()}");
                    doctors.Add(doctor);
                }
            }
            return doctors;
        }

        //Patient's menu
        public void DisplayPatientMenu()
        {
            Console.Clear();
            Console.WriteLine("+---------------------------------+");
            Console.WriteLine("|DOTNET Hospital Management System|");
            Console.WriteLine("|---------------------------------|");
            Console.WriteLine("|           Patient Menu          |");
            Console.WriteLine("+---------------------------------+");
            Console.WriteLine($"Welcome to DOTNET Hospital Management System {Name}");
            Console.WriteLine();
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. List patient details");
            Console.WriteLine("2. List my doctor details");
            Console.WriteLine("3. List all appointments");
            Console.WriteLine("4. Book an appointment");
            Console.WriteLine("5. Log out");
            Console.WriteLine("6. Exit");
            Console.WriteLine();
            Console.Write("Enter your choice: ");
        }

        public void ShowPatientMenu()
        {
            while (true)
            {
                DisplayPatientMenu();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        DisplayUserDetails();
                        break;
                    case "2":
                        DisplayMyDoctorDetails();
                        break;
                    case "3":
                        DisplayAllAppointments();
                        break;
                    case "4":
                        BookAppointment();
                        break;
                    case "5":                        
                        return;
                    case "6":
                        Environment.Exit(0);                        
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

            // method to display my registered doctor details
            void DisplayMyDoctorDetails()
            {
                Console.Clear();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|DOTNET Hospital Management System|");
                Console.WriteLine("|---------------------------------|");
                Console.WriteLine("|       My Doctor's Details       |");
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine();
                Console.WriteLine("Your doctor");
                Console.WriteLine();
                if (myAppointments != null)
                {
                    Utils.DesignUser();
                    myDoctor.UserDetails();
                }
                else
                {
                    Console.WriteLine("No doctor assigned.");
                }
                Console.WriteLine("\n\nPress any key to return to the menu");
                Console.ReadKey();
            }

            // method to list all appointments 
            void DisplayAllAppointments()
            {
                Console.Clear();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|DOTNET Hospital Management System|");
                Console.WriteLine("|---------------------------------|");
                Console.WriteLine("|         My Appointments         |");
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine();
                Console.WriteLine($"Appointments for {Name}");
                Console.WriteLine();
                Utils.DesignAppointment();
                if (myAppointments != null)
                {
                    for (int i = 0; i < myAppointments.Count; i++)
                    {
                        myAppointments[i].ToString(i);
                    }
                }
                else
                {
                    Console.WriteLine("No appointments found.");
                }                
                Console.WriteLine("\n\nPress any key to return to the menu");
                Console.ReadKey();
            }

            // method to book an appointment             
            void BookAppointment()
            {
                Console.Clear();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|DOTNET Hospital Management System|");
                Console.WriteLine("|---------------------------------|");
                Console.WriteLine("|       Book an Appointment       |");
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine();
                if (myDoctor == null)
                {
                    Console.WriteLine("You are not registered with any doctor! Please choose which doctor you would like to register with.");
                    List<Doctor> registeredDoctors = this.LoadDoctors(true);
                    Console.WriteLine("Please choose a doctor: ");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    myDoctor = registeredDoctors[choice - 1];
                    Console.WriteLine("Enter the description of the appointment: ");
                    string description = Console.ReadLine();
                    Console.WriteLine($"You are booking a new appointment with {myDoctor.Name}");
                    Console.WriteLine($"Description of the appointment: {description}");
                    Console.WriteLine("The appointment has been booked successfully!");
                    // add appointment to file appointment.txt
                    using (FileStream fs = new FileStream("appointment.txt", FileMode.Append, FileAccess.Write)) ;
                    string fileText = $"\n{myDoctor.Name},{Name},{description}";
                    File.AppendAllText("appointment.txt", fileText);
                    // add doctor to myDoctor list
                    myDoctor.myPatients.Add(this);
                }
                else
                {
                    Console.WriteLine($"You are already registered with a doctor {myDoctor.Name}.");
                    myDoctor = myDoctor;
                    Console.WriteLine("Enter the description of the appointment: ");
                    string description = Console.ReadLine();
                    Console.WriteLine($"You are booking a new appointment with {myDoctor.Name}");
                    Console.WriteLine($"Description of the appointment: {description}");
                    Console.WriteLine("The appointment has been booked successfully!");
                    // add appointment to file appointment.txt
                    using (FileStream fs = new FileStream("appointment.txt", FileMode.Append, FileAccess.Write)) ;
                    string fileText = $"\n{myDoctor.Name},{Name},{description}";
                    File.AppendAllText("appointment.txt", fileText);
                }
                Console.WriteLine("\nPress any key to return to the menu");
                Console.ReadKey();

            }
        }

        // method ToString() to list necessary details
        public void ToString(int userIndex = 0)
        {
            Utils.WriteAt($"{Name}", 0, 11 + userIndex);
            Utils.WriteAt($"| {myDoctor.Name}", 20, 11 + userIndex);
            Utils.WriteAt($"| {Email}", 40, 11 + userIndex);
            Utils.WriteAt($"| {PhoneNumber}", 65, 11 + userIndex);
            Utils.WriteAt($"| {Address}", 85, 11 + userIndex);
        }
    }
}





























            




    
