using System;
using System.Collections.Generic;

namespace healthPortal.Models;

public partial class FitnessTip
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string FitnessLevel { get; set; } = null!;

    public int? Views { get; set; }

    public DateTime? Date { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
