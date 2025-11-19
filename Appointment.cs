using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assignment
{
    class Appointment
    {
        // class appointment have list of doctor, list of patient, description
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string Description { get; set; }       

        // constructor of appointment class        
        public Appointment(string doctor, string patient, string description)
        {
            DoctorName = doctor;
            PatientName = patient;
            Description = description;
        }                                       
        
        // method to display appointment details, with data from file appointment.txt
        public void ToString(int userIndex = 0)
        {   
            Utils.WriteAt($"{this.DoctorName}", 0, 11 + userIndex);
            Utils.WriteAt($"| {PatientName}", 20, 11 + userIndex);
            Utils.WriteAt($"| {Description}", 40, 11 + userIndex);
        }

    }
}




