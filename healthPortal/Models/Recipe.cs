using System;
using System.Collections.Generic;

namespace healthPortal.Models;

public partial class Recipe
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? PrepTime { get; set; }

    public string? Image { get; set; }

    public string Category { get; set; } = null!;

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public virtual ICollection<Instruction> Instructions { get; set; } = new List<Instruction>();

    public virtual ICollection<Nutrition> Nutritions { get; set; } = new List<Nutrition>();

    public virtual ICollection<RecipeTag> Tags { get; set; } = new List<RecipeTag>();
}
