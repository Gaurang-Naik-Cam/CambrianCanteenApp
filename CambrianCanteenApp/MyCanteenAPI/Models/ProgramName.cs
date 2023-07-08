using System;
using System.Collections.Generic;

namespace MyCanteenAPI.Models;

public partial class ProgramName
{
    public int Id { get; set; }

    public string ProgramName1 { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
