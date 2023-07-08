using System;
using System.Collections.Generic;

namespace MyCanteenAPI.Models;

public partial class Student
{
    public int Id { get; set; }

    public string StudentName { get; set; } = null!;

    public string StudentNumber { get; set; } = null!;

    public int? CurrentProgramId { get; set; }

    public DateTime? EnrolmentDate { get; set; }

    public bool? IsActive { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ProgramName? CurrentProgram { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
