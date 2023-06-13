using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambrianCanteenApp.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string StudentNumber { get; set;}
        public string CurrentProgramName { get; set; }
        public DateTime EnrolmentDate { get; set; }
        public bool IsActive { get; set; }
    }
}
