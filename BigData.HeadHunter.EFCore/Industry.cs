using System;
using System.Collections.Generic;

namespace BigData.HeadHunter.EFCore;

public partial class Industry
{
    public double Id { get; set; }

    public string Name { get; set; } = null!;

    public double? ParentId { get; set; }

    public virtual ICollection<EmployerIndustry> EmployerIndustries { get; } = new List<EmployerIndustry>();
}
