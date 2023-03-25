using System;
using System.Collections.Generic;

namespace BigData.HeadHunter.EFCore;

public partial class EmployerIndustry
{
    public long Id { get; set; }

    public long EmployerId { get; set; }

    public string IndustryId { get; set; } = null!;

    public virtual Employer Employer { get; set; } = null!;

    public virtual Industry Industry { get; set; } = null!;
}
