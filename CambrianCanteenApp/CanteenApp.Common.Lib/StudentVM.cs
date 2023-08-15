using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanteenApp.Common.Lib
{
    public class StudentVM
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string StudentNumber { get; set; }
        public string CurrentProgramName { get; set; }
        public DateTime EnrolmentDate { get; set; }
        public bool IsActive { get; set; }
    }
}
