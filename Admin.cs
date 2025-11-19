using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment
{
    class Admin : User 
    {
        public Admin(string password, string role, string name, string email, string phoneNumber, string address)
            : base(password, role, name, email, phoneNumber, address)
        {
            doctors = new List<Doctor>();
            patients = new List<Patient>();        
        }

        // Admin can access information of all doctors and patients
        public List<Patient> patients { get; set; }
        public List<Doctor> doctors { get; set; }

        // method to load doctor from file user.txt
        public List<Doctor> LoadDoctors()
        {
            string[] lines = FileManager.ReadFromFile("user.txt").Split("\n");
            //List<Doctor> doctors = new List<Doctor>();            
            foreach (string line in lines)
            {
                string[] parts = line.Split(",");
                if (parts[2] == "doctor")
                {
                    Doctor doctor = new Doctor(parts[1], parts[2], parts[3], parts[4], parts[5], parts[6]);                    
                    doctors.Add(doctor);
                }
            }
            return doctors;
        }

        // get doctor by ID from file user.txt
        public Doctor GetDoctorById(string id)
        {
            string[] lines = FileManager.ReadFromFile("user.txt").Split("\n");
            int doctorIndex = 0;

            for (int i = 0; i < lines.Count(); i++)
            {
                string[] parts = lines[i].Split(",");
                if (lines[i] != "" && parts[0] == id)
                {
                    doctors[doctorIndex].Id = id;
                    doctors[doctorIndex].Password = parts[1];
                    doctors[doctorIndex].Role = parts[2];
                    doctors[doctorIndex].Name = parts[3];
                    doctors[doctorIndex].Email = parts[4];
                    doctors[doctorIndex].PhoneNumber = parts[5];
                    doctors[doctorIndex].Address = parts[6];
                    return doctors[doctorIndex];
                }
                else if (parts[2] == "doctor")
                    doctorIndex++;
            } 
            return null;            
        }

        // get doctor by patient name from file appointment.txt
        public Doctor GetDoctorByPatientName(string name)
        {
            string[] lines = FileManager.ReadFromFile("appointment.txt").Split("\n");
            foreach (string line in lines)
            {
                string[] parts = line.Split(",");
                if (parts[1] == name)
                {
                    return GetDoctorByName(parts[0]);
                }
            }
            return null;
        }
        // get doctor by name from file user.txt
        public Doctor GetDoctorByName(string name)
        {
            string[] lines = FileManager.ReadFromFile("user.txt").Split("\n");
            foreach (string line in lines)
            {
                string[] parts = line.Split(",");
                if (parts[3] == name && parts[2] == "doctor")
                {
                    return GetDoctorById(parts[0]);
                }
            }
            return null;
        }

        // method to load patient from file user.txt
        public void LoadPatients()
        {
            string[] lines = FileManager.ReadFromFile("user.txt").Split("\n");
            foreach (string line in lines)
            {
                string[] parts = line.Split(",");
                if (line != "" && parts[2] == "patient")
                {
                    Patient patient = new Patient(parts[1], parts[2], parts[3], parts[4], parts[5], parts[6]);
                    patient.myDoctor = GetDoctorByPatientName(parts[3]);
                    patients.Add(patient);
                }
            }            
        }

        // method to get patient by ID from file user.txt
        public Patient GetPatientById(string id)
        {
            string[] lines = FileManager.ReadFromFile("user.txt").Split("\n");
            int patientIndex = 0;

            for (int i = 0; i < lines.Count(); i++)
            {
                string[] parts = lines[i].Split(",");
                if (lines[i] != "" && parts[0] == id)
                {
                    patients[patientIndex].Id = id;
                    patients[patientIndex].Password = parts[1];
                    patients[patientIndex].Role = parts[2];
                    patients[patientIndex].Name = parts[3];
                    patients[patientIndex].Email = parts[4];
                    patients[patientIndex].PhoneNumber = parts[5];
                    patients[patientIndex].Address = parts[6];
                    patients[patientIndex].myDoctor = GetDoctorByPatientName(parts[3]);
                    patients[patientIndex].LoadAppointments();
                    return patients[patientIndex];
                }
                else if (parts[2] == "patient")
                    patientIndex++;
            }
            return null;
        }

        // Admin's menu
        public void DisplayAdminMenu()
        {
            Console.Clear();
            Console.WriteLine("+---------------------------------+");
            Console.WriteLine("|DOTNET Hospital Management System|");
            Console.WriteLine("|---------------------------------|");
            Console.WriteLine("|        Administrator Menu       |");
            Console.WriteLine("+---------------------------------+");
            Console.WriteLine($"Welcome to DOTNET Hospital Management System {Name}");
            Console.WriteLine();
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. List all doctors");
            Console.WriteLine("2. Check doctor details");
            Console.WriteLine("3. List all patients");
            Console.WriteLine("4. Check patient details");
            Console.WriteLine("5. Add doctor");
            Console.WriteLine("6. Add patient");
            Console.WriteLine("7. Log out");
            Console.WriteLine("8. Exit");
            Console.WriteLine();
            Console.Write("Enter your choice: ");
        }
        public void ShowAdminMenu()
        {
            while (true)
            {
                DisplayAdminMenu();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ListAllDoctors();
                        break;
                    case "2":
                        CheckDoctorDetails();
                        break;
                    case "3":
                        ListAllPatients();
                        break;
                    case "4":
                        CheckPatientDetails();
                        break;
                    case "5":
                        AddDoctor();
                        break;
                    case "6":
                        AddPatient();
                        break;
                    case "7":
                        return;
                    case "8":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            // method to list all doctors
            void ListAllDoctors()
            {
                Console.Clear();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|DOTNET Hospital Management System|");
                Console.WriteLine("|---------------------------------|");
                Console.WriteLine("|            All doctors          |");
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine();
                if (doctors.Count > 0)
                {
                    Console.WriteLine("All doctor registered to the DOTNET Hospital Management System");
                    Utils.DesignUser();
                    for (int i = 0; i < doctors.Count; i++)
                    {
                        doctors[i].UserDetails(i);
                    }
                }
                else
                {
                    Console.WriteLine("No doctors are registered.");
                }
                Console.WriteLine("\n\nPress any key to return to the menu");
                Console.ReadKey();
            }

            // method to check particular doctor details, search by doctor ID
            void CheckDoctorDetails()
            {
                Console.Clear();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|DOTNET Hospital Management System|");
                Console.WriteLine("|---------------------------------|");
                Console.WriteLine("|        Check Doctor Details     |");
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine();
                Console.WriteLine("Please enter the ID of the doctor who's details you are checking. Or press n to return to menu.");
                string doctorId = Console.ReadLine();
                if (doctors.Count == 0)
                {
                    Console.WriteLine("No doctors are registered.");                    
                }
                else
                {
                    Doctor doctor = GetDoctorById(doctorId);
                    if (doctor != null)
                    {
                        Console.WriteLine($"Details for {doctor.Name}");
                        Utils.DesignUser();
                        doctor.UserDetails();
                    }
                    else
                    {
                        Console.WriteLine("Doctor not found.");
                    }
                }
                Console.WriteLine("\nPress any key to return to the menu");
                Console.ReadKey();
            }

            // method to list all patients
            void ListAllPatients()
            {
                Console.Clear();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|DOTNET Hospital Management System|");
                Console.WriteLine("|---------------------------------|");
                Console.WriteLine("|            All patients         |");
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine();
                if (patients.Count > 0)
                {
                    Console.WriteLine("All patients registered to the DOTNET Hospital Management System");
                    Utils.DesignPatient();
                    for (int i = 0; i < patients.Count; i++)
                    {
                        patients[i].ToString(i);
                    }                 
                }
                else
                {
                    Console.WriteLine("No patients are registered.");                    
                }
                Console.WriteLine("\nPress any key to return to the menu");
                Console.ReadKey();
            }


            // method to check particular patient details, search by patient ID
            void CheckPatientDetails()
            {
                Console.Clear();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|DOTNET Hospital Management System|");
                Console.WriteLine("|---------------------------------|");
                Console.WriteLine("|          Patient Details        |");
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine();
                Console.WriteLine("Please enter the ID of the patient who's details you are checking. Or press n to return to menu.");
                if (patients.Count == 0)
                {
                    Console.WriteLine("No patients are registered.");
                }
                else
                {
                    string patientId = Console.ReadLine();
                    Patient patient = GetPatientById(patientId);
                    if (patient != null)
                    {
                        Console.WriteLine($"Details for {patient.Name}");
                        Utils.DesignPatient();
                        patient.ToString();
                    }
                    else
                    {
                        Console.WriteLine("Patient not found.");
                    }
                }
                Console.WriteLine("\nPress any key to return to the menu");
                Console.ReadKey();
            }

            // method to add new doctor
            void AddDoctor()
            {
                Console.Clear();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|DOTNET Hospital Management System|");
                Console.WriteLine("|---------------------------------|");
                Console.WriteLine("|           Add New Doctor        |");
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("Registering a new doctor with the DOTNET Hospital Management System");
                Console.WriteLine("First name: ");
                string firstName = Console.ReadLine();
                Console.WriteLine("Last name: ");
                string lastName = Console.ReadLine();
                string name = firstName + " " + lastName;
                Console.WriteLine("Email: ");
                string email = Console.ReadLine();
                Console.WriteLine("Phone number: ");
                string phoneNumber = Console.ReadLine();
                Console.WriteLine("Street number: ");
                string streetNumber = Console.ReadLine();
                Console.WriteLine("Street: ");
                string street = Console.ReadLine();
                Console.WriteLine("City: ");
                string city = Console.ReadLine();
                Console.WriteLine("State: ");
                string state = Console.ReadLine();
                string address = streetNumber + " " + street + " " + city + " " + state;

                Doctor doctor = new Doctor("password","doctor", name, email, phoneNumber, address);
                doctors.Add(doctor);
                Console.WriteLine($"Dr {name} added to the system."); 
                Console.WriteLine("\nPress any key to return to the menu");
                Console.ReadKey();
                // add to system.IO file name user.txt
                using (FileStream fs = new FileStream("user.txt", FileMode.Append, FileAccess.Write));
                string fileText = $"\n{doctor.Id},{doctor.Password},{doctor.Role},{doctor.Name},{doctor.Email},{doctor.PhoneNumber},{doctor.Address}";
                File.AppendAllText("user.txt", fileText) ;            
               
            }

            // method to add new patient
            void AddPatient()
            {
                Console.Clear();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|DOTNET Hospital Management System|");
                Console.WriteLine("|---------------------------------|");
                Console.WriteLine("|           Add New Patient       |");
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("Registering a new patient with the DOTNET Hospital Management System");
                Console.WriteLine("First name: ");
                string firstName = Console.ReadLine();
                Console.WriteLine("Last name: ");
                string lastName = Console.ReadLine();
                string name = firstName + " " + lastName;
                Console.WriteLine("Email: ");
                string email = Console.ReadLine();
                Console.WriteLine("Phone number: ");
                string phoneNumber = Console.ReadLine();
                Console.WriteLine("Street number: ");
                string streetNumber = Console.ReadLine();
                Console.WriteLine("Street: ");
                string street = Console.ReadLine();
                Console.WriteLine("City: ");
                string city = Console.ReadLine();
                Console.WriteLine("State: ");
                string state = Console.ReadLine();
                string address = streetNumber + " " + street + " " + city + " " + state;

                Patient patient = new Patient("password", "patient", name, email, phoneNumber, address);
                patients.Add(patient);
                Console.WriteLine($"{name} added to the system.");
                Console.WriteLine("\nPress any key to return to the menu");
                Console.ReadKey();
                // add to system.IO file name user.txt
                using (FileStream fs = new FileStream("user.txt", FileMode.Append, FileAccess.Write));
                string fileText = $"\n{patient.Id},{patient.Password},{patient.Role},{patient.Name},{patient.Email},{patient.PhoneNumber},{patient.Address}";
                File.AppendAllText("user.txt", fileText);
            }

        }

    }
}






