using System;
using System.Collections.Generic;

namespace healthPortal.Models;

public partial class Instruction
{
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public int StepNumber { get; set; }

    public string Instruction1 { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
