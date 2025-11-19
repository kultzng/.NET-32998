using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    // Doctor class inherits from User class
    class Doctor : User
    {
        // A doctor has a list of patients and list of appointments

        public List<Patient> myPatients { get; set; }
        public List<Appointment> myAppointments { get; set; }

        public Doctor(string password, string role, string name, string email, string phoneNumber, string address)
            : base(password, role, name, email, phoneNumber, address)
        {
            myPatients = new List<Patient>();
            myAppointments = new List<Appointment>();
        }
        // method to load appointments from file appointment.txt
        public void LoadAppointments()
        {
            string[] lines = FileManager.ReadFromFile("appointment.txt").Split("\n");
            foreach (string line in lines)
            {
                string[] parts = line.Split(",");
                if (line != "" && parts[0] == Name)
                {
                    Appointment appointment = new Appointment(parts[0], parts[1], parts[2]);
                    myAppointments.Add(appointment);
                }
            }
        }

        //method to load patient from file user.txt
        public void LoadPatients()
        {
            string[] lines = FileManager.ReadFromFile("user.txt").Split("\n");
            foreach (string line in lines)
            {
                string[] parts = line.Split(",");
                if (line != "" && parts[2] == "patient")
                {
                    Patient patient = new Patient(parts[1], parts[2], parts[3], parts[4], parts[5], parts[6]);
                    patient.myDoctor = this;
                    myPatients.Add(patient);
                }
            }
        }

        // get patient by id
        public Patient GetPatientById(string id)
        {
            string[] lines = FileManager.ReadFromFile("user.txt").Split("\n");
            int patientIndex = 0;

            for (int i = 0; i < lines.Count(); i++)
            {
                string[] parts = lines[i].Split(",");
                if (lines[i] != "" && parts[0] == id)
                {
                    myPatients[patientIndex].Id = id;
                    myPatients[patientIndex].Password = parts[1];
                    myPatients[patientIndex].Role = parts[2];
                    myPatients[patientIndex].Name = parts[3];
                    myPatients[patientIndex].Email = parts[4];
                    myPatients[patientIndex].PhoneNumber = parts[5];
                    myPatients[patientIndex].Address = parts[6];
                    myPatients[patientIndex].myDoctor = this;
                    myPatients[patientIndex].LoadAppointments();
                    return myPatients[patientIndex];
                }
                else if (parts[2] == "patient")
                {
                    patientIndex++;
                }
            }
            return null;
        }



            // Doctor's menu
            public void DisplayDoctorMenu()
        {
            Console.Clear();
            Console.WriteLine("+---------------------------------+");
            Console.WriteLine("|DOTNET Hospital Management System|");
            Console.WriteLine("|---------------------------------|");
            Console.WriteLine("|           Doctor Menu           |");
            Console.WriteLine("+---------------------------------+");
            Console.WriteLine($"Welcome to DOTNET Hospital Management System {Name}");
            Console.WriteLine();
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. List doctor details");
            Console.WriteLine("2. List my patients details");
            Console.WriteLine("3. List all appointments");
            Console.WriteLine("4. Check particular patient");
            Console.WriteLine("5. List appointment with particular patient");
            Console.WriteLine("6. Log out");
            Console.WriteLine("7. Exit");
            Console.WriteLine();
            Console.Write("Enter your choice: ");
        }

        public void ShowDoctorMenu()
        {
            while (true)
            {
                DisplayDoctorMenu();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        DisplayDoctorDetails();
                        break;
                    case "2":
                        DisplayPatientsDetails();
                        break;
                    case "3":
                        DisplayAppointments();
                        break;
                    case "4":
                        CheckParticularPatient();
                        break;
                    case "5":
                        ListAppointmentWithParticularPatient();
                        break;
                    case "6":
                        return; 
                    case "7":
                        Environment.Exit(0); 
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            // method to display doctor detail
            void DisplayDoctorDetails()
            {
                Console.Clear();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|DOTNET Hospital Management System|");
                Console.WriteLine("|---------------------------------|");
                Console.WriteLine("|            My Details           |");
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine();
                Console.WriteLine($"Doctor {Name} details: ");
                Utils.DesignUser();
                UserDetails();                
                Console.WriteLine("\n\nPress any key to return to the menu");
                Console.ReadKey();
            }

            // method to list all patients
            void DisplayPatientsDetails()
            {
                Console.Clear();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|DOTNET Hospital Management System|");
                Console.WriteLine("|---------------------------------|");
                Console.WriteLine("|          My Patients            |");
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine();
                Console.WriteLine($"Patients assigned to {Name}:");
                Utils.DesignPatient();
                if (myAppointments.Count > 0)
                    for (int i = 0; i < myPatients.Count; i++)
                    {
                        myPatients[i].ToString(i);
                    }                
                else
                {
                    Console.WriteLine("No patient.");
                }
                Console.WriteLine("\n\nPress any key to return to the menu");
                Console.ReadKey();
            }

            // method to list all appointments involving the doctor
            void DisplayAppointments()
            {
                Console.Clear();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|DOTNET Hospital Management System|");
                Console.WriteLine("|---------------------------------|");
                Console.WriteLine("|        All Appointments         |");
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine();
                Console.WriteLine($"All appointments of Doctor {Name} ");
                Console.WriteLine();
                Utils.DesignAppointment();
                if (myAppointments.Count > 0)
                    for (int i = 0; i < myAppointments.Count; i++)
                    {
                        myAppointments[i].ToString(i);
                    }
                else
                {
                    Console.WriteLine("No appointment.");
                }
                Console.WriteLine("\n\nPress any key to return to the menu");
                Console.ReadKey();
            }


            // method to check particular patient, choose patient by id
            void CheckParticularPatient()
            {
                Console.Clear();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|DOTNET Hospital Management System|");
                Console.WriteLine("|---------------------------------|");
                Console.WriteLine("|       Check Patient Details     |");
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine();
                Console.WriteLine("Enter patient ID to check: ");
                string patientId = Console.ReadLine();
                Patient patient = GetPatientById(patientId);

                if (patient != null)
                {
                    Console.WriteLine($"Details of patient {patientId}:");
                    Utils.DesignPatient();
                    patient.ToString();
                }
                else
                {
                    Console.WriteLine("Patient not found.");
                }
                Console.WriteLine("\n\n\nPress any key to return to the menu");
                Console.ReadKey();
            }

            // method to list appointment with particular patient, choose patient by id            
            void ListAppointmentWithParticularPatient()
            {
                Console.Clear();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|DOTNET Hospital Management System|");
                Console.WriteLine("|---------------------------------|");
                Console.WriteLine("|    Appointments with Patient    |");
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine();
                Console.WriteLine("Enter patient ID to check: ");
                string patientId = Console.ReadLine();
                Patient patient = GetPatientById(patientId);
                if (patient != null)
                {
                    Console.WriteLine($"Appointments with patient {patientId}:");
                    Utils.DesignAppointment();
                    for (int i = 0; i < myAppointments.Count; i++)
                    {
                        //if (myAppointments[i].PatientName == patientId)
                        //{
                            myAppointments[i].ToString(i);
                        //}
                    }
                }
                else
                {
                    Console.WriteLine("Patient not found.");
                }
                Console.WriteLine("\n\nPress any key to return to the menu");
                Console.ReadKey();
            }

        }

    }
}



    

