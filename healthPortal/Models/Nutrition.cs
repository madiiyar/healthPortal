using System;
using System.Collections.Generic;

namespace healthPortal.Models;

public partial class Nutrition
{
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public int Calories { get; set; }

    public decimal Fat { get; set; }

    public decimal Protein { get; set; }

    public decimal Carbs { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;
}
