using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Utils
    {
        // method to get role of user from file user.txt
        public static string GetRole(string id)
        {
            string[] lines = FileManager.ReadFromFile("user.txt").Split("\n");
            foreach (string line in lines)
            {
                string[] parts = line.Split(",");
                if (parts[0] == id)
                {
                    return parts[2];
                }
            }
            return "";
        }

        // designing
        public static int origRow;
        public static int origCol;
        public static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        // design doctor-patient-description
        public static void DesignAppointment()
        {
            Utils.WriteAt("Doctor", 0, 9);
            Utils.WriteAt("| Patient", 20, 9);
            Utils.WriteAt("| Description", 40, 9);
            Utils.WriteAt("--------------------------------------------------------------------------\n", 0, 10);
        }

        // design patient-doctor-email-phone-address
        public static void DesignPatient()
        {
            Utils.WriteAt("Patient", 0, 9);
            Utils.WriteAt("| Doctor", 20, 9);
            Utils.WriteAt("| Email", 40, 9);
            Utils.WriteAt("| Phone", 65, 9);
            Utils.WriteAt("| Address", 85, 9);
            Utils.WriteAt("----------------------------------------------------------------------------------------------------------------------------\n", 0, 10);
        }

        // design name-email-phone-address
        public static void DesignUser()
        {
            Utils.WriteAt("Name", 0, 9);
            Utils.WriteAt("| Email Address", 20, 9);
            Utils.WriteAt("| Phone", 45, 9);
            Utils.WriteAt("| Address", 65, 9);
            Utils.WriteAt("-----------------------------------------------------------------------------", 0, 10);
        }
    }
}
