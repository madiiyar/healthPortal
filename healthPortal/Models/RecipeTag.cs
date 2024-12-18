using System;
using System.Collections.Generic;

namespace healthPortal.Models;

public partial class RecipeTag
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
