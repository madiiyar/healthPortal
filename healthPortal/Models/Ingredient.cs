using System;
using System.Collections.Generic;

namespace healthPortal.Models;

public partial class Ingredient
{
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public string Ingredient1 { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
