using System;
using System.Collections.Generic;

namespace BigData.HeadHunter.EFCore;

public partial class EmployerIndustry
{
    public long EmployerId { get; set; }

    public double IndustryId { get; set; }

    public virtual Employer Employer { get; set; } = null!;

    public virtual Industry Industry { get; set; } = null!;
}
