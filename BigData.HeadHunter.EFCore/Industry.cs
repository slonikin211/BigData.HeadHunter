using System;
using System.Collections.Generic;

namespace BigData.HeadHunter.EFCore;

public partial class Industry
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? ParentId { get; set; }

    public virtual ICollection<EmployerIndustry> EmployerIndustries { get; } = new List<EmployerIndustry>();
}
