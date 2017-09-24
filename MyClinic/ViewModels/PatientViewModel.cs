using MyClinic.Models;
using System;
using System.Net;
using System.Collections.Generic;

using System.Web.Http.Routing;

namespace MyClinic.ViewModels
{
    public class PatientViewModel
    {
        public string Link { get; set; }
        public string ObservationsLink { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Doctor { get; set; }
        public List<ObservationViewModel> Observations { get; set; }


        public PatientViewModel()
        {
            
        }
    }


    public class PatientCollection
    {
        public Uri  Next { get; set; }
        public Uri Previous { get; set; }
        public int TotalCount { get; set; }
        public List<PatientViewModel> Patients { get; set; }
    }
}