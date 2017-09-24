using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyClinic.ViewModels
{
    public class ObservationViewModel
    {
        public string PatientsLink { get; set; }
        public string ObservationsLink { get; set; }

        public string ObservationId { get; set; }
        public DateTime Date { get; set; }
        public string Doctor{ get; set; }
        public List<string> ObservationItems { get; set; }

        /*public string Temperature { get; set; }
        public string BloodPressure { get; set; }
        public string BloodGlucose { get; set; }
        public string Conditions { get; set; }
        public string PastConditions { get; set; }*/
    }
}