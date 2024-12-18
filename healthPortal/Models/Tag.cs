using System;
using System.Collections.Generic;

namespace healthPortal.Models;

public partial class Tag
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<FitnessTip> FitnessTips { get; set; } = new List<FitnessTip>();
}
