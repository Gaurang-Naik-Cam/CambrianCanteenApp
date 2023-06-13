using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambrianCanteenApp.Models
{
    internal class Student
    {
        internal int ID { get; set; }
        internal string Name { get; set; }
        internal string StudentNumber { get; set;}
        internal string CurrentProgramName { get;set }
        internal DateTime EnrolmentDate { get; set; }
        internal bool IsActive { get; set; }
    }
}
